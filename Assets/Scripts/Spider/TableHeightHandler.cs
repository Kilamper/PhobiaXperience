using UnityEngine;

public class TableHeightHandler : MonoBehaviour
{
    [SerializeField] private GameObject scene;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject spider;
    [SerializeField] private GameObject leftArm;
    [SerializeField] private GameObject rightArm;

    private static Vector3 sceneInitialPosition;
    private static Vector3 boxInitialPosition;
    private static Vector3 spiderInitialPosition;
    private static float heightDifference;

    private void Start()
    {
        // Initialize the height difference based on the initial positions of the scene and left arm
        sceneInitialPosition = scene.transform.position;
        boxInitialPosition = box.transform.position;
        spiderInitialPosition = spider.transform.position;
    }

    public void AdjustTableHeight()
    {
        heightDifference = sceneInitialPosition.y - leftArm.transform.position.y;
        scene.transform.position = new Vector3(sceneInitialPosition.x, sceneInitialPosition.y + 0.91f - heightDifference, sceneInitialPosition.z);
        box.transform.position = new Vector3(boxInitialPosition.x, boxInitialPosition.y + 0.91f - heightDifference, boxInitialPosition.z);
        spider.transform.position = new Vector3(spiderInitialPosition.x, spiderInitialPosition.y + 0.91f - heightDifference, spiderInitialPosition.z);
    }
}