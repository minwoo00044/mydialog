using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    private Dictionary<int, string> _totalData = new Dictionary<int, string>();
    private void Awake()
    {
        instance = this;

    }
    private void Start()
    {
        _totalData.Add(1000, "\"크아아아아\"\r\n\r\n드래곤중에서도 최강의 투명드래곤이 울부짓었다");
        _totalData.Add(1001, "투명드래곤은 졸라짱쎄서 드래곤중에서 최강이엇다\r\n신이나 마족도 이겼따 다덤벼도 이겼따 투명드래곤은\r\n새상에서 하나였다 어쨌든 걔가 울부짓었다");
        _totalData.Add(1002, "\"으악 제기랄 도망가자\"\r\n\r\n발록들이 도망갔다 투명드래곤이 짱이었따\r\n그래서 발록들은 도망간 것이다\r\n\r\n꼐속");
        _totalData.Add(1003, "후술할 초초초초나 졸라 짱 쎈 등의 수식어들은 모두 여기 나무위키의 유머적 서술이 아닌 투명드래곤 본문에서 발췌한 것이다.");
        _totalData.Add(1103, "후dddddddd dk앟라ㅡㅠㅡㅠㅠㅡㅠㅡㅀ라라라ㅏ.");
    }
    public IEnumerable<KeyValuePair<int, string>> DataProcess(int _id)
    {
        List<KeyValuePair<int, string>> groupedData = new List<KeyValuePair<int, string>>();
        int hundKey = _id / 100;
        foreach (var entry in _totalData)
        {
            if (entry.Key / 100 == hundKey)
            {
                groupedData.Add(entry);
            }
        }
        return groupedData;
    }
}
