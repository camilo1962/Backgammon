using System;
using UnityEngine;

/// <summary>
/// Doubling cube.
/// </summary>
[RequireComponent(typeof(Mover))]
public class DoublingCube : MonoBehaviour
{
    #region Events

    /// <summary>
    /// Event that is raised whenever the offering of a doubling cube is being previewed.
    /// </summary>
    public EventHandler OfferPreviewed;

    /// <summary>
    /// Event that is raised whenever the offering of a doubling cube has been canceled.
    /// </summary>
    public EventHandler OfferCanceled;

    /// <summary>
    /// Event that is raised whenever the doubling cube has been offered.
    /// </summary>
    public EventHandler Offered;

    /// <summary>
    /// Event that is raised whenever the doubling cube has been accepted.
    /// </summary>
    public EventHandler Accepted;

    /// <summary>
    /// Event that is raised whenever the doubling cube has been declined.
    /// </summary>
    public EventHandler Declined;

    #endregion

    #region Public Variables

    /// <summary>
    /// The doubling cube's centered position.
    /// </summary>
    public Vector3 CenteredPosition = new Vector3(0f, 0f, 0f);

    /// <summary>
    /// The doubling cube's resting position when Player 1 is in control.
    /// </summary>
    public Vector3 Player1Position = new Vector3(0f, 0f, 0f);

    /// <summary>
    /// The doubling cube's resting position when Player 2 is in control.
    /// </summary>
    public Vector3 Player2Position = new Vector3(0f, 0f, 0f);

    /// <summary>
    /// The doubling cube's offering position.
    /// </summary>
    public Vector3 OfferingPosition = new Vector3(0f, 0f, 0f);

    #endregion

    #region Variables

    /// <summary>
    /// The value of the doubling cube.
    /// </summary>
    private int m_Value = 1;

    /// <summary>
    /// The player currently in control of the doubling cube.
    /// </summary>
    private Player m_Owner = null;

    /// <summary>
    /// The player being offered the doubling cube.
    /// </summary>
    private Player m_OfferedPlayer = null;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the value of the doubling cube.
    /// </summary>
    public int Value
    {
        get
        {
            return m_Value;
        }
    }

    /// <summary>
    /// Gets the player currently in control of the doubling cube.
    /// </summary>
    public Player Owner
    {
        get
        {
            return m_Owner;
        }
    }

    /// <summary>
    /// Gets the player being offered the doubling cube.
    /// </summary>
    public Player OfferedPlayer
    {
        get
        {
            return m_OfferedPlayer;
        }
    }

    /// <summary>
    /// Gets whether the doubling cube is being offered.
    /// </summary>
    public bool IsBeingOffered
    {
        get
        {
            return m_OfferedPlayer != false;
        }
    }

    #endregion

    #region MonoBehaviour Methods

    // Use this for initialization
    void Start()
    {
        m_Value = 1;
        GetComponent<Mover>().MoveToInstantly(new Node(CenteredPosition, GetRotation(m_Value)));
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Previews the offering of the doubling cube to the specified player.
    /// </summary>
    /// <param name="player"></param>
    public void OfferPreview(Player player)
    {
        if (m_OfferedPlayer == null)
        {
            m_OfferedPlayer = player;
            GetComponent<Mover>().MoveTo(new Node(OfferingPosition, GetRotation(m_Value * 2)));
            EventHelper.Raise(this, OfferPreviewed);
        }
    }

    /// <summary>
    /// Offers the doubling cube the previewed player.
    /// </summary>
    public void Offer()
    {
        if (m_OfferedPlayer != null)
        {
            EventHelper.Raise(this, Offered);
        }
    }

    /// <summary>
    /// Cancels the offering of the doubling cube. Does nothing if the doubling cube is not being offerred. 
    /// </summary>
    public void CancelOffer()
    {
        if (m_OfferedPlayer != null)
        {
            m_OfferedPlayer = null;

            if (m_Owner == null)
            {
                GetComponent<Mover>().MoveTo(new Node(CenteredPosition, GetRotation(m_Value)));
            }
            else
            {
                switch (m_Owner.ID)
                {
                    case (BGPlayerID.Player1): { GetComponent<Mover>().MoveTo(new Node(Player1Position, GetRotation(m_Value))); } break;
                    case (BGPlayerID.Player2): { GetComponent<Mover>().MoveTo(new Node(Player2Position, GetRotation(m_Value))); } break;
                }
            }

            EventHelper.Raise(this, OfferCanceled);
        }
    }

    /// <summary>
    /// Accepts the doubling cube.
    /// </summary>
    public void Accept()
    {
        if (m_OfferedPlayer != null)
        {
            m_Value *= 2;
            m_Owner = m_OfferedPlayer;
            m_OfferedPlayer = null;

            switch (m_Owner.ID)
            {
                case (BGPlayerID.Player1): { GetComponent<Mover>().MoveTo(new Node(Player1Position, GetRotation(m_Value))); } break;
                case (BGPlayerID.Player2): { GetComponent<Mover>().MoveTo(new Node(Player2Position, GetRotation(m_Value))); } break;
            }

            EventHelper.Raise(this, Accepted);
        }
    }

    /// <summary>
    /// Declines the doubling cube.
    /// </summary>
    public void Decline()
    {
        if (m_OfferedPlayer != null)
        {
            m_Value = 1;
            m_Owner = null;
            m_OfferedPlayer = null;

            GetComponent<Mover>().MoveTo(new Node(CenteredPosition, GetRotation(m_Value)));
            EventHelper.Raise(this, Declined);
        }
    }

    /// <summary>
    /// Resets the doubling cube.
    /// </summary>
    public void Reset()
    {
        m_Value = 1;
        m_Owner = null;
        m_OfferedPlayer = null;

        GetComponent<Mover>().MoveToInstantly(new Node(CenteredPosition, GetRotation(m_Value)));
    }

    #endregion

    #region Private Methods

    /// <summary>
    /// Doubles the doubling cube value.
    /// </summary>
    private void Double()
    {
        if (m_Value < 64)
        {
            m_Value *= 2;
        }
    }

    /// <summary>
    /// Gets the rotation of the doubling cube with its value facing up.
    /// </summary>
    private Quaternion GetRotation(int value)
    {
        switch (value)
        {
            case (2):
                {
                    return Quaternion.Euler(new Vector3(0f, 0f, 0f));
                }
            case (4):
                {
                    return Quaternion.Euler(new Vector3(0f, 0f, 90f));
                }
            case (8):
                {
                    return Quaternion.Euler(new Vector3(-90f, 0f, 90f));
                }
            case (16):
                {
                    return Quaternion.Euler(new Vector3(90f, 0f, 90f));
                }
            case (32):
                {
                    return Quaternion.Euler(new Vector3(180f, 0f, 0f));
                }
            case (1):
            case (64):
            default:
                {
                    return Quaternion.Euler(new Vector3(0f, 180f, -90f));
                }
        }
    }

    #endregion
}
