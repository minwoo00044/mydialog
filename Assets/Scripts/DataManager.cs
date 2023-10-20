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

            if (lineData.Length < 2) continue; // 데이터가 없는 줄을 건너뜁니다.

            string name = lineData[0];
            try
            {
                string[] rgbString = lineData[1].Split(' ');
                Color senteceColor = new Color32(byte.Parse(rgbString[0]), byte.Parse(rgbString[1]), byte.Parse(rgbString[2]), 255);
                _nameColorPairData[name] = senteceColor;
            }
            catch (FormatException)
            {
                Debug.Log("컬러값이 적혀있지 않습니다. " + (i + 1));
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
    //    TextAsset data = Resources.Load("OtherCSV") as TextAsset; // 다른 CSV 파일 로드

    //    string[] lines = data.text.Split('\n');

    //    for (int i = 0; i < lines.Length; i++)
    //    {
    //        var parts = lines[i].Split(',');

    //        if (parts.Length < 3) continue; // 라인의 부분이 충분하지 않으면 건너뜀

    //        var nameKeyPart = parts[0];

    //        var innerKeyPart = parts[1];

    //        if (!int.TryParse(innerKeyPart, out int innerKey))
    //        {
    //            Debug.Log($"Invalid Inner Key on Line {i + 1}");
    //            continue;
    //        }

    //        var valuePart = parts[parts.Length - 1]; // 마지막 부분을 값으로 사용

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
//    //반복문 돌려서 데이터 받아서 저장하자구
//    TextAsset data = Resources.Load("NamdIDPairData") as TextAsset;
//    string[] dataLines = data.text.Split('\n');

//    for (int i = 0; i < dataLines.Length; i++)
//    {
//        string[] lineData = dataLines[i].Split(',');

//        if (lineData.Length < 2) continue; // 데이터가 없는 줄을 건너뜁니다.

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
