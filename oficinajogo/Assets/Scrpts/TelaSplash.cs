using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement; // Importante: adicionado para trocar de cena diretamente

public class TelaSplash : MonoBehaviour
{
    IEnumerator Start()
    {
        // Espera 2 segundos na tela de Splash
        yield return new WaitForSeconds(2f);

        // Carrega o Menu Principal diretamente pelo sistema nativo, sem o GameManager
        SceneManager.LoadScene("MenuPrincipal");
    }
}