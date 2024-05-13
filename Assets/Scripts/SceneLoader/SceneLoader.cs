using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    public void MaLoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    private static SceneLoader instance;

    public static SceneLoader Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject sceneLoaderObj = new GameObject("SceneLoader");
                instance = sceneLoaderObj.AddComponent<SceneLoader>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // 等待场景加载完成
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
