using UnityEngine;
using System;

public class SystemB : MonoBehaviour
{
    private void OnEnable()
    {
        // 在启用时注册监听事件
        EventRelay.Instance.RegisterListener("OnFire", OnFire);
    }

    private void OnDisable()
    {
        // 在禁用时取消注册监听事件
        EventRelay.Instance.UnregisterListener("OnFire", OnFire);
    }

    private void OnFire()
    {
        // 响应事件的处理逻辑
        Debug.Log("Firing!");
    }
}