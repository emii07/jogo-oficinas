using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Tempos")]
    public float bootTime = 2f;
    public float splashTime = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        // Boot
        SceneManager.LoadScene("Boot");
        yield return new WaitForSeconds(bootTime);

        // Splash
        SceneManager.LoadScene("Splash");
        yield return new WaitForSeconds(splashTime);

        // Menu
        SceneManager.LoadScene("MenuPrincipal");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}