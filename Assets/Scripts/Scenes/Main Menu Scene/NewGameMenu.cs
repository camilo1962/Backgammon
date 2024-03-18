using System;
using UnityEngine;
using UnityEngine.UI;

public class NewGameMenu : Popup
{
    #region Variables

    /// <summary>
    /// The text displaying the player 1 name.
    /// </summary>
    public Text Player1Name;

    /// <summary>
    /// The text displaying the player 2 name.
    /// </summary>
    public Text Player2Name;

    /// <summary>
    /// The text displaying the match score.
    /// </summary>
    public Text MatchScoreText;

    /// <summary>
    /// The text displaying the initial stakes.
    /// </summary>
    public Text InitialStakesText;

    /// <summary>
    /// Option button toggling whether the Crawford rule is enabled.
    /// </summary>
    public OptionButton CrawfordRule;

    /// <summary>
    /// Option button toggling whether the Murphy rule is enabled.
    /// </summary>
    public OptionButton MurphyRule;

    #endregion

    #region MonoBehaviour Methods

    protected override void Awake()
    {
        base.Awake();

        if (CrawfordRule != null)
        {
            CrawfordRule.GetComponent<Button>().onClick.AddListener(OnCrawfordRuleButtonClick);
        }

        if (MurphyRule != null)
        {
            MurphyRule.GetComponent<Button>().onClick.AddListener(OnMurphyRuleButtonClick);
        }
    }

    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles the click event for the Crawford rule button.
    /// </summary>
    private void OnCrawfordRuleButtonClick()
    {
        GameManager.Instance.IsCrawfordRuleEnabled = !GameManager.Instance.IsCrawfordRuleEnabled;
    }

    /// <summary>
    /// Handles the click event for the Murphy rule button.
    /// </summary>
    private void OnMurphyRuleButtonClick()
    {
        GameManager.Instance.IsMurphyRuleEnabled = !GameManager.Instance.IsMurphyRuleEnabled;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Increases the match score.
    /// </summary>
    public void IncreaseMatchScore()
    {
        if (GameManager.Instance.MatchScore < 30)
        {
            GameManager.Instance.MatchScore++;
            MatchScoreText.text = GameManager.Instance.MatchScore.ToString();
        }
    }

    /// <summary>
    /// Decreases the match score.
    /// </summary>
    public void DecreaseMatchScore()
    {
        if (GameManager.Instance.MatchScore > 1)
        {
            GameManager.Instance.MatchScore--;
            MatchScoreText.text = GameManager.Instance.MatchScore.ToString();
        }
    }

    /// <summary>
    /// Increases the game stakes.
    /// </summary>
    public void IncreaseStakes()
    {
        if (GameManager.Instance.InitialStakes < 100)
        {
            GameManager.Instance.InitialStakes++;
            InitialStakesText.text = GameManager.Instance.InitialStakes.ToString();
        }
    }

    /// <summary>
    /// Decreases the game stakes.
    /// </summary>
    public void DecreaseStakes()
    {
        if (GameManager.Instance.InitialStakes > 1)
        {
            GameManager.Instance.InitialStakes--;
            InitialStakesText.text = GameManager.Instance.InitialStakes.ToString();
        }
    }

    public void Play()
    {
        GetComponent<Animator>().SetTrigger("Play");
    }

    #endregion

    #region Protected Methods

    /// <summary>
    /// Handles any operation that occurs when opening the popup.
    /// </summary>
    protected override void OnOpen()
    {
        if (Player1Name != null)
        {
            Player1Name.text = GameManager.Instance.Player1Name;
        }

        if (Player2Name != null)
        {
            Player2Name.text = GameManager.Instance.Player2Name;
        }

        if (MatchScoreText != null)
        {
            MatchScoreText.text = GameManager.Instance.MatchScore.ToString();
        }

        if (InitialStakesText != null)
        {
            InitialStakesText.text = GameManager.Instance.InitialStakes.ToString();
        }

        if (CrawfordRule != null)
        {
            CrawfordRule.IsOptionEnabled = GameManager.Instance.IsCrawfordRuleEnabled;
        }

        if (MurphyRule != null)
        {
            MurphyRule.IsOptionEnabled = GameManager.Instance.IsMurphyRuleEnabled;
        }

        base.OnOpen();
    }

    #endregion
}