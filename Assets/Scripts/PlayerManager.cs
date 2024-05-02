using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; } // 单例

    private PlayerController playerController; // 玩家控制器

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // 获取玩家身上的PlayerController脚本
        playerController = FindObjectOfType<PlayerController>();
        if (playerController == null)
        {
            Debug.LogError("PlayerController not found on the player object!");
        }
    }

    // 接口用于禁止玩家移动
    public void DisablePlayerMovement()
    {
        if (playerController != null)
        {
            playerController.enabled = false;
                        if ( playerController.animator != null)
            {
                 playerController.animator.Play("Idle"); // 假设有一个名为 "IsMoving" 的布尔参数来控制角色是否在移动
                  playerController.animator.SetBool("Walk", false); // 假设有一个名为 "IsMoving" 的布尔参数来控制角色是否在移动
            }
        }
    }

    // 接口用于启用玩家移动
    public void EnablePlayerMovement()
    {
        if (playerController != null)
        {
            playerController.enabled = true;
             // 将动画状态设置为 Idle

        }
    }
}
