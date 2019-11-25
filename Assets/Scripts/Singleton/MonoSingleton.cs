using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : Component
{
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                    {
                        GameObject obj = new GameObject();
                        obj.hideFlags = HideFlags.HideAndDontSave;//隐藏实例化的new game object，下同
                        instance = (T)obj.AddComponent(typeof(T));
                    }
                }
                return instance;
            }
        }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
