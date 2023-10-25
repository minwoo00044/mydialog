using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NpcState
{
    Love,
    Angry,
    Happy
}
public class StateManager : MonoBehaviour
{
    public static StateManager instance;
    private Dictionary<string, Dictionary<NpcState, int>> _statesAtNpc = new Dictionary<string, Dictionary<NpcState, int>>();
    private void Awake()
    {
        if(instance == null)
            instance = this;
        CreateNpcStateTable();
    }

    public void SetNpcState(string targetName, List<NpcState> targetState, List<int> changeAmount)
    {
        Dictionary<NpcState, int> current = _statesAtNpc[targetName];
        for(int i = 0; i < targetState.Count; i++)
        {
            if (current.ContainsKey(targetState[i]))
            {
                current[targetState[i]] += current[targetState[i]] + changeAmount[i];
            }
        }
    }

    private void CreateNpcStateTable()
    {
        foreach (var npcData in NPCManager.instance.npcDatas)
        {
            int idx = 0;
            Dictionary<NpcState, int> newDic = new Dictionary<NpcState, int>();
            newDic.Clear();
            foreach (NpcState state in Enum.GetValues(typeof(NpcState)))
            {
                newDic.Add((NpcState)idx, 0);
                idx++;
            }
            _statesAtNpc.Add(npcData.Value.NPCName, newDic);
        }
        //foreach(var item in statesAtNpc)
        //{
        //    foreach(var val in item.Value)
        //    {
        //        print($"Owner : {item.Key} / State : {val.Key} / Amount : {val.Value}");
        //    }
        //}
    }
}
