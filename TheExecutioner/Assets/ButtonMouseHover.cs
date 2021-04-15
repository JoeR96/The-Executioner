using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMouseHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(LerpSize(true));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine(LerpSize(false));
    }
    
    private IEnumerator LerpSize(bool scaleUp)
    {
        Vector3 targetScale;
        var startScale = transform.localScale;
        if (scaleUp)
            targetScale = new Vector3(1.25f, 1.25f, 1.25f);
        else
            targetScale = new Vector3(1, 1, 1);
        float timer = 0f;
        float duration = 0.125f;

        while (timer < duration)
        {
            float percentage = Mathf.Min(timer / duration, 1);
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale,targetScale,percentage);
            yield return null;
        }
    }
}
