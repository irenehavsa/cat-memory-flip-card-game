using System.IO;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public static MainManager instance; // supaya bisa diakses dari scene apapun

    // Variables yang persistent
    public int currentLevel = 1;
    public int coins = 0;
    public int hearts = -1;

    // Yang akan diprepare sebelum segala yg ada di Start() di berbagai script mulai dijalanin
    private void Awake()
    {
        // Siapin instance MainManager yg singleton. Kalau udah instance ada (static) MainManager, game object yg ini lgsg destroy sendiri.
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); // Supaya ga ilang saat pindah scene

        LoadData();
    }

    // Siapin class untuk siapin data-data yg perlu disimpan
    [System.Serializable]
    class PlayerData
    {
        public int currentLevel;
        public int coins;
        public int hearts;
    }

    // Method untuk save data
    public void SaveData()
    {
        PlayerData data = new PlayerData();
        data.currentLevel = currentLevel;
        data.coins = coins;
        data.hearts = hearts;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/playerdata.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/playerdata.json";
        if (File.Exists(path))
        {
            // Read data from file, load to PlayerData
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            // Isi ke variabel yg bersangkutan
            currentLevel = data.currentLevel;
            coins = data.coins;
            hearts = data.hearts;
        }
    }
}
