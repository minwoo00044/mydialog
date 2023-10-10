using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialog : MonoBehaviour
{ 
    private List<string> _sentnece = new List<string>();

    private void OnMouseDown()
    {
        Process();
    }
    void Process()
    {
        _sentnece = new List<string>(StageManager.instance.GetDialog());
        if (_sentnece != null)
        {
            TypingTest.instance.StartDialog(_sentnece.Count, _sentnece);
        }
        else
            Debug.Log("저장되어 있지 않은 NPC Name 입니다.");
    }
}
