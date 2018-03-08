using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
	public string tag;
	public GameObject prefab;
	public int size;

}

public class PoolManager : MonoBehaviour 
{
	public static PoolManager instance = null;
	public List<Pool> pools;
	public Dictionary<string , Queue<GameObject>> poolDictionary;
	
	void Awake()
	{
		if( instance == null )
			instance = this;
		else if( instance != this )
			Destroy( gameObject );

		DontDestroyOnLoad( gameObject );

	}

	void Start () 
	{
		poolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach( Pool pool in pools )
		{
			Queue<GameObject> objectPool =  new Queue<GameObject>();

			GameObject emptyGameObject = new GameObject();
			emptyGameObject.name = pool.tag;
			
			for( int i = 0; i <= pool.size; i++ )
			{
				GameObject obj = Instantiate( pool.prefab );
				obj.transform.parent = emptyGameObject.transform;
				obj.SetActive( false );
				objectPool.Enqueue( obj );
			}

			poolDictionary.Add( pool.tag , objectPool );
			emptyGameObject.transform.parent = transform;

		}
	}

	public GameObject SpawnFromPool( string tag , Vector3 position, Quaternion rotation )
	{
		
		if( !poolDictionary.ContainsKey( tag ) )
		{
			Debug.LogWarning( "Pool with tag " + tag + " doesn't exist" );
			return null;
		}
		
		GameObject objToSpawn = poolDictionary[tag].Dequeue();

		objToSpawn.SetActive( true );
		objToSpawn.transform.position = position;
		//objToSpawn.transform.rotation = rotation;

		poolDictionary[tag].Enqueue( objToSpawn );


		return objToSpawn;
	}
	

}
