using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

	public static ObjectPooler instance;

	Dictionary<string, List<GameObject>> objectLookup = new Dictionary<string, List<GameObject>>();


	void Awake(){
		if (instance == null) {
			instance = this;
		}
	}

	public static void StartPool<T>(T prefab, List<GameObject> list) where T:Component
	{
		if (!instance.objectLookup.ContainsKey (prefab.name)) {
			instance.objectLookup.Add (prefab.name, list);
		}
	}

	public static void StartPool<T>(T prefab) where T:Component
	{
		StartPool<T>(prefab, new List<GameObject>());
	}

	public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T:Component
	{
		string name = prefab.name;
		if (instance.objectLookup.ContainsKey (name)) {
			T newObject = null;
			List<GameObject> list = instance.objectLookup [name];
			foreach (GameObject item in list) {
				if (!item.gameObject.activeSelf) {
					newObject = item.GetComponent<T>();
					break;
				}
			}

			if (newObject != null) {
				newObject.transform.position = position;
				newObject.transform.rotation = rotation;
				newObject.gameObject.SetActive (true);
				return (T)newObject;
			}

			newObject = Instantiate (prefab, position, rotation) as T;
			list.Add (newObject.gameObject);
			instance.objectLookup [name] = list;
			return (T)newObject;

		} else {
			StartPool<T> (prefab);
			T newObject = Instantiate (prefab, position, rotation) as T;
			instance.objectLookup [prefab.name].Add (newObject.gameObject);
			return newObject;
		}


	}

	public static T Spawn<T>(T prefab, Vector3 position) where T:Component
	{
		return Spawn<T> (prefab, position, Quaternion.identity);
	}

	public static T Spawn<T>(T prefab) where T:Component
	{
		return Spawn<T> (prefab, Vector3.zero, Quaternion.identity);
	}
}
