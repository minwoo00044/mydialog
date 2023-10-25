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
[CreateAssetMenu(fileName = "New Stage", menuName = "Stage")]
public class Stage : ScriptableObject
{
    [SerializeField] int branchIndex;
    [SerializeField] string[] branchNames;
    [SerializeField] int stageNum;
    Dictionary<string, Branch> branches = new Dictionary<string, Branch>();
    public Sprite image;
    public bool isNonSelectStage = false;

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
