using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteracao : MonoBehaviour
{
    [Header("Configuração de Cenas")]
    public string nomeDaCenaDoMapa = "Jogo"; 

    [Header("Interface de Viagem (UI)")]
    public GameObject painelDeViagem; 

    // Detecta quando o jogador entra na área 2D do NPC
    private void OnTriggerEnter2D(Collider2D other) 
    {
        // Verifica se quem entrou na área foi o Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player colidiu com o Guardião 2D! Abrindo painel de diálogo.");
            
            // Ativa o painel de diálogo IMEDIATAMENTE ao colidir
            if (painelDeViagem != null)
            {
                painelDeViagem.SetActive(true);
            }
            else
            {
                Debug.LogError("ERRO: Você esqueceu de arrastar o 'PainelDeFala' para o campo 'Painel De Viagem' no Inspector do Guardião!");
            }
        }
    }

    // Detecta quando o jogador se afasta do NPC 2D
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player se afastou do Guardião 2D. Fechando painel.");
            
            // Esconde o painel automaticamente ao se afastar
            if (painelDeViagem != null)
            {
                painelDeViagem.SetActive(false);
            }
        }
    }

    // Função que será chamada pelo clique do botão da UI para ir ao mapa
    public void IrParaOMapa()
    {
        SceneManager.LoadScene(nomeDaCenaDoMapa);
    }
}