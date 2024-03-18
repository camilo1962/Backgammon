using UnityEngine;

public class BackgammonClient : Client
{
    #region Public Properties

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

    #region Private Methods

    /// <summary>
    /// Handles incoming data from the server.
    /// </summary>
    /// <param name="data"></param>
    protected override void OnIncomingData(string data)
    {
        Debug.Log(string.Format("Received data from server: {0}", data));

        string[] aData = data.Split('|');

        switch (aData[0])
        {
            case (NetworkCommands.Who):
                {
                    Send(string.Format("{0}|{1}|{2}", NetworkCommands.Who, ClientName, IsHost));
                }
                break;
            case (NetworkCommands.Start):
                {
                    GameManager.Instance.LoadGameScene();
                }
                break;
            case (NetworkCommands.RequestDiceRoll):
                {
                    BackgammonGame.Instance.RollDice();
                }
                break;
            case (NetworkCommands.DiceRoll):
                {
                    int roll1 = 0;
                    int roll2 = 0;

                    if (int.TryParse(aData[1], out roll1) && int.TryParse(aData[2], out roll2))
                    {
                        BackgammonGame.Instance.RollDice(roll1, roll2);
                    }
                }
                break;
            case (NetworkCommands.OfferDoublingCube):
                {
                    BackgammonGame.Instance.OfferDoublingCube();
                }
                break;
            case (NetworkCommands.AcceptDoublingCube):
                {
                    BackgammonGame.Instance.AcceptDoublingCube();
                }
                break;
            case (NetworkCommands.DeclineDoublingCube):
                {
                    BackgammonGame.Instance.DeclineDoublingCube();
                }
                break;
            case (NetworkCommands.MovesMade):
                {
                    for (int i = 1; i < aData.Length; i++)
                    {
                        string[] move = aData[i].Split('-');

                        BGPointID startPointID = (BGPointID)int.Parse(move[0]);
                        BGPointID endPointID = (BGPointID)int.Parse(move[1]);

                        BackgammonGame.Instance.MoveChecker(startPointID, endPointID);
                    }

                    BackgammonGame.Instance.EndTurn();
                }
                break;
        }
    }

    #endregion
}