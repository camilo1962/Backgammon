using System.Collections.Generic;
using UnityEngine;

public class BackgammonServer : Server
{
    #region Private Variables

    private List<ServerClient> _Players = new List<ServerClient>();
    private List<ServerClient> _Spectators = new List<ServerClient>();

    #endregion

    #region Public Properties

    public ServerClient Host
    {
        get
        {
            return _Players[0];
        }
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Handles incoming data from a client.
    /// </summary>
    /// <param name="client"></param>
    /// <param name="data"></param>
    protected override void OnIncomingData(ServerClient client, string data)
    {
        Debug.Log(string.Format("Received data from client {0}: {1}", client.ClientName, data));

        string[] aData = data.Split('|');

        switch (aData[0])
        {
            case (NetworkCommands.Who):
                {
                    client.ClientName = aData[1];
                    client.IsHost = (aData[2] == "True") ? true : false;
                    AddPlayer(client);
                }
                break;
            case (NetworkCommands.RequestDiceRoll):
                {
                    Send(Host, data);
                }
                break;
            case (NetworkCommands.DiceRoll):
                {
                    Relay(client, data);
                }
                break;
            case (NetworkCommands.OfferDoublingCube):
                {
                    Relay(client, data);
                }
                break;
            case (NetworkCommands.AcceptDoublingCube):
                {
                    Relay(client, data);
                }
                break;
            case (NetworkCommands.DeclineDoublingCube):
                {
                    Relay(client, data);
                }
                break;
            case (NetworkCommands.MovesMade):
                {
                    Relay(client, data);
                }
                break;
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Adds the client to the server room.
    /// </summary>
    /// <param name="client"></param>
    private void AddPlayer(ServerClient client)
    {
        if (_Players.Count < 2)
        {
            _Players.Add(client);

            if (_Players.Count == 2)
            {
                Send(_Players[0], NetworkCommands.Start);
                Send(_Players[1], NetworkCommands.Start);
            }
        }
        else
        {
            _Spectators.Add(client);
        }
    }

    #endregion
}
