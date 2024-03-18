using UnityEngine;

public struct Node
{
    #region Properties

    /// <summary>
    /// Gets the node position in world space.
    /// </summary>
    public Vector3 Position { get; private set; }

    /// <summary>
    /// Gets the node rotation in world space.
    /// </summary>
    public Quaternion Rotation { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class.
    /// </summary>
    /// <param name="position">Position.</param>
    /// <param name="rotation">Rotation.</param>
    public Node(Vector3 position, Quaternion rotation)
    {
        this.Position = position;
        this.Rotation = rotation;
    }

    #endregion
}