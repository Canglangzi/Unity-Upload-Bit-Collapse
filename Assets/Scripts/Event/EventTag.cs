using UnityEngine;
using System;

// 创建一个自定义的事件标签类，类似于UE的GameplayTag
[Serializable]
public class EventTag
{
    [SerializeField]
    private string tagName;

    public EventTag(string name)
    {
        tagName = name;
    }

    // 重写ToString方法，返回标签名称
    public override string ToString()
    {
        return tagName;
    }
}