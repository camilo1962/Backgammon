using UnityEngine;

public class AIPlayerDefault : AIPlayer
{
    #region Override Methods

    /// <summary>
    /// Gets the AI perceived value for the specified board.
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    protected override float GetScore(BGBoardMap board)
    {
        if (board.IsRunOut())
        {
            // If the board is a run out:
            //  1) Focus on getting checkers into the inner board as fast as possible and pick up as many checkers as possible.
            //  2) Penalize the score severely for each checker outside the inner table.
            //     - The farther away the checker is, the more severe the penalty.

            float score = 2000000;
            
            score += (board.GetPoint(BGPointID.HomeP2).Count * 10000);

            for (BGPointID id = BGPointID.Point24; id >= BGPointID.Point19; id--)
            {
                BGPoint point = board.GetPoint(id);

                if (point.Player == BGPlayerID.Player2)
                {
                    score += (point.Count * Mathf.Pow((int)id, 2));
                }
            }

            for (BGPointID id = BGPointID.Point18; id >= BGPointID.Point7; id--)
            {
                BGPoint point = board.GetPoint(id);

                if (point.Player == BGPlayerID.Player2)
                {
                    score -= (10 * point.Count * Mathf.Pow(24 + (int)(BGPointID.Point18 - id), 2));
                }
            }

            for (BGPointID id = BGPointID.Point6; id >= BGPointID.Point1; id--)
            {
                BGPoint point = board.GetPoint(id);

                if (point.Player == BGPlayerID.Player2)
                {
                    score -= (100 * point.Count * Mathf.Pow(24 + (17 - (int)id), 2));
                }
            }

            return score;
        }
        else if (board.CanBearOff(BGPlayerID.Player2))
        {
            // If player 2 can bear off:
            //  1) Focus on picking up as many checkers as possible.
            //  2) If player 1 is jailed, penalize the score severely for each vulnerable checker.

            float score = 1000000;

            score += (board.GetPoint(BGPointID.HomeP2).Count * 10000);

            for (BGPointID id = BGPointID.Point24; id >= BGPointID.Point19; id--)
            {
                BGPoint point = board.GetPoint(id);

                if (point.Player == BGPlayerID.Player2)
                {
                    if (point.Count == 1 && board.GetJail(BGPlayerID.Player1).Count > 0)
                    {
                        score -= (10000 * point.Count * Mathf.Pow((int)id, 2));
                    }
                    else
                    {
                        score += (point.Count * Mathf.Pow((int)id, 2));
                    }
                }
            }

            return score;
        }
        else
        {
            float score = 0;
            int stackingPower = 0;

            for (BGPointID id = BGPointID.Point24; id >= BGPointID.Point1; id--)
            {
                BGPoint point = board.GetPoint(id);

                if (point.Player == BGPlayerID.Player2)
                {
                    if (point.Count > 2)
                    {
                        stackingPower++;
                        score += Mathf.Pow((int)id, 2);
                    }
                    else if (point.Count == 2)
                    {
                        stackingPower++;
                        score += (5 * Mathf.Pow((int)id, 2));
                    }
                    else if (point.Count == 1)
                    {
                        score -= (10 * Mathf.Pow((int)id, 2) * board.GetVulnerability(point.ID));
                    }
                }
                else if (point.Player == BGPlayerID.Player1)
                {
                    if (point.Count == 1)
                    {
                        score -= Mathf.Pow(BGPointID.Point24 - id, 2);
                    }
                }

                if (!(point.Player == BGPlayerID.Player2 && point.Count >= 2))
                {
                    if (stackingPower > 1)
                    {
                        score += (Mathf.Pow(stackingPower - 1, 2) * 1000);
                    }

                    stackingPower = 0;
                }
            }

            score += (board.GetPoint(BGPointID.HomeP2).Count * 10000);

            return score;
        }
    }

    #endregion
}