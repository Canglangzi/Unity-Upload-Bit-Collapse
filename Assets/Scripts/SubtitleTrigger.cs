using UnityEngine;

public class SubtitleTrigger : MonoBehaviour
{
    public string[] subtitleLines;
    public SubtitleSystem subtitleSystem; // 引用字幕系统脚本
    public GameObject subtitlePanel; // 字幕面板
    public GameObject subwayObject; // 地铁对象

    private void Start()
    {
        if (subtitleSystem == null)
        {
            Debug.LogError("SubtitleSystem reference is missing!");
            return;
        }
       SoundManager.Instance.PlaySound("序章背景音乐");
        // 延迟两秒后触发字幕显示和播放音效
        Invoke("ShowSubtitleAndPlaySound", 3f);

         Invoke("ShowSubtitle", 4.5f);

        // 延迟30秒后播放地铁对象的进站动画
       // Invoke("PlaySubwayArrivalAnimation", 30f);
        
    }
     private void ShowSubtitle()
    {
        // 激活字幕面板
        subtitlePanel.SetActive(true);

        // 在开始时触发字幕显示
        subtitleSystem.ShowSubtitle(subtitleLines);
        
   
    }
    private void ShowSubtitleAndPlaySound()
    {
        // 播放音效
      
        SoundManager.Instance.PlaySound("列车即将到站");
    }

    private void PlaySubwayArrivalAnimation()
    {
        // 在地铁对象上播放进站动画
      //  subwayObject.GetComponent<Animator>().Play("ArrivalAnimation");
    }
}
