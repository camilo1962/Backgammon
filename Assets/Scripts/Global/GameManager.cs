using System;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The core of the game.
/// </summary>
public class GameManager : IGameManager
{
    #region Singleton

    /// <summary>
    /// GameManager singleton instance.
    /// </summary>
    private static GameManager m_Instance = new GameManager();

    /// <summary>
    /// Gets the GameManager singleton instance.
    /// </summary>
    public static GameManager Instance
    {
        get
        {
            return m_Instance;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GameManager"/> class.
    /// </summary>
    private GameManager()
    {
        this.m_UserName = Environment.MachineName;
        this.m_Player1Name = "SOSA";
        this.m_Player2Name = "JUGADOR 2";
        this.m_Player1ControlMode = PlayerControlMode.User;
        this.m_Player2ControlMode = PlayerControlMode.User;
        this.m_BackgammonPlayMode = BackgammonPlayMode.Match;
        this.m_BackgammonVariant = BackgammonVariants.Standard;
        this.m_MatchScore = 3;
        this.m_InitialStakes = 1;
    }

    #endregion

    #region Variables

    /// <summary>
    /// The user name.
    /// </summary>
    private string m_UserName;

    /// <summary>
    /// The player 1 name.
    /// </summary>
    private string m_Player1Name;

    /// <summary>
    /// The player 2 name.
    /// </summary>
    private string m_Player2Name;

    /// <summary>
    /// The player 1 control mode.
    /// </summary>
    private PlayerControlMode m_Player1ControlMode;

    /// <summary>
    /// The player 2 control mode.
    /// </summary>
    private PlayerControlMode m_Player2ControlMode;

    /// <summary>
    /// The Backgammon play mode.
    /// </summary>
    private BackgammonPlayMode m_BackgammonPlayMode;

    /// <summary>
    /// The Backgammon game variant.
    /// </summary>
    private BackgammonVariants m_BackgammonVariant;

    /// <summary>
    /// Whether the Crawford rule is enabled.
    /// </summary>
    private bool m_IsCrawfordRuleEnabled;

    /// <summary>
    /// Whether the Murphy rule is enabled.
    /// </summary>
    private bool m_IsMurphyRuleEnabled;

    /// <summary>
    /// The match play score to win.
    /// </summary>
    private int m_MatchScore;

    /// <summary>
    /// The money play stakes.
    /// </summary>
    private int m_InitialStakes;
    
    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    public string UserName
    {
        get
        {
            return m_UserName;
        }

        set
        {
            m_UserName = value;
        }
    }

    /// <summary>
    /// Gets or sets the player 1 name.
    /// </summary>
    public string Player1Name
    {
        get
        {
            return m_Player1Name;
        }

        set
        {
            m_Player1Name = value;
        }
    }

    /// <summary>
    /// Gets or sets the player 2 name.
    /// </summary>
    public string Player2Name
    {
        get
        {
            return m_Player2Name;
        }

        set
        {
            m_Player2Name = value;
        }
    }

    /// <summary>
    /// Gets or sets the player 1 control mode.
    /// </summary>
    public PlayerControlMode Player1ControlMode
    {
        get
        {
            return m_Player1ControlMode;
        }
        set
        {
            m_Player1ControlMode = value;
        }
    }

    /// <summary>
    /// Gets or sets the player 2 control mode.
    /// </summary>
    public PlayerControlMode Player2ControlMode
    {
        get
        {
            return m_Player2ControlMode;
        }
        set
        {
            m_Player2ControlMode = value;
        }
    }

    /// <summary>
    /// Gets or sets the Backgammon play mode.
    /// </summary>
    public BackgammonPlayMode BackgammonPlayMode
    {
        get
        {
            return m_BackgammonPlayMode;
        }

        set
        {
            m_BackgammonPlayMode = value;
        }
    }

    /// <summary>
    /// Gets or sets the Backgammon game variant.
    /// </summary>
    public BackgammonVariants BackgammonVariant
    {
        get
        {
            return m_BackgammonVariant;
        }

        set
        {
            m_BackgammonVariant = value;
        }
    }

    /// <summary>
    /// Gets or sets whether the Crawford Rule is enabled.
    /// </summary>
    public bool IsCrawfordRuleEnabled
    {
        get
        {
            return m_IsCrawfordRuleEnabled;
        }

        set
        {
            m_IsCrawfordRuleEnabled = value;
        }
    }

    /// <summary>
    /// Gets or sets whether the Murphy Rule is enabled.
    /// </summary>
    public bool IsMurphyRuleEnabled
    {
        get
        {
            return m_IsMurphyRuleEnabled;
        }

        set
        {
            m_IsMurphyRuleEnabled = value;
        }
    }

    /// <summary>
    /// Gets or sets the match score for match play mode.
    /// </summary>
    public int MatchScore
    {
        get
        {
            return m_MatchScore;
        }

        set
        {
            m_MatchScore = value;
        }
    }

    /// <summary>
    /// Gets or sets the initial stakes for money play mode.
    /// </summary>
    public int InitialStakes
    {
        get
        {
            return m_InitialStakes;
        }

        set
        {
            m_InitialStakes = value;
        }
    }

    #endregion

    #region Game Methods

    /// <summary>
    /// Sets the player 1 control mode with the specified int value.
    /// </summary>
    /// <param name="playerControlMode"></param>
    public void SetPlayer1ControlMode(int playerControlMode)
    {
        m_Player1ControlMode = (PlayerControlMode)playerControlMode;
    }

    /// <summary>
    /// Sets the player 2 control mode with the specified int value.
    /// </summary>
    /// <param name="playerControlMode"></param>
    public void SetPlayer2ControlMode(int playerControlMode)
    {
        m_Player2ControlMode = (PlayerControlMode)playerControlMode;
    }
    
    /// <summary>
    /// Sets the Backgammon play mode with the specified int value.
    /// </summary>
    /// <param name="playMode"></param>
    public void SetBackgammonPlayMode(int playMode)
    {
        m_BackgammonPlayMode = (BackgammonPlayMode)playMode;
    }

    /// <summary>
    /// Sets the Backgammon game variant with the specified int value.
    /// </summary>
    /// <param name="gameMode"></param>
    public void SetBackgammonVariant(int backgammonVariant)
    {
        m_BackgammonVariant = (BackgammonVariants)backgammonVariant;
    }

    /// <summary>
    /// Loads the game scene.
    /// </summary>
    public void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("GameScene");
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    public void LoadMainMenuScene()
    {
        SceneManager.LoadSceneAsync("MainMenuScene");
    }

    /// <summary>
    /// Exits the application.
    /// </summary>
    public void ExitApplication()
    {
        Application.Quit();
    }

    #endregion
}
