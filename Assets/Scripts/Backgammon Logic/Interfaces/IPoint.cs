public interface IPoint
{
    #region Properties

    /// <summary>
    /// Gets the point ID. 
    /// </summary>
    BGPointID ID
    {
        get;
    }

    /// <summary>
    /// Gets or sets the number of checkers in the point.
    /// </summary>
    int Count
    {
        get;
    }

    /// <summary>
    /// Gets or sets the player controlling the point.
    /// </summary>
    BGPlayerID Player
    {
        get;
    }

    #endregion

    #region Methods

    /// <summary>
    /// Sets the number of checkers in the point as well as the player in control.
    /// Returns whether the operation was successful.
    /// </summary>
    bool Set(BGPlayerID player, int count);

    /// <summary>
    /// Gets whether the point can be hit.
    /// </summary>
    bool IsVulnerable();

    /// <summary>
    /// Gets whether the point blocks the specified player.
    /// </summary>
    bool IsBlocking(BGPlayerID player);

    /// <summary>
    /// Gets whether the point is occupied by the specified player.
    /// </summary>
    bool IsOccupiedBy(BGPlayerID player);

    #endregion
}