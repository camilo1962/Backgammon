using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using System.Text;

/// <summary>
/// Oversees all interactions between the user and the backgammon board. Handles all game logic.
/// </summary>
public class BackgammonGame : MonoBehaviour
{
    public AudioSource DadosDice;
    public AudioSource DadosDice1;
    #region Singleton

    /// <summary>
    /// Singleton access.
    /// </summary>
    public static BackgammonGame Instance
    {
        get;
        private set;
    }

    #endregion

    #region GUI Variables

    public Alert Alert;

    #endregion

    #region Events

    /// <summary>
    /// Event that is raised whenever the match has started.
    /// </summary>
    public EventHandler MatchStarted;

    /// <summary>
    /// Event that is raised whenever the match has ended.
    /// </summary>
    public EventHandler MatchEnded;

    /// <summary>
    /// Event that is raised whenever a round has started.
    /// </summary>
    public EventHandler GameStarted;

    /// <summary>
    /// Event that is raised whenever a round has ended.
    /// </summary>
    public EventHandler GameEnded;

    /// <summary>
    /// Event that is raised whenever a turn has started.
    /// </summary>
    public EventHandler TurnStarted;

    /// <summary>
    /// Event that is raised whenever a turn has ended.
    /// </summary>
    public EventHandler TurnEnded;

    /// <summary>
    /// Event that is raised whenever the current player has changed.
    /// </summary>
    public EventHandler CurrentPlayerChanged;

    /// <summary>
    /// Event that is raised whenever a checker has been picked up.
    /// </summary>
    public EventHandler CheckerPickedUp;

    /// <summary>
    /// Event that is raised whenever a selected checker has been returned.
    /// </summary>
    public EventHandler CheckerReturned;

    /// <summary>
    /// Event raised whenever a checker has been moved.
    /// </summary>
    public EventHandler CheckerMoved;

    /// <summary>
    /// Event that is raised whenever the current move options have been updated.
    /// </summary>
    public EventHandler MoveOptionsUpdated;

    /// <summary>
    /// Event that is raised whenever the current stakes has changed.
    /// </summary>
    public EventHandler StakesChanged;

    #endregion

    #region Public Variables

    /// <summary>
    /// The Backgammon board.
    /// </summary>
    public Board Board;

    /// <summary>
    /// Player 1.
    /// </summary>
    public Player Player1;

    /// <summary>
    /// Player 2.
    /// </summary>
    public Player Player2;

    /// <summary>
    /// The doubling cube.
    /// </summary>
    public DoublingCube DoublingCube;

    /// <summary>
    /// The Backgammon game variant.
    /// </summary>
    public BackgammonVariants Variant;

    /// <summary>
    /// Whether the game is a match play or a money play.
    /// </summary>
    public BackgammonPlayMode PlayMode;

    /// <summary>
    /// Whether the Crawford rule is enabled. Only available in match play.
    /// </summary>
    public bool IsCrawfordRuleEnabled;

    /// <summary>
    /// Whether the Murphy rule is enabled.
    /// </summary>
    public bool IsMurphyRuleEnabled;

    /// <summary>
    /// The score needed to win the match.
    /// </summary>
    public int MatchScore = 1;

    /// <summary>
    /// The initial stakes.
    /// </summary>
    public int InitialStakes = 1;

    /// <summary>
    /// The duration, in seconds, after a round has started and before the first turn begins.
    /// </summary>
    public float StartRoundDelay = 2f;

    /// <summary>
    /// The duration, in seconds, after a round has ended and before the next round begins.
    /// </summary>
    public float EndRoundDelay = 2f;

    #endregion

    #region Private Variables

    /// <summary>
    /// The moves made this turn.
    /// </summary>
    private Stack<BGMove> m_MovesMade = new Stack<BGMove>();

    /// <summary>
    /// The client.
    /// </summary>
    private BackgammonClient m_Client;

    /// <summary>
    /// The current stakes.
    /// </summary>
    private int m_Stakes;

