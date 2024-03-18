using UnityEngine.UI;

public class DoublingCubeOfferPopup : Popup
{
    #region Private Variables

    /// <summary>
    /// The message.
    /// </summary>
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
            Message.text = string.Format("{0}, ¿Quieres dados dobles?", BackgammonGame.Instance.DoublingCube.OfferedPlayer.Name).ToUpper();
        }

        base.Display();
    }

    #endregion
}