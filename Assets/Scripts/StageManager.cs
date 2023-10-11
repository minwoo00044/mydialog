using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public int stageIndex = -1;
    public Stage currentStage;

    [SerializeField] List<Stage> stages;

    private void Awake()
    {
        instance = this;
        foreach (var stage in stages)
        {
            stage.LoadDialogs();
        }
    }
    public List<string> GetDialog() => currentStage.GetDialogInStage();

    public void NextStage()
    {
        stageIndex++;
        currentStage = stages[stageIndex];
    }
}
