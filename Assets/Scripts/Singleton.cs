﻿
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {

	private static T Instance;
	public static T instance {

		get {
			if (Instance == null) {
				Instance = FindObjectOfType<T>();
				if (Instance == null) {
					GameObject obj = new GameObject();
					obj.name = typeof(T).Name;
					Instance = obj.AddComponent<T>();
				}
			}
			return Instance;
		}

	}

	public virtual void Awake() {
		if (Instance == null) {
			Instance = this as T;
			DontDestroyOnLoad(this.gameObject);
		}
		else {
			Destroy(gameObject);
		}

		OnAwake();
	}

	protected virtual void OnAwake() { }
}




