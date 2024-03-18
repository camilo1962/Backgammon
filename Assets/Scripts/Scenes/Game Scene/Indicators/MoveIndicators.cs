using System;
using UnityEngine;

public class MoveIndicators : MonoBehaviour
{
    #region Enum

    private enum States
    {
        Available,
        Used,
        Unavailable
    }

    #endregion

    #region Public Variables

    /// <summary>
    /// The dice objects used to indicate the state of a roll for player 1.
    /// </summary>
    [SerializeField]
    private SimpleDie[] m_DicePlayer1 = new SimpleDie[4];

    /// <summary>
    /// The dice objects used to indicate the state of a roll for player 2.
    /// </summary>
    [SerializeField]
    private SimpleDie[] m_DicePlayer2 = new SimpleDie[4];

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Called on the frame when a script is enabled.
    /// </summary>
    void Start()
    {
        BackgammonGame.Instance.MoveOptionsUpdated += OnMoveOptionsUpdated;
        BackgammonGame.Instance.TurnEnded += OnTurnEnded;
        BackgammonGame.Instance.GameEnded += OnGameEnded;
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update()
    {
        // Do nothing.
    }

    #endregion

    #region Event Handler Methods

    /// <summary>
    /// Handles the MoveOptionsUpdated event from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMoveOptionsUpdated(object sender, EventArgs e)
    {
        UpdateMoveIndicators();
    }

    /// <summary>
    /// Handles the TurnEnded event from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnTurnEnded(object sender, EventArgs e)
    {
        ClearMoveIndicators();
    }

    /// <summary>
    /// Handles the GameEnded event from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnGameEnded(object sender, EventArgs e)
    {
        ClearMoveIndicators();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Updates the indicators with the current move options and dice roll from BoardManager.
    /// </summary>
    private void UpdateMoveIndicators()
    {
        BGMoveOptions moveOptions = BackgammonGame.Instance.MoveOptions;
        BGDiceRoll diceRoll = BackgammonGame.Instance.DiceRoll;

        UpdateMoveIndicators(moveOptions, diceRoll);
    }

    /// <summary>
    /// Updates the indicators with the specified move options and dice roll.
    /// </summary>
    private void UpdateMoveIndicators(BGMoveOptions moveOptions, BGDiceRoll diceRoll)
    {
        int index = 0;
        SimpleDie[] dice = GetCurrentMoveIndicators();

        for (int roll = 1; roll <= 6; roll++)
        {
            int total = diceRoll.GetTotalRolls(roll);
            int left = diceRoll.GetRollsLeft(roll);

            for (int current = 0; current < total; current++)
            {
                if (!dice[index].gameObject.activeSelf)
                {
                    dice[index].gameObject.SetActive(true);
                    dice[index].GetComponent<FloatingObject>().CurrentAngle = 0;
                }

                dice[index].Number = roll;

                if (current < total - left)
                {
                    dice[index].GetComponent<Animator>().SetInteger("State", (int)States.Used);
                }
                else if (!moveOptions.CanMoveWith(roll))
                {
                    dice[index].GetComponent<Animator>().SetInteger("State", (int)States.Unavailable);
                }
                else
                {
                    dice[index].GetComponent<Animator>().SetInteger("State", (int)States.Available);
                }

                if (++index >= 4)
                {
                    break;
                }
            }
        }

        for (int i = index; i < 4; i++)
        {
            dice[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Hides and disables the indicators.
    /// </summary>
    private void ClearMoveIndicators()
    {
        foreach (SimpleDie die in m_DicePlayer1)
        {
            die.gameObject.SetActive(false);
        }

        foreach (SimpleDie die in m_DicePlayer2)
        {
            die.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Gets the indicators for the current player.
    /// </summary>
    /// <param name="i"></param>j
    private SimpleDie[] GetCurrentMoveIndicators()
    {
        if (BackgammonGame.Instance.CurrentPlayer != null &&
            BackgammonGame.Instance.CurrentPlayer.IsUserControlled)
        {
            switch (BackgammonGame.Instance.CurrentPlayer.ID)
            {
                case (BGPlayerID.Player1): { return m_DicePlayer1; }
                case (BGPlayerID.Player2): { return m_DicePlayer2; }
            }
        }

        return m_DicePlayer1;
    }


    #endregion
}
