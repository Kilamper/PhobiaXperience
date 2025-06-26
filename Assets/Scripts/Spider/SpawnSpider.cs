using UnityEngine;

public class SpawnSpider : MonoBehaviour
{
    [SerializeField] private GameObject spiderPrefab;

    public void showSpiderModel()
    {
        spiderPrefab.SetActive(!spiderPrefab.activeSelf);
    }
}