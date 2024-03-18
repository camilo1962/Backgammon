using UnityEngine.UI;

public class PopupMessage : Popup
{
    #region Public Methods

    /// <summary>
    /// Displays the specified message. Resets the fade time.
    /// </summary>
    public void Display(string message)
    {
        Text textComponent = GetComponentInChildren<Text>();

        if (textComponent != null)
        {
            textComponent.text = message;
        }

        base.Display();
    }

    #endregion
}