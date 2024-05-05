using UnityEngine;

public class Old_EventListener : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.AddListener("OnPlayerDeath", OnPlayerDeath);
    }

    private void OnDisable()
    {
        EventManager.Instance.RemoveListener("OnPlayerDeath", OnPlayerDeath);
    }

    private void OnPlayerDeath(object eventData)
    {
        Debug.Log("Player died!");
        // 在这里执行其他处理逻辑...
    }
}
