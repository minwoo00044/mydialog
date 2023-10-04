using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
//Dotween : ����Ƽ ��ǻ� �ʼ� ����
// ������Ʈ�� �ִϸ��̼�, �ε巯�� �̵� ���� �ش� ������ �������ִ�
//�Լ��� ���� ������ ����� �� �ְ� �����ݴϴ�.
public class UIHandler : MonoBehaviour
{
    public GameObject handlingUI;
    [SerializeField] bool isAnimate;
    [SerializeField] Text text;
    [SerializeField] Material material;
    private void Start()
    {
        DOTween.Init(); //��Ʈ���⺻����
        transform.localScale = Vector3.one * 0.1f;
        handlingUI.SetActive(false);
        material.color = Color.white;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&!isAnimate)
        {
            if (!handlingUI.activeInHierarchy)
                Open();
            else
                Closed();
        }
    }

    public void Open()
    {
        if(!handlingUI.activeInHierarchy)
        {
            isAnimate = true;
            handlingUI.SetActive(true);
   
            //DOTween���� �������ִ� �Լ��� ���������� �����ϵ��� ���ִ� ���� ����
            var sequence = DOTween.Sequence();
            //�Ķ���� �������, Scale���� �ð��� �ۼ�
            sequence.Append(handlingUI.transform.DOScale(1.5f, 0.5f));
            sequence.Append(handlingUI.transform.DOScale(1.0f, 0.5f));
            sequence.Append(text.DOText("������ ������ ��. ģ��", 0.5f));
            material.DOColor(Color.red, sequence.Duration());
            //������ �۵�
            sequence.Play();
            sequence.Play().OnComplete(() =>
            {
               isAnimate = false;
            });
        }
    }

    public void Closed()
    {
        isAnimate = true;
        text.text = string.Empty;
        var sequence = DOTween.Sequence();
       // transform.localScale = Vector3.one * 0.2f;
        sequence.Append(handlingUI.transform.DOScale(1.5f, 0.5f));
        sequence.Append(handlingUI.transform.DOScale(0.1f, 0.5f));

        sequence.Play().OnComplete(() =>
        {
            isAnimate = false;
            handlingUI.SetActive(false);
            material.color = Color.white;
        });
    }
}
