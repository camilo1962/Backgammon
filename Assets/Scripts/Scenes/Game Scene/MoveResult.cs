public enum MoveResult
{
    /// <summary>
    /// The move was made successfully.
    /// </summary>
    Success,

    /// <summary>
    /// The move was invalid.
    /// </summary>
    Invalid,

    /// <summary>
    /// The move was made successfully and results in a Player 1 win.
    /// </summary>
    Player1Win,

    /// <summary>
    /// The move was made successfully and results in a Player 2 win.
    /// </summary>
    Player2Win
}