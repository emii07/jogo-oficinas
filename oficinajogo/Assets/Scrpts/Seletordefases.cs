using UnityEngine;
using UnityEngine.SceneManagement; // Sistema nativo para carregar cenas

public class SeletorDeFases : MonoBehaviour
{
    [Tooltip("sala dos intelectuais.")]
    [SerializeField] private string nomeDaCenaFase;

    public void AbrirFase()
    {
        // Verifica se você não esqueceu de digitar o nome da cena no Inspector
        if (!string.IsNullOrEmpty(nomeDaCenaFase))
        {
            SceneManager.LoadScene(nomeDaCenaFase);
        }
        else
        {
            Debug.LogError($"ERRO: O botão '{gameObject.name}' não possui o nome da cena configurado no Inspector!");
        }
    }
}