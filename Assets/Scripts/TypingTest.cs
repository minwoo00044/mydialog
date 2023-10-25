using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class TypingTest : MonoBehaviour
{
    public static TypingTest instance;
    public Text text;
    public Slider txtSpeedSlider;

    [SerializeField] int index;
    [SerializeField]private int _datasCount;
    [SerializeField] private float txtReadingTime;
    [SerializeField] private float txtSpeed = 1f;
    [SerializeField] private float txtSpeedAccel = 3f;
    [SerializeField] private float maxReadingTime;
    [SerializeField] private float minReadingTime;
    private WaitForSeconds waitForSeconds;
    private Tweener _currentTween;

    IEnumerator _currentCoroutine;


    private List<string> _currentDialog = new List<string>();
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DOTween.Init();
        waitForSeconds = new WaitForSeconds(txtReadingTime + 0.5f);
    }
    public void StartDialog(int sentenceCount, List<string> datas_origin)
    {
        KillTweenAndStopCoroutine();

        text.text = string.Empty;
        _currentDialog.Clear();
        _currentDialog = datas_origin;
        index = 0;
        _datasCount = sentenceCount;

        print(_currentDialog[index]);
        _currentCoroutine = DialogRead(_currentDialog[index]);
        StartCoroutine(_currentCoroutine);
    }
    IEnumerator DialogRead(string data)
    {
        print(data);
        string[] colorAndSentence = data.Split('&');
        text.color = NPCManager.instance.GetTextColor(colorAndSentence[0]);
        NPCManager.instance.ChangeNpcState(colorAndSentence[0], colorAndSentence[2]);


        _currentTween = text.DOText(colorAndSentence[1], SetTxtReadingTime(colorAndSentence[1]));
        yield return waitForSeconds;
        NextDialog();
    }

    private float SetTxtReadingTime(string Sentence)
    {
        txtReadingTime = Sentence.Length / (txtSpeed * txtSpeedAccel);
        txtReadingTime = Mathf.Clamp(txtReadingTime, minReadingTime, maxReadingTime);
        waitForSeconds = new WaitForSeconds(txtReadingTime + 0.5f);

        return txtReadingTime;
    }

    private void NextDialog()
    {
        text.text = string.Empty;
        if (index >= _datasCount -1 )
        {
            if(!StageManager.instance.currentStage.isNonSelectStage)
                SelectManager.instance.ToggleSelectBtn();
            return;
        }
        index++;
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = DialogRead(_currentDialog[index]);
            StartCoroutine(_currentCoroutine);
        }
    }

    public void ChangeSpeed()
    {
        txtSpeed = txtSpeedSlider.value;
        if (_currentTween != null && _currentTween.IsActive() && !_currentTween.IsComplete())
        {
            string currentText = text.text; // 현재 텍스트 스냅샷
            _currentTween?.Kill(); // 현재 트윈 종료 체인지 스피드만 텍스트를 비우지 않기 때문에 예외적으로 처리
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            float remainingRatio = (float)(_currentDialog[index].Split('&')[1].Length - currentText.Length) / _currentDialog[index].Split('&')[1].Length;
            // 남아 있는 문자열 비율 계산

            _currentTween = text.DOText(_currentDialog[index].Split('&')[1].Substring(currentText.Length), SetTxtReadingTime(_currentDialog[index].Split('&')[1].Substring(currentText.Length)) * remainingRatio)
                .SetRelative()
                .SetDelay(0.1f)
                .OnComplete(() =>
                {
                    StartCoroutine(NextAfterWating());
                });
        }
    }

    IEnumerator NextAfterWating()
    {
        yield return new WaitForSeconds(0.5f);
        NextDialog();
    }

    public void SkipAndNextDIalog()
    {
        if (index == _datasCount - 1 || _currentDialog.Count == 0)
        {
            return;
        }
        index++;
        KillTweenAndStopCoroutine();

        text.text = "";

        _currentCoroutine = DialogRead(_currentDialog[index]);
        StartCoroutine(_currentCoroutine);
    }
    public void SkipAndPrevDIalog()
    {
        if (index == 0)
            return;
        KillTweenAndStopCoroutine();
        text.text = string.Empty;
        index--;
        _currentCoroutine = DialogRead(_currentDialog[index]);
        StartCoroutine(_currentCoroutine);
    }
    void KillTweenAndStopCoroutine()
    {
        if (_currentTween != null)
        {
            _currentTween.Kill();
            _currentTween = null;

            if (_currentCoroutine != null) StopCoroutine(_currentCoroutine);
            text.text = string.Empty;
        }
    }
}