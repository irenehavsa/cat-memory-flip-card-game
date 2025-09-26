[System.Serializable]
public class LevelConfig
{
    public int level;
    public string type;
    public int pairs;
    public int steps;
    public int row;
    public int col;
}

public class LevelConfigList
{
    public LevelConfig[] levels;
}
