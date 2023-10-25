using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public static SelectManager instance;

    [SerializeField] GameObject SelectCanvas;
    [SerializeField] GameObject btnPanel;
    private Button[] btns;
    private Dictionary<Stage, Dictionary<Branch, Select>> _selectData = new Dictionary<Stage, Dictionary<Branch, Select>>();

    public Stage test;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        LoadSelectData();
        btns = btnPanel.transform.GetComponentsInChildren<Button>();
        foreach(var item in btns)
        {
            item.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        SettingSelectCanvas(test);
    }
    private void LoadSelectData()
    {
        Select[] resources = Resources.LoadAll<Select>("SelectData");
        foreach (var resource in resources)
        {
            Branch attachedBranch = resource.attachedStage.GetBranchOnName(resource.attachedBranchName);
            _selectData.Add(resource.attachedStage, new Dictionary<Branch, Select>
            {
                { attachedBranch, resource}
            });
        }
    }
    public void ToggleSelectBtn()
    {
        SelectCanvas.SetActive(!SelectCanvas.activeInHierarchy);
        //if (SelectCanvas.activeInHierarchy)
        //    SettingSelectCanvas();
    }

    private void SettingSelectCanvas()
    {
        List<Choice> currentSelect = _selectData[StageManager.instance.currentStage][StageManager.instance.currentBranch].choices;
        for (int i = 0; i < currentSelect.Count; i++)
        {
            btns[i].gameObject.SetActive(true);
            btns[i].onClick.AddListener(() => currentSelect[i].Execute());
        }
    }
    private void SettingSelectCanvas(Stage test)
    {
        List<Choice> currentSelect = _selectData[test][test.GetBranchOnName("default")].choices;
        print(currentSelect.Count);
        for (int i = 0; i < currentSelect.Count; i++)
        {
            int index = i; // 현재 인덱스를 복사
            btns[i].gameObject.SetActive(true);
            print($"{btns[i].name} \n");
            print($"{currentSelect[i].choiceTxt} \n");
            btns[i].onClick.AddListener(() => currentSelect[index].Execute()); // 복사한 인덱스를 사용
        }

    }
}
