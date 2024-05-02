using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class SoundManager : MonoBehaviour
{
    // 单例模式
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "SoundManager";
                    _instance = obj.AddComponent<SoundManager>();
                }
            }
            return _instance;
        }
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public SoundData data;
    }

    // 存储音效的字典
    private Dictionary<string, SoundData> _audioClips = new Dictionary<string, SoundData>();

    public Sound[] sounds;

    // 初始化
    void Awake()
    {
        foreach (Sound sound in sounds)
        {
            AddSound(sound.name, sound.data);
        }
    }

    // 添加音效到字典中
    public void AddSound(string name, SoundData data)
    {
        if (!_audioClips.ContainsKey(name))
        {
            _audioClips[name] = data;
        }
        else
        {
            Debug.LogError("Sound with name " + name + " already exists in the dictionary.");
        }
    }

    // 播放音效
    public void PlaySound(string name, float volume = 1f)
    {
        if (_audioClips.ContainsKey(name))
        {
            SoundData soundData = _audioClips[name];
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = soundData.clip;
            source.volume = soundData.volume * volume;
            source.loop = soundData.loop;
            source.Play();
        }
        else
        {
            Debug.LogError("Sound not found: " + name);
        }
    }

    // 调整音效音量
    public void AdjustVolume(string name, float volume)
    {
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip != null && source.clip.name == name)
            {
                source.volume = volume;
                return;
            }
        }
        Debug.LogWarning("AudioSource for sound " + name + " not found.");
    }
    // 停止特定音效的播放
public void StopSound(string name)
{
    AudioSource[] sources = GetComponents<AudioSource>();
    foreach (AudioSource source in sources)
    {
        if (source.clip != null && source.clip.name == name)
        {
            Destroy(source.gameObject); // 销毁包含指定音效的 AudioSource 对象
        }
    }
}
// 停止所有音效的播放
public void StopAllSounds()
{
    AudioSource[] sources = GetComponents<AudioSource>();
    foreach (AudioSource source in sources)
    {
        Destroy(source.gameObject); // 销毁所有 AudioSource 对象
    }
}

// 播放音效，并以淡入淡出效果
public void PlaySoundWithFade(string name, float fadeInDuration, float fadeOutDuration, float volume = 1f)
{
    if (_audioClips.ContainsKey(name))
    {
        SoundData soundData = _audioClips[name];
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = soundData.clip;
        source.volume = 0f; // 初始音量为0
        source.loop = soundData.loop;
        source.Play();

        StartCoroutine(FadeIn(source, fadeInDuration, volume)); // 淡入
        StartCoroutine(FadeOutAndDestroy(source, fadeInDuration + source.clip.length, fadeOutDuration)); // 播放结束后淡出并销毁 AudioSource
    }
    else
    {
        Debug.LogError("Sound not found: " + name);
    }
}

// 淡入
private IEnumerator FadeIn(AudioSource source, float duration, float targetVolume)
{
    float currentTime = 0f;
    float startVolume = source.volume;

    while (currentTime < duration)
    {
        currentTime += Time.deltaTime;
        source.volume = Mathf.Lerp(startVolume, targetVolume, currentTime / duration);
        yield return null;
    }
}

// 淡出并销毁 AudioSource
private IEnumerator FadeOutAndDestroy(AudioSource source, float delay, float duration)
{
    yield return new WaitForSeconds(delay);

    float currentTime = 0f;
    float startVolume = source.volume;

    while (currentTime < duration)
    {
        currentTime += Time.deltaTime;
        source.volume = Mathf.Lerp(startVolume, 0f, currentTime / duration);
        yield return null;
    }

    Destroy(source.gameObject);
}
// 预加载所有音效
public void PreloadSounds()
{
    foreach (Sound sound in sounds)
    {
        if (!_audioClips.ContainsKey(sound.name))
        {
            _audioClips[sound.name] = sound.data;
        }
    }
}

}
