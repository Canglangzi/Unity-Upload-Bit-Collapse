using UnityEngine;
using System.Collections.Generic;

public class EntityManager : MonoBehaviour
{
    private static Dictionary<uint, IEntity> entityDictionary = new Dictionary<uint, IEntity>();

    public static void AddEntity(uint id, IEntity entity)
    {
        if (!entityDictionary.ContainsKey(id))
        {
            entityDictionary.Add(id, entity);
        }
        else
        {
            Debug.LogError("Entity with ID " + id + " already exists in the dictionary.");
        }
    }

    public static IEntity GetEntityByID(uint id)
    {
        if (entityDictionary.ContainsKey(id))
        {
            return entityDictionary[id];
        }
        else
        {
            Debug.LogError("Entity with ID " + id + " does not exist in the dictionary.");
            return null;
        }
    }

    public static void RemoveEntity(uint id)
    {
        if (entityDictionary.ContainsKey(id))
        {
            entityDictionary.Remove(id);
        }
        else
        {
            Debug.LogError("Entity with ID " + id + " does not exist in the dictionary.");
        }
    }

    public static T GetEntityComponent<T>(uint id) where T : MonoBehaviour
    {
        IEntity entity = GetEntityByID(id);
        if (entity != null)
        {
            GameObject gameObject = ((MonoBehaviour)entity).gameObject;
            T component = gameObject.GetComponent<T>();
            return component;
        }
        else
        {
            return null;
        }
    }
}