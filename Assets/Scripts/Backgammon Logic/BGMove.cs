/// <summary>
/// Lightweight logical representation of a valid move that can be made.
/// </summary>
public class BGMove
{
    #region Properties

    /// <summary>
    /// Gets the ID of the point the checker moves from.
    /// </summary>
    public BGPointID StartPointID
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the ID of the point the checker moves to.
    /// </summary>
    public BGPointID EndPointID
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets the dice roll needed to make this move.
    /// </summary>
    public int DiceRoll
    {
        get;
        private set;
    }

    /// <summary>
    /// Gets whether the move results in a hit.
    /// </summary>
    public bool IsHit
    {
        get;
        private set;
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BGMove"/> class.
    /// </summary>
    /// <param name="start">Start.</param>
    /// <param name="end">End.</param>
    /// <param name="diceRoll">Dice roll.</param>
    public BGMove(BGPoint startPoint, BGPoint endPoint, int diceRoll)
    {
        this.StartPointID = startPoint.ID;
        this.EndPointID = endPoint.ID;
        this.DiceRoll = diceRoll;
        this.IsHit = (startPoint.Player != endPoint.Player) && (endPoint.Count == 1);
    }

    #endregion

    #region Overriden Methods

    public override string ToString()
    {
        return string.Format("[{0}=>{1}: {2}]", StartPointID, EndPointID, DiceRoll);
    }

    #endregion
}