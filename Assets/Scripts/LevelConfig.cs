[System.Serializable]
public class LevelConfig
{
    public int level;
    public int numberOfPairs;
    public int availableSteps;
    public int numberOfColumns;
}

public class LevelConfigList
{
    public LevelConfig[] levels;
}
