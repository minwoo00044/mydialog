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
    
    Dictionary<string, Dictionary<NpcState, int>> statesAtNpc = new Dictionary<string, Dictionary<NpcState, int>>();
    private void Awake()
    {
        //foreach (var npcData in NPCManager.instance.npcDatas)
        //{
        //    foreach (NpcState state in Enum.GetValues(typeof(NpcState)))
        //    {
        //        Dictionary<NpcState, int> newDic = new Dictionary<NpcState, int>({ });
        //    }
        //    Dictionary<NpcState, int> statesAmount;
        //    statesAtNpc.Add(npcData.Key, NpcState);
        //}
    }
}
