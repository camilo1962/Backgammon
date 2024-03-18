using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mover : MonoBehaviour
{
    #region Public Variables

    /// <summary>
    /// Determines how the game object will be traverse between two nodes.
    /// </summary>
    public TranslationMode Mode = TranslationMode.Lerp;

    /// <summary>
    /// The speed at which the game object moves.
    /// </summary>
    public float Speed = 1f;

    #endregion

    #region Private Variables

    /// <summary>
    /// Queue of nodes.
    /// </summary>
    private Queue<Node> m_Nodes = new Queue<Node>();

    /// <summary>
    /// The current transition between nodes.
    /// </summary>
    private float m_Transition;

    /// <summary>
    /// Whether the mover has completed moving.
    /// </summary>
    private bool m_IsCompleted;

    #endregion

    #region Properties

    /// <summary>
    /// Gets or sets the previous position.
    /// </summary>
    public Vector3 PreviousPosition
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the previous rotation.
    /// </summary>
    public Quaternion PreviousRotation
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the target position.
    /// </summary>
    public Vector3 TargetPosition
    {
        get;
        set;
    }

    /// <summary>
    /// Gets or sets the target rotation.
    /// </summary>
    public Quaternion TargetRotation
    {
        get;
        set;
    }

    #endregion

    #region MonoBehaviour methods

    private void Start()
    {
        PreviousPosition = TargetPosition = transform.position;
        PreviousRotation = TargetRotation = transform.rotation;
        m_IsCompleted = true;
        m_Transition = 1;
    }

    private void Update()
    {
        if (!m_IsCompleted)
        {
            m_Transition += Time.deltaTime * Speed;

            if (m_Transition > 1)
            {
                m_Transition = 1;
                PreviousPosition = TargetPosition;
                PreviousRotation = TargetRotation;

                if (m_Nodes.Count() > 0)
                {
                    Load(m_Nodes.Dequeue());
                }
                else
                {
                    m_IsCompleted = true;
                }
            }

            transform.position = GetNextPosition();
            transform.rotation = GetNextRotation();
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Moves through a series of nodes.
    /// </summary>
    /// <param name="nodes"></param>
    public void MoveThrough(params Node[] nodes)
    {
        if (nodes.Length == 0)
        {
            return;
        }

        m_Nodes.Clear();

        foreach (Node node in nodes)
        {
            m_Nodes.Enqueue(node);
        }

        Load(m_Nodes.Dequeue());
    }

    /// <summary>
    /// Moves to the specified node.
    /// </summary>
    /// <param name="node"></param>
    public void MoveTo(Node node)
    {
        m_Nodes.Clear();

        Load(node);
    }

    /// <summary>
    ///Se mueve al nodo especificado instantáneamente.
    /// </summary>
    /// <param name="node"></param>
    public void MoveToInstantly(Node node)
    {
        m_Nodes.Clear();

        transform.position = PreviousPosition = TargetPosition = node.Position;
        transform.rotation = PreviousRotation = TargetRotation = node.Rotation;
        m_IsCompleted = true;
        m_Transition = 1;
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Loads data from the specified node.
    /// </summary>
    /// <param name="node"></param>
    private void Load(Node node)
    {
        TargetPosition = node.Position;
        TargetRotation = node.Rotation;
        m_IsCompleted = false;
        m_Transition = 0;
    }

    /// <summary>
    /// Gets the next position.
    /// </summary>
    /// <returns></returns>
    private Vector3 GetNextPosition()
    {
        switch (Mode)
        {
            default:
            case (TranslationMode.Lerp): { return Vector3.Lerp(PreviousPosition, TargetPosition, m_Transition); }
            case (TranslationMode.Slerp): { return Vector3.Slerp(PreviousPosition, TargetPosition, m_Transition); }
        }
    }

    /// <summary>
    /// Gets the next rotation.
    /// </summary>
    /// <returns></returns>
    private Quaternion GetNextRotation()
    {
        return Quaternion.Lerp(PreviousRotation, TargetRotation, m_Transition);
    }

    #endregion
}