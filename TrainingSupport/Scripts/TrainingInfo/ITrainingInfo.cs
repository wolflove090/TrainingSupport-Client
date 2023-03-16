public interface ITrainingInfo
{
    int IntervalTimeSeconds();

    TrainingDetail[] GetTrainingList();
}