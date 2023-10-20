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
    private Dictionary<string, NPC> _currentNPCList = new Dictionary<string, NPC>();
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
                _currentNPCList.Add(_names[i], _NPCList[i]);
                _NPCList[i].InitNpc(_names[i]);
            }
        }
        AssignNpcPlace();
    }
    public void ChangeNpcState(string _name, string _imotion)
    {
        foreach(var chara in _currentNPCList)
        {
            if(chara.Value.gameObject.name != _name)
            {
                Color newColor = chara.Value.gameObject.GetComponent<SpriteRenderer>().color;
                newColor.a = 0.5f; // 0.5�� ���ϴ� ���� ������ ���� ����
                chara.Value.gameObject.GetComponent<SpriteRenderer>().color = newColor;
            }
            else if(chara.Value.gameObject.name == _name)
            {
                Color newColor = chara.Value.gameObject.GetComponent<SpriteRenderer>().color;
                newColor.a = 1f; // 0.5�� ���ϴ� ���� ������ ���� ����
                chara.Value.gameObject.GetComponent<SpriteRenderer>().color = newColor;
                chara.Value.gameObject.GetComponent<SpriteRenderer>().sprite = GetNPCImotion(_imotion, chara.Value.data);
            }
        }
    }
    public Color GetTextColor(string _name)
    {
        return _currentNPCList[_name].GetTextColorInNPC();
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
                print("���� Ŀ����ǥ���� �������� �ʽ��ϴ�");
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
        int npcCount = _currentNPCList.Count; // NPC�� ��
        int count = 0;
        foreach(var NPC in _currentNPCList)
        {
            // ��ġ ���: Ȧ�� ������ ��� �߾ӿ� �� ��, ¦�� ������ ��� �߾� �翷�� ��ġ
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
            npc.Value.data = null;
        }
        _currentNPCList.Clear();
    }
}
