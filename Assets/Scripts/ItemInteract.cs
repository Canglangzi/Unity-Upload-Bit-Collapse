using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
namespace CLz
{
public class ItemInteract : MonoBehaviour
{
    public GameObject explorationPanel; // 探索面板
    public TMP_Text explorationText; // 探索文本（TextMeshPro）
    //public KeyCode interactKey = KeyCode.F; // 互动按键，默认为F
   // public KeyCode nextTextKey = KeyCode.K; // 进入下一个文本按键，默认为K

    public GameObject explorationPanelInRange;
    public bool isInRange = false; // 是否在物品触发器范围内
    public bool isExploring = false; // 是否正在探索物品
    public string[] explorationContent; // 探索内容
    private int currentTextIndex = 0; // 当前显示的文本索引
    public float textSpeed = 0.05f; // 文本打字速度

    private bool isTextDisplaying = false; // 是否正在显示文字

    private InputManager inputManager;
public void Start()
{
    inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            explorationPanelInRange.SetActive(true);
           // 在其他脚本中禁止玩家移动
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            explorationPanelInRange.SetActive(false);
           
            isInRange = false;
        }
    }

    private void Update()
    {
        if (isInRange && Input.GetKeyDown(inputManager.interactKey) && !isExploring)
        {
            StartExploration();
        }

        if (isExploring && !isTextDisplaying && Input.GetKeyDown(inputManager.interactKey))
        {
            ShowNextText();
        }
    }

    private void StartExploration()
    {
        isExploring = true;
        explorationPanel.SetActive(true);
        currentTextIndex = 0;
        StartCoroutine(DisplayTextCoroutine());
        PlayerManager.Instance.DisablePlayerMovement();
    }

    private void ShowNextText()
    {
        currentTextIndex++;
        if (currentTextIndex < explorationContent.Length)
        {
            explorationText.text = ""; // 清空文本
            StartCoroutine(DisplayTextCoroutine());
        }
        else
        {
            EndExploration();
        }
    }

    private void EndExploration()
    {
        
        isExploring = false;
        explorationPanel.SetActive(false); // 关闭探索面板
        explorationText.text = ""; // 清空探索文本
    
           PlayerManager.Instance.EnablePlayerMovement();
    }


    private IEnumerator DisplayTextCoroutine()
    {
        isTextDisplaying = true; // 设置为正在显示文字状态
        string targetText = explorationContent[currentTextIndex];
        for (int i = 0; i < targetText.Length; i++)
        {
            explorationText.text += targetText[i];
            yield return new WaitForSeconds(textSpeed);
        }
        isTextDisplaying = false; // 文字显示完毕，设置为未显示文字状态
       
    }
}


}
