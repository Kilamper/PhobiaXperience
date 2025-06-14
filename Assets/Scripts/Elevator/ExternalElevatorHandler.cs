using UnityEngine;
using System.Collections;

public class ExternalElevatorHandler : MonoBehaviour
{
    [SerializeField] GameObject elevator; // Referencia al ascensor
    [SerializeField] AudioSource elevatorSound; // Referencia al objeto de sonido del ascensor

    [SerializeField] float speed; // Velocidad de movimiento del ascensor

    [SerializeField] int wantedFloor; // Piso al que se desea mover el ascensor

    private Vector3 startPosition; // Posición inicial del ascensor
    private Vector3 targetPosition; // Posición objetivo del ascensor
    private float actualHeight; // Altura actual del ascensor

    void Start()
    {
        startPosition = elevator.transform.localPosition; // Guardar la posición inicial del ascensor
    }

    public void MoveElevator()
    {
        StopAllCoroutines(); // Detener cualquier movimiento en curso del ascensor

        switch (wantedFloor)
        {
            case 0:
                targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z); // Posición objetivo al mover hacia abajo
                break;
            case 1:
                targetPosition = new Vector3(startPosition.x, 16.98f, startPosition.z); // Posición objetivo al mover hacia arriba
                break;
            case 2:
                targetPosition = new Vector3(startPosition.x, 24.68f, startPosition.z); // Posición objetivo al mover hacia arriba
                break;
            case 3:
                targetPosition = new Vector3(startPosition.x, 32.34f, startPosition.z); // Posición objetivo al mover hacia arriba
                break;
            case 4:
                targetPosition = new Vector3(startPosition.x, 40.06f, startPosition.z); // Posición objetivo al mover hacia arriba
                break;
            case 5:
                targetPosition = new Vector3(startPosition.x, 47.8f, startPosition.z); // Posición objetivo al mover hacia arriba
                break;
            case 6:
                targetPosition = new Vector3(startPosition.x, 55.46f, startPosition.z); // Posición objetivo al mover hacia arriba
                break;
            case 7:
                targetPosition = new Vector3(startPosition.x, 63.14f, startPosition.z); // Posición objetivo al mover hacia arriba
                break;
            case 8:
                targetPosition = new Vector3(startPosition.x, 70.9f, startPosition.z); // Posición objetivo al mover hacia arriba
                break;
        }
        Debug.Log("Moving elevator to floor: " + wantedFloor + " at position: " + targetPosition);
        StartCoroutine(MoveElevatorCoroutine(speed)); // Iniciar la corrutina para mover el ascensor
    }

    private IEnumerator MoveElevatorCoroutine(float speed)
    {
        elevatorSound.Play(); // Reproducir el sonido del ascensor
        while (Vector3.Distance(elevator.transform.localPosition, targetPosition) > 0.01f)
        {
            elevator.transform.localPosition = Vector3.MoveTowards(elevator.transform.localPosition, targetPosition, speed * Time.deltaTime); // Mover el ascensor
            yield return null; // Esperar un frame
        }
        elevatorSound.Stop(); // Detener el sonido del ascensor al llegar a la posición objetivo

        elevator.transform.localPosition = targetPosition; // Asegurarse de que el ascensor llegue a la posición objetivo
    }
}