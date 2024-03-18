using UnityEngine;

public class Popup : MonoBehaviour
{
    #region Protected Variables

    protected Animator m_Animator;
    protected CanvasGroup m_CanvasGroup;

    #endregion

    #region Public Properties

    public virtual bool IsOpen
    {
        get
        {
            return m_Animator.GetBool("IsOpen");
        }
        set
        {
            if (value)
            {
                OnOpen();
            }
            else
            {
                OnHide();
            }

            m_Animator.SetBool("IsOpen", value);
        }
    }

    #endregion

    #region MonoBehaviour Methods

    protected virtual void Awake()
    {
        m_Animator = GetComponent<Animator>();
        m_CanvasGroup = GetComponent<CanvasGroup>();
    }

    protected virtual void Update()
    {
        if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            m_CanvasGroup.blocksRaycasts = false;
            m_CanvasGroup.interactable = false;
        }
        else
        {
            m_CanvasGroup.blocksRaycasts = true;
            m_CanvasGroup.interactable = true;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Show the popup.
    /// </summary>
    public virtual void Display()
    {
        IsOpen = true;
    }

    /// <summary>
    /// Hides the popup.
    /// </summary>
    public virtual void Hide()
    {
        IsOpen = false;
    }

    /// <summary>
    /// Toggles the popup's visibility.
    /// </summary>
    public virtual void Toggle()
    {
        IsOpen = !IsOpen;
    }

    #endregion

    /// <summary>
    /// Handles any operation that occurs when opening the popup.
    /// </summary>
    protected virtual void OnOpen()
    {
    }

    /// <summary>
    /// Handles any operation that occurs when closing the popup.
    /// </summary>
    protected virtual void OnHide()
    {
    }
}
