using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] Image backGround;
    [SerializeField] Sprite startImg;
    [SerializeField] GameObject interactiveCanvas;
    [SerializeField] GameObject startCanvas;
    private List<string> _sentnece = new List<string>();

    private void Awake()
    {
        instance = this;
        //interactiveCanvas.SetActive(false);
    }
    public void StartGame()
    {
        interactiveCanvas.SetActive(true);
        startCanvas.SetActive(false);
        StageMove();
    }

    private void StageMove()
    {
        StageManager.instance.NextStage();
        ChangeBackGround(StageManager.instance.currentStage.image);
        Invoke("Process", 0.5f);
    }

    public void ChangeBackGround(Sprite nextImg)
    {
        backGround.sprite = nextImg;
    }
    void Process()
    {
        _sentnece = new List<string>(StageManager.instance.GetDialog());

        if (_sentnece != null)
        {
            TypingTest.instance.StartDialog(_sentnece.Count, _sentnece);
        }
        else
            Debug.Log("저장되어 있지 않은 NPC Name 입니다.");
    }
}
