using UnityEngine;
using UnityEngine.SceneManagement; // Importante para carregar a cena do mapa!

public class NPCInteracao : MonoBehaviour
{
    [Header("Configuração de Viagem")]
    [Tooltip("Nome exato da cena para onde o jogador vai ao clicar no botão.")]
    public string nomeDaCenaDoMapa = "mapa do jogo";

    [Header("Interface do Diálogo (UI)")]
    [Tooltip("Arraste aqui o seu 'Paneldefala' que contém o texto e os botões.")]
    public GameObject painelDeViagem;

    private bool playerEstaPerto = false;

    private void Start()
    {
        // Garante que o painel comece escondido
        if (painelDeViagem != null)
        {
            painelDeViagem.SetActive(false);
        }
    }

    // Quando o jogador entra na área do NPC
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEstaPerto = true;

            if (painelDeViagem != null)
            {
                painelDeViagem.SetActive(true);
            }
        }
    }

    // Quando o jogador sai da área do NPC
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEstaPerto = false;

            if (painelDeViagem != null)
            {
                painelDeViagem.SetActive(false);
            }
        }
    }

    // Botão Viajar
    public void IrParaOMapa()
    {
        if (playerEstaPerto && !string.IsNullOrEmpty(nomeDaCenaDoMapa))
        {
            SceneManager.LoadScene(nomeDaCenaDoMapa);
        }
        else
        {
            Debug.LogWarning("Não foi possível viajar.");
        }
    }

    // Botão Voltar/Fechar
    public void FecharDialogo()
    {
        if (painelDeViagem != null)
        {
            painelDeViagem.SetActive(false);
        }
    }
}