using System;
using UnityEngine;
using UnityEngine.UI;

public class GameHud : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// The text displaying the player 1 name.
    /// </summary>
    public Text Player1NameText;

    /// <summary>
    /// The text displaying the player 2 name.
    /// </summary>
    public Text Player2NameText;

    /// <summary>
    /// The popup indicating that the current turn belongs to player 1.
    /// </summary>
    public Popup Player1TurnIndicator;

    /// <summary>
    /// The popup indicating that the current turn belongs to player 2.
    /// </summary>
    public Popup Player2TurnIndicator;

    /// <summary>
    /// The text displaying the current stakes.
    /// </summary>
    public Text StakesText;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Called on the frame when a script is enabled.
    /// </summary>
    protected virtual void Start()
    {
        Player1NameText.text = BackgammonGame.Instance.Player1.Name;
        Player2NameText.text = BackgammonGame.Instance.Player2.Name;

        RefreshStakes();

        BackgammonGame.Instance.CurrentPlayerChanged += OnCurrentPlayerChanged;
        BackgammonGame.Instance.StakesChanged += OnStakesChanged;
    }

    #endregion

    #region Event Handler Methods

    /// <summary>
    /// Handles the CurrentPlayerChanged event from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCurrentPlayerChanged(object sender, EventArgs e)
    {
        RefreshTurnIndicators();
    }

    /// <summary>
    /// Handles the StakesChanged event from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnStakesChanged(object sender, EventArgs e)
    {
        RefreshStakes();
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Refreshes the player turn indicators.
    /// </summary>
    private void RefreshTurnIndicators()
    {
        if (BackgammonGame.Instance.CurrentPlayer == null)
        {
            Player1TurnIndicator.Hide();
            Player2TurnIndicator.Hide();
        }
        else if (BackgammonGame.Instance.CurrentPlayer.ID == BGPlayerID.Player1)
        {
            Player1TurnIndicator.Display();
            Player2TurnIndicator.Hide();
        }
        else if (BackgammonGame.Instance.CurrentPlayer.ID == BGPlayerID.Player2)
        {
            Player1TurnIndicator.Hide();
            Player2TurnIndicator.Display();
        }
    }

    /// <summary>
    /// Refresh the stakes.
    /// </summary>
    private void RefreshStakes()
    {
        StakesText.text = BackgammonGame.Instance.Stakes.ToString();
    }

    #endregion
}
