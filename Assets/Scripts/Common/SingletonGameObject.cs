using UnityEngine;

/// <summary>
/// Boilerplate generic singleton base class for GameObjects.
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonGameObject<T> : MonoBehaviour where T : Component
{
    /// <summary>
    /// Singleton instance.
    /// </summary>
    private static T m_Instance;

    /// <summary>
    /// Gets the singleton instance.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<T>();

                if (m_Instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    m_Instance = obj.AddComponent<T>();
                }
            }

            return m_Instance;
        }
    }

    /// <summary>
    /// Used to initialize any variables or game state before the game starts.
    /// </summary>
    public virtual void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}