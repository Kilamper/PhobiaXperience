using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class BurgerMenu : MonoBehaviour
{
    [SerializeField] GameObject menuPanel; // El panel del menú de ajustes
    [SerializeField] Button burgerButton;  // Botón de menú

    [SerializeField] List<GameObject> additionalPanels;

    void Start()
    {
        // Asegurar que el menú está oculto al iniciar
        menuPanel.SetActive(false);

        // Asignar la función al botón
        burgerButton.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        foreach (GameObject panel in additionalPanels)
        {
            panel.SetActive(false);
        }
    }
}
