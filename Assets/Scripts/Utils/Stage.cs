using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Branch
{
    public string branchName;
    public List<string> dialogs = new List<string>();
    public Branch(string _name="default")
    {
        branchName = _name;
    }
    public List<NPCData> actorNpcList = new List<NPCData>(); 
}
[Serializable]
public class AutoSelectedBranch
{
    [SerializeField] private string _targetBranchName;
    [SerializeField] List<CheckingData> checkingDatas;

    public string TargetBranchName { get => _targetBranchName; set => _targetBranchName = value; }
    public List<CheckingData> CheckingDatas { get => checkingDatas; set => checkingDatas = value; }

    public bool ConditionCheck()
    {
        int count = 1;
        for (int i = 0; i < checkingDatas.Count; i++)
        {
            if (checkingDatas[i].CheckingNpc is null)
            {
                checkingDatas.RemoveAt(i);
                continue;
            }
            int conditionCheckCount = 1;
            string currentNpcName = checkingDatas[i].CheckingNpc.NPCName;
            for(int j = 0; j< checkingDatas[i].TargetStates.Count; j++)
            {
                if (StateManager.instance.StatesAtNpc[currentNpcName][checkingDatas[i].TargetStates[j]] == checkingDatas[i].Amounts[j])
                    conditionCheckCount++;
            }
            if (conditionCheckCount >= checkingDatas[i].TargetStates.Count)
                count++;
        }
        return count >= checkingDatas.Count;
    }
}
[Serializable]
public class CheckingData
{
    [SerializeField] NPCData checkingNpc;
    [Header("state와 amount를 1대1 대응 시키는 것을 추천")]
    [SerializeField] List<NpcState> targetStates;
    [SerializeField] List<int> amounts;

    public NPCData CheckingNpc { get => checkingNpc; set => checkingNpc = value; }
    public List<NpcState> TargetStates { get => targetStates; set => targetStates = value; }
    public List<int> Amounts { get => amounts; set => amounts = value; }
}
[CreateAssetMenu(fileName = "New Stage", menuName = "Stage")]
public class Stage : ScriptableObject
{
    [SerializeField] int branchIndex;
    [SerializeField] string[] branchNames;
    [SerializeField] int stageNum;
    [Header("조건에 맞춰서 자동으로 연결될 브랜치가 없다면 비워둘 것")]
    [SerializeField] List<AutoSelectedBranch> autoSelectedBranches;
    Dictionary<string, Branch> branches = new Dictionary<string, Branch>();
    public Sprite image;
    public bool isNonSelectStage = false;

    public List<AutoSelectedBranch> AutoSelectedBranches { get => autoSelectedBranches; private set => autoSelectedBranches = value; }

    public List<string> GetDialogInStage(Branch _branch) => _branch.dialogs;
    public Branch GetBranchOnName(string _branchName)
    {
        return branches.ContainsKey(_branchName) ? branches[_branchName] : branches["default"];
    }
    public void LoadDialogs()
    {
        TextAsset data = Resources.Load("Stage" + stageNum.ToString()) as TextAsset;
        string[] lines = data.text.Split('\n');

        branches.Clear();
        branches.Add("default", new Branch("default"));
        for (int i = 0; i < branchNames.Length; i++)
        {
            branches.Add(branchNames[i], new Branch(branchNames[i]));
        }
        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');

            if (parts.Length < 4) continue; // 라인의 부분이 충분하지 않으면 건너뜀

            var branchKeyPart = parts[0];
            var charaKeyPart = parts[1];
            var imotionKeyPart = parts[2];
            var sentenceKeyPart = parts[3];

            if (branches.TryGetValue(branchKeyPart, out Branch currentBranch))
            {
                currentBranch = branches[branchKeyPart];
                string dialogEntry = charaKeyPart + '&' + sentenceKeyPart + '&' + imotionKeyPart;
                currentBranch.dialogs.Add(dialogEntry);
                currentBranch.actorNpcList.Add(NPCManager.instance.GetNpcData(charaKeyPart));
            }
            //else
            //{
            //    Debug.LogWarning("Branch " + branchKeyPart + " not found.");
            //}
        }
        foreach(Branch branch in branches.Values)
        {
            branch.actorNpcList = branch.actorNpcList.Distinct().ToList();
        }
    }


}
