using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public int stageIndex = -1;
    public Stage currentStage;
    private Branch _currentBranchInCurrentStage;
    public Branch currentBranch => _currentBranchInCurrentStage;

    [SerializeField] List<Stage> stages;

    private void Awake()
    {
        instance = this;
        foreach (var stage in stages)
        {
            stage.LoadDialogs();
        }
    }
    public void NextStage(string _branchName = "default")
    {
        if (stageIndex == stages.Count - 1)
            return;
        stageIndex++;
        currentStage = stages[stageIndex];
        SetBranch(_branchName);
        NPCManager.instance.ChangeNPC(_currentBranchInCurrentStage.actorNpcList);
        GameManager.instance.DialogProcess();
    }
    public List<string> GetDialog() => currentStage.GetDialogInStage(_currentBranchInCurrentStage);
    private void SetBranch(string _branchName)
    {
        if(currentStage.AutoSelectedBranches.Count > 0)
        {
            foreach(var autoBranch in currentStage.AutoSelectedBranches)
            {
                if (autoBranch.ConditionCheck())
                {
                    _branchName = autoBranch.TargetBranchName;
                    break;
                }
            }
        }
        _currentBranchInCurrentStage = currentStage.GetBranchOnName(_branchName);
    }
}
