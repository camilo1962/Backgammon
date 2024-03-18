using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Events

    /// <summary>
    /// Event that is raised whenever the player scores has changed.
    /// </summary>
    public EventHandler ScoresChanged;

    #endregion

    #region Public Variables

    /// <summary>
    /// Identifies the player as player 1 or player 2.
    /// </summary>
    public BGPlayerID ID = BGPlayerID.Player1;

    /// <summary>
    /// The player's name.
    /// </summary>
    public string Name = "Jugador";

    /// <summary>
    /// The player's checkers.
    /// </summary>
    public List<Checker> Checkers = new List<Checker>();

    /// <summary>
    /// The player's dice.
    /// </summary>
    public Dice Dice;

    /// <summary>
    /// The player's checker prefab.
    /// </summary>
    public GameObject CheckerPrefab;

    /// <summary>
    /// Gets whether the player is controlled by the user, AI, or by another user via multiplayer.
    /// </summary>
    public PlayerControlMode PlayerControlMode = PlayerControlMode.User;

    #endregion

    #region Private Variables

    /// <summary>
    /// The player's score.
    /// </summary>
    public int m_Score = 0;

    /// <summary>
    /// The number of rounds the player has won.
    /// </summary>
    public int m_RoundsWon = 0;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the players score.
    /// </summary>
    public int Score
    {
        get
        {
            return m_Score;
        }
        set
        {
            m_Score = value;
            EventHelper.Raise(this, ScoresChanged);
        }
    }

    /// <summary>
    /// Gets or sets the number of rounds the player has won.
    /// </summary>
    public int RoundsWon
    {
        get
        {
            return m_RoundsWon;
        }
        set
        {
            m_RoundsWon = value;
        }
    }

    /// <summary>
    /// Gets the opponent player ID.
    /// </summary>
    public BGPlayerID OpponentID
    {
        get
        {
            switch (ID)
            {
                default:
                case (BGPlayerID.Player1): { return BGPlayerID.Player2; }
                case (BGPlayerID.Player2): { return BGPlayerID.Player1; }
            }
        }
    }

    /// <summary>
    /// Gets whether the player is controlled by the user.
    /// </summary>
    public bool IsUserControlled
    {
        get
        {
            return PlayerControlMode == PlayerControlMode.User;
        }
    }

    /// <summary>
    /// Gets whether the player is controlled by an AI.
    /// </summary>
    public bool IsAIControlled
    {
        get
        {
            return PlayerControlMode == PlayerControlMode.AI;
        }
    }

    /// <summary>
    /// Gets whether the player is controlled by another user via multiplayer.
    /// </summary>
    public bool IsMultiplayerControlled
    {
        get
        {
            return PlayerControlMode == PlayerControlMode.Multiplayer;
        }
    }

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Used to initialize any variables or game state before the game starts.
    /// </summary>
    void Awake()
    {
        switch (GameManager.Instance.BackgammonVariant)
        {
            default:
            case (BackgammonVariants.Standard):
            case (BackgammonVariants.SuddenDeath):
            case (BackgammonVariants.Rush):
                {
                    CreateCheckers();
                }
                break;
        }
    }

    /// <summary>
    /// Called on the frame when a script is enabled.
    /// </summary>
    void Start()
    {
    }

    #endregion

    #region Public Region

    /// <summary>
    /// Creates 15 checkers for the player.
    /// </summary>
    public void CreateCheckers()
    {
        CreateCheckers(15);
    }

    /// <summary>
    /// Creates the specified number of checkers for the player.
    /// </summary>
    /// <param name="count"></param>
    public void CreateCheckers(int count)
    {
        DestroyCheckers();

        for (int i = 0; i < count; i++)
        {
            GameObject checkerObject = MonoBehaviour.Instantiate(CheckerPrefab) as GameObject;
            checkerObject.transform.SetParent(this.transform);

            Checker checker = checkerObject.GetComponent<Checker>();
            checker.Owner = this;

            Checkers.Add(checker);
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Destroys all checkers.
    /// </summary>
    private void DestroyCheckers()
    {
        if (Checkers.Count > 0)
        {
            foreach (Checker checker in Checkers)
            {
                Destroy(checker.gameObject);
            }

            Checkers.Clear();
        }
    }

    #endregion
}
