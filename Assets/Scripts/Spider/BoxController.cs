using UnityEngine;
using System.Collections;

public class BoxController : MonoBehaviour
{
    [SerializeField] private GameObject boxPrefab;
    private static Vector3 initialPosition;
    private static Vector3 initialRotation;

    private void Start()
    {
        initialPosition = boxPrefab.transform.position;
        initialRotation = boxPrefab.transform.eulerAngles;
    }

    public void MoveBoxToInitialPosition()
    {
        StopAllCoroutines(); // Detener cualquier movimiento en curso
        boxPrefab.transform.position = initialPosition;
        boxPrefab.transform.eulerAngles = initialRotation;
    }

    public void MoveBox()
    {
        if (boxPrefab.transform.position == initialPosition)
        {
            StartCoroutine(MoveBoxSequence());
        }
        else
        {
            StartCoroutine(ReturnBoxSequence());
        }

    }

    private IEnumerator MoveBoxSequence()
    {
        // Primera etapa: subir 0.2 en Y
        Vector3 startPosition = boxPrefab.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 0.1f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition));

        // Segunda etapa: mover -0.5 en X
        startPosition = boxPrefab.transform.position;
        targetPosition = startPosition + new Vector3(-0.5f, 0, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition));

        // Tercera etapa: bajar 0.2 en Y
        startPosition = boxPrefab.transform.position;
        targetPosition = startPosition + new Vector3(0, -0.1f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition));
    }

    private IEnumerator ReturnBoxSequence()
    {
        // Primera etapa: subir 0.2 en Y
        Vector3 startPosition = boxPrefab.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(0, 0.1f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition));

        // Segunda etapa: mover -0.5 en X
        startPosition = boxPrefab.transform.position;
        targetPosition = startPosition + new Vector3(0.5f, 0, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition));

        // Tercera etapa: bajar 0.2 en Y
        startPosition = boxPrefab.transform.position;
        targetPosition = startPosition + new Vector3(0, -0.1f, 0);
        yield return StartCoroutine(MoveToPosition(targetPosition));
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        while (Vector3.Distance(boxPrefab.transform.position, targetPosition) > 0.01f)
        {
            boxPrefab.transform.position = Vector3.MoveTowards(
                boxPrefab.transform.position,
                targetPosition,
                Time.deltaTime
            );
            yield return null;
        }

        boxPrefab.transform.position = targetPosition; // Asegura que termina exactamente en destino
    }
}