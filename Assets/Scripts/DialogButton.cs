using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogButton : MonoBehaviour
{
    public int npcId;
    public int QuestId = 0;

    [SerializeField]private Button myButton;

    private Dictionary<int, string> _sentnece = new Dictionary<int, string>();

    void Start()
    {
        myButton = GetComponent<Button>();
        myButton.onClick.AddListener(() => Process(npcId, QuestId));
    }

    void Process(int _npcId, int questId)
    {
        _sentnece =  new Dictionary<int, string>(DataManager.instance.DataProcess(_npcId + questId));
        TypingTest.instance.StartDialog(_npcId, questId, _sentnece.Count, _sentnece);
    }
}
