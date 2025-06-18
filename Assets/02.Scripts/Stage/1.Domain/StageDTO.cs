using System.Collections.Generic;

public class StageDTO
{
    public readonly int LevelNumber;
    public int SubLevelNumber => _currentLevel.CurrentLevel;

    private readonly StageLevel _currentLevel;

    // ToDO: StageLevelDTO
    public  StageLevel CurrentLevel => _currentLevel;

    private readonly float _progressTime;

    public readonly List<StageLevel> Levels;

    public StageDTO(Stage stage)
    {
        LevelNumber = stage.LevelNumber;
        _progressTime = stage.ProgressTime;
        Levels = new List<StageLevel>(stage.Levels);
        _currentLevel = Levels[LevelNumber - 1];
    }
}
