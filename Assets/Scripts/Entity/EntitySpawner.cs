using UnityEngine;

public class EntitySpawner : MonoBehaviour
{
    public GameObject entityPrefab;

    private void Start()
    {
        SpawnEntity();
    }

    private void SpawnEntity()
    {
        GameObject newEntity = Instantiate(entityPrefab, transform.position, Quaternion.identity);
        Entity entityComponent = newEntity.GetComponent<Entity>();
        if (entityComponent != null)
        {
            Debug.Log("Spawned entity with ID: " + entityComponent.GetID());
        }
        else
        {
            Debug.LogError("Failed to spawn entity. Entity component not found.");
        }
    }
}