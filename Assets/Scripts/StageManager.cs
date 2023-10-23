using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;
    public int stageIndex = -1;
    public Stage currentStage;
    private Branch currentBranchInCurrentStage;

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
        stageIndex++;
        currentStage = stages[stageIndex];
        SetBranch(_branchName);
        NPCManager.instance.ChangeNPC(currentBranchInCurrentStage.actorNpcList);
    }
    public List<string> GetDialog() => currentStage.GetDialogInStage(currentBranchInCurrentStage);
    private void SetBranch(string _branchName)
    {
        currentBranchInCurrentStage = currentStage.GetCurrentBranch(_branchName);
    }
}
