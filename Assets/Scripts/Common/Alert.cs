using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// A message displaying component that will fade after a certain period of time.
/// </summary>
[RequireComponent(typeof(Animator))]
public class Alert : MonoBehaviour
{
    #region Private Variables
    
    /// <summary>
    /// Whether the message will be displayed in all upper case.
    /// </summary>
    [SerializeField]
    private bool m_UpperCase = false;

    /// <summary>
    /// The message.
    /// </summary>
    private Text m_Message;

    #endregion

    #region MonoBehaviour Methods

    private void Start()
    {
        this.m_Message = transform.Find("Message").GetComponent<Text>();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Shows the specified message.
    /// </summary>
    public void Show(string message)
    {
        m_Message.text = m_UpperCase ? message.ToUpper() : message;
        GetComponent<Animator>().SetTrigger("Show");
    }

    #endregion
}