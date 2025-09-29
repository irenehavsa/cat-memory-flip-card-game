using UnityEngine;
using System.IO;
using System.Collections;
using UnityEngine.Networking;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance { get; private set; }

    public LevelConfigList levelConfigList { get; private set; }

    private void Awake()
    {
        // --- Singleton setup ---
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // survives scene changes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Public coroutine for loading levels
    public IEnumerator LoadLevels()
    {
        if (levelConfigList != null) // already loaded
        {
            yield break;
        }

        Debug.Log("Start load JSON");
        string path = Path.Combine(Application.streamingAssetsPath, "levelsBeta.json");

        string jsonContent = null;

        if (path.Contains("://")) // Android / iOS (inside .apk or .obb)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(path))
            {
                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.Success)
                {
                    jsonContent = www.downloadHandler.text;
                }
                else
                {
                    Debug.LogError("Failed to load levels file at " + path + " : " + www.error);
                }
            }
        }
        else // PC / Editor
        {
            if (File.Exists(path))
            {
                jsonContent = File.ReadAllText(path);
            }
            else
            {
                Debug.LogError("Levels file not found at " + path);
            }
        }

        if (!string.IsNullOrEmpty(jsonContent))
        {
            levelConfigList = JsonUtility.FromJson<LevelConfigList>(jsonContent);
            Debug.Log("Loaded " + levelConfigList.levels.Length + " levels.");
        }
    }

    // Get a level config
    public LevelConfig GetLevel(int index)
    {
        if (levelConfigList != null && index >= 0 && index < levelConfigList.levels.Length)
        {
            return levelConfigList.levels[index];
        }
        return null;
    }
}
