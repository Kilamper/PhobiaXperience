using UnityEngine;

public class ElevatorZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has entered the elevator zone.");
            // Hacer al jugador hijo del ascensor
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has exited the elevator zone.");
            // Quitar la relación padre-hijo
            other.transform.SetParent(null);
        }
    }
}
