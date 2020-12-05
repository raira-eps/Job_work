///
/// BG_SthootingStarEffectがこのスクリプト持ってるので、
///いじる時は気を付けること！
///特に""Silder-loadingSlider""はBG_Loadが持っている。
///


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionEffects : MonoBehaviour
{
    [SerializeField]
    private GameObject bg_LoadObj = null;
    [SerializeField]
    private Slider loadingSlider = null;
    [SerializeField]
    private GameObject bg_endSthootingStar = null;

    #region Test
    private float d_count = 0;
    #endregion

    public static TransitionEffects instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private delegate void OnComplete(Animator animator);

    public void StartSthootingStar(Animator sthootingStar)
    {
        //ToDo アニメーター名追加する
        sthootingStar.Play("SnowStar_AnimEffect");
    }

    public void EndSthootingStar(Animator endSthootingStar)
    {
        endSthootingStar.Play("EndSnowStar_AnimEffect");
    }

    public void  StartLoading()
    {
        Debug.Log("start loading");
        bg_LoadObj.SetActive(true);
        //StartCoroutine(NowLoadting());

    }

    //private IEnumerator NowLoadting()
    //{

    //    while (d_count < 100)
    //    {
    //        var value = Mathf.Clamp01(d_count / 0.9f);
    //        loadingSlider.value = value;
    //        if (loadingSlider.value >= 1f)
    //        {
    //            break;
    //        }
    //    }
    //    yield return null;
    //}

    private void LoadBar()
    {
        loadingSlider.value += d_count;
    }

    #region Test
    private void testCountUp()
    {
        d_count = d_count + Time.deltaTime / 10;
    }
    private void Update()
    {
        if (bg_LoadObj.activeSelf)
        {
            testCountUp();
            LoadBar();
        }

    }


    #endregion
}
