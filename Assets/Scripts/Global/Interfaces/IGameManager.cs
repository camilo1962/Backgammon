
public interface IGameManager
{
    #region Properties

    /// <summary>
    /// Gets or sets the user name.
    /// </summary>
    string UserName
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the player 1 name.
    /// </summary>
    string Player1Name
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the player 2 name.
    /// </summary>
    string Player2Name
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the player 1 control mode.
    /// </summary>
    PlayerControlMode Player1ControlMode
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the player 2 control mode.
    /// </summary>
    PlayerControlMode Player2ControlMode
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the Backgammon play mode.
    /// </summary>
    BackgammonPlayMode BackgammonPlayMode
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the Backgammon game variant.
    /// </summary>
    BackgammonVariants BackgammonVariant
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets whether the Crawford Rule is enabled.
    /// </summary>
    bool IsCrawfordRuleEnabled
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets whether the Murphy Rule is enabled.
    /// </summary>
    bool IsMurphyRuleEnabled
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the match score for match play mode.
    /// </summary>
    int MatchScore
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the initial stakes for money play mode.
    /// </summary>
    int InitialStakes
    {
        get;
        set;
    }
    
    #endregion

    #region Game Methods

    /// <summary>
    /// Sets the player 1 control mode with the specified int value.
    /// </summary>
    /// <param name="playerControlMode"></param>
    void SetPlayer1ControlMode(int playerControlMode);

    /// <summary>
    /// Sets the player 2 control mode with the specified int value.
    /// </summary>
    /// <param name="playerControlMode"></param>
    void SetPlayer2ControlMode(int playerControlMode);

    /// <summary>
    /// Sets the Backgammon play mode with the specified int value.
    /// </summary>
    /// <param name="playMode"></param>
    void SetBackgammonPlayMode(int playMode);

    /// <summary>
    /// Sets the Backgammon variant with the specified int value.
    /// </summary>
    /// <param name="aiDifficulty"></param>
    void SetBackgammonVariant(int gameMode);

    /// <summary>
    /// Starts a new Backgammon match.
    /// </summary>
    void LoadGameScene();

    /// <summary>
    /// Ends the backgammon match. Returns the user to the main menu.
    /// </summary>
    void LoadMainMenuScene();

    /// <summary>
    /// Exits the application.
    /// </summary>
    void ExitApplication();

    #endregion
}