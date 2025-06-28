using UnityEngine;

public class SpawnSpider : MonoBehaviour
{
    [SerializeField] private GameObject spiderPrefab;
    private static Vector3 initialPosition;
    private static Vector3 initialRotation;

    private void Start()
    {
        initialPosition = spiderPrefab.transform.position;
        initialRotation = spiderPrefab.transform.eulerAngles;
    }

    public void showSpiderModel()
    {
        spiderPrefab.SetActive(!spiderPrefab.activeSelf);
    }

    public void moveSpiderToInitialPosition()
    {
        spiderPrefab.transform.position = initialPosition;
        spiderPrefab.transform.eulerAngles = initialRotation;
    }
}