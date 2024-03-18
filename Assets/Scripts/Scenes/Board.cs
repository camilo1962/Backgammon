using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Component defining the physical backgammon board.
/// </summary>
public class Board : MonoBehaviour
{
    #region Properties

    /// <summary>
    /// Gets the 24 points of the backgammon board.
    /// </summary>
    public Point[] Points { get; private set; }

    /// <summary>
    /// Gets the player 1 home.
    /// </summary>
    public Point HomeP1 { get; private set; }

    /// <summary>
    /// Gets the player 2 home.
    /// </summary>
    public Point HomeP2 { get; private set; }

    /// <summary>
    /// Gets the player 1 jail.
    /// </summary>
    public Point JailP1 { get; private set; }

    /// <summary>
    /// Gets the player 2 jail.
    /// </summary>
    public Point JailP2 { get; private set; }

    /// <summary>
    /// Gets the player 1 inner table. Points 6 to 1.
    /// </summary>
    public Point[] InnerTableP1 { get; private set; }

    /// <summary>
    /// Gets the player 2 inner table. Points 24 to 19.
    /// </summary>
    public Point[] InnerTableP2 { get; private set; }

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake()
    {
        Points = new Point[24];
        InnerTableP1 = new Point[6];
        InnerTableP2 = new Point[6];

        foreach (Point point in GetComponentsInChildren<Point>())
        {
            switch (point.ID)
            {
                case (BGPointID.HomeP1): { HomeP1 = point; } break;
                case (BGPointID.HomeP2): { HomeP2 = point; } break;
                case (BGPointID.JailP1): { JailP1 = point; } break;
                case (BGPointID.JailP2): { JailP2 = point; } break;
                default: { Points[(int)point.ID] = point; } break;
            }
        }

        for (int i = 0; i < 6; i++)
        {
            InnerTableP1[i] = Points[i];
            InnerTableP2[i] = Points[23 - i];
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Gets the point with the specified point ID.
    /// </summary>
    public Point GetPoint(BGPointID id)
    {
        switch (id)
        {
            case (BGPointID.HomeP1): return HomeP1;
            case (BGPointID.HomeP2): return HomeP2;
            case (BGPointID.JailP1): return JailP1;
            case (BGPointID.JailP2): return JailP2;
            default: return Points[(int)id];
        }
    }

    /// <summary>
    /// Gets the home for the specified player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Point GetHome(Player player)
    {
        return GetHome(player.ID);
    }

    /// <summary>
    /// Gets the home for the specified player ID.
    /// </summary>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public Point GetHome(BGPlayerID playerID)
    {
        switch (playerID)
        {
            case (BGPlayerID.Player1): return HomeP1;
            case (BGPlayerID.Player2): return HomeP2;
            default: return null;
        }
    }

    /// <summary>
    /// Gets the jail for the specified player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Point GetJail(Player player)
    {
        return GetJail(player.ID);
    }

    /// <summary>
    /// Gets the jail for the specified player ID.
    /// </summary>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public Point GetJail(BGPlayerID playerID)
    {
        switch (playerID)
        {
            case (BGPlayerID.Player1): return JailP1;
            case (BGPlayerID.Player2): return JailP2;
            default: return null;
        }
    }

    /// <summary>
    /// Gets the inner table for the specified player.
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public Point[] GetInnerTable(Player player)
    {
        return GetInnerTable(player.ID);
    }

    /// <summary>
    /// Gets the inner table for the specified player ID.
    /// </summary>
    /// <param name="playerID"></param>
    /// <returns></returns>
    public Point[] GetInnerTable(BGPlayerID playerID)
    {
        switch (playerID)
        {
            case (BGPlayerID.Player1): return InnerTableP1;
            case (BGPlayerID.Player2): return InnerTableP2;
            default: return null;
        }
    }
    /// <summary>
    /// Gets whether the specified player has any checkers in jail.
    /// </summary>
    public bool IsJailed(Player player)
    {
        return player.Checkers.Any(checker => checker.Point == GetJail(player));
    }

    /// <summary>
    /// Gets whether the specified player has any checkers in jail and is blocked from entering.
    /// </summary>
    public bool IsBlockedOff(Player player)
    {
        if (IsJailed(player))
        {
            return GetInnerTable(player.OpponentID).All(point => ((point.Count >= 2) && (point.CurrentChecker.Owner != player)));
        }

        return false;
    }

    /// <summary>
    /// Gets whether the specified player has borne off all checkers.
    /// </summary>
    public bool IsComplete(Player player)
    {
        return player.Checkers.All(checker => checker.Point == GetHome(player));
    }

    /// <summary>
    /// Gets whether the specified player has borne off any checkers.
    /// </summary>
    public bool HasBorneOff(Player player)
    {
        return player.Checkers.Any(checker => checker.Point == GetHome(player));
    }

    /// <summary>
    /// Gets whether the specified player has any checkers in the opponent's inner table.
    /// </summary>
    public bool IsInOpponentInnerTable(Player player)
    {
        return GetInnerTable(player.OpponentID).Any(point => ((point.Count >= 1) && (point.CurrentChecker.Owner == player)));
    }

    /// <summary>
    /// Gets a lightweight logical map of the board's current state.
    /// </summary>
    /// <returns>The board.</returns>
    public BGBoardMap GetBoardMap()
    {
        BGBoardMap boardMap = new BGBoardMap();

        foreach (Point point in Points)
        {
            boardMap.SetPoint(point.ID, ((point.Count == 0) ? BGPlayerID.None : point.CurrentChecker.Owner.ID), point.Count);
        }

        boardMap.SetPoint(HomeP1.ID, ((HomeP1.Count == 0) ? BGPlayerID.None : HomeP1.CurrentChecker.Owner.ID), HomeP1.Count);
        boardMap.SetPoint(HomeP2.ID, ((HomeP2.Count == 0) ? BGPlayerID.None : HomeP2.CurrentChecker.Owner.ID), HomeP2.Count);
        boardMap.SetPoint(JailP1.ID, ((JailP1.Count == 0) ? BGPlayerID.None : JailP1.CurrentChecker.Owner.ID), JailP1.Count);
        boardMap.SetPoint(JailP2.ID, ((JailP2.Count == 0) ? BGPlayerID.None : JailP2.CurrentChecker.Owner.ID), JailP2.Count);
        
        return boardMap;
    }

    /// <summary>
    /// Sets up the players' checkers according to the specified Backgammon game variant.
    /// </summary>
    /// <param name="gameMode"></param>
    /// <param name="player1"></param>
    /// <param name="player2"></param>
    public void SetupCheckers(BackgammonVariants gameMode, Player player1, Player player2)
    {
        foreach (Point point in Points)
        {
            point.Clear();
        }

        HomeP1.Clear();
        HomeP2.Clear();
        JailP1.Clear();
        JailP2.Clear();

        switch (gameMode)
        {
            case (BackgammonVariants.Standard):
                {
                    Points[(int)BGPointID.Point24].PlaceInstantly(player1.Checkers[0]);
                    Points[(int)BGPointID.Point24].PlaceInstantly(player1.Checkers[1]);
                    Points[(int)BGPointID.Point13].PlaceInstantly(player1.Checkers[2]);
                    Points[(int)BGPointID.Point13].PlaceInstantly(player1.Checkers[3]);
                    Points[(int)BGPointID.Point13].PlaceInstantly(player1.Checkers[4]);
                    Points[(int)BGPointID.Point13].PlaceInstantly(player1.Checkers[5]);
                    Points[(int)BGPointID.Point13].PlaceInstantly(player1.Checkers[6]);
                    Points[(int)BGPointID.Point8].PlaceInstantly(player1.Checkers[7]);
                    Points[(int)BGPointID.Point8].PlaceInstantly(player1.Checkers[8]);
                    Points[(int)BGPointID.Point8].PlaceInstantly(player1.Checkers[9]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[10]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[11]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[12]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[13]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[14]);

                    Points[(int)BGPointID.Point1].PlaceInstantly(player2.Checkers[0]);
                    Points[(int)BGPointID.Point1].PlaceInstantly(player2.Checkers[1]);
                    Points[(int)BGPointID.Point12].PlaceInstantly(player2.Checkers[2]);
                    Points[(int)BGPointID.Point12].PlaceInstantly(player2.Checkers[3]);
                    Points[(int)BGPointID.Point12].PlaceInstantly(player2.Checkers[4]);
                    Points[(int)BGPointID.Point12].PlaceInstantly(player2.Checkers[5]);
                    Points[(int)BGPointID.Point12].PlaceInstantly(player2.Checkers[6]);
                    Points[(int)BGPointID.Point17].PlaceInstantly(player2.Checkers[7]);
                    Points[(int)BGPointID.Point17].PlaceInstantly(player2.Checkers[8]);
                    Points[(int)BGPointID.Point17].PlaceInstantly(player2.Checkers[9]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[10]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[11]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[12]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[13]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[14]);
                }
                break;
            case (BackgammonVariants.SuddenDeath):
                {
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[0]);
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[1]);
                    Points[(int)BGPointID.Point2].PlaceInstantly(player1.Checkers[2]);
                    Points[(int)BGPointID.Point2].PlaceInstantly(player1.Checkers[3]);
                    Points[(int)BGPointID.Point3].PlaceInstantly(player1.Checkers[4]);
                    Points[(int)BGPointID.Point3].PlaceInstantly(player1.Checkers[5]);
                    Points[(int)BGPointID.Point4].PlaceInstantly(player1.Checkers[6]);
                    Points[(int)BGPointID.Point4].PlaceInstantly(player1.Checkers[7]);
                    Points[(int)BGPointID.Point5].PlaceInstantly(player1.Checkers[8]);
                    Points[(int)BGPointID.Point5].PlaceInstantly(player1.Checkers[9]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[10]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[11]);
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[12]);
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[13]);
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[14]);

