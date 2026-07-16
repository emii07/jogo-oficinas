using UnityEngine;
using UnityEngine.SceneManagement; // Importante para carregar a cena do mapa!

public class NPCInteracao : MonoBehaviour
{
    [Header("Configuração de Viagem")]
    [Tooltip("Nome exato da cena para onde o jogador vai ao clicar no botão.")]
    public string nomeDaCenaDoMapa = "mapa do jogo"; 

    [Header("Interface do Diálogo (UI)")]
    [Tooltip("Arraste aqui o seu 'Paneldefala' que contém o texto e o botão de viajar.")]
    public GameObject painelDeViagem; 

    private bool playerEstaPerto = false;

    private void Start()
    {
        // Garante que o painel de diálogo comece desativado quando o jogo iniciar
        if (painelDeViagem != null)
        {
            painelDeViagem.SetActive(false);
        }
    }

    // Detecta quando o jogador entra na área de colisão (Trigger) do NPC
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            playerEstaPerto = true;
            Debug.Log("Player chegou perto do NPC! Abrindo painel de diálogo.");
            
            if (painelDeViagem != null)
            {
                painelDeViagem.SetActive(true);
            }
        }
    }

    // Detecta quando o jogador sai da área de colisão do NPC
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            playerEstaPerto = false;
            Debug.Log("Player se afastou do NPC! Fechando painel de diálogo.");
            
            if (painelDeViagem != null)
            {
                painelDeViagem.SetActive(false);
            }
        }
    }

    // Função que seu botão "viajar" vai chamar ao ser clicado
    public void IrParaOMapa()
    {
        // Segurança extra: Só viaja se o player estiver perto do NPC e a cena estiver configurada
        if (playerEstaPerto && !string.IsNullOrEmpty(nomeDaCenaDoMapa))
        {
            SceneManager.LoadScene(nomeDaCenaDoMapa);
        }
        else
        {
            Debug.LogWarning("Não foi possível viajar. O jogador está longe ou o nome da cena está em branco!");
        }
    }
}