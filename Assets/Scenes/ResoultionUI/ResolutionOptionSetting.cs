using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 해상도를 설정하는 기능을 가진 클래스
/// </summary>
public class ResolutionOptionSetting : MonoBehaviour
{
    #region public Fields
    public Dropdown dropdown;
    public Toggle fullScreenButton;
    public int resolutionIndex;
    #endregion

    #region private Fields
    List<Resolution> resolutions = new List<Resolution>();
    FullScreenMode fullScreenMode;
    private bool isFullscreen = false;
    #endregion

    private void Start()
    {
        InitUI();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleFullscreen();
        }
    }

    private void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;
        Screen.fullScreen = isFullscreen;
    }
    /// <summary>
    /// ui초기화, 해상도의 리스트에서부터 값을 가져옵니다.
    /// </summary>
    void InitUI()
    {
        //1. 스크린의 해상도를 리스트에 전체 추가합니다.
        //resolutions.AddRange(Screen.resolutions);
        //Add : 값 하나, AddRange : 배열, 리스트
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRateRatio.value > 140)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        //2. 드롭 다운에 대한 초기화
        dropdown.options.Clear();

        int optionValue = 0;

        //3. 채워넣기
        foreach (Resolution resolution in resolutions)
        {
            Dropdown.OptionData optionData = new Dropdown.OptionData();
            optionData.text = $"{resolution.width} X {resolution.height} : {resolution.refreshRateRatio} hz";
            dropdown.options.Add(optionData);

            if(resolution.width == Screen.width && resolution.height == Screen.height)
            {
                dropdown.value = optionValue;
            }
            optionValue++;
        }
        //옵션 목록 수정 후, 드롭 다운의 시각적인 상태를 업데이트한 옵션과 일치하도록 설정합니다.
        dropdown.RefreshShownValue();

        //풀스크린 모드에 대한 처리
        fullScreenButton.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
    /// <summary>
    /// 드롭다운 옵션을 변경하는 메소드 , 드롭다운의 OnValueChanged에서 작업합니다.
    /// </summary>
    /// <param name="value">드롭박스에 저장된 값에 대한 표현</param>
    public void DropboxOptionChange(int value)
    {
        resolutionIndex = value;
    }
    /// <summary>
    /// 리스트 안 해상도를 선택하는 기능
    /// </summary>
    public void OnOKButtonEnter()
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, fullScreenMode);

    }
    /// <summary>
    /// 풀 스크린 해상도를 선택하는 기능
    /// 받아온 값이 true일 경우 풀 스크린을 처리
    /// 받아온 값이 false 일 경우 창 모드를 처리.
    /// </summary>
    public void OnFullScreenButton(bool isFull)
    {
        fullScreenMode = isFull ? FullScreenMode.FullScreenWindow: FullScreenMode.Windowed;
    }
}
