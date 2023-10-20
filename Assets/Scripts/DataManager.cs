using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class ColorManager : MonoBehaviour
{
    public static ColorManager instance;
    //private Dictionary<string, int> _nameIDPairData = new Dictionary<string, int>();
    private Dictionary<string, Color> _nameColorPairData = new Dictionary<string, Color>();
    private void Awake()
    {
        instance = this;
        LoadNameColorPair();
    }
    private void Start()
    {

    }
    //public IEnumerable<KeyValuePair<int, string>> DataProcess(string _name, int _questId)
    //{
    //    //Dictionary<int, string> _currentDialog = new Dictionary<int, string>(_IDSentencePairData[_name]);
    //    List<KeyValuePair<int, string>> groupedData = new List<KeyValuePair<int, string>>();
    //    int hundKey = _questId / 100;
    //    foreach (var entry in _currentDialog)
    //    {
    //        if (entry.Key / 100 == hundKey)
    //        {
    //            groupedData.Add(entry);
    //        }
    //    }
    //    return groupedData;
    //    return null;
    //}

    public void LoadNameColorPair()
    {
        TextAsset data = Resources.Load("NameColorPairData") as TextAsset;
        string[] dataLines = data.text.Split('\n');
        for (int i = 0; i < dataLines.Length; i++)
        {
            string[] lineData = dataLines[i].Split(',');

            if (lineData.Length < 2) continue; // �����Ͱ� ���� ���� �ǳʶݴϴ�.

            string name = lineData[0];
            try
            {
                string[] rgbString = lineData[1].Split(' ');
                Color senteceColor = new Color32(byte.Parse(rgbString[0]), byte.Parse(rgbString[1]), byte.Parse(rgbString[2]), 255);
                _nameColorPairData[name] = senteceColor;
            }
            catch (FormatException)
            {
                Debug.Log("�÷����� �������� �ʽ��ϴ�. " + (i + 1));
                continue;
            }
        }
    }
    public Color GetTextColor(string _name)
    {
        if (_nameColorPairData.TryGetValue(_name, out Color color))
        {
            print(color);
            return color;
        }
        else
        {
            print("!");
            return Color.black;
        }
    }

}
    //private Dictionary<string, Dictionary<int, string>> _IDSentencePairData = new Dictionary<string, Dictionary<int, string>>();
    //void LoadIDSentencePairData()
    //{
    //    TextAsset data = Resources.Load("OtherCSV") as TextAsset; // �ٸ� CSV ���� �ε�

    //    string[] lines = data.text.Split('\n');

    //    for (int i = 0; i < lines.Length; i++)
    //    {
    //        var parts = lines[i].Split(',');

    //        if (parts.Length < 3) continue; // ������ �κ��� ������� ������ �ǳʶ�

    //        var nameKeyPart = parts[0];

    //        var innerKeyPart = parts[1];

    //        if (!int.TryParse(innerKeyPart, out int innerKey))
    //        {
    //            Debug.Log($"Invalid Inner Key on Line {i + 1}");
    //            continue;
    //        }

    //        var valuePart = parts[parts.Length - 1]; // ������ �κ��� ������ ���

    //        string outerKey = nameKeyPart;

    //        if (outerKey == null)
    //        {
    //            Debug.Log($"Invalid Outer Key on Line {i + 1}");
    //            continue;
    //        }

    //        if (!_IDSentencePairData.ContainsKey(outerKey))
    //        {
    //            _IDSentencePairData.Add(outerKey, new Dictionary<int, string>());
    //        }

    //        _IDSentencePairData[outerKey][innerKey] = valuePart;
    //    }
    //}
//public void InitNameIDPair()
//{
//    //_nameIDPairData.Add("NPC0", 1000);
//    //�ݺ��� ������ ������ �޾Ƽ� �������ڱ�
//    TextAsset data = Resources.Load("NamdIDPairData") as TextAsset;
//    string[] dataLines = data.text.Split('\n');

//    for (int i = 0; i < dataLines.Length; i++)
//    {
//        string[] lineData = dataLines[i].Split(',');

//        if (lineData.Length < 2) continue; // �����Ͱ� ���� ���� �ǳʶݴϴ�.

//        string name = lineData[0];
//        if (!int.TryParse(lineData[1], out int id))
//        {
//            Debug.Log("Invalid ID at line " + (i + 1));
//            continue;
//        }
//        _nameIDPairData[name] = id;
//    }
//}
//public int GetId(string _name)
//{
//    return (_nameIDPairData.ContainsKey(_name)) ? _nameIDPairData[_name] : -1;
//}
