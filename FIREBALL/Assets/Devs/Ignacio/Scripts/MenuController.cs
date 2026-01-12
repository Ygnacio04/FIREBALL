using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject MainCanvas, OptionsCanvas;
    [SerializeField] private Slider AmbienceSlider, MusicSlider, FXSlider;
    [SerializeField] private SceneController sceneController;

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
        if (sceneController != null)
        {
            sceneController.QuitApplication();
        }
        else
        {
            Application.Quit();
        }
    }

    public void IniciarMainGame()
    {
        // Por ahora carga la escena de créditos como prueba
        if (sceneController != null)
        {
            sceneController.LoadCreditosScene();
        }
        else
        {
            SceneManager.LoadScene("Creditos");
        }
    }

    public void OnAmbienceVolumeChanged()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetAmbienceVolume(AmbienceSlider.value);
            PlayerPrefs.SetFloat("AmbienceVolume", AmbienceSlider.value);
        }
    }

    public void OnMusicVolumeChanged()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetMusicVolume(MusicSlider.value);
            PlayerPrefs.SetFloat("MusicVolume", MusicSlider.value);
        }
    }

    public void OnFXVolumeChanged()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetFxVolume(FXSlider.value);
            PlayerPrefs.SetFloat("FXVolume", FXSlider.value);
        }
    }
}