using UnityEngine;
using System.Collections;
using System;

public class GameSceneCamera : MonoBehaviour
{
    #region Private Variables

    private Animator m_Animator;

    #endregion

    #region Properties

    public int CurrentPlayer
    {
        get
        {
            return m_Animator.GetInteger("CurrentPlayer");
        }
        set
        {
            m_Animator.SetInteger("CurrentPlayer", value);
        }
    }

    #endregion

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        m_Animator = GetComponent<Animator>();

        BackgammonGame.Instance.CurrentPlayerChanged += OnCurrentPlayerChanged;
    }

    #region Event Handler Methods

    /// <summary>
    /// Handles the CurrentPlayerChanged event from BackgammonGame.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnCurrentPlayerChanged(object sender, EventArgs e)
    {
        if (BackgammonGame.Instance.CurrentPlayer != null &&
            BackgammonGame.Instance.CurrentPlayer.IsUserControlled)
        {
            CurrentPlayer = (int)BackgammonGame.Instance.CurrentPlayer.ID;
        }
    }
    
    #endregion
}
