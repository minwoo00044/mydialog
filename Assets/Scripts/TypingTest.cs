using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TypingTest : MonoBehaviour
{
    public static TypingTest instance;
    public Text text;
    public Slider txtSpeedSlider;

    [SerializeField] int index;
    private int _datasCount;
    [SerializeField] private float txtSpeed;
    private WaitForSeconds waitForSeconds;
    private Tweener _currentTween;
    const float ORIGIN_SPEED = 6f;

    IEnumerator _currentCoroutine;

    private Dictionary<int, string> _currentIdSentencePair = new Dictionary<int, string>();
    [SerializeField]int key;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DOTween.Init();
        txtSpeed = ORIGIN_SPEED;
        waitForSeconds = new WaitForSeconds(txtSpeed + 0.5f);
    }
    public void StartDialog(int questId, int sentenceCount, Dictionary<int, string> datas_origin)
    {
        KillTweenAndStopCoroutine();

        text.text = string.Empty;
        _currentIdSentencePair.Clear();
        _currentIdSentencePair = datas_origin;
        index = 0;
        _datasCount = sentenceCount;

        key = questId + index;
        _currentCoroutine = DialogRead(_currentIdSentencePair[key]);
        StartCoroutine(_currentCoroutine);
    }
    IEnumerator DialogRead(string data)
    {
        _currentTween = text.DOText(data, txtSpeed);
        yield return waitForSeconds;
        NextDialog();
    }
    private void NextDialog()
    {
        index++;
        key += 1;
        text.text = string.Empty;
        if (index >= _datasCount)
        {
            index = 0;
            key = 0;
            return;
        }
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = DialogRead(_currentIdSentencePair[key]);
            StartCoroutine(_currentCoroutine);
        }
    }

    public void ChangeSpeed()
    {
        txtSpeed = ORIGIN_SPEED - txtSpeedSlider.value;
        waitForSeconds = null;
        waitForSeconds = new WaitForSeconds(txtSpeed + 0.5f);
        if (_currentTween != null && _currentTween.IsActive() && !_currentTween.IsComplete())
        {
            string currentText = text.text; // 현재 텍스트 스냅샷
            _currentTween?.Kill(); // 현재 트윈 종료 체인지 스피드만 텍스트를 비우지 않기 때문에 예외적으로 처리
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            float remainingRatio = (float)(_currentIdSentencePair[key].Length - currentText.Length) / _currentIdSentencePair[key].Length;
            // 남아 있는 문자열 비율 계산

            _currentTween = text.DOText(_currentIdSentencePair[key].Substring(currentText.Length), txtSpeed * remainingRatio)
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
        if (index == _datasCount - 1 || _currentIdSentencePair.Count == 0)
        {
            return;
        }
        index++;
        key += 1;

        KillTweenAndStopCoroutine();

        text.text = "";

        _currentCoroutine = DialogRead(_currentIdSentencePair[key]);
        StartCoroutine(_currentCoroutine);
    }
    public void SkipAndPrevDIalog()
    {
        if (index == 0)
            return;
        KillTweenAndStopCoroutine();
        text.text = string.Empty;
        index--;
        key -= 1;
  

        _currentCoroutine = DialogRead(_currentIdSentencePair[key]);
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