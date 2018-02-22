using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CreativeSpore.SuperTilemapEditor
{
    partial class TilemapChunk
    {
        [SerializeField, HideInInspector]
        private MeshFilter m_meshFilter;
        [SerializeField, HideInInspector]
        private MeshRenderer m_meshRenderer;

        /// <summary>
        /// Next time UpdateMesh is called, the tile mesh will be rebuild
        /// </summary>
        public void InvalidateMesh()
        {
            m_needsRebuildMesh = true;
        }

        /// <summary>
        /// Invalidates brushes, so all tiles with brushes call again the brush refresh method on next UpdateMesh call
        /// </summary>
        public void InvalidateBrushes()
        {
            m_invalidateBrushes = true;
        }

        public void SetSharedMaterial(Material material)
        {
            m_meshRenderer.sharedMaterial = material;
            m_needsRebuildMesh = true;
        }

        private bool m_needsRebuildMesh = false;
        private bool m_invalidateBrushes = false;
        /// <summary>
        /// Update the mesh and return false if all tiles are empty
        /// </summary>
        /// <returns></returns>
        public bool UpdateMesh()
        {
            if (ParentTilemap == null)
            {
                if (transform.parent == null) gameObject.hideFlags = HideFlags.None; //Unhide orphan tilechunks. This shouldn't happen
                ParentTilemap = transform.parent.GetComponent<Tilemap>();
            }
            gameObject.layer = ParentTilemap.gameObject.layer;
            transform.localPosition = new Vector2(GridPosX * CellSize.x, GridPosY * CellSize.y);

            if (m_meshFilter.sharedMesh == null)
            {
                //Debug.Log("Creating new mesh for " + name);
                m_meshFilter.sharedMesh = new Mesh();
                m_meshFilter.sharedMesh.hideFlags = HideFlags.DontSave;
                m_meshFilter.sharedMesh.name = ParentTilemap.name + "_mesh";
                m_needsRebuildMesh = true;
            }
#if UNITY_EDITOR
            // fix prefab preview, not compatible with MaterialPropertyBlock. I need to create a new material and change the main texture and color directly.
            if (UnityEditor.PrefabUtility.GetPrefabType(gameObject) == UnityEditor.PrefabType.Prefab)
            {
                gameObject.hideFlags |= HideFlags.HideInHierarchy;
                if (m_meshRenderer.sharedMaterial == null || m_meshRenderer.sharedMaterial == ParentTilemap.Material)
                {
                    m_meshRenderer.sharedMaterial = new Material(ParentTilemap.Material);
                    m_meshRenderer.sharedMaterial.name += "_copy";
                    m_meshRenderer.sharedMaterial.hideFlags = HideFlags.DontSave;
                    m_meshRenderer.sharedMaterial.color = ParentTilemap.TintColor;
                    m_meshRenderer.sharedMaterial.mainTexture = ParentTilemap.Tileset ? ParentTilemap.Tileset.AtlasTexture : null;
                }
            }
            else
#endif
            //NOTE: else above
            {
                m_meshRenderer.sharedMaterial = ParentTilemap.Material;
            }
            m_meshRenderer.enabled = ParentTilemap.IsVisible;
            if (m_needsRebuildMesh)
            {
                m_needsRebuildMesh = false;
                if (FillMeshData())
                {
                    m_invalidateBrushes = false;
                    Mesh mesh = m_meshFilter.sharedMesh;
                    mesh.Clear();
#if UNITY_5_0 || UNITY_5_1
                    mesh.vertices = s_vertices.ToArray();
                    mesh.triangles = s_triangles.ToArray();
                    mesh.uv = m_uv.ToArray();
#else
                    mesh.SetVertices(s_vertices);
                    mesh.SetTriangles(s_triangles, 0);
                    mesh.SetUVs(0, m_uv);
#endif
                    mesh.RecalculateNormals(); //NOTE: allow directional lights to work properly
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void DestroyMeshIfNeeded()
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if (meshFilter.sharedMesh != null
                && (meshFilter.sharedMesh.hideFlags & HideFlags.DontSave) != 0)
            {
                //Debug.Log("Destroy Mesh of " + name);
                DestroyImmediate(meshFilter.sharedMesh);
            }
        }

        static TilemapChunk s_currUpdatedTilechunk;
        static int s_currUVVertex;
        public static void RegisterAnimatedBrush(IBrush brush, int subTileIdx = -1)
        {
            if (subTileIdx >= 0)
                s_currUpdatedTilechunk.m_animatedTiles.Add(new AnimTileData() { VertexIdx = s_currUVVertex + (subTileIdx << 2), Brush = brush, SubTileIdx = subTileIdx });
            else
                s_currUpdatedTilechunk.m_animatedTiles.Add(new AnimTileData() { VertexIdx = s_currUVVertex, Brush = brush, SubTileIdx = subTileIdx });
        }

        /// <summary>
        /// Fill the mesh data and return false if all tiles are empty
        /// </summary>
        /// <returns></returns>
        private bool FillMeshData()
        {
            //Debug.Log( "[" + ParentTilemap.name + "] FillData -> " + name);
            if (!Tileset || !Tileset.AtlasTexture)
            {
                return false;
            }

            int totalTiles = m_width * m_height;
            if (s_vertices == null) s_vertices = new List<Vector3>(totalTiles * 4);
            else s_vertices.Clear();
            if (s_triangles == null) s_triangles = new List<int>(totalTiles * 6);
            else s_triangles.Clear();
            if (m_uv == null) m_uv = new List<Vector2>(totalTiles * 4);
            else m_uv.Clear();

            //+++ MeshCollider
            if (s_meshCollVertices == null)
            {
                s_meshCollVertices = new List<Vector3>(totalTiles * 4);
                s_meshCollTriangles = new List<int>(totalTiles * 6);
            }
            else
            {
                s_meshCollVertices.Clear();
                s_meshCollTriangles.Clear();
            }
            //---
            Vector2[] subTileOffset = new Vector2[]
            {
                new Vector2( 0f, 0f ),
                new Vector2( CellSize.x / 2f, 0f ),
                new Vector2( 0f, CellSize.y / 2f ),
                new Vector2( CellSize.x / 2f, CellSize.y / 2f ),
            };
            Vector2 subTileSize = CellSize / 2f;
            m_animatedTiles.Clear();
            bool isEmpty = true;
            for (int ty = 0, tileIdx = 0; ty < m_height; ++ty)
            {
                for (int tx = 0; tx < m_width; ++tx, ++tileIdx)
                {
                    uint tileData = m_tileDataList[tileIdx];
                    if (tileData != Tileset.k_TileData_Empty)
                    {
                        int brushId = (int)((tileData & Tileset.k_TileDataMask_BrushId) >> 16);
                        int tileId = (int)(tileData & Tileset.k_TileDataMask_TileId);
                        Tile tile = Tileset.GetTile(tileId);
                        TilesetBrush tileBrush = null;
                        if (brushId > 0)
                        {
                            tileBrush = Tileset.FindBrush(brushId);
                            if (tileBrush == null)
                            {
                                Debug.LogWarning(ParentTilemap.name + "\\" + name + ": BrushId " + brushId + " not found! GridPos(" + tx + "," + ty + ") tilaData 0x" + tileData.ToString("X"));
                                m_tileDataList[tileIdx] = tileData & ~Tileset.k_TileDataMask_BrushId;
                            }
                            if (tileBrush != null && (m_invalidateBrushes || (tileData & Tileset.k_TileFlag_Updated) == 0))
                            {
                                tileData = tileBrush.Refresh(ParentTilemap, GridPosX + tx, GridPosY + ty, tileData);
                                //+++NOTE: this code add support for animated brushes inside a random brush
                                // Collateral effects of supporting changing the brush id in Refresh:
                                // - When the random brush select a tile data with another brush id, this tile won't be a random tile any more
                                // - If the tilemap is refreshed several times, and at least a tile data contains another brush id, then all tiles will loose the brush id of the random brush
                                if (BrushBehaviour.Instance.BrushTilemap == ParentTilemap) // avoid changing brushId when updating the BrushTilemap
                                {
                                    tileData &= ~Tileset.k_TileDataMask_BrushId;
                                    tileData |= (uint)( brushId << 16 );
                                }
                                int newBrushId = (int)((tileData & Tileset.k_TileDataMask_BrushId) >> 16);
                                if(brushId != newBrushId)
                                {
                                    brushId = newBrushId;
                                    tileBrush = Tileset.FindBrush(brushId);
                                }
                                //---
                                tileData |= Tileset.k_TileFlag_Updated;// set updated flag
                                m_tileDataList[tileIdx] = tileData; // update tileData                                
                                tileId = (int)(tileData & Tileset.k_TileDataMask_TileId);
                                tile = Tileset.GetTile(tileId);
                                // update created objects
                                if (tile != null && tile.prefabData.prefab != null)
                                    CreateTileObject(tileIdx, tile.prefabData);
                                else
                                    DestroyTileObject(tileIdx);
                            }
                        }

                        isEmpty = false;

                        if (tileBrush != null && tileBrush.IsAnimated())
                        {
                            m_animatedTiles.Add(new AnimTileData() { VertexIdx = s_vertices.Count, Brush = tileBrush, SubTileIdx = -1 });
                        }

                        s_currUpdatedTilechunk = this;
                        s_currUVVertex = s_vertices.Count;
                        Rect tileUV;
                        uint[] subtileData = tileBrush != null ? tileBrush.GetSubtiles(ParentTilemap, GridPosX + tx, GridPosY + ty, tileData) : null;
                        if (subtileData == null)
                        {
                            if (tile != null)
                            {
                                if (tile.prefabData.prefab == null || tile.prefabData.showTileWithPrefab //hide the tiles with prefabs ( unless showTileWithPrefab is true )
                                    || tileBrush && tileBrush.IsAnimated()) // ( skip if it's an animated brush )
                                {
                                    tileUV = tile.uv;
                                    _AddTileToMesh(tileUV, tx, ty, tileData, Vector2.zero, CellSize);
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < subtileData.Length; ++i)
                            {
                                uint subTileData = subtileData[i];
                                int subTileId = (int)(subTileData & Tileset.k_TileDataMask_TileId);
                                Tile subTile = Tileset.GetTile(subTileId);
                                tileUV = subTile != null ? subTile.uv : default(Rect);
                                //if (tileUV != default(Rect)) //NOTE: if this is uncommented, there won't be coherence with geometry ( 16 vertices per tiles with subtiles ). But it means also, the tile shouldn't be null.
                                {
                                    _AddTileToMesh(tileUV, tx, ty, subTileData, subTileOffset[i], subTileSize, i);
                                }
                            }
                        }
                    }
                }
            }

            //NOTE: the destruction of tileobjects needs to be done here to avoid a Undo/Redo bug. Check inside DestroyTileObject for more information.
            for (int i = 0; i < m_tileObjToBeRemoved.Count; ++i)
            {
                DestroyTileObject(m_tileObjToBeRemoved[i]);
            }
            m_tileObjToBeRemoved.Clear();

            return !isEmpty;
        }

        static Vector2[] s_tileUV = new Vector2[4];
        private void _AddTileToMesh(Rect tileUV, int tx, int ty, uint tileData, Vector2 subtileOffset, Vector2 subtileCellSize, int subTileIdx = -1)
        {
            float px0 = tx * CellSize.x + subtileOffset.x;
            float py0 = ty * CellSize.y + subtileOffset.y;
            //NOTE: px0 and py0 values are not used to avoid float errors and line artifacts. Don't forget Pixel Snap has to be disabled as well.
            float px1 = tx * CellSize.x + subtileOffset.x + subtileCellSize.x;
            float py1 = ty * CellSize.y + subtileOffset.y + subtileCellSize.y;           

            int vertexIdx = s_vertices.Count;
            s_vertices.Add(new Vector3(px0, py0, 0));
            s_vertices.Add(new Vector3(px1, py0, 0));
            s_vertices.Add(new Vector3(px0, py1, 0));
            s_vertices.Add(new Vector3(px1, py1, 0));

            s_triangles.Add(vertexIdx + 3);
            s_triangles.Add(vertexIdx + 0);
            s_triangles.Add(vertexIdx + 2);
            s_triangles.Add(vertexIdx + 0);
            s_triangles.Add(vertexIdx + 3);
            s_triangles.Add(vertexIdx + 1);

            bool flipH = (tileData & Tileset.k_TileFlag_FlipH) != 0;
            bool flipV = (tileData & Tileset.k_TileFlag_FlipV) != 0;
            bool rot90 = (tileData & Tileset.k_TileFlag_Rot90) != 0;

            //NOTE: xMinMax and yMinMax is opposite if width or height is negative
            float u0 = tileUV.xMin + Tileset.AtlasTexture.texelSize.x * InnerPadding;
            float v0 = tileUV.yMin + Tileset.AtlasTexture.texelSize.y * InnerPadding;
            float u1 = tileUV.xMax - Tileset.AtlasTexture.texelSize.x * InnerPadding;
            float v1 = tileUV.yMax - Tileset.AtlasTexture.texelSize.y * InnerPadding;

            if (flipV)
            {
                float v = v0;
                v0 = v1;
                v1 = v;
            }
            if (flipH)
            {
                float u = u0;
                u0 = u1;
                u1 = u;
            }
            if (rot90)
            {
                s_tileUV[0] = new Vector2(u1, v0);
                s_tileUV[1] = new Vector2(u1, v1);
                s_tileUV[2] = new Vector2(u0, v0);
                s_tileUV[3] = new Vector2(u0, v1);
            }
            else
            {
                s_tileUV[0] = new Vector2(u0, v0);
                s_tileUV[1] = new Vector2(u1, v0);
                s_tileUV[2] = new Vector2(u0, v1);
                s_tileUV[3] = new Vector2(u1, v1);
            }
            if (subTileIdx >= 0)
            {
                for (int i = 0; i < 4; ++i)
                {
                    if (i == subTileIdx) continue;
                    s_tileUV[i] = (s_tileUV[i] + s_tileUV[subTileIdx]) / 2f;
                }
            }
            for (int i = 0; i < 4; ++i)
            {
                m_uv.Add(s_tileUV[i]);
            }
        }
    }
}
