using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; // Slider para controlar el volumen

    void Start()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 1f); // Establece un volumen por defecto
            Load();
        }
        else
        {
            Load(); // Carga el volumen guardado
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value; // Cambia el volumen global
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume"); // Carga el volumen guardado desde PlayerPrefs
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value); // Guarda el volumen en PlayerPrefs
    }
}