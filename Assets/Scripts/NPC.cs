using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    [SerializeField]private NPCData _data;

    public NPCData data
    { 
        get { return _data; }
        set { _data = value; }
    }

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void InitNpc()
    {
        spriteRenderer.sprite = _data.NPCImg;
    }
}
