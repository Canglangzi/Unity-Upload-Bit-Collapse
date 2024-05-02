using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


namespace CLz{

    public class HoverAndReplaceImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 原始Image
    public Image originalImage;

    // 替换的Image
    public Sprite replacementSprite;

    // 当鼠标悬停在UI元素上时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 替换原始Image的Sprite
        originalImage.sprite = replacementSprite;
    }

    // 当鼠标移出UI元素时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        // 恢复原始Image的Sprite
        originalImage.sprite = originalSprite;
    }

    private Sprite originalSprite;

    // 在脚本启用时记录原始Image的Sprite
    void Start()
    {
        originalSprite = originalImage.sprite;
    }
}

}
