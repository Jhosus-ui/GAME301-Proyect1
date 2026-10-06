using UnityEngine;
using System.Collections;

public class ButtonVisual : MonoBehaviour
{
    [SerializeField] private float pressDistance = 0.1f;
    [SerializeField] private float pressSpeed = 5f;

    private Vector3 originalPosition;
    

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void PlayPressEffect()
    {
        StopAllCoroutines();
        StartCoroutine(PressAnimation());

    }

    private IEnumerator PressAnimation()
    {
        Vector3 pressedPosition = originalPosition  + Vector3.down * pressDistance;

        while (Vector3.Distance(transform.localPosition, pressedPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, pressedPosition, pressSpeed * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(transform.localPosition, originalPosition) > 0.01f)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPosition, pressSpeed * Time.deltaTime);
            yield return null;
        }

        transform.localPosition = originalPosition;
    }

    
}