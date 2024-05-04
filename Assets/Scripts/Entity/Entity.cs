using UnityEngine;
using System.Collections.Generic;

public interface IEntity
{
    uint GetID();
}

public class Entity : MonoBehaviour, IEntity
{
    private static uint nextAvailableID = 1;

    private uint id;

    protected virtual void Awake()
    {
        id = GetNextAvailableID();
        gameObject.name += "_entity_" + id.ToString(); // 将ID添加到对象名字后面
    }

    public uint GetID()
    {
        return id;
    }

    public static uint GetNextAvailableID()
    {
        return nextAvailableID++;
    }
}