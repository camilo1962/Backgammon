using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    #region Static Variables
    
    /// <summary>
    /// The conversion ratio for converting radians to degrees.
    /// </summary>
    private static float RadToDegree = Mathf.PI / 180;

    #endregion

    #region Public Variables

    /// <summary>
    /// The start position.
    /// </summary>
    public Vector3 StartPosition;

    /// <summary>
    /// The oscillation amplitude.
    /// </summary>
    public float Amplitude = 1f;

    /// <summary>
    /// The oscillation speed.
    /// </summary>
    public float Speed = 200f;

    /// <summary>
    /// The start angle.
    /// </summary>
    public float StartAngle = 0f;

    /// <summary>
    /// The current angle.
    /// </summary>
    public float CurrentAngle = 0f;

    /// <summary>
    /// Whether the object is suspended from floating.
    /// </summary>
    public bool Paused = false;

    #endregion
  
    #region MonoBehaviour Methods

    // Use this for initialization
    void Start()
    {
        StartPosition = transform.position;
        CurrentAngle = StartAngle;
     
    }

    // Update is called once per frame
    void Update()
    {
        if (Paused)
        {
            return;
        }

        CurrentAngle += Speed * Time.deltaTime;

        if (CurrentAngle > 360)
        {
            CurrentAngle -= 360;
        }

        float height = Amplitude * Mathf.Sin(CurrentAngle * RadToDegree);

        transform.position = new Vector3(StartPosition.x, StartPosition.y + height, StartPosition.z);
    }

    #endregion;
}
