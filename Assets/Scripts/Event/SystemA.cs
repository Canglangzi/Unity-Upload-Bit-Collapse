using UnityEngine;

public class SystemA : MonoBehaviour
{
    private void Update()
    {
        // 示例：按下空格键触发开火事件
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 触发开火事件
            EventRelay.Instance.InvokeEventAsync("OnFire");
        }
    }
}