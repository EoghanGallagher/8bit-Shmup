using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CreativeSpore.SuperTilemapEditor
{
    [AddComponentMenu("SuperTilemapEditor/TilemapGroup", 10)]
    [DisallowMultipleComponent]
    [ExecuteInEditMode] // allow OnTransformChildrenChanged to be called
    public class TilemapGroup : MonoBehaviour 
    {
        public Tilemap SelectedTilemap { 
            get { return m_selectedIndex >= 0 && m_selectedIndex < m_tilemaps.Count ? m_tilemaps[m_selectedIndex] : null; }
            set{ m_selectedIndex = m_tilemaps != null? m_tilemaps.IndexOf(value) : -1; }
        }
        public List<Tilemap> Tilemaps { get { return m_tilemaps; } }
        public float UnselectedColorMultiplier { get { return m_unselectedColorMultiplier; } set { m_unselectedColorMultiplier = value; } }
        public bool DisplayTilemapRList { get { return m_displayTilemapRList; } set { m_displayTilemapRList = value; } }
        public Tilemap this[int idx] { get { return m_tilemaps[idx]; } }
        public Tilemap this[string name] { get { return FindTilemapByName(name); } }

        [SerializeField]
        private List<Tilemap> m_tilemaps;
        [SerializeField]
        private int m_selectedIndex = -1;
        [SerializeField, Range(0f, 1f)]
        private float m_unselectedColorMultiplier = 1f;
        [SerializeField]
        private bool m_displayTilemapRList = true;

        void OnValidate()
        {
            if (Tilemaps.Count != transform.childCount)
            {
                Refresh();
            }
        }

        void OnTransformChildrenChanged()
        {
            Refresh();
        }

	    void Start () 
        {
            Refresh();
	    }

        void OnDrawGizmosSelected()
        {
            if(SelectedTilemap)
            {
                SelectedTilemap.SendMessage("DoDrawGizmos", SendMessageOptions.DontRequireReceiver);
            }
        }
	    
        public Tilemap FindTilemapByName(string name)
        {
            return Tilemaps.Find(x => x.name == name);
        }

        public void Refresh()
        {
            m_tilemaps = new List<Tilemap>( GetComponentsInChildren<Tilemap>(true) );
            if (m_tilemaps.Count > 0 && m_selectedIndex < 0) m_selectedIndex = 0;
            m_selectedIndex = Mathf.Clamp(m_selectedIndex, -1, m_tilemaps.Count);
        }
    }
}