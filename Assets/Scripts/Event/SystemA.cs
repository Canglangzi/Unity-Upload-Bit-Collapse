using UnityEngine;

public class SystemA : MonoBehaviour
{
    private async void Start()
    {
        // 示例：在启动时异步发送事件
        await EventRelay.Instance.InvokeEventAsync("OnHealthChanged");
    }
}

