using Common;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Lightweight logical adaption of the backgammon board. Used for in depth calculations.
/// </summary>
public class BGBoardMap : IBoardMap, ICopy<BGBoardMap>
{
    #region Variables

    /// <summary>
    /// The points of the Backgammon board. Numbered 1 to 24.
    /// </summary>
    private BGPoint[] m_Points;

    /// <summary>
    /// The player 1 home.
    /// </summary>
    private BGPoint m_HomeP1;

    /// <summary>
    /// The player 2 home.
    /// </summary>
    private BGPoint m_HomeP2;

    /// <summary>
    /// The player 1 jail.
    /// </summary>
    private BGPoint m_JailP1;

    /// <summary>
    /// The player 2 jail.
    /// </summary>
    public BGPoint m_JailP2;

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance of the <see cref="MoveCalculator"/> class.
    /// </summary>
    public BGBoardMap()
    {
        this.m_Points = new BGPoint[24];
        this.m_HomeP1 = new BGPoint(BGPointID.HomeP1);
        this.m_HomeP2 = new BGPoint(BGPointID.HomeP2);
        this.m_JailP1 = new BGPoint(BGPointID.JailP1);
        this.m_JailP2 = new BGPoint(BGPointID.JailP2);

        for (int i = 0; i < 24; i++)
        {
            this.m_Points[i] = new BGPoint((BGPointID)i);
        }
    }

    #endregion

    #region ICopy Methods

