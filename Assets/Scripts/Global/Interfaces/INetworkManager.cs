
using UnityEngine.UI;

public interface INetworkManager
{
    /// <summary>
    /// Starts a server.
    /// </summary>
    void StartServer();

    /// <summary>
    /// Starts a client using the host address from the specified input field.
    /// </summary>
    /// <param name="hostAddressInputField"></param>
    void StartClient(InputField hostAddressInputField);

    /// <summary>
    /// Starts a client using the specified host address.
    /// </summary>
    /// <param name="hostAddress"></param>
    void StartClient(string hostAddress);

    /// <summary>
    /// Stops the server.
    /// </summary>
    void StopServer();

    /// <summary>
    /// Stops the client.
    /// </summary>
    void StopClient();
}