using Common;

/// <summary>
/// Lightweight logical adaption of a single point on the backgammon board.
/// </summary>
public class BGPoint : IPoint, ICopy<BGPoint>
{
    #region Properties
    
    /// <summary>
    /// Gets the point ID. 
    /// </summary>
    public BGPointID ID
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets or sets the number of checkers in the point.
    /// </summary>
    public int Count
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the player controlling the point.
    /// </summary>
    public BGPlayerID Player
    {
        get;
        set;
    }
    
    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BGPoint"/> class.
    /// </summary>
    /// <param name="id">Identifier.</param>
    public BGPoint(BGPointID id)
    {
        this.ID = id;
        this.Count = 0;
        this.Player = BGPlayerID.None;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BGPoint"/> class.
    /// </summary>
    /// <param name="id">Identifier.</param>
    /// <param name="count">Count.</param>
    /// <param name="player">Player.</param>
    private BGPoint(BGPointID id, int count, BGPlayerID player)
    {
        this.ID = id;
        this.Count = count;
        this.Player = player;
    }

    #endregion

    #region ICopy Methods

    /// <summary>
    /// Returns a deep copy of the point.
    /// </summary>
    /// <returns></returns>
    public BGPoint Copy()
    {
        return new BGPoint(ID, Count, Player);
    }
    
    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the number of checkers in the point as well as the player in control.
    /// Returns whether the operation was successful.
    /// </summary>
    public bool Set(BGPlayerID player, int count)
    {
        if ((player == BGPlayerID.None && count != 0) ||
            (player != BGPlayerID.None && count == 0))
        {
            return false;
        }

        Player = player;
        Count = count;

        return true;
    }

    /// <summary>
    /// Gets whether the point can be hit.
    /// </summary>
    public bool IsVulnerable()
    {
        return Count == 1;
    }

    /// <summary>
    /// Gets whether the point blocks the specified player.
    /// </summary>
    public bool IsBlocking(BGPlayerID player)
    {
        return (player != BGPlayerID.None) && (Count >= 2) && (Player != player);
    }

    /// <summary>
    /// Gets whether the point is occupied by at least 2 checkers from the specified player.
    /// </summary>
    public bool IsControlledBy(BGPlayerID player)
    {
        return (player != BGPlayerID.None) && (Count >= 2) && (Player == player);
    }

    /// <summary>
    /// Gets whether the point is occupied by the specified player.
    /// </summary>
    public bool IsOccupiedBy(BGPlayerID player)
    {
        return (player != BGPlayerID.None) && (Count >= 1) && (Player == player);
    }

    /// <summary>
    /// Gets the pip for the specified player.
    /// </summary>
    public int GetPip(BGPlayerID player)
    {
        if (Player != player || player == BGPlayerID.None)
        {
            return 0;
        }

        switch (ID)
        {
            case (BGPointID.HomeP1):
            case (BGPointID.HomeP2):
                {
                    return 0;
                }
            case (BGPointID.JailP1):
            case (BGPointID.JailP2):
                {
                    return Count * 25;
                }
            default:
                {
                    return Count * ((Player == BGPlayerID.Player1) ? (1 + (int)ID) : (24 - (int)ID));
                }
        }
    }

    #endregion

    #region Override Methods

    public override string ToString()
    {
        return string.Format("{0}:{1},{2}", ID, Count, Player);
    }

    #endregion
}