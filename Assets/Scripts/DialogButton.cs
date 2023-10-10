using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogButton : MonoBehaviour
{
    public string npcName;
    public int questId = 0;

    [SerializeField] private Button myButton;

    private List<string> _sentnece = new List<string>();

    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => Process());
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
