using UnityEngine;

public class TriggerAnimationController : MonoBehaviour
{
    public GameObject targetObject;

    public string targetAnimation;
    public string currentString; // 用于存储当前动画状态名称
    private Animator targetAnimator;
    public Animator 判断;
    public string TriggerTag;

    void Start()
    {
        targetAnimator = targetObject.GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("有物体进入！");
        // 检查是否为目标对象
        if (other.CompareTag(TriggerTag) && targetAnimator != null)
        {
            Debug.Log("进入触发器！");

            AnimatorStateInfo currentState = 判断.GetCurrentAnimatorStateInfo(0);
            Debug.Log("当前动画状态：" + currentState.fullPathHash);

            // 检查当前动画状态是否与指定的动画状态名称相符
            if (currentState.IsName(currentString))
            {
                // 播放目标动画
                targetAnimator.Play(targetAnimation);
                Debug.Log("播放动画：" + targetAnimation);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 判断是否为目标对象退出了触发器范围内
        if (other.gameObject == targetObject)
        {
            Debug.Log("离开触发器！");
        }
    }
}
