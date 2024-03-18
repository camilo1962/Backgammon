using UnityEngine;
using UnityEngine.UI;

public class SelectionMenu : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// The selected color hue.
    /// </summary>
    public Color SelectedColor;

    /// <summary>
    /// The unselected color hue.
    /// </summary>
    public Color UnselectedColor;

    #endregion

    #region Private Variables

    /// <summary>
    /// All game mode buttons.
    /// </summary>
    private Button[] m_Buttons;

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Use this for initialization.
    /// </summary>
    void Start()
    {
        m_Buttons = GetComponentsInChildren<Button>();

        for (int i = 0; i < m_Buttons.Length; i++)
        {
            Button button = m_Buttons[i];
            button.onClick.AddListener(() => Select(button));
        }

        if (m_Buttons.Length > 0)
        {
            Select(m_Buttons[0]);
        }
    }

    /// <summary>
    /// Update is called once per frame.
    /// </summary>
    void Update()
    {
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Handles the selection of the specified button.
    /// </summary>
    /// <param name="selectedButton"></param>
    public void Select(Button selectedButton)
    {
        foreach (Button button in m_Buttons)
        {
            button.GetComponent<Image>().color = UnselectedColor;
        }

        selectedButton.GetComponent<Image>().color = SelectedColor;
    }

    #endregion
}
