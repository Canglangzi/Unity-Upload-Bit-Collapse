using System.Reflection;
using System;
using System.Linq; // 添加使用 LINQ 的命名空间
using UnityEngine;
using UnityEngine.SceneManagement;

public static class GetComponentInjection
{
    public static event Action<Scene, LoadSceneMode> SceneLoaded;

    public static InjectEvent<GetComponentAttribute, FieldInfo, MonoBehaviour> SingleObjectClassifier;
    public static InjectEvent<GetComponentAttribute, FieldInfo, MonoHolder<MonoBehaviour, MonoBehaviour[]>> MultipleObjectClassifier;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Initialize()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneLoaded?.Invoke(scene, mode);
    }

    public static void Inject(Scene scene)
    {
        if (!scene.isLoaded)
            return;

        MonoBehaviour[] objs = scene.GetRootGameObjects()
            .SelectMany(go => go.GetComponentsInChildren<MonoBehaviour>(true))
            .ToArray();
        foreach (var obj in objs)
        {
            Type type = obj.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            GetComponentAttribute attr;
            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(GetComponentAttribute), false))
                {
                    attr = field.GetCustomAttribute<GetComponentAttribute>();
                    SingleObjectClassifier?.Invoke(attr, field, obj);
                    MultipleObjectClassifier?.Invoke(attr, field, new MonoHolder<MonoBehaviour, MonoBehaviour[]>(obj, objs));
                }
            }
        }
    }
}