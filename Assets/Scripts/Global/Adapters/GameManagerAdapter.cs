using UnityEngine;

public class GameManagerAdapter : MonoBehaviour, IGameManager
{
    #region Properties

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    public string UserName
    {
        get
        {
            return GameManager.Instance.UserName;
        }

        set
        {
            GameManager.Instance.UserName = value;
        }
    }

    /// <summary>
    /// Gets or sets the player 1 name.
    /// </summary>
    public string Player1Name
    {
        get
        {
            return GameManager.Instance.Player1Name;
        }

        set
        {
            GameManager.Instance.Player1Name = value;
        }
    }

    /// <summary>
    /// Gets or sets the player 2 name.
    /// </summary>
    public string Player2Name
    {
        get
        {
            return GameManager.Instance.Player2Name;
        }

        set
        {
            GameManager.Instance.Player2Name = value;
        }
    }

    /// <summary>
    /// Gets or sets the player 1 control mode.
    /// </summary>
    public PlayerControlMode Player1ControlMode
    {
        get
        {
            return GameManager.Instance.Player1ControlMode;
        }

        set
        {
            GameManager.Instance.Player1ControlMode = value;
        }
    }

    /// <summary>
    /// Gets or sets the player 2 control mode.
    /// </summary>
    public PlayerControlMode Player2ControlMode
    {
        get
        {
            return GameManager.Instance.Player2ControlMode;
        }

        set
        {
            GameManager.Instance.Player2ControlMode = value;
        }
    }

    /// <summary>
    /// Gets or sets the Backgammon play mode.
    /// </summary>
    public BackgammonPlayMode BackgammonPlayMode
    {
        get
        {
            return GameManager.Instance.BackgammonPlayMode;
        }

        set
        {
            GameManager.Instance.BackgammonPlayMode = value;
        }
    }

    /// <summary>
    /// Gets or sets the Backgammon game variant.
    /// </summary>
    public BackgammonVariants BackgammonVariant
    {
        get
        {
            return GameManager.Instance.BackgammonVariant;
        }

        set
        {
            GameManager.Instance.BackgammonVariant = value;
        }
    }

    /// <summary>
    /// Gets or sets whether the Crawford Rule is enabled.
    /// </summary>
    public bool IsCrawfordRuleEnabled
    {
        get
        {
            return GameManager.Instance.IsCrawfordRuleEnabled;
        }

        set
        {
            GameManager.Instance.IsCrawfordRuleEnabled = value;
        }
    }

    /// <summary>
    /// Gets or sets whether the Murphy Rule is enabled.
    /// </summary>
    public bool IsMurphyRuleEnabled
    {
        get
        {
            return GameManager.Instance.IsMurphyRuleEnabled;
        }

        set
        {
            GameManager.Instance.IsMurphyRuleEnabled = value;
        }
    }

    /// <summary>
    /// Gets or sets the match score for match play mode.
    /// </summary>
    public int MatchScore
    {
        get
        {
            return GameManager.Instance.MatchScore;
        }

        set
        {
            GameManager.Instance.MatchScore = value;
        }
    }

    /// <summary>
    /// Gets or sets the initial stakes for money play mode.
    /// </summary>
    public int InitialStakes
    {
        get
        {
            return GameManager.Instance.InitialStakes;
        }

        set
        {
            GameManager.Instance.InitialStakes = value;
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
        GameManager.Instance.SetPlayer1ControlMode(playerControlMode);
    }

    /// <summary>
    /// Sets the player 2 control mode with the specified int value.
    /// </summary>
    /// <param name="playerControlMode"></param>
    public void SetPlayer2ControlMode(int playerControlMode)
    {
        GameManager.Instance.SetPlayer2ControlMode(playerControlMode);
    }

    /// <summary>
    /// Sets the Backgammon play mode with the specified int value.
    /// </summary>
    /// <param name="playMode"></param>
    public void SetBackgammonPlayMode(int playMode)
    {
        GameManager.Instance.SetBackgammonPlayMode(playMode);
    }

    /// <summary>
    /// Set the Backgammon variant with the specified int value.
    /// </summary>
    /// <param name="gameMode"></param>
    public void SetBackgammonVariant(int backgammonVariant)
    {
        GameManager.Instance.SetBackgammonVariant(backgammonVariant);
    }

    /// <summary>
    /// Starts a new Backgammon match.
    /// </summary>
    public void LoadGameScene()
    {
        GameManager.Instance.LoadGameScene();
    }

    /// <summary>
    /// Ends the backgammon match. Returns the user to the main menu.
    /// </summary>
    public void LoadMainMenuScene()
    {
        GameManager.Instance.LoadMainMenuScene();
    }

    /// <summary>
    /// Exits the application.
    /// </summary>
    public void ExitApplication()
    {
        GameManager.Instance.ExitApplication();
    }

    #endregion
}