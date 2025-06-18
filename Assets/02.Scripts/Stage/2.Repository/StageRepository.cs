using System.Collections.Generic;

public class StageRepository
{
    public StageSaveData Load()
    {
        return null;
    }

    public void Save()
    {

    }
}

public class StageSaveData
{
    public int LevelNumber;
    public int SubLevelNumber;
    public float ProgressTime;
    public List<StageLevelSO> Levels;
}
