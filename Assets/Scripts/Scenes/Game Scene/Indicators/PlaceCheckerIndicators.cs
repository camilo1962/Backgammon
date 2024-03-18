using UnityEngine;

/// <summary>
/// Visually aids for helping the player place a selected checker.
/// </summary>
public class PlaceCheckerIndicators : Indicators
{
    #region Inherited Methods

    /// <summary>
    /// Fills the Backgammon board with place checker indicators.
    /// </summary>
    protected override void Fill()
    {
        if (BackgammonGame.Instance.SelectedChecker != null)
        {
            Point point = BackgammonGame.Instance.SelectedChecker.Point;

            for (int diceRoll = 1; diceRoll <= 6; diceRoll++)
            {
                BGMove move = BackgammonGame.Instance.MoveOptions.GetMoveFrom(point.ID, diceRoll);

                if (move != null)
                {
                    Vector3 position = BackgammonGame.Instance.Board.GetPoint(move.EndPointID).transform.position;
                    position.y += 0.075f;

                    GameObject indicatorObject = Add();
                    indicatorObject.transform.position = position;

                    SimpleDie simpleDie = indicatorObject.GetComponentInChildren<SimpleDie>();

                    if (simpleDie != null)
                    {
                        simpleDie.Number = move.DiceRoll;
                    }
                }
            }
        }
    }

    #endregion
}
