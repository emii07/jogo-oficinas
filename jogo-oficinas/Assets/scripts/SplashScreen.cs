using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private string nextScene = "Menu";

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nextScene);
    }
}