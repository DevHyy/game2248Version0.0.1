using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance = null;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                T[] Ts = FindObjectsOfType<T>();
                if (Ts.Length > 0)
                    instance = Ts[0];
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        T[] Ts = FindObjectsOfType<T>();
        if (Ts.Length > 1)
            Destroy(this);
    }
}
