using UnityEngine;


namespace CLz
{
    public class InputManager : MonoBehaviour
{
    public static InputManager instance; // 单例

    public KeyCode nextDialogueKey = KeyCode.Space; // 显示下一条对话的按键
    public KeyCode endDialogueKey = KeyCode.F; // 结束对话的按键f
    public KeyCode interactKey= KeyCode.F;
    private bool canInteract = true; // 是否可以进行交互

    void Awake()
    {
        // 创建单例
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 如果正在交互中，则不接受按键输入
        if (!canInteract)
            return;

     
    }

    // 设置是否可以进行交互
    public void SetInteractable(bool interactable)
    {
        canInteract = interactable;
    }
}

}

