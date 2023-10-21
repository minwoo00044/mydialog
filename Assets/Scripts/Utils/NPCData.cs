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
    [Header("==========ǥ�� ����, �� ���� ���� ����==========")]
    [Header("==========�̸��� ǥ���� 1��1 ������ ��Ģ==========")]
    [Tooltip("ǥ���� �̸��� ������ �ݺ��ؼ� ǥ���� �߰��� ")]
    [SerializeField] List<string> NPCImotionNames = new List<string>();
    [Tooltip("�̸� ����Ʈ�� �Ѿ�� ǥ���� 0�� ǥ������ ����")]
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
