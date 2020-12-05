using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransitionManager : MonoBehaviour
{
    /// <summary>
    /// スタート→ゲーム 画面遷移用に制作
    /// 急ぐわけではないので後ででも良き
    /// </summary>

    [SerializeField]
    private Animator Animator;

    private void StartAnimation()
    {
        Animator.SetBool("IsScreenTransition", true);
    }
}
