using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlayer
{
    #region Variables

    private float m_BestScore = int.MinValue;
    private List<BGMove> m_BestMoves = new List<BGMove>();

    #endregion

    #region Public Methods

    /// <summary>
    /// Determines which move to use given the current board map, move options and available dice rolls.
    /// </summary>
    /// <param name="boardMap"></param>
    /// <param name="moveOptions"></param>
    /// <returns></returns>
    public List<BGMove> GetMove(BGBoardMap boardMap, BGMoveOptions moveOptions, BGDiceRoll diceRoll)
    {
        Evaluate(new BGState(boardMap, moveOptions, diceRoll));

        return m_BestMoves;
    }

    #endregion

    #region Private Methods

    private void Evaluate(BGState state)
    {
        m_BestScore = float.MinValue;
        m_BestMoves.Clear();

        Debug.Log("IA comienza la evaluación de su juego.");

        Evaluate(state, new List<BGMove>());
    }

    private void Evaluate(BGState state, List<BGMove> moves)
    {
        if (state.Moves.Count > 0)
        {
            foreach (BGMove move in state.Moves)
            {
                moves.Add(move);
                Evaluate(state.NextStates[move], moves);
            }
        }
        else
        {
            float score = GetScore(state.BoardMap);

            if (score > m_BestScore)
            {
                // Debug.Log(string.Format("Found new best move set with score of {0}: {1}", score, ListHelper.ConvertToString(moves)));

                m_BestScore = score;
                m_BestMoves.Clear();
                m_BestMoves.AddRange(moves);
            }
            else
            {
                // Debug.Log(string.Format("Evaluated move set with score of {0}: {1}", score, ListHelper.ConvertToString(moves)));
            }
        }

        if (moves.Count > 0)
        {
            moves.RemoveAt(moves.Count - 1);
        }
    }

    #endregion

    #region Abstract Methods

    /// <summary>
    /// Gets the AI perceived value for the specified board.
    /// </summary>
    /// <param name="board"></param>
    /// <returns></returns>
    protected abstract float GetScore(BGBoardMap board);

    #endregion
}
