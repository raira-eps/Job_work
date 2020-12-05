using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem tapEffect = null;
    [SerializeField]
    private Camera tapCamera = null;

    private void Update()
    {
        //スマホ用
        if (Input.touchSupported)
        {
            generateEffect();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            generateEffect();
        }
    }

    private void generateEffect()
    {
        var pos = tapCamera.ScreenToWorldPoint(Input.mousePosition + tapCamera.transform.forward * 10);
        tapEffect.transform.position = pos;
        tapEffect.Emit(1);
    }
}
