using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instancia;

    public enum EstadoJogo
    {
        Iniciando,
        MenuPrincipal,
        Gameplay
    }

    public EstadoJogo estadoAtual;

    private PlayerInput entradaJogador;

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        MudarEstado(EstadoJogo.Iniciando);

        AlocarInput();

        CarregarCena("Splash");
    }

    public void MudarEstado(EstadoJogo novoEstado)
    {
        estadoAtual = novoEstado;

        Debug.Log("Estado atual: " + estadoAtual);
    }

    public void CarregarCena(string nomeCena)
    {
        if (nomeCena == "Jogo")
        {
            SceneManager.LoadScene("Jogo");
            SceneManager.LoadScene("GUI", LoadSceneMode.Additive);

            MudarEstado(EstadoJogo.Gameplay);
        }
        else
        {
            SceneManager.LoadScene(nomeCena);

            if (nomeCena == "Splash")
            {
                MudarEstado(EstadoJogo.Iniciando);
            }
            else if (nomeCena == "MenuPrincipal")
            {
                MudarEstado(EstadoJogo.MenuPrincipal);
            }
        }

        AlocarInput();
    }

    public void AlocarInput()
    {
        entradaJogador = FindFirstObjectByType<PlayerInput>();

        if (entradaJogador != null)
        {
            Debug.Log("Player Input encontrado!");
        }
        else
        {
            Debug.Log("Nenhum Player Input encontrado.");
        }
    }

    public void SairJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
    }
}