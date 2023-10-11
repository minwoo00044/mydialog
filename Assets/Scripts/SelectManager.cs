using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;

    [SerializeField] GameObject SelectCanvas;
    private Dictionary<int, string> _selectData;
    private void Awake()
    {
        instance = this;
    }
    private void LoadSelectData()
    {
        TextAsset data = Resources.Load("SelectData") as TextAsset;
        string[] lines = data.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');

            if (parts.Length < 3) continue; // ������ �κ��� ������� ������ �ǳʶ�

            var branchKeyPart = parts[0];
            var charaKeyPart = parts[1];
            var sentenceKeyPart = parts[2];
        }
    }
    public void OnSelectBtn()
    {
        SelectCanvas.SetActive(true);
    }

}
