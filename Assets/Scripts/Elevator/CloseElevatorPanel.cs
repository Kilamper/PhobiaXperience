using UnityEngine;
using UnityEngine.UI;

public class CloseElevatorPanel : MonoBehaviour
{
    [SerializeField] GameObject elevatorPanel; // El panel del menú de ajustes
    [SerializeField] Button closeButton;  // Botón de menú

    void Start()
    {
        // Asignar la función al botón
        closeButton.onClick.AddListener(ToggleMenu);
    }

    void ToggleMenu()
    {
        elevatorPanel.SetActive(false);
    }
}
