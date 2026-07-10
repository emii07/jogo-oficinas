using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void Jogar()
    {
        Debug.Log("▶ Iniciando o jogo...");
        SceneManager.LoadScene("Jogo");
    }

    public void Sair()
    {
        Debug.Log("❌ Saindo do jogo...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}