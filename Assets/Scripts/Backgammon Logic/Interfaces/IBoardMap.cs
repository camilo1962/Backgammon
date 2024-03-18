interface IBoardMap
{
    #region Methods

    /// <summary>
    /// Sets the point with the specified point Id with the specified player and checker count.
    /// Returns whether the operation was successful.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="player"></param>
    /// <param name="count"></param>
    bool SetPoint(BGPointID id, BGPlayerID player, int count);

    /// <summary>
    /// Gets the point with the specified point ID.
    /// </summary>
    BGPoint GetPoint(BGPointID id);

    /// <summary>
    /// Gets the home for the specified player.
    /// </summary>
    BGPoint GetHome(BGPlayerID player);

    /// <summary>
    /// Gets the jail for the specified player.
    /// </summary>
    BGPoint GetJail(BGPlayerID player);

    /// <summary>
    /// Gets the inner table for the specified player.
    /// </summary>
    BGPoint[] GetInnerTable(BGPlayerID player);

    /// <summary>
    /// Gets the outer table for the specified player.
    /// </summary>
    BGPoint[] GetOuterTable(BGPlayerID player);

    /// <summary>
    /// Gets the pip count for the specified player.
    /// </summary>
    int GetPipCount(BGPlayerID player);

    /// <summary>
    /// Applies the specified move to the board map.
    /// Returns whether the move has been applied successfully, resulting in the board map being changed.
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    bool Use(BGMove move);

    /// <summary>
    /// Gets the chance that the specified point can be hit by an opposing player.
    /// </summary>
    /// <param name="pointID"></param>
    /// <returns></returns>
    float GetVulnerability(BGPointID pointID);

    /// <summary>
    /// Gets the point the specified distance behind the given point with respect to the particular player.
    /// Does not consider jail or home points.
    /// Returns null, if no such point exists.
    /// </summary>
    /// <param name="pointID"></param>
    /// <param name="player"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    BGPoint GetPreviousPoint(BGPointID pointID, BGPlayerID player, int distance);

    /// <summary>
    /// Gets whether the current board set up is a run out. 
    /// A run out is when both players can no longer hit each other. 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    bool IsRunOut();

    /// <summary>
    /// Gets whether the specified player can bear off. 
    /// A player can bear off checkers if the player does not have any checkers in the outer table or in jail.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    bool CanBearOff(BGPlayerID player);

    #endregion
}
