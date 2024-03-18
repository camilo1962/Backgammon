using System;
using UnityEngine;

/// <summary>
/// Handles dialogs.
/// </summary>
public class PopupManager : MonoBehaviour
{
    #region Public Variables

    public Popup EndTurnPopup;
    public Popup DoublingCubeOfferPreviewPopup;
    public Popup DoublingCubeOfferPopup;
    public Popup DoublingCubeWaitPopup;
    public Popup LeaveGamePopup;
    public Popup MatchPlayEndPopup;
    public Popup MoneyPlayEndPopup;
    public Popup UndoPopup;
    public Popup IsCrawfordGamePopup;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Called on the frame when a script is enabled.
    /// </summary>
    void Start()
    {
        BackgammonGame.Instance.MoveOptionsUpdated += OnMoveOptionUpdated;
        BackgammonGame.Instance.MatchEnded += OnMatchEnded;
        BackgammonGame.Instance.GameStarted += OnGameStarted;
        BackgammonGame.Instance.GameEnded += OnGameEnded;
        BackgammonGame.Instance.TurnEnded += OnTurnEnded;
        BackgammonGame.Instance.CheckerPickedUp += OnCheckerPickedUp;
        BackgammonGame.Instance.CheckerReturned += OnCheckerReturned;
        BackgammonGame.Instance.CheckerMoved += OnCheckerMoved;
        BackgammonGame.Instance.DoublingCube.OfferPreviewed += OnDoublingCubeOfferPreviewed;
        BackgammonGame.Instance.DoublingCube.OfferCanceled += OnDoublingCubeOfferCanceled;
        BackgammonGame.Instance.DoublingCube.Offered += OnDoublingCubeOffered;
        BackgammonGame.Instance.DoublingCube.Accepted += OnDoublingCubeAccepted;
        BackgammonGame.Instance.DoublingCube.Declined += OnDoublingCubeDeclined;
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            LeaveGamePopup.Toggle();
        }
    }

    #endregion

    #region Event Handler Methods

    /// <summary>
    /// Handles the MoveOptionsUpdated event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMoveOptionUpdated(object sender, EventArgs e)
    {
        RefreshUndoPopup();
    }

    /// <summary>
    /// Handles the MatchEnded event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnMatchEnded(object sender, EventArgs e)
    {
        switch (BackgammonGame.Instance.PlayMode)
        {
            case (BackgammonPlayMode.Match):
                {
                    MatchPlayEndPopup.Display();
                }
                break;
            case (BackgammonPlayMode.Money):
                {
                    MoneyPlayEndPopup.Display();
                }
                break;
        }

        UndoPopup.Hide();
    }

    /// <summary>
    /// Handles the GameStarted event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnGameStarted(object sender, EventArgs e)
    {
        RefreshIsCrawfordGamePopup();
    }

    /// <summary>
    /// Handles the GameEnded event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnGameEnded(object sender, EventArgs e)
    {
        EndTurnPopup.Hide();
        UndoPopup.Hide();
    }

    /// <summary>
    /// Handles the TurnEnded event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnTurnEnded(object sender, EventArgs e)
    {
        EndTurnPopup.Hide();
        UndoPopup.Hide();
    }

    /// <summary>
    /// Handles the CheckerPickedUp event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCheckerPickedUp(object sender, EventArgs e)
    {
        RefreshUndoPopup();
    }

    /// <summary>
    /// Handles the CheckerReturned event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCheckerReturned(object sender, EventArgs e)
    {
        RefreshUndoPopup();
    }

    /// <summary>
    /// Handles the MoveOptionsUpdated event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCheckerMoved(object sender, EventArgs e)
    {
        if (!BackgammonGame.Instance.MoveOptions.CanMove() && BackgammonGame.Instance.CurrentPlayer.IsUserControlled)
        {
            EndTurnPopup.Display();
        }
        else
        {
            EndTurnPopup.Hide();
        }
    }

    /// <summary>
    /// Handles the OfferPreviewed event from BackgammonGame doubling cube.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDoublingCubeOfferPreviewed(object sender, EventArgs e)
    {
        DoublingCubeOfferPreviewPopup.Display();
    }

    /// <summary>
    /// Handles the OfferCanceled event from BackgammonGame doubling cube.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDoublingCubeOfferCanceled(object sender, EventArgs e)
    {
        DoublingCubeOfferPreviewPopup.Hide();
    }

    /// <summary>
    /// Handles the Offered event from BackgammonGame doubling cube.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDoublingCubeOffered(object sender, EventArgs e)
    {
        DoublingCubeOfferPreviewPopup.Hide();
        DoublingCubeOfferPopup.Display();
    }

    /// <summary>
    /// Handles the Accepted event from BackgammonGame doubling cube.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDoublingCubeAccepted(object sender, EventArgs e)
    {
        DoublingCubeOfferPopup.Hide();
        DoublingCubeWaitPopup.Hide();
    }

    /// <summary>
    /// Handles the Declined event from BackgammonGame doubling cube.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnDoublingCubeDeclined(object sender, EventArgs e)
    {
        DoublingCubeOfferPopup.Hide();
        DoublingCubeWaitPopup.Hide();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Leave the game.
    /// </summary>
    public void LeaveGame()
    {
        GameManager.Instance.LoadMainMenuScene();
    }

    /// <summary>
    /// Refreshes the Crawford popup.
    /// </summary>
    public void RefreshIsCrawfordGamePopup()
    {
        if (BackgammonGame.Instance.IsCrawfordGame)
        {
            IsCrawfordGamePopup.Display();
        }
        else
        {
            IsCrawfordGamePopup.Hide();
        }
    }

    /// <summary>
    /// Refreshes the Undo popup.
    /// </summary>
    public void RefreshUndoPopup()
    {
        if (BackgammonGame.Instance.CurrentPlayer.IsUserControlled && 
            (BackgammonGame.Instance.SelectedChecker != null || BackgammonGame.Instance.DiceRoll.HasUsedRolls))
        {
            UndoPopup.Display();
        }
        else
        {
            UndoPopup.Hide();
        }
    }

    #endregion
}