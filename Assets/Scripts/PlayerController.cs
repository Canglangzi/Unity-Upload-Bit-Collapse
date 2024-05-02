using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeedMultiplier = 2f; // 跑步时的速度倍数
    public float jumpForce = 10f; // 跳跃力大小
    public Rigidbody rb;
    public Animator animator;
    public bool isFacingRight = true; // 是否面向右边
    public bool isGrounded = true; // 是否在地面上

    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
      //  animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckGrounded();

        // 检测跳跃
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // 检测奔跑
        float moveSpeedMultiplier = Input.GetKey(KeyCode.LeftShift) ? runSpeedMultiplier : 1f;

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed * moveSpeedMultiplier, rb.linearVelocity.y);

        // 设置奔跑动画播放状态
        animator.SetBool("isRunning", Mathf.Abs(moveInput) > 0 && Input.GetKey(KeyCode.LeftShift));
        // 设置行走动画播放状态
        animator.SetBool("Walk", Mathf.Abs(moveInput) > 0);
        // 设置跳跃动画播放状态
        animator.SetBool("Jump", !isGrounded);

        // 调整角色朝向
        if (moveInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && isFacingRight)
        {
            Flip();
        }
    }

    void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;
    }
}
