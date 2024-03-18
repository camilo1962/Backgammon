using System;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManager : SingletonGameObject<NetworkManager>, INetworkManager
{
    #region Public Variables

    /// <summary>
    /// Backgammon server.
    /// </summary>
    public BackgammonServer Server;

    /// <summary>
    /// Backgammon client.
    /// </summary>
    public BackgammonClient Client;

    #endregion

    #region Server Method

    /// <summary>
    /// Starts a server.
    /// </summary>
    public void StartServer()
    {
        try
        {
            Instantiate(Server).GetComponent<BackgammonServer>();

            BackgammonClient client = Instantiate(Client).GetComponent<BackgammonClient>();
            client.ClientName = GameManager.Instance.UserName;
            client.IsHost = true;
            client.ConnectToServer("127.0.0.1", 9000);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// Closes any server.
    /// </summary>
    public void StopServer()
    {
        Server server = FindObjectOfType<Server>();

        if (server != null)
        {
            Destroy(server.gameObject);
        }
    }

    #endregion

    #region Client Methods

    /// <summary>
    /// Starts a client using the host address from the specified input field.
    /// </summary>
    /// <param name="hostAddressInputField"></param>
    public void StartClient(InputField hostAddressInputField)
    {
        StartClient(hostAddressInputField.text);
    }

    /// <summary>
    /// Starts a client using the specified host address.
    /// </summary>
    /// <param name="hostAddress"></param>
    public void StartClient(string hostAddress)
    {
        if (string.IsNullOrEmpty(hostAddress))
        {
            hostAddress = "127.0.0.1";
        }

        try
        {
            BackgammonClient client = Instantiate(Client).GetComponent<BackgammonClient>();
            client.ClientName = GameManager.Instance.UserName;
            client.IsHost = false;
            client.ConnectToServer(hostAddress, 9000);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// Closes any client.
    /// </summary>
    public void StopClient()
    {
        Client client = FindObjectOfType<Client>();

        if (client != null)
        {
            Destroy(client.gameObject);
        }
    }

    #endregion
}
