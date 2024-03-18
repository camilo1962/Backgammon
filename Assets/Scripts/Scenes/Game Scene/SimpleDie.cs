using UnityEngine;

public class SimpleDie : MonoBehaviour, IDie
{
    #region Public Properties

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
            transform.rotation = Quaternion.Euler(GetEuler(value));
        }
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Gets the euler angle corresponding to the specified die number.
    /// </summary>
    private Vector3 GetEuler(int diceNumber)
    {
        switch (diceNumber)
        {
            case (1): { return new Vector3(0f, 0f, 0f); }
            case (2): { return new Vector3(-90f, 0f, 90f); }
            case (3): { return new Vector3(0f, 180f, -90f); }
            case (4): { return new Vector3(0f, 0f, 90f); }
            case (5): { return new Vector3(180f, 0f, 0f); }
            case (6): { return new Vector3(90f, 0f, 0f); }
            default: { return new Vector3(0f, 0f, 0f); }
        }
    }

    #endregion
}