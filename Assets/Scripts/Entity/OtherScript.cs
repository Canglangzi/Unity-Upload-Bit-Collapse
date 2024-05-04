using UnityEngine;

public class OtherScript : MonoBehaviour
{
    private void Start()
    {
        uint id = 1; 
        ExampleScript exampleScript = EntityManager.GetEntityComponent<ExampleScript>(id);

        if (exampleScript != null)
        {
            // 在这里使用 exampleScript
            Debug.Log("ExampleScript found on entity with ID: " + id);
        }
        else
        {
            Debug.LogError("ExampleScript not found on entity with ID: " + id);
        }
    }
}