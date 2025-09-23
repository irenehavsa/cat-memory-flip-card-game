[System.Serializable]
public class LevelConfig
{
    public int level;
    public int numberOfPairs;
    public int availableSteps;
    public int row;
    public int col;
}

public class LevelConfigList
{
    public LevelConfig[] levels;
}
