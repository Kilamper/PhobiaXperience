using UnityEngine;
using System.Collections;

public class ElevatorMovementHandler : MonoBehaviour
{
    [SerializeField] GameObject elevator; // Referencia al ascensor
    [SerializeField] AudioSource elevatorSound; // Referencia al objeto de sonido del ascensor

    [SerializeField] float speed; // Velocidad de movimiento del ascensor
    [SerializeField] bool moveUp; // Indica si el ascensor se mueve hacia arriba o hacia abajo

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

        actualHeight = elevator.transform.localPosition.y; // Obtener la altura actual del ascensor
        if (moveUp)
        {
            switch (actualHeight)
            {
                case < 16.98f:
                    targetPosition = new Vector3(startPosition.x, 16.98f, startPosition.z); // Posición objetivo al mover hacia arriba
                    break;
                case < 24.68f:
                    targetPosition = new Vector3(startPosition.x, 24.68f, startPosition.z); // Posición objetivo al mover hacia arriba
                    break;
                case < 32.34f:
                    targetPosition = new Vector3(startPosition.x, 32.34f, startPosition.z); // Posición objetivo al mover hacia arriba
                    break;
                case < 40.06f:
                    targetPosition = new Vector3(startPosition.x, 40.06f, startPosition.z); // Posición objetivo al mover hacia arriba
                    break;
                case < 47.8f:
                    targetPosition = new Vector3(startPosition.x, 47.8f, startPosition.z); // Posición objetivo al mover hacia arriba
                    break;
                case < 55.46f:
                    targetPosition = new Vector3(startPosition.x, 55.46f, startPosition.z); // Posición objetivo al mover hacia arriba
                    break;
                case < 63.14f:
                    targetPosition = new Vector3(startPosition.x, 63.14f, startPosition.z); // Posición objetivo al mover hacia arriba
                    break;
                case <= 70.9f:
                    targetPosition = new Vector3(startPosition.x, 70.9f, startPosition.z); // Posición objetivo al mover hacia arriba
                    break;
            }
        }
        else if (!moveUp)
        {
            switch (actualHeight)
            {
                case <= 16.98f:
                    targetPosition = new Vector3(startPosition.x, startPosition.y, startPosition.z); // Posición objetivo al mover hacia abajo
                    break;
                case <= 24.68f:
                    targetPosition = new Vector3(startPosition.x, 16.98f, startPosition.z); // Posición objetivo al mover hacia abajo
                    break;
                case <= 32.34f:
                    targetPosition = new Vector3(startPosition.x, 24.68f, startPosition.z); // Posición objetivo al mover hacia abajo
                    break;
                case <= 40.06f:
                    targetPosition = new Vector3(startPosition.x, 32.34f, startPosition.z); // Posición objetivo al mover hacia abajo
                    break;
                case <= 47.8f:
                    targetPosition = new Vector3(startPosition.x, 40.06f, startPosition.z); // Posición objetivo al mover hacia abajo
                    break;
                case <= 55.46f:
                    targetPosition = new Vector3(startPosition.x, 47.8f, startPosition.z); // Posición objetivo al mover hacia abajo
                    break;
                case <= 63.14f:
                    targetPosition = new Vector3(startPosition.x, 55.46f, startPosition.z); // Posición objetivo al mover hacia abajo
                    break;
                case <= 70.9f:
                    targetPosition = new Vector3(startPosition.x, 63.14f, startPosition.z); // Posición objetivo al mover hacia abajo
                    break;
            }
        }
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