using System;
using UnityEngine;

/// <summary>
/// Visually aids for helping the player pick up available checker.
/// </summary>
public class SelectCheckerIndicators : Indicators
{
    #region Inherited Methods

    /// <summary>
    /// Fills the Backgammon board with select checker indicators.
    /// </summary>
    protected override void Fill()
    {
        if (BackgammonGame.Instance.SelectedChecker == null)
        {
            foreach (BGPointID pointID in Enum.GetValues(typeof(BGPointID)))
            {
                if (BackgammonGame.Instance.MoveOptions.CanMoveFrom(pointID))
                {
                    Vector3 position = BackgammonGame.Instance.Board.GetPoint(pointID).GetCurrentPosition();
                    position.y += 0.075f;

                    GameObject indicatorObject = Add();
                    indicatorObject.transform.position = position;
                }
            }
        }
    }

    #endregion
}
