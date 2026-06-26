using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DOTween.SetTweensCapacity(500,20);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    //DOTWEEN CONTROLS

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }

    public void DoTilt(float zTilt)
    {
        transform.DOLocalRotate(new Vector3(0, 0, zTilt), 0.25f);
    }

    public void DoLean(float xTilt)
    {
        transform.DOLocalRotate(new Vector3(xTilt, 0, 0), 0.25f);
    }

    public void DoShake(float shakeAmt, float length)
    {
        Vector3 originalPos = transform.localPosition;
        Sequence shakeSequence = DOTween.Sequence();
        shakeSequence.Append(transform.DOShakePosition(length, new Vector3(shakeAmt, shakeAmt, 0), vibrato: 10, randomness: 0.5f, snapping: false, fadeOut: true));
        shakeSequence.Append(transform.DOLocalMove(originalPos, 0.1f));
    }
}
