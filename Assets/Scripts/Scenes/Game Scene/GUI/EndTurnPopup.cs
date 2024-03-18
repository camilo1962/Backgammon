using UnityEngine.UI;

public class EndTurnPopup : Popup
{
    #region Private Variables

    public Text Message;

    #endregion

    #region  Public Methods

    /// <summary>
    /// Show the popup.
    /// </summary>
    public override void Display()
    {
        if (Message != null)
        {
            Message.text = string.Format("{0}, ¡acaba el turno!", BackgammonGame.Instance.CurrentPlayer.Name).ToUpper();
        }

        base.Display();
    }

    #endregion
}
