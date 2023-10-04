using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine.UI;
//Dotween : 유니티 사실상 필수 에셋
// 오브젝트의 애니메이션, 부드러운 이동 등을 해당 에셋이 제공해주는
//함수를 통해 간단히 사용할 수 있게 도와줍니다.
public class UIHandler : MonoBehaviour
{
    public GameObject handlingUI;
    [SerializeField] bool isAnimate;
    [SerializeField] Text text;
    [SerializeField] Material material;
    private void Start()
    {
        DOTween.Init(); //두트윈기본설정
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
   
            //DOTween에서 제공해주는 함수를 순차적으로 수행하도록 해주는 보조 도구
            var sequence = DOTween.Sequence();
            //파라미터 순서대로, Scale값과 시간을 작성
            sequence.Append(handlingUI.transform.DOScale(1.5f, 0.5f));
            sequence.Append(handlingUI.transform.DOScale(1.0f, 0.5f));
            sequence.Append(text.DOText("마음껏 설정해 봐. 친구", 0.5f));
            material.DOColor(Color.red, sequence.Duration());
            //시퀀스 작동
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
