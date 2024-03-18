using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(BoxCollider))]
public class Point : MonoBehaviour
{
    #region Nested Enums

    /// <summary>
    /// Stack direction.
    /// </summary>
    private enum StackDirection
    {
        Foward,
        Backward
    }

    /// <summary>
    /// Checker orientation.
    /// </summary>
    private enum CheckerOrientation
    {
        Horizontal,
        Vertical
    }

    /// <summary>
    /// Floor.
    /// </summary>
    private enum Floor
    {
        Top,
        Bottom
    }

    #endregion

    #region Variables

    /// <summary>
    /// The point ID.
    /// </summary>
    [SerializeField]
    private BGPointID m_ID = BGPointID.Point1;

    /// <summary>
    /// The height of the checkers in this point.
    /// </summary>
    [SerializeField]
    private float m_CheckerHeight = 0.015f;

    /// <summary>
    /// The diameter of the checker in this point
    /// </summary>
    [SerializeField]
    private float m_CheckerDiameter = 0.06f;

    /// <summary>
    /// The number of stacks in this point.
    /// </summary>
    [SerializeField]
    private int m_Stacks = 1;

    /// <summary>
    /// The direction in which stacks are laid.
    /// </summary>
    [SerializeField]
    private StackDirection m_StackDirection = StackDirection.Foward;

    /// <summary>
    /// The orientation of the checkers.
    /// </summary>
    [SerializeField]
    private CheckerOrientation m_CheckerOrientation = CheckerOrientation.Horizontal;

    /// <summary>
    /// Whether the checkers will be placed starting at the bottom or the top of the box collider.
    /// </summary>
    [SerializeField]
    private Floor m_Floor = Floor.Bottom;

    /// <summary>
    /// The checkers in this point.
    /// </summary>
    private Stack<Checker> m_Checkers = new Stack<Checker>();

    /// <summary>
    /// The position of the first checker.
    /// </summary>
    private Vector3 m_Start = new Vector3(0f, 0f, 0f);

    /// <summary>
    /// The distance between each checker.
    /// </summary>
    private Vector3 m_Increment = new Vector3(0f, 0f, 0f);

    #endregion

    #region Properties

    /// <summary>
    /// Gets the point ID.
    /// </summary>
    public BGPointID ID
    {
        get
        {
            return m_ID;
        }
    }

    /// <summary>
    /// Gets the number of checkers in the point.
    /// </summary>
    public int Count
    {
        get
        {
            return m_Checkers.Count;
        }
    }

    /// <summary>
    /// Gets the last checker added.
    /// </summary>
    public Checker CurrentChecker
    {
        get
        {
            return m_Checkers.FirstOrDefault();
        }
    }

