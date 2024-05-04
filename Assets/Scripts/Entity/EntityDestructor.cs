using UnityEngine;

public class EntityDestructor : MonoBehaviour
{
    private Entity entityComponent;

    private void Start()
    {
        entityComponent = GetComponent<Entity>();
        if (entityComponent == null)
        {
            Debug.LogError("Entity component not found.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyEntity();
        }
    }

    private void DestroyEntity()
    {
        if (entityComponent != null)
        {
            Destroy(gameObject);
            Debug.Log("Entity with ID " + entityComponent.GetID() + " destroyed.");
        }
    }
}