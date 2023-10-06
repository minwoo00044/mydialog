using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] Image backGround;
    private void Awake()
    {
        Instance = this;
    }

    public void ChangeBackGround(Sprite nextImg)
    {
        backGround.sprite = nextImg;
    }
    
}
