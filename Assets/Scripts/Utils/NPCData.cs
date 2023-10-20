using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New NPCData", menuName = "NPCData")]
public class NPCData : ScriptableObject
{
    public string NPCName;
    [Header("==========ǥ��==========")]
    public Sprite nomral_imo;
    public Sprite smile;
    public Sprite angry;
    public Sprite sad;
    [Tooltip("���� ���� ���� ���")]
    public List<Sprite>customImotion = new List<Sprite>();
}
