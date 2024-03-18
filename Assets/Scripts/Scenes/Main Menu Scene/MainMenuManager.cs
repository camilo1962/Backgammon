using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    #region Singleton Property

    /// <summary>
    /// Singleton access.
    /// </summary>
    public static MainMenuManager Instance
    {
        get;
        private set;
    }

    #endregion

    #region Public Variables

    /// <summary>
    /// The current menu.
    /// </summary>
    public Popup CurrentMenu;

    /// <summary>
    /// The main menu scene camera.
    /// </summary>
    public MainMenuSceneCamera Camera;

    /// <summary>
    /// The main menu scene lights.
    /// </summary>
    public MainMenuSceneLights Lights;

    /// <summary>
    /// The backgammon board preview.
    /// </summary>
    public BoardPreview BoardPreview;

    /// <summary>
    /// The exit game popup.
    /// </summary>
    public Popup ExitGamePopup;

    #endregion

    #region Private Variables

    /// <summary>
    /// Previous menus.
    /// </summary>
    private Stack<Popup> m_PreviousMenus = new Stack<Popup>();

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Used to initialize any variables or game state before the game starts.
    /// </summary>
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowMenu(CurrentMenu);
    }

    /// <summary>
    /// Called once per frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitGamePopup.Toggle();
        }
    }


    #endregion

    #region Public Methods

    /// <summary>
    /// Displays the specified menu.
    /// </summary>
    /// <param name="menu"></param>
    public void ShowMenu(Popup menu)
    {
        if (menu == null)
        {
            return;
        }

        if (CurrentMenu != null)
        {
            CurrentMenu.IsOpen = false;
            m_PreviousMenus.Push(CurrentMenu);
        }

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
        UpdateCameraPosition();
    }

    /// <summary>
    /// Return to the previous menu.
    /// </summary>
    public void GoBack()
    {
        if (m_PreviousMenus.Count > 0)
        {
            if (CurrentMenu != null)
            {
                CurrentMenu.IsOpen = false;
            }

            CurrentMenu = m_PreviousMenus.Pop();
            CurrentMenu.IsOpen = true;
            UpdateCameraPosition();
        }
    }

    /// <summary>
    /// Starts a new game with the current selected settings.
    /// </summary>
    public void Play()
    {
        Invoke("LoadGameScene", 1f);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Loads the game scene.
    /// </summary>
    private void LoadGameScene()
    {
        GameManager.Instance.LoadGameScene();
    }

    private void UpdateCameraPosition()
    {
        switch (CurrentMenu.gameObject.name)
        {
            default:
            case ("Main Menu"):
                {
                    Camera.Placement = MainMenuSceneCamera.Placements.MainMenu;
                    Lights.IsPlay = false;
                }
                break;
            case ("Match Play"):
            case ("Money Play"):
            case ("Match Play Menu"):
            case ("Money Play Menu"):
                {
                    Camera.Placement = MainMenuSceneCamera.Placements.Play;
                    Lights.IsPlay = true;
                }
                break;
        }
    }

    #endregion
}