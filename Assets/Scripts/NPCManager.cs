using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] float distance;
    public static NPCManager instance;
    [SerializeField] GameObject NPCs;
    private Dictionary<string, NPCData> _npcDatas = new Dictionary<string, NPCData>();
    public Dictionary<string, NPCData> npcDatas => _npcDatas;
    private Dictionary<string, NPC> _currentNPCList = new Dictionary<string, NPC>();
    private List<NPC> _NPCList = new List<NPC>();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        LoadNPCData();
        InitNPCList();
    }
    public void ChangeNpcState(string _name, string _imotion)
    {
        foreach(var chara in _currentNPCList)
        {
            if(chara.Value.gameObject.name != _name)
            {
                Color newColor = chara.Value.gameObject.GetComponent<SpriteRenderer>().color;
                newColor.a = 0.5f; // 0.5는 원하는 알파 값으로 변경 가능
                chara.Value.gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
            else if(chara.Value.gameObject.name == _name)
            {
                Color newColor = chara.Value.gameObject.GetComponent<SpriteRenderer>().color;
                newColor.a = 1f; // 0.5는 원하는 알파 값으로 변경 가능
                chara.Value.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                chara.Value.gameObject.GetComponent<SpriteRenderer>().sprite = GetNPCImotion(_imotion, chara.Value.data);
            }
        }
    }
    public void ChangeNPC(List<NPCData> _currentNpcs)
    {
        ResetCurrentNPCList();

        for (int i = 0; i < _currentNpcs.Count; i++)
        {
            _NPCList[i].data = _currentNpcs[i];
            _currentNPCList.Add(_currentNpcs[i].NPCName, _NPCList[i]);
            _NPCList[i].InitNpc(_currentNpcs[i].NPCName);
        }
        AssignNpcPlace();
    }

    public Color GetTextColor(string _name)
    {
        return _currentNPCList[_name].GetTextColorInNPC();
    }
    public NPCData GetNpcData(string _name)
    {
        return _npcDatas.ContainsKey(_name) ? _npcDatas[_name] : _npcDatas["Error"];
    }
    private Sprite GetNPCImotion(string _imotion, NPCData charaData)
    {
        return charaData.imotions.ContainsKey(_imotion) ? charaData.imotions[_imotion] : charaData.imotions.First().Value;
    }
    private void InitNPCList()
    {
        for(int i = 0; i < NPCs.transform.childCount; i++)
        {
            _NPCList.Add(NPCs.GetComponentsInChildren<NPC>()[i]);
        }
    }
    private void LoadNPCData()
    {
        NPCData[] resources = Resources.LoadAll<NPCData>("NPCData");
        foreach (NPCData resource in resources)
        {
            _npcDatas.Add(resource.NPCName, resource);
        }
    }
    private void AssignNpcPlace()
    {
        int npcCount = _currentNPCList.Count; // NPC의 수
        int count = 0;
        foreach(var NPC in _currentNPCList)
        {
            // 위치 계산: 홀수 개수의 경우 중앙에 한 개, 짝수 개수의 경우 중앙 양옆에 배치
            float positionX = (count - npcCount / 2) * distance;
            if (npcCount % 2 == 0)
                positionX += distance / 2;
            Vector3 newPosition = new Vector3(positionX, Camera.main.transform.position.y, Camera.main.transform.position.z + distance);
            NPC.Value.transform.position = newPosition;
            count++;
        }
    }
    private void ResetCurrentNPCList()
    {
        foreach(var npc in _currentNPCList)
        {
            npc.Value.ResetNpc();
            npc.Value.data = null;
        }
        _currentNPCList.Clear();
    }
}
