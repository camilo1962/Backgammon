using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerAdapter: MonoBehaviour, INetworkManager
{
    #region 

    void OnApplicationQuit()
    {
        StopServer();
        StopClient();
    }

    #endregion

    #region Server Methods

    /// <summary>
    /// Starts a server.
    /// </summary>
    public void StartServer()
    {
        NetworkManager.Instance.StartServer();
    }

    /// <summary>
    /// Stops the server.
    /// </summary>
    public void StopServer()
    {
        NetworkManager.Instance.StopServer();
    }

    #endregion

    #region Client Methods

    /// <summary>
    /// Starts a client using the host address from the specified input field.
    /// </summary>
    /// <param name="hostAddressInputField"></param>
    public void StartClient(InputField hostAddressInputField)
    {
        NetworkManager.Instance.StartClient(hostAddressInputField);
    }

    /// <summary>
    /// Starts a client using the specified host address.
    /// </summary>
    /// <param name="hostAddress"></param>
    public void StartClient(string hostAddress)
    {
        NetworkManager.Instance.StartClient(hostAddress);
    }

    /// <summary>
    /// Stops the client.
    /// </summary>
    public void StopClient()
    {
        NetworkManager.Instance.StopClient();
    }

    #endregion
}