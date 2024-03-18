using UnityEngine;

[RequireComponent(typeof(Animator))]
public class MainMenuSceneLights : MonoBehaviour
{
    public bool IsPlay
    {
        get
        {
            return GetComponent<Animator>().GetBool("IsPlay");
        }
        set
        {
            GetComponent<Animator>().SetBool("IsPlay", value);
        }
    }
}