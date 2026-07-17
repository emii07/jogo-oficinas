using UnityEngine;
using UnityEngine.SceneManagement; // Importante: adicionado para carregar as cenas diretamente

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelConfiguracoes;

    public void IniciarJogo()
    {
        // Carrega a cena do jogo diretamente sem depender do GameManager
        SceneManager.LoadScene("Jogo");
    }

    public void AbrirConfiguracoes()
    {
        painelMenu.SetActive(false);
        painelConfiguracoes.SetActive(true);
    }

    public void VoltarMenu()
    {
        painelConfiguracoes.SetActive(false);
        painelMenu.SetActive(true);
    }

    public void Sair()
    {
        Debug.Log("Saindo do jogo...");
        
        // Fecha o aplicativo/jogo de verdade (funciona após a Build do jogo)
        Application.Quit();
    }
}