using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Die : MonoBehaviour, IDie
{
    #region Enums

    /// <summary>
    /// State of the die.
    /// </summary>
    private enum State
    {
        Presenting,
        Rolling,
        Dropping
    }

    #endregion

    #region Public Variables

    /// <summary>
    /// The presenting position of the die.
    /// </summary>
    public Vector3 m_PresentingPosition;

    /// <summary>
    /// The rolling position of the die.
    /// </summary>
    public Vector3 m_RollingPosition;

    /// <summary>
    /// The move speed of the die.
    /// </summary>
    public float MoveSpeed = 0.1f;

    #endregion

    #region Private Variables

    /// <summary>
    /// The current state of the die.
    /// </summary>
    private State m_State;

    #endregion

    #region Properties

    

    /// <summary>
    /// Gets whether the die is resting.
    /// </summary>
    public bool IsResting
    {
        get
        {
            return m_State == State.Presenting;
        }
    }

    /// <summary>
    /// Gets whether the die is rolling.
    /// </summary>
    public bool IsRolling
    {
        get
        {
            return m_State == State.Rolling;
        }
    }

    /// <summary>
    /// Gets or sets the die number.
    /// </summary>
    public int Number
    {
        get
        {
            float threshold = 0.99f;

            float dotUp = Vector3.Dot(transform.up, Vector3.up);

            if (dotUp >= threshold)
            {
                return 1;
            }
            else if (dotUp <= -(threshold))
            {
                return 5;
            }

            float dotFoward = Vector3.Dot(transform.forward, Vector3.up);

            if (dotFoward >= threshold)
            {
                return 2;
            }
            else if (dotFoward <= -(threshold))
            {
                return 6;
            }

            float dotRight = Vector3.Dot(transform.right, Vector3.up);

            if (dotRight >= threshold)
            {
                return 4;
            }
            else if (dotRight <= -(threshold))
            {
                return 3;
            }

            return 0;
        }
        set
        {
            transform.rotation = Quaternion.Euler(GetEulerAngle(value));
        }
    }

    #endregion

    #region MonoBehaviour Methods
    
    // Use this for initialization
    void Start()
    {
        Present();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTransform();
    }

    #endregion

    #region Public Method

    /// <summary>
    /// Returns the die to its resting position.
    /// </summary>
    public void Present()
    {
        GetComponent<FloatingObject>().CurrentAngle = 0;
        GetComponent<FloatingObject>().Paused = false;
        GetComponent<Rigidbody>().useGravity = false;
        transform.position = m_PresentingPosition;

        m_State = State.Presenting;

        gameObject.SetActive(true);
    }

    /// <summary>
    /// Rolls the die.
    /// </summary>
    public void Roll()
    {
        GetComponent<FloatingObject>().Paused = true;
        GetComponent<Rigidbody>().useGravity = false;
        //Invoke("Sonar", 0.5f);
        m_State = State.Rolling;
    }
    
    /// <summary>
    /// Drops the die.
    /// </summary>
    public void Drop()
    {
        GetComponent<Rigidbody>().useGravity = true;

        m_State = State.Dropping;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Updates the die's transform.
    /// </summary>
    private void UpdateTransform()
    {
        if (m_State == State.Presenting)
        {
            
            GetComponent<Rigidbody>().position = Vector3.Lerp(transform.position, m_PresentingPosition, MoveSpeed);
        }
        else if (m_State == State.Rolling)
        {
            
            GetComponent<Rigidbody>().position = Vector3.Lerp(transform.position, m_RollingPosition, MoveSpeed);
            GetComponent<Rigidbody>().rotation = Random.rotation;
            

        }
    }

    /// <summary>
    /// Gets the euler angle corresponding to the specified dice number.
    /// </summary>
    private Vector3 GetEulerAngle(int diceNumber)
    {
        switch (diceNumber)
        {
            case (1):
                {
                    return new Vector3(0f, 0f, 0f);
                }
            case (2):
                {
                    return new Vector3(-90f, 0f, 90f);
                }
            case (3):
                {
                    return new Vector3(0f, 180f, -90f);
                }
            case (4):
                {
                    return new Vector3(0f, 0f, 90f);
                }
            case (5):
                {
                    return new Vector3(180f, 0f, 0f);
                }
            case (6):
                {
                    return new Vector3(90f, 0f, 0f);
                }
            default:
                {
                    return new Vector3(0f, 0f, 0f);
                }
        }
    }

    #endregion
}