    /// <summary>
    /// The current player.
    /// </summary>
    public Player m_CurrentPlayer;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the current player.
    /// </summary>
    public Player CurrentPlayer
    {
        get
        {
            return m_CurrentPlayer;
        }
        private set
        {
            m_CurrentPlayer = value;
            EventHelper.Raise(this, CurrentPlayerChanged);
        }
    }

    /// <summary>
    /// Gets the winner of this match. Returns null if the match has not yet ended.
    /// </summary>
    public Player Winner { get; private set; }

    /// <summary>
    /// Gets the loser of this match. Returns null if the match has not yet ended.
    /// </summary>
    public Player Loser { get; private set; }

    /// <summary>
    /// Gets whether the dice is currently rolling.
    /// </summary>
    public bool IsDiceRolling
    {
        get
        {
            return Player1.Dice.IsRolling || Player2.Dice.IsRolling;
        }
    }

    /// <summary>
    /// Gets whether the dice have been rolled this turn.
    /// </summary>
    public bool IsDiceRolled { get; private set; }

    /// <summary>
    /// Gets the current dice roll.
    /// </summary>
    public BGDiceRoll DiceRoll { get; private set; }

    /// <summary>
    /// Gets the current move options.
    /// </summary>
    public BGMoveOptions MoveOptions { get; private set; }

    /// <summary>
    /// Gets the current selected checker.
    /// </summary>
    public Checker SelectedChecker { get; private set; }

    /// <summary>
    /// Gets whether the game is a Crawford Game. In a Crawford Game, the doubling cube is disabled.
    /// </summary>
    public bool IsCrawfordGame { get; private set; }

    /// <summary>
    /// Gets whether the Crawford game as already been played
    /// </summary>
    public bool IsCrawfordGamePlayed { get; private set; }

    /// <summary>
    /// Gets the Murphy Rule multiplier.
    /// </summary>
    public int MurphyRuleMultiplier { get; private set; }

    /// <summary>
    /// Gets the current turn number.
    /// </summary>
    public int Turn { get; private set; }

    /// <summary>
    /// Gets whether the current turn has begun.
    /// </summary>
    public bool IsTurnStarted { get; private set; }

    /// <summary>
    /// Gets whether the current turn is the first turn of a round.
    /// </summary>
    public bool IsFirstTurn
    {
        get
        {
            return Turn == 0;
        }
    }

    /// <summary>
    /// Gets or sets the stakes of the current game.
    /// </summary>
    public int Stakes
    {
        get
        {
            return m_Stakes;
        }
        private set
        {
            m_Stakes = value;
            EventHelper.Raise(this, StakesChanged);
        }
    }

    /// <summary>
    /// Gets whether any user interactions with the board pieces is currently prevented.
    /// </summary>
    public bool IsLocked { get; private set; }

    /// <summary>
    /// Gets whether the player is playing against the another via multiplayer.
    /// </summary>
    public bool IsMultiplayerGame
    {
        get
        {
            return Player1.IsMultiplayerControlled || Player2.IsMultiplayerControlled;
        }
    }

    /// <summary>
    /// Gets whether the player is playing against the computer.
    /// </summary>
    public bool IsAIGame
    {
        get
        {
            return Player1.IsAIControlled || Player2.IsAIControlled;
        }
    }

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Used to initialize any variables or game state before the game starts.
    /// </summary>
    void Awake()
    {
        Instance = this;

        Player1.Name = GameManager.Instance.Player1Name;
        Player2.Name = GameManager.Instance.Player2Name;
        Player1.PlayerControlMode = GameManager.Instance.Player1ControlMode;
        Player2.PlayerControlMode = GameManager.Instance.Player2ControlMode;
        IsCrawfordRuleEnabled = GameManager.Instance.IsCrawfordRuleEnabled;
        IsMurphyRuleEnabled = GameManager.Instance.IsMurphyRuleEnabled;
        Variant = GameManager.Instance.BackgammonVariant;
        PlayMode = GameManager.Instance.BackgammonPlayMode;
        MatchScore = GameManager.Instance.MatchScore;
        InitialStakes = GameManager.Instance.InitialStakes;

        if (IsMultiplayerGame)
        {
            m_Client = FindObjectOfType<BackgammonClient>();
        }
    }

