using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] float distance;
    public static NPCManager instance;
    [SerializeField] GameObject NPCs;
    private Dictionary<string, NPCData> npcDatas = new Dictionary<string, NPCData>();
    private List<NPC> _currentNPCList = new List<NPC>();
    private List<NPC> _NPCList = new List<NPC>();

    private void Awake()
    {
        instance = this;
        LoadNPCData();
        InitNPCList();
    }
    public void ChangeNPC(string[] _names)
    {
        ResetCurrentNPCList();
        for (int i = 0; i < _names.Length; i++)
        {
            if (npcDatas.ContainsKey(_names[i]))
            {
                _NPCList[i].data = npcDatas[_names[i]];
                _currentNPCList.Add(_NPCList[i]);
                _NPCList[i].InitNpc(_names[i]);
            }
        }
        AssignNpcPlace();
    }
    public void ChangeNpcState(string _name, string _imotion)
    {
        foreach(var chara in _currentNPCList)
        {
            if(chara.gameObject.name != _name)
            {
                Color newColor = chara.gameObject.GetComponent<SpriteRenderer>().color;
                newColor.a = 0.5f; // 0.5는 원하는 알파 값으로 변경 가능
                chara.gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
            else if(chara.gameObject.name == _name)
            {
                Color newColor = chara.gameObject.GetComponent<SpriteRenderer>().color;
                newColor.a = 1f; // 0.5는 원하는 알파 값으로 변경 가능
                chara.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                chara.gameObject.GetComponent<SpriteRenderer>().sprite = GetNPCImotion(_imotion, chara.data);
            }
        }
    }
    private Sprite GetNPCImotion(string _imotion, NPCData charaData)
    {
        switch(_imotion)
        {
            case "normal":
                return charaData.nomral_imo;
            case "smile":
                return charaData.smile;
            case "angry":
                return charaData.angry;
            case "sad":
                return charaData.sad;
            default:
                print("아직 커스텀표정은 지원되지 않습니다");
                return charaData.nomral_imo;
        }
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
            npcDatas.Add(resource.NPCName, resource);
        }
    }
    private void AssignNpcPlace()
    {
        int npcCount = _currentNPCList.Count; // NPC의 수

        for (int i = 0; i < npcCount; i++)
        {
            // 위치 계산: 홀수 개수의 경우 중앙에 한 개, 짝수 개수의 경우 중앙 양옆에 배치
            float positionX = (i - npcCount / 2) * distance;
            if (npcCount % 2 == 0)
                positionX += distance / 2;

            Vector3 newPosition = new Vector3(positionX, Camera.main.transform.position.y, Camera.main.transform.position.z + distance);

            _currentNPCList[i].transform.position = newPosition;
        }
    }
    private void ResetCurrentNPCList()
    {
        foreach(var npc in _currentNPCList)
        {
            npc.data = null;
        }
        _currentNPCList.Clear();
    }
}
