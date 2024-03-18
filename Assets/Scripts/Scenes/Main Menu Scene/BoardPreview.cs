using UnityEngine;

public class BoardPreview : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// The backgammon board.
    /// </summary>
    public Board Board;

    /// <summary>
    /// Player 1.
    /// </summary>
    public Player Player1;

    /// <summary>
    /// Player 2.
    /// </summary>
    public Player Player2;

    /// <summary>
    /// The Backgammon game variant currently being previewed.
    /// </summary>
    private BackgammonVariants Variant = (BackgammonVariants)int.MaxValue;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Called on the frame when a script is enabled.
    /// </summary>
    private void Start()
    {
        SetupBoard();
    }

    /// <summary>
    /// Called every fixed framerate frame
    /// </summary>
    private void FixedUpdate()
    {
        if (Variant != GameManager.Instance.BackgammonVariant)
        {
            SetupBoard();
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets up the preview board according to the current game settings.
    /// </summary>
    private void SetupBoard()
    {
        Board.SetupCheckers(GameManager.Instance.BackgammonVariant, Player1, Player2);
    }

    #endregion
}