    /// <summary>
    /// Returns a deep copy of the board map.
    /// </summary>
    /// <returns></returns>
    public BGBoardMap Copy()
    {
        BGBoardMap boardMap = new BGBoardMap();

        boardMap.m_HomeP1 = this.m_HomeP1.Copy();
        boardMap.m_HomeP2 = this.m_HomeP2.Copy();
        boardMap.m_JailP1 = this.m_JailP1.Copy();
        boardMap.m_JailP2 = this.m_JailP2.Copy();

        for (int i = 0; i < 24; i++)
        {
            boardMap.m_Points[i] = this.m_Points[i].Copy();
        }

        return boardMap;
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Sets the point with the specified point Id with the specified player and checker count.
    /// Returns whether the operation was successful.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="player"></param>
    /// <param name="count"></param>
    public bool SetPoint(BGPointID id, BGPlayerID player, int count)
    {
        return GetPoint(id).Set(player, count);
    }

    /// <summary>
    /// Gets the point with the specified point ID.
    /// </summary>
    public BGPoint GetPoint(BGPointID id)
    {
        switch (id)
        {
            case (BGPointID.HomeP1): return m_HomeP1;
            case (BGPointID.HomeP2): return m_HomeP2;
            case (BGPointID.JailP1): return m_JailP1;
            case (BGPointID.JailP2): return m_JailP2;
            default: return m_Points[(int)id];
        }
    }

    /// <summary>
    /// Gets the home for the specified player.
    /// </summary>
    public BGPoint GetHome(BGPlayerID player)
    {
        switch (player)
        {
            case (BGPlayerID.Player1): return m_HomeP1;
            case (BGPlayerID.Player2): return m_HomeP2;
            default: return null;
        }
    }

    /// <summary>
    /// Gets the jail for the specified player.
    /// </summary>
    public BGPoint GetJail(BGPlayerID player)
    {
        switch (player)
        {
            case (BGPlayerID.Player1): return m_JailP1;
            case (BGPlayerID.Player2): return m_JailP2;
            default: return null;
        }
    }

    /// <summary>
    /// Gets the inner table for the specified player.
    /// </summary>
    public BGPoint[] GetInnerTable(BGPlayerID player)
    {
        switch (player)
        {
            case (BGPlayerID.Player1):
                {
                    BGPoint[] innerTable = new BGPoint[6];

                    for (int i = 0; i < 6; i++)
                    {
                        innerTable[i] = this.m_Points[i];
                    }

                    return innerTable;
                }
            case (BGPlayerID.Player2):
                {
                    BGPoint[] innerTable = new BGPoint[6];

                    for (int i = 0; i < 6; i++)
                    {
                        innerTable[i] = this.m_Points[23 - i];
                    }

                    return innerTable;
                }
            default:
                {
                    return null;
                }
        }
    }

    /// <summary>
    /// Gets the outer table for the specified player.
    /// </summary>
    public BGPoint[] GetOuterTable(BGPlayerID player)
    {
        switch (player)
        {
            case (BGPlayerID.Player1):
                {
                    BGPoint[] outerTable = new BGPoint[18];

                    for (int i = 0; i < 18; i++)
                    {
                        outerTable[i] = this.m_Points[23 - i];
                    }

                    return outerTable;
                }
            case (BGPlayerID.Player2):
                {
                    BGPoint[] outerTable = new BGPoint[18];

                    for (int i = 0; i < 18; i++)
                    {
                        outerTable[i] = this.m_Points[i];
                    }

                    return outerTable;
                }
            default:
                {
                    return null;
                }
        }
    }

    /// <summary>
    /// Gets the pip count for the specified player.
    /// </summary>
    public int GetPipCount(BGPlayerID player)
    {
        return m_Points.Sum(point => point.GetPip(player));
    }

    /// <summary>
    /// Applies the specified move to the board map.
    /// Returns whether the move has been applied successfully, resulting in the board map being changed.
    /// </summary>
    /// <param name="move"></param>
    /// <returns></returns>
    public bool Use(BGMove move)
    {
        BGPoint startPoint = GetPoint(move.StartPointID);
        BGPoint endPoint = GetPoint(move.EndPointID);

        // Return immediately if the move is invalid.

        if (startPoint.Count == 0)
        {
            return false;
        }

        // Case 1: The endpoint does not have any checkers.
        // Case 2: The endpoint is occupied by the same player occupying the start point.

        if (endPoint.Player == BGPlayerID.None || startPoint.Player == endPoint.Player)
        {
            startPoint.Count--;
            endPoint.Count++;
            endPoint.Player = startPoint.Player;

            if (startPoint.Count == 0)
            {
                startPoint.Player = BGPlayerID.None;
            }

            return true;
        }

        // Case 3: The endpoint is occupied by the opponent of the player occupying the start point and is vulnerable.

        else if (endPoint.Player != startPoint.Player && endPoint.Count == 1)
        {
            GetJail(endPoint.Player).Count++;
            startPoint.Count--;
            endPoint.Player = startPoint.Player;

            if (startPoint.Count == 0)
            {
                startPoint.Player = BGPlayerID.None;
            }

            return true;
        }

        return false;
    }

    /// <summary>
    /// Gets the chance that the specified point can be hit by an opposing player.
    /// </summary>
    /// <param name="pointID"></param>
    /// <returns></returns>
    public float GetVulnerability(BGPointID pointID)
    {
        BGPoint point = GetPoint(pointID);

        if (point.IsVulnerable())
        {
            float vulnerability = 0f;
            bool[,] hitTable = new bool[6, 6];
            BGPlayerID player = point.Player;
            BGPlayerID opponent = (player == BGPlayerID.Player1) ? BGPlayerID.Player2 : BGPlayerID.Player1;

            for (int roll1 = 1; roll1 <= 6; roll1++)
            {
                BGPoint point1 = GetPreviousPoint(pointID, opponent, roll1);

                if (point1 != null)
                {
                    if (point1.IsOccupiedBy(opponent))
                    {
                        //Debug.Log(string.Format("{0} is vulnerable to any roll with {1}", point.ID, roll1));

                        for (int i = 0; i < 6; i++)
                        {
                            hitTable[roll1 - 1, i] = hitTable[i, roll1 - 1] = true;
                        }
                    }
                    else if (!point1.IsControlledBy(player))
                    {
                        for (int roll2 = 1; roll2 <= 6; roll2++)
                        {
                            BGPoint point2 = GetPreviousPoint(point1.ID, opponent, roll2);

                            if (point2 != null && point2.IsOccupiedBy(opponent))
                            {
                                //Debug.Log(string.Format("{0} is vulnerable to roll ({1}, {2}) from {3}", point.ID, roll1, roll2, point2));

                                hitTable[roll1 - 1, roll2 - 1] = hitTable[roll2 - 1, roll1 - 1] = true;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (hitTable[i, j])
                    {
                        //Debug.Log(string.Format(">> {0},{1}", i + 1, j + 1));

                        vulnerability += 1f / 36f;
                    }
                }
            }

            //Debug.Log(string.Format("Total vulnerability for {0} = {1}", point.ID, vulnerability));

            return vulnerability;
        }
        else
        {
            return 0f;
        }
    }

    /// <summary>
    /// Gets the point the EXACT specified distance behind the given BOARD point with respect to the particular player.
    /// Returns null, if no such point exists.
    /// </summary>
    /// <param name="pointID"></param>
    /// <param name="player"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public BGPoint GetPreviousPoint(BGPointID pointID, BGPlayerID player, int distance)
    {
        if (pointID <= BGPointID.Point1 || pointID >= BGPointID.Point24)
        {
            return null;
        }

        if (player == BGPlayerID.Player1)
        {
            BGPoint previousPoint = null;
            int previousIndex = ((int)pointID + distance);

            if (previousIndex <= 23)
            {
                previousPoint = GetPoint((BGPointID)previousIndex);
            }
            else if (previousIndex == 24)
            {
                previousPoint = GetJail(BGPlayerID.Player2);
            }

            return previousPoint;
        }
        else if (player == BGPlayerID.Player2)
        {
            BGPoint previousPoint = null;
            int previousIndex = ((int)pointID - distance);

            if (previousIndex >= 0)
            {
                previousPoint = GetPoint((BGPointID)previousIndex);
            }
            else if (previousIndex == -1)
            {
                previousPoint = GetJail(BGPlayerID.Player1);
            }

            return previousPoint;
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Gets whether the current board set up is a run out. 
    /// A run out is when both players can no longer hit each other. 
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool IsRunOut()
    {
        if (m_JailP1.Count > 0 || m_JailP2.Count > 0)
        {
            return false;
        }
        
        bool p2 = false;

        for (int i = 0; i < m_Points.Length; i++)
        {
            if (m_Points[i].Player == BGPlayerID.Player2)
            {
                p2 = true;
            }
            else if (m_Points[i].Player == BGPlayerID.Player1 && p2)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Gets whether the specified player can bear off. 
    /// A player can bear off checkers if the player does not have any checkers in the outer table or in jail.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool CanBearOff(BGPlayerID player)
    {
        if (player == BGPlayerID.None)
        {
            return false;
        }

        return !GetOuterTable(player).Any(point => point.IsOccupiedBy(player)) && !GetJail(player).IsOccupiedBy(player);
    }

    #endregion

    #region Override Methods

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < 24; i++)
        {
            sb.AppendLine(m_Points[i].ToString());
        }

        sb.AppendLine(m_HomeP1.ToString());
        sb.AppendLine(m_HomeP2.ToString());
        sb.AppendLine(m_JailP1.ToString());
        sb.AppendLine(m_JailP2.ToString());

        return string.Format(sb.ToString());
    }

    #endregion

    #region Debug

    public void PrintAllVulnerability()
    {
        for (BGPointID point = BGPointID.Point1; point < BGPointID.Point24; point++)
        {
            Debug.Log(string.Format("Vulnerability for {0} = {1}", point, GetVulnerability(point)));
        }
    }

    #endregion
}



