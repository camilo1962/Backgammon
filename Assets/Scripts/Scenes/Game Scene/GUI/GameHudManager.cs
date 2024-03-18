using UnityEngine;

public class GameHudManager : MonoBehaviour
{
    #region Public Variables

    public MatchPlayHud MatchPlayHud;
    public MoneyPlayHud MoneyPlayHud;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        switch (GameManager.Instance.BackgammonPlayMode)
        {
            case (BackgammonPlayMode.Match):
                {
                    MatchPlayHud.gameObject.SetActive(true);
                    MoneyPlayHud.gameObject.SetActive(false);
                }
                break;
            case (BackgammonPlayMode.Money):
                {
                    MatchPlayHud.gameObject.SetActive(false);
                    MoneyPlayHud.gameObject.SetActive(true);
                }
                break;
        }
    }
    
    #endregion
}
