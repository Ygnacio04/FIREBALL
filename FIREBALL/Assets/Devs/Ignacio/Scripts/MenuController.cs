using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas, PlayCanvas, OptionsCanvas;
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
        PlayCanvas.SetActive(false);
        OptionsCanvas.SetActive(false);
    }

    public void MostrarPanelMenuJuego()
    {
        MainCanvas.SetActive(false);
        PlayCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);
    }

    public void MostrarPanelAjustes()
    {
        MainCanvas.SetActive(false);
        PlayCanvas.SetActive(false);
        OptionsCanvas.SetActive(true);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void IniciarTutorial()
    {
        //GameSettings.CurrentGameMode = "Tutorial"; 
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
