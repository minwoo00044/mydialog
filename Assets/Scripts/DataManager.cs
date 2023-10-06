using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private Dictionary<string, int> _nameIDPairData = new Dictionary<string, int>();
    private Dictionary<int, Dictionary<int, string>> _IDSentencePairData = new Dictionary<int, Dictionary<int, string>>();
    private void Awake()
    {
        instance = this;
        InitNameIDPair();
        LoadIDSentencePairData();
    }
    private void Start()
    {

    }
    public IEnumerable<KeyValuePair<int, string>> DataProcess(string _name, int _questId)
    {
        int _id = GetId(_name);
        if (_id == -1)
            return null;
        Dictionary<int, string> _currentDialog = new Dictionary<int, string>(_IDSentencePairData[_id]);
        List<KeyValuePair<int, string>> groupedData = new List<KeyValuePair<int, string>>();
        int hundKey = _questId / 100;
        foreach (var entry in _currentDialog)
        {
            if (entry.Key / 100 == hundKey)
            {
                groupedData.Add(entry);
            }
        }
        return groupedData;
    }
    public void InitNameIDPair()
    {
        //_nameIDPairData.Add("NPC0", 1000);
        //반복문 돌려서 데이터 받아서 저장하자구
        TextAsset data = Resources.Load("NamdIDPairData") as TextAsset;
        string[] dataLines = data.text.Split('\n');

        for (int i = 0; i < dataLines.Length; i++)
        {
            string[] lineData = dataLines[i].Split(',');

            if (lineData.Length < 2) continue; // 데이터가 없는 줄을 건너뜁니다.

            string name = lineData[0];
            if (!int.TryParse(lineData[1], out int id))
            {
                Debug.Log("Invalid ID at line " + (i + 1));
                continue;
            }
            _nameIDPairData[name] = id;
        }
    }
    public int GetId(string _name)
    {
        return (_nameIDPairData.ContainsKey(_name)) ? _nameIDPairData[_name] : -1;
    }
    void LoadIDSentencePairData()
    {
        TextAsset data = Resources.Load("OtherCSV") as TextAsset; // 다른 CSV 파일 로드

        string[] lines = data.text.Split('\n');

        for (int i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(',');

            if (parts.Length < 3) continue; // 라인의 부분이 충분하지 않으면 건너뜀

            var nameKeyPart = parts[0];

            var innerKeyPart = parts[1];

            if (!int.TryParse(innerKeyPart, out int innerKey))
            {
                Debug.Log($"Invalid Inner Key on Line {i + 1}");
                continue;
            }

            var valuePart = parts[parts.Length - 1]; // 마지막 부분을 값으로 사용

            int outerKey = GetId(nameKeyPart);

            if (outerKey == -1)
            {
                Debug.Log($"Invalid Outer Key on Line {i + 1}");
                continue;
            }

            if (!_IDSentencePairData.ContainsKey(outerKey))
            {
                _IDSentencePairData.Add(outerKey, new Dictionary<int, string>());
            }

            _IDSentencePairData[outerKey][innerKey] = valuePart;
        }
    }
}
