using UnityEngine;
using System;

public class SystemB : MonoBehaviour
{
    private void OnEnable()
    {
        // 在启用时注册监听事件
        EventRelay<int>.Instance.RegisterListener(EventType.OnFire, OnFire);
    }

    private void OnDisable()
    {
        // 在禁用时取消注册监听事件
        EventRelay<int>.Instance.UnregisterListener(EventType.OnFire, OnFire);
    }

    private void OnFire(int damage)
    {
        // 处理事件，并获取传递的参数
        Debug.Log("Firing with damage: " + damage);
    }
}