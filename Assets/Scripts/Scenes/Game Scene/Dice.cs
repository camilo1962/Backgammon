using UnityEngine;

public class Dice : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// The first die.
    /// </summary>
    public Die Die1;

    /// <summary>
    /// The second die.
    /// </summary>
    public Die Die2;

    #endregion

    #region Public Propeties

    /// <summary>
    /// Gets whether the dice is currently rolling.
    /// </summary>
    public bool IsRolling
    {
        get
        {
            return Die1.IsRolling || Die2.IsRolling;
        }
    }

    #endregion

    #region Public Methods
    /// <summary>
    /// Presents the dice.
    /// </summary>
    public void Present()
    {
        Die1.Present();
        Die2.Present();
    }

    /// <summary>
    /// Rolls the dice.
    /// </summary>
    public void Roll()
    {
        Die1.Roll();
        Die2.Roll();
    }

    /// <summary>
    /// Rolls the first die.
    /// </summary>
    public void RollDie1()
    {
        Die1.Roll();
    }

    /// <summary>
    /// Rolls the second die.
    /// </summary>
    public void RollDie2()
    {
        Die2.Roll();
    }

    /// <summary>
    /// Drops the dice.
    /// </summary>
    public void Drop()
    {
        Die1.Drop();
        Die2.Drop();
    }

    /// <summary>
    /// Hides the dice.
    /// </summary>
    public void Hide()
    {
        Die1.Hide();
        Die2.Hide();
    }

    #endregion
}