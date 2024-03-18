using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct BGMoveOptions : IMoveOptions
{
    #region Variables

    /// <summary>
    /// Gets the list of available moves.
    /// </summary>
    public List<BGMove> Moves
    {
        get;
        private set;
    }

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="BGMoveOptions"/> class.
    /// </summary>
    /// <param name="player"></param>
    /// <param name="boardMap"></param>
    /// <param name="diceRoll"></param>
    public BGMoveOptions(BGPlayerID player, BGBoardMap boardMap, BGDiceRoll diceRoll)
    {
        Moves = null;
        Moves = GetMoves(player, boardMap, diceRoll);

        // A player must use both numbers of a roll if possible.
        // If either number can be played but not both, the player must play the larger one.

        if (diceRoll.Roll1 != diceRoll.Roll2 && 
            diceRoll.CanUse(diceRoll.Roll1) &&
            diceRoll.CanUse(diceRoll.Roll2))
        {
            List<BGMove> roll1OnlyMoves = new List<BGMove>();
            List<BGMove> roll2OnlyMoves = new List<BGMove>();
            List<BGMove> bothRollMoves = new List<BGMove>();

            foreach (BGMove move in Moves)
            {
                BGBoardMap newBoardMap = boardMap.Copy();
                newBoardMap.Use(move);

                BGDiceRoll newDiceRoll = diceRoll.Copy();
                newDiceRoll.Use(move);

                BGMoveOptions newMoveOptions = new BGMoveOptions(player, newBoardMap, newDiceRoll);

                if (move.DiceRoll == diceRoll.Roll1 && newMoveOptions.CanMoveWith(diceRoll.Roll2) ||
                    move.DiceRoll == diceRoll.Roll2 && newMoveOptions.CanMoveWith(diceRoll.Roll1))
                {
                    bothRollMoves.Add(move);
                }
                else if (move.DiceRoll == diceRoll.Roll1 && !newMoveOptions.CanMoveWith(diceRoll.Roll2))
                {
                    roll1OnlyMoves.Add(move);
                }
                else if (move.DiceRoll == diceRoll.Roll2 && !newMoveOptions.CanMoveWith(diceRoll.Roll1))
                {
                    roll2OnlyMoves.Add(move);
                }                
            }

            if (bothRollMoves.Count > 0)
            {
                Moves = bothRollMoves;
            }
            else if (diceRoll.Roll1 > diceRoll.Roll2 && roll1OnlyMoves.Count > 0)
            {
                Moves = roll1OnlyMoves;
            }
            else if (diceRoll.Roll2 > diceRoll.Roll1 && roll2OnlyMoves.Count > 0)
            {
                Moves = roll2OnlyMoves;
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets whether there are any moves available.
    /// </summary>
    public bool CanMove()
    {
        return Moves.Count > 0;
    }

    /// <summary>
    /// Gets whether a move can be made with the specified roll.
    /// </summary>
    /// <param name="roll"></param>
    /// <returns></returns>
    public bool CanMoveWith(int roll)
    {
        return Moves.Any(move => move.DiceRoll == roll);
    }

    /// <summary>
    /// Gets whether a move can be made from the point with the specified ID.
    /// </summary>
    /// <param name="pointID"></param>
    /// <returns></returns>
    public bool CanMoveFrom(BGPointID pointID)
    {
        return Moves.Any(move => move.StartPointID == pointID);
    }

    /// <summary>
    /// Gets whether a move can be made from the point with the specified ID using the specified dice roll.
    /// </summary>
    /// <param name="pointID"></param>
    /// <param name="diceRoll"></param>
    /// <returns></returns>
    public bool CanMoveFrom(BGPointID pointID, int diceRoll)
    {
        return Moves.Any(move => move.StartPointID == pointID && move.DiceRoll == diceRoll);
    }

    /// <summary>
    /// Gets a move that can be made from the point with the specified ID using the specified dice roll.
    /// </summary>
    /// <param name="pointID"></param>
    /// <param name="diceRoll"></param>
    /// <returns></returns>
    public BGMove GetMoveFrom(BGPointID pointID, int diceRoll)
    {
        return Moves.FirstOrDefault(move => move.StartPointID == pointID && move.DiceRoll == diceRoll);
    }

    /// <summary>
    /// Gets whether a move exists between the start and end points with the specified IDs.
    /// </summary>
    public bool CanMove(BGPointID startPointID, BGPointID endPointID)
    {
        return Moves.Any(move => move.StartPointID == startPointID && move.EndPointID == endPointID);
    }

    /// <summary>
    /// Gets a move involving the start and end points with the specified IDs. Returns null if no such move exists.
    /// </summary>
    /// <param name="startPointID"></param>
    /// <param name="endPointID"></param>
    /// <returns></returns>
    public BGMove GetMove(BGPointID startPointID, BGPointID endPointID)
    {
        return Moves.FirstOrDefault(move => move.StartPointID == startPointID && move.EndPointID == endPointID);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Gets a list of all moves that can be made for the specified player with the specified board and dice roll.
    /// </summary>
    private List<BGMove> GetMoves(BGPlayerID player, BGBoardMap board, BGDiceRoll diceRoll)
    {
        List<BGMove> moves = new List<BGMove>();

        if (player == BGPlayerID.None)
        {
            return moves;
        }

        BGPoint home = board.GetHome(player);
        BGPoint jail = board.GetJail(player);
        BGPoint[] innerTable = board.GetInnerTable(player);
        BGPoint[] outerTable = board.GetOuterTable(player);

        // Find available moves.

        if (jail.Count > 0)
        {
            for (int roll = 1; roll <= 6; roll++)
            {
                if (diceRoll.CanUse(roll) && !outerTable[roll - 1].IsBlocking(player))
                {
                    moves.Add(new BGMove(jail, outerTable[roll - 1], roll));
                }
            }
        }
        else
        {
            for (int roll = 1; roll <= 6; roll++)
            {
                if (diceRoll.CanUse(roll))
                {
                    for (BGPointID point = BGPointID.Point1; point <= BGPointID.Point24; point++)
                    {
                        BGPoint from = board.GetPoint(point);

                        if (from.IsOccupiedBy(player))
                        {
                            BGPoint to = null;

                            if (player == BGPlayerID.Player1 && ((point - roll) >= BGPointID.Point1))
                            {
                                to = board.GetPoint(point - roll);
                            }
                            else if (player == BGPlayerID.Player2 && ((point + roll) <= BGPointID.Point24))
                            {
                                to = board.GetPoint(point + roll);
                            }

                            if (to != null && !to.IsBlocking(player))
                            {
                                moves.Add(new BGMove(from, to, roll));
                            }
                        }
                    }
                }
            }

            if (!outerTable.Any(point => point.IsOccupiedBy(player)))
            {
                // Get max distance from home.

                int maxDistance = 0;

                for (int i = 0; i < innerTable.Length; i++)
                {
                    if (innerTable[i].IsOccupiedBy(player))
                    {
                        maxDistance = i + 1;
                    }
                }

                // Find moves that can be used to bear off.

                for (int roll = 1; roll <= 6; roll++)
                {
                    if (diceRoll.CanUse(roll))
                    {
                        if (innerTable[roll - 1].IsOccupiedBy(player))
                        {
                            moves.Add(new BGMove(innerTable[roll - 1], home, roll));
                        }
                        else if (maxDistance > 0 && innerTable[maxDistance - 1].IsOccupiedBy(player) && roll >= maxDistance)
                        {
                            moves.Add(new BGMove(innerTable[maxDistance - 1], home, roll));
                        }
                    }
                }
            }
        }

        return moves;
    }

    #endregion

    #region Override Methods

    /// <summary>
    /// ToString()
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        foreach(BGMove move in Moves)
        {
            sb.AppendLine(move.ToString());
        }

        return string.Format(sb.ToString());
    }

    #endregion
}
