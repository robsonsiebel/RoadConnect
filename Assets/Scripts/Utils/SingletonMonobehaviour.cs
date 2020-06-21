using UnityEngine;


public class SingletonMonobehavior<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object m_Lock = new object();
    private static T m_Instance;

    protected SingletonMonobehavior()
    {
    }

    public static T Instance
    {
        get
        {
            lock (m_Lock)
            {
                if (m_Instance == null)
                {
                    m_Instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        string.Format("Internal Error : more than one object of type {0} found!", typeof(T).ToString());
                    }
                }

                return m_Instance;
            }
        }
    }
}