                    Points[(int)BGPointID.Point24].PlaceInstantly(player2.Checkers[0]);
                    Points[(int)BGPointID.Point24].PlaceInstantly(player2.Checkers[1]);
                    Points[(int)BGPointID.Point23].PlaceInstantly(player2.Checkers[2]);
                    Points[(int)BGPointID.Point23].PlaceInstantly(player2.Checkers[3]);
                    Points[(int)BGPointID.Point22].PlaceInstantly(player2.Checkers[4]);
                    Points[(int)BGPointID.Point22].PlaceInstantly(player2.Checkers[5]);
                    Points[(int)BGPointID.Point21].PlaceInstantly(player2.Checkers[6]);
                    Points[(int)BGPointID.Point21].PlaceInstantly(player2.Checkers[7]);
                    Points[(int)BGPointID.Point20].PlaceInstantly(player2.Checkers[8]);
                    Points[(int)BGPointID.Point20].PlaceInstantly(player2.Checkers[9]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[10]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[11]);
                    Points[(int)BGPointID.Point24].PlaceInstantly(player2.Checkers[12]);
                    Points[(int)BGPointID.Point24].PlaceInstantly(player2.Checkers[13]);
                    Points[(int)BGPointID.Point24].PlaceInstantly(player2.Checkers[14]);
                }
                break;
            case (BackgammonVariants.Rush):
                {
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[0]);
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[1]);
                    Points[(int)BGPointID.Point2].PlaceInstantly(player1.Checkers[2]);
                    Points[(int)BGPointID.Point2].PlaceInstantly(player1.Checkers[3]);
                    Points[(int)BGPointID.Point3].PlaceInstantly(player1.Checkers[4]);
                    Points[(int)BGPointID.Point3].PlaceInstantly(player1.Checkers[5]);
                    Points[(int)BGPointID.Point4].PlaceInstantly(player1.Checkers[6]);
                    Points[(int)BGPointID.Point4].PlaceInstantly(player1.Checkers[7]);
                    Points[(int)BGPointID.Point5].PlaceInstantly(player1.Checkers[8]);
                    Points[(int)BGPointID.Point5].PlaceInstantly(player1.Checkers[9]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[10]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[11]);
                    Points[(int)BGPointID.Point18].PlaceInstantly(player1.Checkers[12]);
                    Points[(int)BGPointID.Point17].PlaceInstantly(player1.Checkers[13]);
                    Points[(int)BGPointID.Point16].PlaceInstantly(player1.Checkers[14]);

                    Points[(int)BGPointID.Point24].PlaceInstantly(player2.Checkers[0]);
                    Points[(int)BGPointID.Point24].PlaceInstantly(player2.Checkers[1]);
                    Points[(int)BGPointID.Point23].PlaceInstantly(player2.Checkers[2]);
                    Points[(int)BGPointID.Point23].PlaceInstantly(player2.Checkers[3]);
                    Points[(int)BGPointID.Point22].PlaceInstantly(player2.Checkers[4]);
                    Points[(int)BGPointID.Point22].PlaceInstantly(player2.Checkers[5]);
                    Points[(int)BGPointID.Point21].PlaceInstantly(player2.Checkers[6]);
                    Points[(int)BGPointID.Point21].PlaceInstantly(player2.Checkers[7]);
                    Points[(int)BGPointID.Point20].PlaceInstantly(player2.Checkers[8]);
                    Points[(int)BGPointID.Point20].PlaceInstantly(player2.Checkers[9]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[10]);
                    Points[(int)BGPointID.Point19].PlaceInstantly(player2.Checkers[11]);
                    Points[(int)BGPointID.Point7].PlaceInstantly(player2.Checkers[12]);
                    Points[(int)BGPointID.Point8].PlaceInstantly(player2.Checkers[13]);
                    Points[(int)BGPointID.Point9].PlaceInstantly(player2.Checkers[14]);
                }
                break;
            case (BackgammonVariants.Test1):
                {
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[0]);
                    Points[(int)BGPointID.Point1].PlaceInstantly(player1.Checkers[1]);
                    Points[(int)BGPointID.Point2].PlaceInstantly(player1.Checkers[2]);
                    Points[(int)BGPointID.Point2].PlaceInstantly(player1.Checkers[3]);
                    Points[(int)BGPointID.Point2].PlaceInstantly(player1.Checkers[4]);
                    Points[(int)BGPointID.Point2].PlaceInstantly(player1.Checkers[5]);
                    Points[(int)BGPointID.Point4].PlaceInstantly(player1.Checkers[6]);
                    Points[(int)BGPointID.Point4].PlaceInstantly(player1.Checkers[7]);
                    Points[(int)BGPointID.Point5].PlaceInstantly(player1.Checkers[8]);
                    Points[(int)BGPointID.Point5].PlaceInstantly(player1.Checkers[9]);
                    Points[(int)BGPointID.Point5].PlaceInstantly(player1.Checkers[10]);
                    Points[(int)BGPointID.Point6].PlaceInstantly(player1.Checkers[11]);
                    Points[(int)BGPointID.Point8].PlaceInstantly(player1.Checkers[12]);
                    Points[(int)BGPointID.Point8].PlaceInstantly(player1.Checkers[13]);
                    Points[(int)BGPointID.Point8].PlaceInstantly(player1.Checkers[14]);

                    HomeP2.PlaceInstantly(player2.Checkers[0]);
                    HomeP2.PlaceInstantly(player2.Checkers[1]);
                    HomeP2.PlaceInstantly(player2.Checkers[2]);
                    HomeP2.PlaceInstantly(player2.Checkers[3]);
                    HomeP2.PlaceInstantly(player2.Checkers[4]);
                    HomeP2.PlaceInstantly(player2.Checkers[5]);
                    HomeP2.PlaceInstantly(player2.Checkers[6]);
                    HomeP2.PlaceInstantly(player2.Checkers[7]);
                    HomeP2.PlaceInstantly(player2.Checkers[8]);
                    HomeP2.PlaceInstantly(player2.Checkers[9]);
                    HomeP2.PlaceInstantly(player2.Checkers[10]);
                    HomeP2.PlaceInstantly(player2.Checkers[11]);
                    HomeP2.PlaceInstantly(player2.Checkers[12]);
                    HomeP2.PlaceInstantly(player2.Checkers[13]);
                    Points[(int)BGPointID.Point21].PlaceInstantly(player2.Checkers[14]);
                }
                break;
        }
    }

    #endregion
}