using UnityEngine;

public class MainMenuSceneCamera : MonoBehaviour
{
    public static class Placements
    {
        public const int MainMenu = 0;
        public const int NewGameMenu = 1;
        public const int Play = 2;
    }

    /// <summary>
    /// The camera's animator.
    /// </summary>
    private Animator m_Animator;

    public virtual int Placement
    {
        get
        {
            return m_Animator.GetInteger("Placement");
        }
        set
        {
            m_Animator.SetInteger("Placement", value);
        }
    }

    #region MonoBehaviour Methods

    public void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }

    #endregion
}
