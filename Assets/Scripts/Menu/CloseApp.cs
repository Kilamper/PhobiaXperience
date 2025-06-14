using UnityEngine;
using UnityEngine.UI;

public class CloseApp : MonoBehaviour
{
    [SerializeField] Button closeButton; // Botón para cerrar la app

    void Start()
    {
        // Asigna la función al botón
        closeButton.onClick.AddListener(CloseApplication);
    }

    void CloseApplication()
    {
        Debug.Log("Cerrando la aplicación...");
        Application.Quit(); // Cierra la aplicación

        // Para detener el editor en modo Play (Solo en Unity Editor)
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
