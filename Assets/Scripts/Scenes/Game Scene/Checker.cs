using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Checker : MonoBehaviour
{
    #region Variables

    /// <summary>
    /// The point the checker is in.
    /// </summary>
    private Point m_Point;

    /// <summary>
    /// Whether the checker is currently being dragged by the user.
    /// </summary>
    private bool m_IsDragging;

    /// <summary>
    /// The position the checker is being dragged to.
    /// </summary>
    private Vector3 m_DragPosition;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the point the checker is in.
    /// </summary>
    public Point Point
    {
        get
        {
            return m_Point;
        }
        set
        {
            m_Point = value;
            m_IsDragging = false;
        }
    }

    /// <summary>
    /// Gets or sets the owner of the checker.
    /// </summary>
    public Player Owner
    {
        get;
        set;
    }

    #endregion

    #region MonoBehaviour Method

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_IsDragging)
        {
            Vector3 position = Vector3.Lerp(transform.position, m_DragPosition, 0.25f);
            transform.position = position;
            GetComponent<Mover>().PreviousPosition = position;
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Moves the checker towards a point in space above the specified position.
    /// </summary>
    public void Drag(Vector3 position)
    {
        m_IsDragging = true;
        m_DragPosition = position;
    }

    #endregion
}
