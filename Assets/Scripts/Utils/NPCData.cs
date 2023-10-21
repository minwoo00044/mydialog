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
    [Header("==========Ç¥Á¤==========")]
    [SerializeField] List<string> NPCImotionNames = new List<string>();
    [SerializeField] List<Sprite> imotionSprites = new List<Sprite>();
    public Dictionary<string, Sprite> imotions = new Dictionary<string, Sprite>();
    private string defalutImotionName = "default";


    public int test;
    private void OnEnable()
    {
        imotions.Clear();
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
