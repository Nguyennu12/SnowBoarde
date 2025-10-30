using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] GameObject itemPrefab;
    [SerializeField] BoxCollider2D spawnArea;
    [SerializeField] int numberOfItems = 15;

    void Start()
    {
        SpawnRandomItems();
    }

    void SpawnRandomItems()
    {
        Bounds bounds = spawnArea.bounds;

        for (int i = 0; i < numberOfItems; i++)
        {
            float randomX = Random.Range(bounds.min.x, bounds.max.x);
            float randomY = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 spawnPosition = new Vector2(randomX, randomY);

            Instantiate(itemPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }
}
