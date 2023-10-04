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
    private Sequence _sequence;

    private void Start()
    {
        DOTween.Init();
        txtSpeed = 2f;
        waitForSeconds = new WaitForSeconds(txtSpeed + 0.5f);
        _sequence = DOTween.Sequence();
    }
    public void StartDialog()
    {
        if (_sequence.active)
            return;
        if (_sequence != null) 
        {
            SequenceInit();
        }
        StartCoroutine(DialogRead(dialogs));
    }
    IEnumerator DialogRead(string[] data)
    {
        _sequence.Append(text.DOText(data[nextIndex], txtSpeed));
        _sequence.Play();
        yield return waitForSeconds;
        NextDialog();
    }
    private void NextDialog()
    {
        nextIndex++;
        text.text = null;
        SequenceInit();
        if (nextIndex > dialogs.Length)
        {
            nextIndex = 0;
            return;
        }
        StartCoroutine(DialogRead(dialogs));
    }
    private void SequenceInit()
    {
        _sequence.Kill();
        _sequence = DOTween.Sequence();
    }

    public void ChangeSpeed()
    {
        _sequence.timeScale = txtSpeedSlider.value;
        _sequence.Restart();
        Debug.Log($"타임스케일{_sequence.timeScale}, 밸류 {txtSpeedSlider.value}");
    }
}
