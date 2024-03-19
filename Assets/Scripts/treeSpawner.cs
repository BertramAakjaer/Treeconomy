using UnityEngine;

public class treeSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float spawnHeight = 10f;
    public float despawnHeight = -10f;
    public float minX = -5f;
    public float maxX = 5f;
    public float moveSpeed = 1f;

    public void SpawnPrefab()
    {
        GameObject selectedPrefab = prefabs[Random.Range(0, prefabs.Length)];
        Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnHeight, 0f);
        Quaternion spawnRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        GameObject newPrefab = Instantiate(selectedPrefab, spawnPosition, spawnRotation);
        StartCoroutine(MovePrefabDown(newPrefab));
    }

    System.Collections.IEnumerator MovePrefabDown(GameObject prefab)
    {
        while (prefab.transform.position.y > despawnHeight)
        {
            prefab.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime, Space.World);
            yield return null;
        }

        Destroy(prefab);
    }
}