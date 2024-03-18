using System;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// TCP client base class definition.
/// </summary>
public class Client : MonoBehaviour
{
    #region Private Variables

    private bool m_SocketReady;
    private TcpClient m_Socket;
    private NetworkStream m_Stream;
    private StreamWriter m_StreamWriter;
    private StreamReader m_StreamReader;

    #endregion

    #region  Monobehaviour Methods
    
    // Use this for initialization
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    private void Update()
    {
        if (m_SocketReady)
        {
            if (m_Stream.DataAvailable)
            {
                string data = m_StreamReader.ReadLine();

                if (data != null)
                {
                    OnIncomingData(data);
                }
            }
        }
    }

    private void OnApplicationQuit()
    {
        CloseSocket();
    }

    private void OnDisable()
    {
        CloseSocket();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Connects to the server with the specified host name and port number.
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <returns></returns>
    public bool ConnectToServer(string host, int port)
    {
        if (m_SocketReady)
        {
            return false;
        }

        try
        {
            m_Socket = new TcpClient(host, port);
            m_Stream = m_Socket.GetStream();
            m_StreamWriter = new StreamWriter(m_Stream);
            m_StreamReader = new StreamReader(m_Stream);
            m_SocketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        return m_SocketReady;
    }
    
    /// <summary>
    /// Send data to the server.
    /// </summary>
    /// <param name="data"></param>
    public void Send(string data)
    {
        if (!m_SocketReady)
        {
            return;
        }

        m_StreamWriter.WriteLine(data);
        m_StreamWriter.Flush();
	}

    #endregion

    #region Protected Methods

    /// <summary>
    /// Handles incoming data from the server.
    /// </summary>
    /// <param name="data"></param>
    protected virtual void OnIncomingData(string data)
    {
        Debug.Log(string.Format("Received data from server: {0}", data));
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Closes the socket.
    /// </summary>
    private void CloseSocket()
    {
        if (!m_SocketReady)
        {
            return;
        }

        m_StreamWriter.Close();
        m_StreamReader.Close();
        m_Stream.Close();
        m_SocketReady = false;
    }

    #endregion
}