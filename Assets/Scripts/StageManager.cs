using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public int stageIndex;

    [SerializeField] List<Stage> stages;
    [SerializeField] Stage currentStage;

    private void Awake()
    {
        instance = this;
        foreach (var stage in stages) 
        {
            stage.LoadDialogs();
        }
    }
    public List<string> GetDialog() => currentStage.GetDialogInStage();

    public void SetStage(int _index)
    {
        stageIndex = _index;
        currentStage = stages[stageIndex];
    }
}
