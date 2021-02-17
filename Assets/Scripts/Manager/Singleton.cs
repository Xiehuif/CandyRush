using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// where 限制了T的范围
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T m_instance;
    public static T Instance
    {
        get
        {
            if (m_instance == null)
                Create(typeof(T).Name);
            return m_instance;
        }
    }
    private static void Create(string name)
        {
            if (m_instance == null)
            {
                m_instance = new GameObject().AddComponent<T>();
                m_instance.gameObject.name = name;
            }
        }
    public static bool IsInitialized
    {
        get
        {
            return m_instance != null;
        }
    }

    protected virtual void Awake()
    {
        if (m_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_instance = (T)this;
        }
    }

    protected virtual void OnDestroy()
    {
        if (m_instance == this)
        {
            m_instance = null;
        }
    }
}
