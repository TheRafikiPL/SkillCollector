using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UIElements.Button startButton;
    [SerializeField]
    UnityEngine.UIElements.Button exitButton;
    [SerializeField]
    GameObject NewUI;
    [SerializeField]
    UnityEngine.UI.Slider masterSlider;
    [SerializeField]
    UnityEngine.UI.Slider musicSlider;
    [SerializeField]
    UnityEngine.UI.Slider effectsSlider;
    void Start() 
    {
        var root = NewUI.GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<UnityEngine.UIElements.Button>("StartButton");
        startButton.clicked += StartGame;
        exitButton = root.Q<UnityEngine.UIElements.Button>("ExitButton");
        exitButton.clicked += ExitGame;
        masterSlider.onValueChanged.AddListener(AudioController.instance.SetMasterAudio);
        musicSlider.onValueChanged.AddListener(AudioController.instance.SetMusicVolume);
        effectsSlider.onValueChanged.AddListener(AudioController.instance.SetEffectsVolume);
        LoadSettings();
    }

    void StartGame()
    {
        SaveSettings();
        SceneManager.LoadScene(1);
        AudioController.instance.PlayBattleTheme();
    }
    void ExitGame()
    {
        SaveSettings();
        Application.Quit();
    }
    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("EffectsVolume"))
        {
            effectsSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        }
    }
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("EffectsVolume", effectsSlider.value);
    }
}