    /// <summary>
    /// Gets the checker rotation.
    /// </summary>
    private Quaternion CheckerRotation
    {
        get
        {
            switch (m_CheckerOrientation)
            {
                case CheckerOrientation.Horizontal:
                    {
                        return Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                case CheckerOrientation.Vertical:
                    {
                        return Quaternion.Euler(new Vector3(90, 0, 0));
                    }
                default:
                    {
                        return Quaternion.Euler(new Vector3(0, 0, 0));
                    }
            }
        }
    }

    #endregion

    #region MonoBehaviour Methods

    /// <summary>
    /// Used to initialize any variables or game state before the game starts.
    /// </summary>
    void Awake()
    {
        SetIncrements();
        SetStartPosition();
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Places the specified checker in this point.
    /// </summary>
    public void Place(Checker checker)
    {
        if (checker.Point == this)
        {
            checker.GetComponent<Mover>().MoveTo(new Node(GetNextPosition(), CheckerRotation));
        }
        else
        {
            checker.Point = this;

            Vector3 endPosition = GetNextPosition();
            Vector3 pos1 = endPosition;
            pos1.y = m_Start.y + 0.05f;

            Node node1 = new Node(pos1, checker.transform.rotation);
            Node node2 = new Node(endPosition, CheckerRotation);

            checker.GetComponent<Mover>().MoveThrough(node1, node2);
        }

        m_Checkers.Push(checker);
    }

    /// <summary>
    /// Places the specified checker in this point instantly.
    /// </summary>
    public void PlaceInstantly(Checker checker)
    {
        checker.Point = this;
        checker.GetComponent<Mover>().MoveToInstantly(new Node(GetNextPosition(), CheckerRotation));        
        m_Checkers.Push(checker);
    }

    /// <summary>
    /// Retrieves a checker from the specified point.
    /// </summary>
    /// <param name="point"></param>
    public void GetCheckerFrom(Point point)
    {
        Checker checker = point.PickUp();

        if (checker != null)
        {
            checker.Point = this;

            Vector3 endPosition = GetNextPosition();

            Vector3 pos1 = checker.transform.position;
            pos1.y = m_Start.y + 0.1f;

            Vector3 pos2 = endPosition;
            pos2.y = m_Start.y + 0.1f;

            Node node1 = new Node(pos1, checker.transform.rotation);
            Node node2 = new Node(pos2, checker.transform.rotation);
            Node node3 = new Node(endPosition, CheckerRotation);

            checker.GetComponent<Mover>().MoveThrough(node1, node2, node3);
            m_Checkers.Push(checker);
        }
    }

    /// <summary>
    /// Picks up the last checker in this point.
    /// </summary>
    public Checker PickUp()
    {
        Checker checker = (m_Checkers.Count > 0) ? m_Checkers.Pop() : null;

        if (checker != null)
        {
            Vector3 pos1 = checker.transform.position;
            pos1.y = m_Start.y + 0.05f;

            Node node1 = new Node(pos1, checker.transform.rotation);

            checker.GetComponent<Mover>().MoveTo(node1);
        }

        return checker;
    }

    /// <summary>
    /// Remove and returns all checkers from this point.
    /// </summary>
    public IEnumerable<Checker> Clear()
    {
        IEnumerable<Checker> removedCheckers = m_Checkers.Select(checker => checker);
        m_Checkers.Clear();
        return removedCheckers;
    }

    /// <summary>
    /// Gets the position in the point for the most recent checker added.
    /// </summary>
    public Vector3 GetCurrentPosition()
    {
        return (m_Checkers.Count > 0) ? GetPosition(m_Checkers.Count - 1) : GetPosition(0);
    }

    /// <summary>
    /// Gets the position for the next checker.
    /// </summary>
    public Vector3 GetNextPosition()
    {
        return GetPosition(m_Checkers.Count);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Sets the increments (the distance between each checker).
    /// </summary>
    private void SetIncrements()
    {
        switch (m_CheckerOrientation)
        {
            case (CheckerOrientation.Horizontal):
                {
                    m_Increment.y = m_CheckerHeight;
                    m_Increment.z = m_CheckerDiameter;
                }
                break;
            case (CheckerOrientation.Vertical):
                {
                    m_Increment.y = m_CheckerDiameter;
                    m_Increment.z = m_CheckerHeight;
                }
                break;
        }
    }

    /// <summary>
    /// Sets the start position (the position of the first checker).
    /// </summary>
    private void SetStartPosition()
    {
        BoxCollider boxCollider = GetComponent<BoxCollider>();
        Vector3[] vertices = BoxColliderHelper.GetVertices(boxCollider);

        m_Start.x = transform.position.x;

        switch (m_Floor)
        {
            case (Floor.Bottom):
                {
                    m_Start.y = Vector3Helper.GetMinY(vertices) + (m_Increment.y / 2);
                }
                break;
            case (Floor.Top):
                {
                    m_Start.y = Vector3Helper.GetMaxY(vertices) + (m_Increment.y / 2);
                }
                break;
        }

        switch (m_StackDirection)
        {
            case (StackDirection.Foward):
                {
                    m_Start.z = Vector3Helper.GetMinZ(vertices) + (m_Increment.z / 2);
                }
                break;
            case (StackDirection.Backward):
                {
                    m_Start.z = Vector3Helper.GetMaxZ(vertices) - (m_Increment.z / 2);
                }
                break;
        }
    }

    /// <summary>
    /// Gets the position of the ith checker.
    /// </summary>
    private Vector3 GetPosition(int i)
    {
        Vector3 position = m_Start;

        position.y += (i / m_Stacks) * m_Increment.y;

        switch (m_StackDirection)
        {
            case (StackDirection.Foward):
                {
                    position.z += (i % m_Stacks) * m_Increment.z;
                }
                break;
            case (StackDirection.Backward):
                {
                    position.z -= (i % m_Stacks) * m_Increment.z;
                }
                break;
        }

        return position;
    }

    #endregion
}
