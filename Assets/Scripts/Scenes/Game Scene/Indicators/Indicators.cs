using System;
using UnityEngine;

/// <summary>
/// Visuall aids for helping the player in select various items.
/// </summary>
public abstract class Indicators : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// The indicators objects.
    /// </summary>
    [SerializeField]
    private GameObjectGroup IndicatorObjects = new GameObjectGroup();

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Called on the frame when a script is enabled.
    /// </summary>
    void Start()
    {
        BackgammonGame.Instance.MoveOptionsUpdated += OnMoveOptionsUpdated;
        BackgammonGame.Instance.CheckerPickedUp += OnCheckerPickedUp;
        BackgammonGame.Instance.CheckerReturned += OnCheckerReturned;
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
        Refresh();
    }

    /// <summary>
    /// Handles the CheckerPickedUp event from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCheckerPickedUp(object sender, EventArgs e)
    {
        Refresh();
    }

    /// <summary>
    /// Handles the CheckerReturned event from BoardManager.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCheckerReturned(object sender, EventArgs e)
    {
        Refresh();
    }

    #endregion

    #region Abstract Methods

    /// <summary>
    /// Fills the Backgammon board with indicator game objects.
    /// </summary>
    protected abstract void Fill();

    #endregion

    #region Protected Methods

    /// <summary>
    /// Adds and returns an indicator game object.
    /// </summary>
    /// <returns></returns>
    protected GameObject Add()
    {
        GameObject indicatorObject = IndicatorObjects.Add();
        indicatorObject.transform.SetParent(transform);
        return indicatorObject;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Refresh.
    /// </summary>
    private void Refresh()
    {
        IndicatorObjects.Clear();

        if (BackgammonGame.Instance.CurrentPlayer.IsUserControlled)
        {
            Fill();
        }
    }

    #endregion
}
