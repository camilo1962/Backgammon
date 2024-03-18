using Common;
using System.Linq;

/// <summary>
/// Lightweight logical adaption of a dice roll.
/// </summary>
public class BGDiceRoll : ICopy<BGDiceRoll>
{
    #region Variables

    /// <summary>
    /// The first roll.
    /// </summary>
    private int m_Roll1;

    /// <summary>
    /// The second roll.
    /// </summary>
    private int m_Roll2;

    /// <summary>
    /// Total number of times each roll can be used.
    /// </summary>
    private int[] m_Total;

    /// <summary>
    /// Current number of times each roll can be used.
    /// </summary>
    private int[] m_Left;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the first roll.
    /// </summary>
    public int Roll1
    {
        get
        {
            return m_Roll1;
        }
    }

    /// <summary>
    /// Gets the second roll.
    /// </summary>
    public int Roll2
    {
        get
        {
            return m_Roll2;
        }
    }

    /// <summary>
    /// Gets whether any rolls have been used.
    /// </summary>
    public bool HasUsedRolls
    {
        get
        {
            for (int i = 0; i < m_Total.Length; i++)
            {
                if (m_Left[i] < m_Total[i])
                {
                    return true;
                }
            }

            return false;
        }
    }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="BGDiceRoll"/> class.
    /// </summary>
    /// <param name="roll1">Roll1.</param>
    /// <param name="roll2">Roll2.</param>
    public BGDiceRoll(int roll1, int roll2)
    {
        this.m_Roll1 = roll1;
        this.m_Roll2 = roll2;
        this.m_Total = new int[7];
        this.m_Left = new int[7];

        if (roll1 == roll2)
        {
            m_Total[roll1] = m_Left[roll1] = 4;
        }
        else
        {
            m_Total[roll1] = m_Left[roll1] = 1;
            m_Total[roll2] = m_Left[roll2] = 1;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="BGDiceRoll"/> class.
    /// </summary>
    /// <param name="roll1">Roll1.</param>
    /// <param name="roll2">Roll2.</param>
    /// <param name="total">Total.</param>
    /// <param name="left">Left.</param>
    private BGDiceRoll(int roll1, int roll2, int[] total, int[] left)
    {
        this.m_Roll1 = roll1;
        this.m_Roll2 = roll2;
        this.m_Total = (int[])total.Clone();
        this.m_Left = (int[])left.Clone();
    }

    #endregion

    #region ICopy Methods

    /// <summary>
    /// Returns a deep copy of the dice roll.
    /// </summary>
    /// <returns></returns>
    public BGDiceRoll Copy()
    {
        return new BGDiceRoll(m_Roll1, m_Roll2, m_Total, m_Left);
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets whether the specified move can be used.
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public bool CanUse(BGMove move)
    {
        return CanUse(move.DiceRoll);
    }

    /// <summary>
    /// Gets whether the specified roll can be used.
    /// </summary>
    /// <param name="roll"></param>
    /// <returns></returns>
    public bool CanUse(int roll)
    {
        return (roll < 1 || roll > 6) ? false : m_Left[roll] > 0;
    }

    /// <summary>
    /// Uses the specified move. Returns whether the move was used successfully.
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public bool Use(BGMove move)
    {
        return Use(move.DiceRoll);
    }

    /// <summary>
    /// Restores the specified move. Returns whether the move was restored successfully.
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public bool Restore(BGMove move)
    {
        return Restore(move.DiceRoll);
    }

    /// <summary>
    /// Gets the number of rolls left for the specified roll.
    /// </summary>
    /// <param name="roll"></param>
    /// <returns></returns>
    public int GetRollsLeft(int roll)
    {
        return (roll < 1 || roll > 6) ? 0 : m_Left[roll];
    }

    /// <summary>
    /// Gets the total number of rolls for the specified roll.
    /// </summary>
    /// <param name="roll"></param>
    /// <returns></returns>
    public int GetTotalRolls(int roll)
    {
        return (roll < 1 || roll > 6) ? 0 : m_Total[roll];
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Uses the specified roll. Returns whether the roll was used successfully.
    /// </summary>
    /// <param name="roll"></param>
    /// <returns></returns>
    private bool Use(int roll)
    {
        if (CanUse(roll))
        {
            m_Left[roll]--;

            return true;
        }

        return false;
    }

    /// <summary>
    /// Restores the specified roll. Returns whether the roll was restored successfully.
    /// </summary>
    /// <param name="roll"></param>
    /// <returns></returns>
    private bool Restore(int roll)
    {
        if (m_Left[roll] < m_Total[roll])
        {
            m_Left[roll]++;

            return true;
        }

        return false;
    }

    #endregion

    #region Override Methods

    public override string ToString()
    {
        return string.Format("Roll1={0}, Roll2={1}, Total={2}, Left={3}",
            Roll1,
            Roll2,
            ArrayHelper.ConvertToString(m_Total),
            ArrayHelper.ConvertToString(m_Left));
    }

    #endregion
}
