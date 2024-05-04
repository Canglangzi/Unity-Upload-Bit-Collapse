using UnityEngine;

/*指定获取来源 你可以使用 GetComponentFrom 参数来指定获取组件的来源。
GetComponentFrom.SameGameObject: 从同一游戏对象获取组件。
GetComponentFrom.SceneObject: 从场景中获取组件，将从第一个对象中获取。
GetComponentFrom.TargetGameObject: 从指定的目标对象获取组件。*/
/*GetComponentFrom.TargetGameObject 目前只在 Awake 和 Start 方法之间运行。
确保目标对象的名称正确并且在场景中是唯一的，否则可能会导致获取失败或错误的组件。*/

public class Example : MonoBehaviour
{
    // 从同一游戏对象获取组件
    [GetComponent]
    private Rigidbody rigidbody;

    // 从场景中获取组件，将从第一个对象中获取
    [GetComponent(GetComponentFrom.SceneObject)]
    private Rigidbody sceneRigidbody;

    // 从指定的目标对象获取组件
    [GetComponent(GetComponentFrom.TargetGameObject, "MyTargetObjectName")]
    private Rigidbody targetRigidbody;

    private void Awake()
    {
        // 可以直接使用这些字段，而不需要在 Awake 或 Start 方法中调用 GetComponent
        rigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        sceneRigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        targetRigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }
}