using System;
using UnityEngine;
using UnityEngine.UI;

public class MatchPlayHud : GameHud
{
    #region Public Variables

    /// <summary>
    /// The text displaying the current match score.
    /// </summary>
    public Text MatchScoreText;

    /// <summary>
    /// The text displaying the player 1 score.
    /// </summary>
    public Text Player1ScoreText;

    /// <summary>
    /// The text displaying the player 2 score.
    /// </summary>
    public Text Player2ScoreText;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Called on the frame when a script is enabled.
    /// </summary>
    protected override void Start()
    {
        base.Start();

        MatchScoreText.text = BackgammonGame.Instance.MatchScore.ToString();

        RefreshPlayer1Score();
        RefreshPlayer2Score();

        BackgammonGame.Instance.Player1.ScoresChanged += OnPlayer1ScoresChanged;
        BackgammonGame.Instance.Player2.ScoresChanged += OnPlayer2ScoresChanged;
    }

    #endregion

    #region Event Handler Methods

    /// <summary>
    /// Handles the ScoresChanged event for player 1 from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnPlayer1ScoresChanged(object sender, EventArgs e)
    {
        RefreshPlayer1Score();
    }

    /// <summary>
    /// Handles the ScoresChanged event for player 2 from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnPlayer2ScoresChanged(object sender, EventArgs e)
    {
        RefreshPlayer2Score();
    }

    #endregion

    /// <summary>
    /// Refreshes the player 1 score.
    /// </summary>
    private void RefreshPlayer1Score()
    {
        Player1ScoreText.text = BackgammonGame.Instance.Player1.Score.ToString();
    }

    /// <summary>
    /// Refreshes the player 2 score.
    /// </summary>
    private void RefreshPlayer2Score()
    {
        Player2ScoreText.text = BackgammonGame.Instance.Player2.Score.ToString();
    }
}