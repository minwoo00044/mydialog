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
        _totalData.Add(1000, "\"ũ�ƾƾƾ�\"\r\n\r\n�巡���߿����� �ְ��� ����巡���� ���������");
        _totalData.Add(1001, "����巡���� ����¯�꼭 �巡���߿��� �ְ��̾���\r\n���̳� ������ �̰�� �ٴ����� �̰�� ����巡����\r\n���󿡼� �ϳ����� ��·�� �°� ���������");
        _totalData.Add(1002, "\"���� ����� ��������\"\r\n\r\n�߷ϵ��� �������� ����巡���� ¯�̾���\r\n�׷��� �߷ϵ��� ������ ���̴�\r\n\r\n����");
        _totalData.Add(1003, "�ļ��� �������ʳ� ���� ¯ �� ���� ���ľ���� ��� ���� ������Ű�� ������ ������ �ƴ� ����巡�� �������� ������ ���̴�.");
        _totalData.Add(1103, "��dddddddd dk�۶�ѤФѤФФѤФѤ�����.");
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
