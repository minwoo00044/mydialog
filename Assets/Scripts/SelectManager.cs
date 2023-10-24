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
    }
    public void OnSelectBtn()
    {
        SelectCanvas.SetActive(true);
    }

}
