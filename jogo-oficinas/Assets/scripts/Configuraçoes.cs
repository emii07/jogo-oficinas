using UnityEngine;
using UnityEngine.UI;

public class Configuracoes : MonoBehaviour
{
    public GameObject painelConfiguracoes;

    public Slider volumeSlider;
    public Toggle telaCheiaToggle;

    private void Start()
    {
        painelConfiguracoes.SetActive(false);

        volumeSlider.value = AudioListener.volume;
        telaCheiaToggle.isOn = Screen.fullScreen;
    }

    public void AbrirConfiguracoes()
    {
        painelConfiguracoes.SetActive(true);
    }

    public void FecharConfiguracoes()
    {
        painelConfiguracoes.SetActive(false);
    }

    public void AlterarVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    public void AlterarTelaCheia(bool telaCheia)
    {
        Screen.fullScreen = telaCheia;
    }
}