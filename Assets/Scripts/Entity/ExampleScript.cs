using UnityEngine;

public class ExampleScript : MonoBehaviour, IEntity
{
    private uint id;

    protected virtual void Awake()
    {
        id = Entity.GetNextAvailableID(); // 从 Entity 类获取ID
        EntityManager.AddEntity(id, this); // 将实体添加到 EntityManager
        Debug.Log("id:" + id);
    }

    public uint GetID()
    {
        return id;
    }

    // 这里是你脚本的其他功能
}