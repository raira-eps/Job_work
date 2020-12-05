using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{
    [SerializeField]
    private GameObject sthootingStarEffObj = null;
    [SerializeField]
    private GameObject loadEffObj = null;
    [SerializeField]
    private Animator sthootingStartAnimator = null;
    [SerializeField]
    private Animator endSthootingStartAnimator = null;
    #region //これは良くない システムを見直す必要あり
    [SerializeField]
    private GameObject bg_endSthooting = null;
    [SerializeField]
    private Slider slider = null;
    private bool set = false;
    #endregion
    private void Awake()
    {
        Init();
    }
    private void Start()
    {
        TransitionEffects.instance.StartSthootingStar(sthootingStartAnimator);
    }

    private void Init()
    {
        sthootingStarEffObj.SetActive(true);
        loadEffObj.SetActive(false);
        set = false;
    }

    private void Update()
    {
        if (slider.value == 1f && set == false)
        {
            sthootingStarEffObj.SetActive(false);
            loadEffObj.SetActive(false);
            set = true;
            bg_endSthooting.SetActive(true);
            TransitionEffects.instance.EndSthootingStar(endSthootingStartAnimator);
        } 
    }
}