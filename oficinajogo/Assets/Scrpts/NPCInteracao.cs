using UnityEngine;
using UnityEngine.SceneManagement; // Importante: adicionado para gerenciar a troca de cenas diretamente

public class DialogoNPC : MonoBehaviour
{
    [SerializeField] private GameObject painelDialogo;
    [SerializeField] private string nomeDaProximaCena = "Jogo";

    private void Start()
    {
        // Garante que o painel comece fechado chamando a função correspondente
        FecharPainel();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AbrirPainel();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FecharPainel();
        }
    }

    // --- FUNÇÕES DE CONTROLE DA INTERFACE ---

    public void AbrirPainel()
    {
        if (painelDialogo != null)
        {
            painelDialogo.SetActive(true);
        }
    }

    public void FecharPainel()
    {
        if (painelDialogo != null)
        {
            painelDialogo.SetActive(false);
        }
    }

    // --- AÇÃO DO BOTÃO VIAJAR ---

    public void Viajar()
    {
        // Validação simples: avisa se você esqueceu de dar um nome para a cena destino no Inspector
        if (string.IsNullOrEmpty(nomeDaProximaCena))
        {
            Debug.LogError("ERRO: O campo 'Nome Da Proxima Cena' está vazio no Inspector do Guardião!");
            return;
        }

        // Carrega a cena diretamente pelo sistema nativo, sem depender de GameManager
        SceneManager.LoadScene(nomeDaProximaCena);
    }
}