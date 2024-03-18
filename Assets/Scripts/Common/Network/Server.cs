using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// TCP Server base class definition.
/// </summary>
public class Server : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// The server port number.
    /// </summary>
    public int Port = 9000;

    #endregion

    #region Private Variables

    /// <summary>
    /// The TCP listener.
    /// </summary>
    private TcpListener _Server;

    /// <summary>
    /// Whether the server has started.
    /// </summary>
    private bool _ServerStarted;

    /// <summary>
    /// The connected clients.
    /// </summary>
    private List<ServerClient> _Clients = new List<ServerClient>();

    /// <summary>
    /// The clients that have disconnected and are pending removal.
    /// </summary>
    private List<ServerClient> _DisconnectedClients = new List<ServerClient>();

    #endregion

    #region Monobehaviour Methods

    /// <summary>
    /// Used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake()
    {
		StartServer ();
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        if (!_ServerStarted)
        {
            return;
        }

        foreach (ServerClient client in _Clients)
        {
            if (IsConnected(client.TcpClient))
            {
                NetworkStream s = client.TcpClient.GetStream();

                if (s.DataAvailable)
                {
                    StreamReader reader = new StreamReader(s);
                    string data = reader.ReadLine();

                    if (data != null)
                    {
                        OnIncomingData(client, data);
                    }
                }
            }
            else
            {
                client.TcpClient.Close();
                _DisconnectedClients.Add(client);
                continue;
            }
        }

        for (int i = 0; i < _DisconnectedClients.Count; i++)
        {
            _Clients.Remove(_DisconnectedClients[i]);
            _DisconnectedClients.RemoveAt(i);
        }
    }

    /// <summary>
    /// Called when the MonoBehaviour will be destroyed.
    /// </summary>
	void OnDestroy()
    {
		CloseServer ();
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Broadcasts data to all clients.
    /// </summary>
    /// <param name="data"></param>
    protected void Broadcast(string data)
    {
        Broadcast(_Clients, data);
    }

    /// <summary>
    /// Broadcasts data to specific clients.
    /// </summary>
    /// <param name="clients"></param>
    /// <param name="data"></param>
    protected void Broadcast(List<ServerClient> clients, string data)
    {
        foreach (ServerClient client in clients)
        {
            Send(client, data);
        }
    }

    /// <summary>
    /// Relays data from a client to all other clients.
    /// </summary>
    /// <param name="relayClient"></param>
    /// <param name="data"></param>
    protected void Relay(ServerClient relayClient, string data)
    {
        foreach (ServerClient client in _Clients)
        {
            if (client != relayClient)
            {
                Send(client, data);
            }
        }
    }

    /// <summary>
    /// Sends data to a specific client.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="data"></param>
    protected void Send(ServerClient client, string data)
    {
        try
        {
            StreamWriter sw = new StreamWriter(client.TcpClient.GetStream());
            sw.WriteLine(data);
            sw.Flush();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// Handles incoming data from a client.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="data"></param>
    protected virtual void OnIncomingData(ServerClient client, string data)
    {
        Debug.Log(string.Format("Received data from client {0}: {1}", client.ClientName, data));
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Starts the server.
    /// </summary>
    private void StartServer()
	{
		DontDestroyOnLoad(gameObject);

        try
		{
			Debug.Log(string.Format("Starting server on port {0}...", Port));

			_Server = new TcpListener(IPAddress.Any, Port);
            _Server.Start();
            _ServerStarted = true;

            StartListening();

			Debug.Log(string.Format("Server started."));
		}
		catch (Exception e)
		{
			Debug.Log(e.Message);
		}
	}

	/// <summary>
	/// Closes the server.
	/// </summary>
	private void CloseServer()
	{
		try
		{
			foreach (ServerClient client in _Clients)
			{
				client.TcpClient.Close();
			}
		}
		catch (Exception e)
		{
			Debug.LogException(e);
		}
		finally
		{
			Debug.Log("Stopping TCP server...");

			_Server.Stop();
            _ServerStarted = false;

			Debug.Log(string.Format("Server stopped."));
		}
	}

    /// <summary>
    /// Listens for TCP clients.
    /// </summary>
    private void StartListening()
    {
        _Server.BeginAcceptTcpClient(AcceptTcpClient, _Server);
    }

    /// <summary>
    /// Accepts a connected TCP Client.
    /// </summary>
    /// <param name="ar"></param>
    private void AcceptTcpClient(IAsyncResult ar)
    {
        TcpListener listener = ar.AsyncState as TcpListener;
        ServerClient serverClient = new ServerClient(listener.EndAcceptTcpClient(ar));
        _Clients.Add(serverClient);

        Send(serverClient, NetworkCommands.Who);

        StartListening();
    }

    /// <summary>
    /// Gets whether the specified TCP client is actively connected.
    /// </summary>
    /// <param name="tcpClient"></param>
    /// <returns></returns>
    private bool IsConnected(TcpClient tcpClient)
    {
        try
        {
            if (tcpClient != null && tcpClient.Client != null && tcpClient.Client.Connected)
            {
                if (tcpClient.Client.Poll(0, SelectMode.SelectRead))
                {
                    return !(tcpClient.Client.Receive(new byte[1], SocketFlags.Peek) == 0);
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }

    #endregion
}
