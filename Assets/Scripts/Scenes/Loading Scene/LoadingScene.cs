using System.Collections;
using UnityEngine;

/// <summary>
/// The loading scene.
/// </summary>
public class LoadingScene : MonoBehaviour
{
    #region MonoBehaviour

    IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        GameManager.Instance.LoadMainMenuScene();
    }

    #endregion
}
