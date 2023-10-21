using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New NPCData", menuName = "NPCData")]
public class NPCData : ScriptableObject
{
    public string NPCName;
    public Color txtColor;
    [Header("==========표정 설정, 각 변수 툴팁 참고==========")]
    [Header("==========이름과 표정은 1대1 대응이 원칙==========")]
    [Tooltip("표정의 이름의 수까지 반복해서 표정에 추가됨 ")]
    [SerializeField] List<string> NPCImotionNames = new List<string>();
    [Tooltip("이름 리스트가 넘어가는 표정은 0번 표정으로 지정")]
    [SerializeField] List<Sprite> imotionSprites = new List<Sprite>();
    public Dictionary<string, Sprite> imotions = new Dictionary<string, Sprite>();


    public int test;
    private void OnEnable()
    {
        if (imotions.Count > 0)
            return;
        for (int i = 0; i < NPCImotionNames.Count; i++)
        {
            try
            {
                imotions.Add(NPCImotionNames[i], imotionSprites[i]);
            }
            catch (ArgumentOutOfRangeException)
            {
                imotions.Add(NPCImotionNames[i], imotionSprites[0]);
            }
        }
    }

}
