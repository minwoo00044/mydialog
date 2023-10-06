using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{
    public string npcName;
    public int questId = 0;
    private Dictionary<int, string> _sentnece = new Dictionary<int, string>();

    private void OnMouseDown()
    {
        Process();
    }
    void Process()
    {
        _sentnece = new Dictionary<int, string>(DataManager.instance.DataProcess(npcName, questId));
        if (_sentnece != null)
        {
            print(_sentnece.Count);
            TypingTest.instance.StartDialog(questId, _sentnece.Count, _sentnece);
        }
        else
            Debug.Log("����Ǿ� ���� ���� NPC Name �Դϴ�.");
    }
}
