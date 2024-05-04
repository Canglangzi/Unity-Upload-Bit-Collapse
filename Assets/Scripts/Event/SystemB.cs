using UnityEngine;
using System.Threading.Tasks;
public class SystemB : MonoBehaviour
{
    private void OnEnable()
    {
        // 在启用时注册监听事件
        EventRelay.Instance.RegisterListener("OnHealthChanged", OnHealthChanged);
    }

    private void OnDisable()
    {
        // 在禁用时取消注册监听事件
        EventRelay.Instance.UnregisterListener("OnHealthChanged", OnHealthChanged);
    }

    private async void OnHealthChanged()
    {
        // 响应事件的处理逻辑
        Debug.Log("Health has changed!");
        await Task.Yield();
    }
}