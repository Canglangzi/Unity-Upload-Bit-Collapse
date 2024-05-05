using UnityEngine;
using System;

public class SystemB : MonoBehaviour
{
    private void OnEnable()
    {
        // 在启用时注册监听事件
        EventRelay<String>.Instance.RegisterListener(CLZ_EventType.OnFire, OnFire);
    }

    private void OnDisable()
    {
        // 在禁用时取消注册监听事件
        EventRelay<String>.Instance.UnregisterListener(CLZ_EventType.OnFire, OnFire);
    }

    private void OnFire(String damage)
    {
        // 处理事件，并获取传递的参数
        Debug.Log("Firing with damage: " + damage);
    }
}