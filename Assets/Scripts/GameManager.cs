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
    private void Awake()
    {
        instance = this;
        //interactiveCanvas.SetActive(false);
    }

    public void ChangeBackGround(Sprite nextImg)
    {
        backGround.sprite = nextImg;
    }
    
}
