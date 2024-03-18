using System.Collections.Generic;

public class BGState
{
    #region Variables

    /// <summary>
    /// The board map of this state.
    /// </summary>
    public BGBoardMap BoardMap
    {
        get;
        private set;
    }

    /// <summary>
    /// The moves that can be made from this state.
    /// </summary>
    public List<BGMove> Moves
    {
        get;
        private set;
    }

    /// <summary>
    /// Possible states by move.
    /// </summary>
    public Dictionary<BGMove, BGState> NextStates
    {
        get;
        private set;
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BGState"/> class.
    /// </summary>
    /// <param name="boardMap">Board map.</param>
    /// <param name="moveOptions">Move options.</param>
    /// <param name="diceRoll">Dice roll.</param>
    public BGState(BGBoardMap boardMap, BGMoveOptions moveOptions, BGDiceRoll diceRoll)
    {
        BoardMap = boardMap;
        Moves = new List<BGMove>();
        NextStates = new Dictionary<BGMove, BGState>();

        foreach (BGMove move in moveOptions.Moves)
        {
            BGBoardMap newBoardMap = boardMap.Copy();
            newBoardMap.Use(move);

            BGDiceRoll newDiceRoll = diceRoll.Copy();
            newDiceRoll.Use(move);

            BGMoveOptions newMoveOptions = new BGMoveOptions(BGPlayerID.Player2, newBoardMap, newDiceRoll);

            Moves.Add(move);
            NextStates[move] = new BGState(newBoardMap, newMoveOptions, newDiceRoll);
        }
    }

    #endregion
}