using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TypingTest : MonoBehaviour
{
    public Text text;
    public Slider txtSpeedSlider;
    public string[] dialogs;

    private int nextIndex;
    private float txtSpeed;
    private WaitForSeconds waitForSeconds;
    private Tweener _currentTween;
    const float ORIGIN_SPEED = 4F;

    IEnumerator _currentCoroutine;
    private void Start()
    {
        DOTween.Init();
        txtSpeed = ORIGIN_SPEED;
        waitForSeconds = new WaitForSeconds(txtSpeed + 0.5f);
    }
    public void StartDialog()
    {
        _currentCoroutine = DialogRead(dialogs);
        StartCoroutine(_currentCoroutine);
    }
    IEnumerator DialogRead(string[] data)
    {
        _currentTween = text.DOText(data[nextIndex], txtSpeed);
        yield return waitForSeconds;
        NextDialog();
    }
    private void NextDialog()
    {
        nextIndex++;
        text.text = string.Empty;
        if (nextIndex >= dialogs.Length)
        {
            nextIndex = 0;
            return;
        }
        StartCoroutine(_currentCoroutine);
    }

    public void ChangeSpeed()
    {
        txtSpeed = ORIGIN_SPEED - txtSpeedSlider.value;
        if (_currentTween != null && _currentTween.IsActive() && !_currentTween.IsComplete())
        {
            string currentText = text.text; // ���� �ؽ�Ʈ ������
            _currentTween?.Kill(); // ���� Ʈ�� ����

            float remainingRatio = (float)(dialogs[nextIndex].Length - currentText.Length) / dialogs[nextIndex].Length;
            // ���� �ִ� ���ڿ� ���� ���

            _currentTween = text.DOText(dialogs[nextIndex].Substring(currentText.Length), txtSpeed * remainingRatio)
                .SetRelative()
                .SetDelay(0.1f);
            // ���Ӱ� ���� ���ڿ����� �ٽ� ����, SetRelative�� ����� ������ ���ڿ��� �߰��ǵ��� ����
        }
    }


    public void SkipAndNextDIalog()
    {
        if (nextIndex == dialogs.Length - 1)
            return;
        _currentTween?.Kill();
        text.text = string .Empty;
        nextIndex++;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = DialogRead(dialogs);
        StartCoroutine(_currentCoroutine);
    }
    public void SkipAndPrevDIalog()
    {
        if (nextIndex == 0)
            return;
        _currentTween?.Kill();
        text.text = string.Empty;
        nextIndex--;
        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        _currentCoroutine = DialogRead(dialogs);
        StartCoroutine(_currentCoroutine);
    }
}