using UnityEngine;

public class OptionButton : MonoBehaviour
{
    #region Protected Variables

    /// <summary>
    /// The animator.
    /// </summary>
    protected Animator m_Animator;

    /// <summary>
    /// The canvas group
    /// </summary>
    protected CanvasGroup m_CanvasGroup;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets whether the option is enabled.
    /// </summary>
    public bool IsOptionEnabled
    {
        get
        {
            return m_Animator.GetBool("IsOptionEnabled");
        }
        set
        {
            if (value)
            {
                OnOptionEnabled();
            }
            else
            {
                OnOptionDisabled();
            }

            m_Animator.SetBool("IsOptionEnabled", value);
        }
    }

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Used to initialize any variables or game state before the game starts.
    /// </summary>
    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Toggle the option button between option enabled and option disabled.
    /// </summary>
    public void Toggle()
    {
        IsOptionEnabled = !IsOptionEnabled;
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Handles any operation that occurs when the option button is set to enabled.
    /// </summary>
    protected virtual void OnOptionEnabled()
    {
    }

    /// <summary>
    /// Handles any operation that occurs when the option button is set to disabled.
    /// </summary>
    protected virtual void OnOptionDisabled()
    {
    }

    #endregion
}
