using UnityEngine;
using System.IO;

public class LevelLoader : MonoBehaviour
{
    public LevelConfigList levelConfigList;

    private void Awake()
    {
        LoadLevels();
    }

    // Load the levels from a JSON file
    private void LoadLevels()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "levels.json");
        if (File.Exists(path))
        {
            // Read the json file and convert the content into list of levels
            string jsonContent = File.ReadAllText(path);
            levelConfigList = JsonUtility.FromJson<LevelConfigList>(jsonContent);
            Debug.Log("Loaded " + levelConfigList.levels.Length + " levels.");
        }
        else
        {
            Debug.Log("Levels file not found at " + path);
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
