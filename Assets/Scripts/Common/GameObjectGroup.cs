using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObjectGroup
{
    #region Private Variables

    /// <summary>
    /// The game object used in this group.
    /// </summary>
    [SerializeField]
    private GameObject m_Prefab = null;

    /// <summary>
    /// The game objects.
    /// </summary>
    private List<GameObject> m_GameObjects;

    #endregion

    #region Contructor

    /// <summary>
    /// Initializes a new instance of the <see cref="GameObjectGroup"/> class.
    /// </summary>
    public GameObjectGroup()
    {
        this.m_GameObjects = new List<GameObject>();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Adds and returns a game object of this group.
    /// </summary>
    /// <returns></returns>
    public GameObject Add()
    {
        GameObject gameObject = MonoBehaviour.Instantiate(m_Prefab) as GameObject;
        m_GameObjects.Add(gameObject);

        return gameObject;
    }

    /// <summary>
    /// Removes and destroys all game objects in this group.
    /// </summary>
    public void Clear()
    {
        foreach (GameObject gameObject in m_GameObjects)
        {
            UnityEngine.Object.Destroy(gameObject);
        }

        m_GameObjects.Clear();
    }

    #endregion
}
