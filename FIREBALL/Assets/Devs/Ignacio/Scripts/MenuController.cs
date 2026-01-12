using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas, OptionsCanvas;
    [SerializeField] private Slider AmbienceSlider, MusicSlider, FXSlider;

    void Start()
    {
        MostrarPanelMenuPrincipal();

        if (SoundManager.Instance != null)
        {
            AmbienceSlider.value = PlayerPrefs.GetFloat("AmbienceVolume", 1f);  
            MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1f);       
            FXSlider.value = PlayerPrefs.GetFloat("FXVolume", 1f);              
        }

        OnMusicVolumeChanged();  
        OnAmbienceVolumeChanged(); 

    }

    public void MostrarPanelMenuPrincipal()
    {
        MainCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);
    }

    public void MostrarPanelAjustes()
    {
        MainCanvas.SetActive(false);
        OptionsCanvas.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void IniciarMainGame()
    {
        //GameSettings.CurrentGameMode = "MainGame"; 

    }

    public void OnAmbienceVolumeChanged()
    {
        SoundManager.Instance.SetAmbienceVolume(AmbienceSlider.value);
        PlayerPrefs.SetFloat("AmbienceVolume", AmbienceSlider.value); 
    }

    public void OnMusicVolumeChanged()
    {
        SoundManager.Instance.SetMusicVolume(MusicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
    }

    public void OnFXVolumeChanged()
    {
        SoundManager.Instance.SetFxVolume(FXSlider.value);
        PlayerPrefs.SetFloat("FXVolume", FXSlider.value); 
    }
}
