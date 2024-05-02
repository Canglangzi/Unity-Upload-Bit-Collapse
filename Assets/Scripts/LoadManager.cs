using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadManager : MonoBehaviour
{
    public GameObject loadScreen;
    public Slider slider;
    public Text text;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("MainMenu");
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            slider.value = operation.progress;
            text.text = (operation.progress * 100) + "%";

            if (operation.progress >= 0.9f)
            {
                slider.value = 1;
                text.text = "请按下任意按键";
                if (Input.anyKeyDown)
                {
                    operation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadLevel());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
