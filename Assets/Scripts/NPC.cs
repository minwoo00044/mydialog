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
    public Color GetTextColorInNPC()
    {
        return _data.txtColor;
    }
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer
    {
        get => _spriteRenderer;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void InitNpc(string _name)
    {
        _spriteRenderer.sprite = _data.nomral_imo;
        gameObject.name = _name;

    }
}