    /// <summary>
    /// Called on the frame when a script is enabled.
    /// </summary>
    void Start()
    {
        StartMatch();
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update()
    {
        if (CurrentPlayer == null || !CurrentPlayer.IsUserControlled)
        {
            return;
        }

        if (IsDiceRolled)
        {
            UpdateCheckerSelection();
        }
        else if (!IsDiceRolling)
        {
            UpdateDiceSelection();
        }
    }

    #endregion

    #region Update Methods

    /// <summary>
    /// Actualiza la interacción del usuario con las fichas.
    /// </summary>
    private void UpdateCheckerSelection()
    {
        if (IsLocked)
        {
            return;
        }

        if (SelectedChecker == null)
        {
            if (Input.GetMouseButtonDown(0) && MoveOptions.CanMove())
            {
                SelectChecker();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                Undo();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                DropChecker();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                ReturnChecker();
            }
        }
    }

    /// <summary>
    /// Actualiza la interacción del usuario con los dados.
    /// </summary>
    private void UpdateDiceSelection()
    {
        if (IsLocked)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            
            SelectDice();
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Starts a new match.
    /// </summary>
    private void StartMatch()
    {
        EventHelper.Raise(this, MatchStarted);
        StartCoroutine(StartGame());
    }

    /// <summary>
    /// Ends the current match.
    /// </summary>
    private void EndMatch()
    {
        IsLocked = true;
        EventHelper.Raise(this, MatchEnded);
    }

    /// <summary>
    /// Starts a new game.
    /// </summary>
    private IEnumerator StartGame()
    {
        if (PlayMode == BackgammonPlayMode.Match && IsCrawfordRuleEnabled)
        {
            if (IsCrawfordGamePlayed)
            {
                IsCrawfordGame = false;
            }
            else if (Player1.Score == (MatchScore - 1) || Player2.Score == (MatchScore - 1))
            {
                IsCrawfordGame = true;
                IsCrawfordGamePlayed = true;
            }
        }

        Turn = 0;
        Stakes = (PlayMode == BackgammonPlayMode.Money) ? InitialStakes : 1;
        Board.SetupCheckers(Variant, Player1, Player2);
        Player1.Dice.Die1.Present();
        Player2.Dice.Die1.Present();

        yield return Wait(StartRoundDelay);

        EventHelper.Raise(this, GameStarted);

        StartCoroutine(StartTurn());
    }

    /// <summary>
    /// Ends the current game.
    /// </summary>
    private IEnumerator EndGame()
    {
        IsTurnStarted = false;
        EventHelper.Raise(this, GameEnded);

        switch (PlayMode)
        {
            case (BackgammonPlayMode.Match):
                {
                    if (Player1.Score >= MatchScore)
                    {
                        Winner = Player1;
                        Loser = Player2;
                        EndMatch();
                    }
                    else if (Player2.Score >= MatchScore)
                    {
                        Winner = Player2;
                        Loser = Player1;
                        EndMatch();
                    }
                    else
                    {
                        yield return Wait(EndRoundDelay);

                        Player1.Dice.Hide();
                        Player2.Dice.Hide();
                        DoublingCube.Reset();
                        StartCoroutine(StartGame());
                    }
                }
                break;
            case (BackgammonPlayMode.Money):
                {
                    Winner = CurrentPlayer;
                    Loser = GetOpponent(CurrentPlayer);
                    EndMatch();
                }
                break;
        }
    }

    /// <summary>
    /// Starts a new turn.
    /// </summary>
    private IEnumerator StartTurn()
    {
        m_MovesMade.Clear();
        IsDiceRolled = false;
        IsTurnStarted = true;

        EventHelper.Raise(this, TurnStarted);

        if (IsFirstTurn)
        {
            RollDice();
            
        }
        else if (Board.IsBlockedOff(CurrentPlayer))
        {
            ShowAlert(string.Format("{0} Está cerrado y debe ceder el turno.", CurrentPlayer.Name));
            yield return Wait(3f);
            EndTurn();
        }
        else
        {
            ShowAlert(string.Format("turno de {0}", CurrentPlayer.Name));

            CurrentPlayer.Dice.Present();

            if (CurrentPlayer.IsAIControlled)
            {
                yield return Wait(1f);
                RollDice();
            }
        }
    }

    /// <summary>
    /// Ends the current turn.
    /// </summary>
    public void EndTurn()
    {
        if (!IsTurnStarted)
        {
            return;
        }

        if (IsMultiplayerGame && CurrentPlayer.PlayerControlMode == PlayerControlMode.User)
        {
            BGMove[] moves = m_MovesMade.ToArray();

            if (moves.Length > 0)
            {
                StringBuilder sb = new StringBuilder(NetworkCommands.MovesMade);

                for (int i = moves.Length - 1; i >= 0; i--)
                {
                    sb.AppendFormat("|{0}-{1}", (int)moves[i].StartPointID, (int)moves[i].EndPointID);
                }

                m_Client.Send(sb.ToString());
            }
        }

        Turn++;
        Player1.Dice.Hide();
        Player2.Dice.Hide();
        CurrentPlayer = GetOpponent(CurrentPlayer);
        IsTurnStarted = false;

        EventHelper.Raise(this, TurnEnded);

        StartCoroutine(StartTurn());
    }

    #endregion

    #region Dice Interaction Methods

    /// <summary>
    /// Selecciona los dados.
    /// </summary>
    private void SelectDice()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, LayerMask.GetMask("Dice")))
        {
            if (hit.collider.tag == "Dice" && !DoublingCube.IsBeingOffered)
            {
                if (IsMultiplayerGame && !m_Client.IsHost)
                {
                    m_Client.Send(NetworkCommands.RequestDiceRoll);
                }
                else
                {
                    RollDice();
                }
            }
            else if (hit.collider.tag == "Doubling Cube")
            {
                if (IsAIGame)
                {
                    ShowAlert("El dado de duplicación está desactivado cuando se juega contra la IA.");
                }
                else
                {
                    OfferDoublingCubePreview();
                }
            }
        }
    }

    /// <summary>
    /// <summary>
    /// Tira los dados con resultado aleatorio.
    /// </summary>
    public void RollDice()
    {
        if (IsMultiplayerGame && !m_Client.IsHost)
        {
            return;
        }

        if (!IsDiceRolled && !IsDiceRolling)
        {
            int roll1 = UnityEngine.Random.Range(1, 7);
            int roll2 = UnityEngine.Random.Range(1, 7);

            RollDice(roll1, roll2);
            Invoke("Sonar", -0.2f);
            //AudioManager.Instance.PlaySFX("DadosDice");
            if (IsMultiplayerGame)
            {
                m_Client.Send(string.Format("{0}|{1}|{2}", NetworkCommands.DiceRoll, roll1, roll2));
            }
        }
    }

    /// <summary>
    /// Rolls the dice with the predetermined outcomes.
    /// </summary>
    /// <param name="roll1"></param>
    /// <param name="roll2"></param>
    public void RollDice(int roll1, int roll2)
    {
        StartCoroutine(RollDice(roll1, roll2, 1.05f));
       
    }

    /// <summary>
    /// Rolls the dice for the specified number of seconds with the predetermined outcome.
    /// </summary>
    /// <param name="seconds">Seconds.</param>
    private IEnumerator RollDice(int roll1, int roll2, float seconds)
    {
        if (IsFirstTurn)
        {
            Player1.Dice.RollDie1();
           
            Player2.Dice.RollDie1();
           

            yield return new WaitForSeconds(seconds);

            Player1.Dice.Drop();
            Player2.Dice.Drop();

            Player1.Dice.Die1.Number = roll1;
            Player2.Dice.Die1.Number = roll2;
        }
        else
        {
            CurrentPlayer.Dice.Roll();
           
            yield return new WaitForSeconds(seconds);

            CurrentPlayer.Dice.Drop();

            CurrentPlayer.Dice.Die1.Number = roll1;
            CurrentPlayer.Dice.Die2.Number = roll2;
        }

        StartCoroutine(WaitDice(1f));
    }

    /// <summary>
    /// Wait the specified number of seconds for the dice to settle.
    /// </summary>
    /// <param name="seconds"></param>
    /// <returns></returns>
    private IEnumerator WaitDice(float seconds)
    {
        IsLocked = true;
       
        yield return new WaitForSeconds(seconds);

        int roll1;
        int roll2;

        if (IsFirstTurn)
        {
            roll1 = Player1.Dice.Die1.Number;
            roll2 = Player2.Dice.Die1.Number;
        }
        else
        {
            roll1 = CurrentPlayer.Dice.Die1.Number;
            roll2 = CurrentPlayer.Dice.Die2.Number;
        }

        if (IsFirstTurn && roll1 == roll2)
        {
            if (IsMurphyRuleEnabled)
            {
                Stakes *= 2;
            }

            ShowAlert("Volver a lanzarlos");
            RollDice();
        }
        else
        {
            if (IsFirstTurn)
            {
                CurrentPlayer = (roll1 > roll2) ? Player1 : Player2;
                ShowAlert(string.Format("{0} se mueve primero.", CurrentPlayer.Name));
            }

            IsDiceRolled = true;
            DiceRoll = new BGDiceRoll(roll1, roll2);
            MoveOptions = new BGMoveOptions(CurrentPlayer.ID, Board.GetBoardMap(), DiceRoll);
            EventHelper.Raise(this, MoveOptionsUpdated);

            if (!MoveOptions.CanMove())
            {
                ShowAlert(string.Format("{0} no puede moverse y acabó este turno.", CurrentPlayer.Name));
                yield return Wait(3f);
                EndTurn();
            }
            else if (CurrentPlayer.IsAIControlled)
            {
                List<BGMove> moves = new AIPlayerDefault().GetMove(Board.GetBoardMap(), MoveOptions, DiceRoll);

                foreach (BGMove move in moves)
                {
                    yield return new WaitForSeconds(1f);
                    MoveChecker(move);
                }

                yield return new WaitForSeconds(3f);
                EndTurn();
            }
        }

        IsLocked = false;
    }

    #endregion

    #region Doubling Cube Interaction Methods

    /// <summary>
    /// Previews the offering of the doubling cube.
    /// </summary>
    public void OfferDoublingCubePreview()
    {
        if (IsFirstTurn)
        {
            return;
        }
        else if (IsCrawfordGame)
        {
            ShowAlert("No se puede sacar el dado doble en un juego de Crawford.");
            return;
        }
        else if (DoublingCube.Owner == null || DoublingCube.Owner == CurrentPlayer)
        {
            DoublingCube.OfferPreview(GetOpponent(CurrentPlayer));
        }
        else
        {
            ShowAlert("Debes controlar los dados dobles para ofrecerlos.");
        }
    }

    /// <summary>
    /// Offers the doubling cube.
    /// </summary>
    public void OfferDoublingCube()
    {
        if (IsMultiplayerGame && CurrentPlayer.PlayerControlMode == PlayerControlMode.User)
        {
            m_Client.Send(NetworkCommands.OfferDoublingCube);
        }

        DoublingCube.Offer();
    }

    /// <summary>
    /// Cancels the offering of the doubling cube. Does nothing if the doubling cube is not being offered.
    /// </summary>
    public void CancelOfferDoublingCube()
    {
        DoublingCube.CancelOffer();
    }

    /// <summary>
    /// Occurs when the user has accepted the doubling cube. Does nothing if the doubling cube is not being offered.
    /// </summary>
    public void AcceptDoublingCube()
    {
        if (!DoublingCube.IsBeingOffered)
        {
            return;
        }

        if (IsMultiplayerGame)
        {
            m_Client.Send(NetworkCommands.AcceptDoublingCube);
        }

        Stakes *= 2;
        DoublingCube.Accept();
    }

    /// <summary>
    /// Occurs when the user has declined the doubling cube. Does nothing if the doubling cube is not being offered.
    /// </summary>
    public void DeclineDoublingCube()
    {
        if (!DoublingCube.IsBeingOffered)
        {
            return;
        }

        if (IsMultiplayerGame)
        {
            m_Client.Send(NetworkCommands.DeclineDoublingCube);
        }

        ShowAlert(string.Format("{0} pierde este juego.", GetOpponent(CurrentPlayer).Name));

        CurrentPlayer.Score += Stakes;
        DoublingCube.Decline();

        StartCoroutine(EndGame());
    }

    #endregion

    #region Checker Interaction Methods

    /// <summary>
    /// Selects a checker.
    /// </summary>
    private void SelectChecker()
    {
        if (EventSystem.current.IsPointerOverGameObject() || SelectedChecker != null)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, LayerMask.GetMask("Points")))
        {
            if ((hit.collider.tag == "Point" && !Board.IsJailed(CurrentPlayer)) || (hit.collider.tag == "Jail"))
            {
                Point point = hit.collider.gameObject.GetComponent<Point>();
                PickupChecker(point);
            }
        }
    }

    /// <summary>
    /// Recoge una ficha del punto especificado.
    /// </summary>
    /// <param name="point"></param>
    private void PickupChecker(Point point)
    {
        Checker checker = point.CurrentChecker;

        if (checker != null && MoveOptions.CanMoveFrom(point.ID))
        {
            SelectedChecker = point.PickUp();
            EventHelper.Raise(this, CheckerPickedUp);
        }
    }

    /// <summary>
    /// Drops the current selected checker. Does nothing if no checker is currently selected.
    /// </summary>
    private void DropChecker()
    {
        if (SelectedChecker == null)
        {
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000, LayerMask.GetMask("Points")))
        {
           
            if (hit.collider.tag == "Point" || hit.collider.tag == "Home")
            {
                Point point = hit.collider.gameObject.GetComponent<Point>();
                PlaceChecker(point);
            }
        }
    }
    public void Sonar()
    {
        DadosDice.Play();
        DadosDice1.Play();
    }
    /// <summary>
    /// Coloca la ficha seleccionada actualmente en el punto con el ID especificado.
    /// </summary>
    /// <param name="point"></param>
    public void PlaceChecker(BGPointID pointID)
    {
        PlaceChecker(Board.GetPoint(pointID));
    }

    /// <summary>
    /// Coloca la ficha seleccionada actualmente en el punto especificado.
    /// </summary>
    private void PlaceChecker(Point point)
    {
        if (SelectedChecker == null)
        {
            return;
        }
        else if (SelectedChecker.Point == point)
        {
            point.Place(SelectedChecker);
            SelectedChecker = null;

            EventHelper.Raise(this, CheckerReturned);
        }
        else
        {
            BGMove move = MoveOptions.GetMove(SelectedChecker.Point.ID, point.ID);

            if (move != null && DiceRoll.Use(move))
            {
                if (move.IsHit)
                {
                    JailChecker(point.PickUp());
                }

                point.Place(SelectedChecker);
                SelectedChecker = null;

                m_MovesMade.Push(move);
                MoveOptions = new BGMoveOptions(CurrentPlayer.ID, Board.GetBoardMap(), DiceRoll);
                EventHelper.Raise(this, MoveOptionsUpdated);
                EventHelper.Raise(this, CheckerMoved);
            }
        }

        // Check if the move results in a win.

        if (SelectedChecker == null)
        {
            CheckForWin(CurrentPlayer);
        }
    }

    /// <summary>
    /// Automatically moves a checker using the specified move.
    /// </summary>
    /// <param name="move"></param>
    public MoveResult MoveChecker(BGMove move)
    {
        return MoveChecker(move.StartPointID, move.EndPointID);
    }

    /// <summary>
    /// Automatically moves a checker from and to the given points with the specified IDs.
    /// </summary>
    /// <param name="startPointID"></param>
    /// <param name="endPointID"></param>
    public MoveResult MoveChecker(BGPointID startPointID, BGPointID endPointID)
    {
        return MoveChecker(Board.GetPoint(startPointID), Board.GetPoint(endPointID));
    }

    /// <summary>
    /// Automatically moves a checker from and to the specified points.
    /// </summary>
    /// <param name="startPoint"></param>
    /// <param name="endPoint"></param>
    private MoveResult MoveChecker(Point startPoint, Point endPoint)
    {
        BGMove move = MoveOptions.GetMove(startPoint.ID, endPoint.ID);

        if (move != null && DiceRoll.Use(move))
        {
            if (move.IsHit)
            {
                JailChecker(endPoint.PickUp());
            }

            endPoint.GetCheckerFrom(startPoint);

            m_MovesMade.Push(move);
            MoveOptions = new BGMoveOptions(CurrentPlayer.ID, Board.GetBoardMap(), DiceRoll);
            EventHelper.Raise(this, MoveOptionsUpdated);
            EventHelper.Raise(this, CheckerMoved);

            if (CheckForWin(CurrentPlayer))
            {
                return CurrentPlayer.ID == BGPlayerID.Player1 ? MoveResult.Player1Win : MoveResult.Player2Win;
            }
            else
            {
                return MoveResult.Success;
            }
        }
        else
        {
            return MoveResult.Invalid;
        }
    }

    /// <summary>
    /// Jails the specified checker.
    /// </summary>
    private void JailChecker(Checker checker)
    {
        Point jail = Board.GetJail(checker.Owner);
        jail.Place(checker);
    }

    /// <summary>
    /// Returns the current selected checker to its point.
    /// </summary>
    public void ReturnChecker()
    {
        if (SelectedChecker != null)
        {
            SelectedChecker.Point.Place(SelectedChecker);
            SelectedChecker = null;

            EventHelper.Raise(this, CheckerReturned);
        }
    }

    /// <summary>
    /// Undos the last move made.
    /// </summary>
    public void Undo()
    {
        if (SelectedChecker != null)
        {
            ReturnChecker();
        }
        else if (m_MovesMade.Count > 0)
        {
            BGMove move = m_MovesMade.Pop();

            Board.GetPoint(move.StartPointID).PlaceInstantly(Board.GetPoint(move.EndPointID).PickUp());

            if (move.IsHit)
            {
                Point jail = Board.GetJail(GetOpponent(CurrentPlayer));
                Board.GetPoint(move.EndPointID).PlaceInstantly(jail.PickUp());
            }

            DiceRoll.Restore(move);
            MoveOptions = new BGMoveOptions(CurrentPlayer.ID, Board.GetBoardMap(), DiceRoll);
            EventHelper.Raise(this, MoveOptionsUpdated);
            EventHelper.Raise(this, CheckerMoved);
        }
    }

    /// <summary>
    /// Check if the specified player has won this round. If so, ends this round.
    /// </summary>
    private bool CheckForWin(Player player)
    {
        if (Board.IsComplete(player))
        {
            Player opponent = GetOpponent(player);

            if (Board.IsJailed(opponent) || Board.IsInOpponentInnerTable(opponent))
            {
                ShowAlert(string.Format("{0} ¡GANA EL BACKGAMMOND!", player.Name));
                Stakes *= 3;
            }
            else if (!Board.HasBorneOff(opponent))
            {
                ShowAlert(string.Format("{0} ¡Gana un gammon!", player.Name));
                Stakes *= 2;
            }
            else
            {
                ShowAlert(string.Format("{0} ¡Gana este juego!", player.Name));
            }

            player.Score += Stakes;
            player.RoundsWon++;

            StartCoroutine(EndGame());

            return true;
        }

        return false;
    }

    #endregion

    #region Player Methods

    /// <summary>
    /// Gets the opponent of the player.
    /// </summary>
    private Player GetOpponent(Player player)
    {
        switch (player.ID)
        {
            case (BGPlayerID.Player1): return Player2;
            case (BGPlayerID.Player2): return Player1;
            default: return null;
        }
    }

    #endregion
    
    #region Timer Methods

    /// <summary>
    /// Wait the specified seconds, locking user interaction for the duration.
    /// </summary>
    private IEnumerator Wait(float seconds)
    {
        IsLocked = true;
        yield return new WaitForSeconds(seconds);
        IsLocked = false;
    }

    #endregion

    #region UI Methods

    /// <summary>
    /// Displays the specified alert message to the player.
    /// </summary>
    /// <param name="message"></param>
    private void ShowAlert(string message)
    {
        Alert.Show(message);
    }

    #endregion
}