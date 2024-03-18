public class WaitingForOpponentsMenu : Popup
{
    public override bool IsOpen
    {
        get
        {
            return base.IsOpen;
        }
        set
        {
            if (!value)
            {
                NetworkManager.Instance.StopClient();
                NetworkManager.Instance.StopServer();
            }

            base.IsOpen = value;
        }
    }
}
