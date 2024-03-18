using System.Net.Sockets;

public class ServerClient
{
    #region Public Properties

    public TcpClient TcpClient
    {
        get;
        private set;
    }

    public string ClientName
    {
        get;
        set;
    }

    public bool IsHost
    {
        get;
        set;
    }

    #endregion

    #region Constructor

    public ServerClient(TcpClient TcpClient)
    {
        this.TcpClient = TcpClient;
    }

    #endregion
}