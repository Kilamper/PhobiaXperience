using UnityEngine;
using System.Collections;

public class ElevatorDoorOpener : MonoBehaviour
{
    [SerializeField] GameObject elevatorInDoorLeft; // Referencia a la puerta izquierda del ascensor
    [SerializeField] GameObject elevatorInDoorRight; // Referencia a la puerta derecha del ascensor
    [SerializeField] GameObject elevatorOutDoorLeft; // Referencia a la puerta izquierda del ascensor de salida
    [SerializeField] GameObject elevatorOutDoorRight; // Referencia a la puerta derecha del ascensor de salida

    [SerializeField] AudioSource doorSound; // Referencia al objeto de sonido de la puerta

    [SerializeField] float openSpeed; // Velocidad de apertura de la puerta
    [SerializeField] float openDistance; // Distancia a la que se abrirá la puerta
    [SerializeField] bool isOpen; // Estado de la puerta (abierta o cerrada)

    private Vector3 leftInDoorStartPos; // Posición inicial de la puerta izquierda
    private Vector3 rightInDoorStartPos; // Posición inicial de la puerta derecha
    private Vector3 leftOutDoorStartPos; // Posición inicial de la puerta izquierda de salida
    private Vector3 rightOutDoorStartPos; // Posición inicial de la puerta derecha de salida

    private Vector3 leftInDoorTargetPos; // Posición objetivo de la puerta izquierda
    private Vector3 rightInDoorTargetPos; // Posición objetivo de la puerta derecha
    private Vector3 leftOutDoorTargetPos; // Posición objetivo de la puerta izquierda de salida
    private Vector3 rightOutDoorTargetPos; // Posición objetivo de la puerta derecha de salida

    void Start()
    {
        // Guardar las posiciones iniciales de las puertas interiores
        leftInDoorStartPos = elevatorInDoorLeft.transform.localPosition;
        rightInDoorStartPos = elevatorInDoorRight.transform.localPosition;

        // Guardar las posiciones iniciales de las puertas exteriores
        leftOutDoorStartPos = elevatorOutDoorLeft.transform.localPosition;
        rightOutDoorStartPos = elevatorOutDoorRight.transform.localPosition;

        // Calcular las posiciones objetivo de las puertas interiores
        leftInDoorTargetPos = elevatorInDoorLeft.transform.localPosition + new Vector3(-openDistance, 0, 0);
        rightInDoorTargetPos = elevatorInDoorRight.transform.localPosition + new Vector3(openDistance, 0, 0);

        // Calcular las posiciones objetivo de las puertas exteriores
        leftOutDoorTargetPos = elevatorOutDoorLeft.transform.localPosition + new Vector3(-openDistance, 0, 0);
        rightOutDoorTargetPos = elevatorOutDoorRight.transform.localPosition + new Vector3(openDistance, 0, 0);
    }

    public void OpenDoors()
    {
        StopAllCoroutines(); // Stop any ongoing door movement
        doorSound.Play(); // Play door sound
        if (!isOpen)
        {
            isOpen = true;
            StartCoroutine(MoveDoor(elevatorInDoorLeft, leftInDoorTargetPos));
            StartCoroutine(MoveDoor(elevatorInDoorRight, rightInDoorTargetPos));
            StartCoroutine(MoveDoor(elevatorOutDoorLeft, leftOutDoorTargetPos));
            StartCoroutine(MoveDoor(elevatorOutDoorRight, rightOutDoorTargetPos));
        }
        else if (isOpen)
        {
            isOpen = false;
            StartCoroutine(MoveDoor(elevatorInDoorLeft, leftInDoorStartPos));
            StartCoroutine(MoveDoor(elevatorInDoorRight, rightInDoorStartPos));
            StartCoroutine(MoveDoor(elevatorOutDoorLeft, leftOutDoorStartPos));
            StartCoroutine(MoveDoor(elevatorOutDoorRight, rightOutDoorStartPos));
        }
    }

    private IEnumerator MoveDoor(GameObject door, Vector3 targetPosition)
    {
        while (Vector3.Distance(door.transform.localPosition, targetPosition) > 0.01f)
        {
            door.transform.localPosition = Vector3.MoveTowards(door.transform.localPosition, targetPosition, openSpeed * Time.deltaTime);
            yield return null;
        }
        door.transform.localPosition = targetPosition; // Ensure exact position at the end
    }
}