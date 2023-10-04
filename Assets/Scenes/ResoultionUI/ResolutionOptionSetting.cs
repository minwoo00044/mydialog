using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ػ󵵸� �����ϴ� ����� ���� Ŭ����
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
    /// ui�ʱ�ȭ, �ػ��� ����Ʈ�������� ���� �����ɴϴ�.
    /// </summary>
    void InitUI()
    {
        //1. ��ũ���� �ػ󵵸� ����Ʈ�� ��ü �߰��մϴ�.
        //resolutions.AddRange(Screen.resolutions);
        //Add : �� �ϳ�, AddRange : �迭, ����Ʈ
        for(int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].refreshRateRatio.value > 140)
            {
                resolutions.Add(Screen.resolutions[i]);
            }
        }
        //2. ��� �ٿ ���� �ʱ�ȭ
        dropdown.options.Clear();

        int optionValue = 0;

        //3. ä���ֱ�
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
        //�ɼ� ��� ���� ��, ��� �ٿ��� �ð����� ���¸� ������Ʈ�� �ɼǰ� ��ġ�ϵ��� �����մϴ�.
        dropdown.RefreshShownValue();

        //Ǯ��ũ�� ��忡 ���� ó��
        fullScreenButton.isOn = Screen.fullScreenMode.Equals(FullScreenMode.FullScreenWindow) ? true : false;
    }
    /// <summary>
    /// ��Ӵٿ� �ɼ��� �����ϴ� �޼ҵ� , ��Ӵٿ��� OnValueChanged���� �۾��մϴ�.
    /// </summary>
    /// <param name="value">��ӹڽ��� ����� ���� ���� ǥ��</param>
    public void DropboxOptionChange(int value)
    {
        resolutionIndex = value;
    }
    /// <summary>
    /// ����Ʈ �� �ػ󵵸� �����ϴ� ���
    /// </summary>
    public void OnOKButtonEnter()
    {
        Screen.SetResolution(resolutions[resolutionIndex].width, resolutions[resolutionIndex].height, fullScreenMode);

    }
    /// <summary>
    /// Ǯ ��ũ�� �ػ󵵸� �����ϴ� ���
    /// �޾ƿ� ���� true�� ��� Ǯ ��ũ���� ó��
    /// �޾ƿ� ���� false �� ��� â ��带 ó��.
    /// </summary>
    public void OnFullScreenButton(bool isFull)
    {
        fullScreenMode = isFull ? FullScreenMode.FullScreenWindow: FullScreenMode.Windowed;
    }
}
