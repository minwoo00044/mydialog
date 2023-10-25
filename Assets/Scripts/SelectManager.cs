using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    private void Awake()
    {
        if (instance == null)
            instance = this;
        LoadSelectData();
        btns = btnPanel.transform.GetComponentsInChildren<Button>();
        ResetSelect();
    }

    private void ResetSelect()
    {
        foreach (var item in btns)
        {
            item.GetComponentInChildren<TMP_Text>().text = "";
            item.onClick.RemoveAllListeners();
            item.gameObject.SetActive(false);
        }
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
        
        if (!SelectCanvas.activeInHierarchy) //킬때
        {
            SelectCanvas.SetActive(!SelectCanvas.activeInHierarchy);
            SettingSelectCanvas();
        }
        else //끌때
        {
            ResetSelect();
            SelectCanvas.SetActive(!SelectCanvas.activeInHierarchy);
        }
            
    }

    private void SettingSelectCanvas()
    {
        List<Choice> currentSelect = _selectData[StageManager.instance.currentStage][StageManager.instance.currentBranch].choices;
        for (int i = 0; i < currentSelect.Count; i++)
        {
            int index = i;
            btns[i].gameObject.SetActive(true);
            btns[index].GetComponentInChildren<TMP_Text>().text = currentSelect[index].choiceTxt;
            btns[index].onClick.AddListener(() => currentSelect[index].Execute());
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
            btns[i].onClick.AddListener(() => currentSelect[index].Execute()); // 복사한 인덱스를 사용
        }

    }
}
