using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject painelMenu;
    [SerializeField] private GameObject painelConfiguracoes;

    public void IniciarJogo()
    {
        GameManager.Instancia.CarregarCena("Jogo");
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
        GameManager.Instancia.SairJogo();
    }
}