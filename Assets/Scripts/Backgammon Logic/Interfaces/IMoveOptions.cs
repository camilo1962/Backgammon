using System.Collections.Generic;

public interface IMoveOptions
{
    #region Methods

    /// <summary>
    /// Gets whether there are any moves available.
    /// </summary>
    bool CanMove();

    /// <summary>
    /// Gets whether a move can be made with the specified roll.
    /// </summary>
    /// <param name="roll"></param>
    /// <returns></returns>
    bool CanMoveWith(int roll);

    /// <summary>
    /// Gets whether a move can be made from the point with the specified ID.
    /// </summary>
    /// <param name="pointID"></param>
    /// <returns></returns>
    bool CanMoveFrom(BGPointID pointID);

    /// <summary>
    /// Gets whether a move can be made from the point with the specified ID using the specified dice roll.
    /// </summary>
    /// <param name="pointID"></param>
    /// <param name="diceRoll"></param>
    /// <returns></returns>
    bool CanMoveFrom(BGPointID pointID, int diceRoll);

    /// <summary>
    /// Gets a move that can be made from the point with the specified ID using the specified dice roll.
    /// </summary>
    /// <param name="pointID"></param>
    /// <param name="diceRoll"></param>
    /// <returns></returns>
    BGMove GetMoveFrom(BGPointID pointID, int diceRoll);

    /// <summary>
    /// Gets whether a move exists between the start and end points with the specified IDs.
    /// </summary>
    bool CanMove(BGPointID startPointID, BGPointID endPointID);

    /// <summary>
    /// Gets a move involving the start and end points with the specified IDs. Returns null if no such move exists.
    /// </summary>
    /// <param name="startPointID"></param>
    /// <param name="endPointID"></param>
    /// <returns></returns>
    BGMove GetMove(BGPointID startPointID, BGPointID endPointID);

    #endregion
}