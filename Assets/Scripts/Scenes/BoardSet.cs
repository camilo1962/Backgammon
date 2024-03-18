using System;
using UnityEngine;

[Serializable]
public class BoardSet
{
    #region Variables

    /// <summary>
    /// The board prefab.
    /// </summary>
    [SerializeField]
    private Board m_Board = null;

    /// <summary>
    /// The player 1 checker prefab.
    /// </summary>
    [SerializeField]
    private Checker m_CheckerP1 = null;

    /// <summary>
    /// The player 2 checker prefab.
    /// </summary>
    [SerializeField]
    private Checker m_CheckerP2 = null;

    /// <summary>
    /// The player 1 die prefab.
    /// </summary>
    [SerializeField]
    private Die m_DieP1 = null;

    /// <summary>
    /// The player 2 die prefab.
    /// </summary>
    [SerializeField]
    private Die m_DieP2 = null;

    /// <summary>
    /// The doubling cube prefab.
    /// </summary>
    [SerializeField]
    private DoublingCube m_DoublingCube = null;

    /// <summary>
    /// Gets the origin indicator.
    /// </summary>
    [SerializeField]
    private GameObject m_OriginIndicator = null;

    /// <summary>
    /// Gets the destination indicator.
    /// </summary>
    [SerializeField]
    private GameObject m_DestinationIndicator = null;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the board prefab.
    /// </summary>
    public Board Board
    {
        get
        {
            return m_Board;
        }
    }

    /// <summary>
    /// Gets the player 1 checker prefab.
    /// </summary>
    public Checker CheckerP1
    {
        get
        {
            return m_CheckerP1;
        }
    }
    /// <summary>
    /// Gets the player 2 checker prefab.
    /// </summary>
    public Checker CheckerP2
    {
        get
        {
            return m_CheckerP2;
        }
    }

    /// <summary>
    /// Gets the player 1 die prefab.
    /// </summary>
    public Die DieP1
    {
        get
        {
            return m_DieP1;
        }
    }

    /// <summary>
    /// Gets the player 2 die prefab.
    /// </summary>
    public Die DieP2
    {
        get
        {
            return m_DieP2;
        }
    }

    /// <summary>
    /// Gets the doubling cube prefab.
    /// </summary>
    public DoublingCube DoublingCube
    {
        get
        {
            return m_DoublingCube;
        }
    }

    /// <summary>
    /// Gets the origin indicator.
    /// </summary>
    public GameObject OriginIndicator
    {
        get
        {
            return m_OriginIndicator;
        }
    }

    /// <summary>
    /// Gets the destination indicator.
    /// </summary>
    public GameObject DestinationIndicator
    {
        get
        {
            return m_DestinationIndicator;
        }
    }

    #endregion
}
