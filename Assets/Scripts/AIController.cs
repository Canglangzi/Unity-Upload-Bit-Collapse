using UnityEngine;

public class AIController : MonoBehaviour
{
    public float moveSpeed = 5f; // AI移动速度
    private Transform player; // 玩家的Transform
    public string sceneNames;
    private void Start()
    {
        // 获取玩家对象
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // 让AI沿X轴移动，并朝向玩家
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        transform.right = player.position - transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 如果碰撞到玩家，则销毁玩家并重新加载场景
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            ReloadScene();
        }
    }

    private void ReloadScene()
    {
        // 重新加载当前场景
          SceneLoader.Instance.LoadScene(sceneNames);
    }
}
