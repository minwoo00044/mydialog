using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New NPCData", menuName = "NPCData")]
public class NPCData : ScriptableObject
{
    public string NPCName;
    [Header("==========표정==========")]
    public Sprite nomral_imo;
    public Sprite smile;
    public Sprite angry;
    public Sprite sad;
    [Tooltip("아직 구현 못한 기능")]
    public List<Sprite>customImotion = new List<Sprite>();
}
