using juden_suzuki;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace job
{
    public class NewCharaFound : MonoBehaviour
    {
        [SerializeField]
        private Image charaImageObj = null;
        [SerializeField]
        private Text charaText = null;       //Tips用 Text

        private bool isCharaFound = false;
        public bool IsCharaFound
        {
            get => isCharaFound;
            set => isCharaFound = value;
        }

        private int slideCountNum = 0;          //表示数 カウント用

        [SerializeField]
        private GameObject miniGameManagerObj = null;
        MiniGameManager miniGameManager = null;

        #region Debug
        [SerializeField]
        private Text restNumText = null;
        [SerializeField]
        private Sprite debugSprite = null;
        [SerializeField]
        private string debugString = "debug";
        private int foundHitNum = 0;

        /// <summary>
        /// 新規キャラ獲得 現在の取得状況と照らし合わせて、未発見であればTrueを返して使用
        /// </summary>
        private GameObject newFoundBoard = null;
        private bool isNewFound = false;
        public bool IsNewFound 
        {
            get => isNewFound;
            set => isNewFound = value;
        }
        #endregion

        private void OnEnable()
        {
            if (miniGameManagerObj.TryGetComponent<MiniGameManager>(out miniGameManager))
            {
                foundHitNum = miniGameManager.HitNums;
                slideCountNum = foundHitNum - 1;        //残り表示回数 = 新規取得キャラ数 - 初回表示
                Debug.Log($"残り表示枚数{slideCountNum}");
                restNumText.text = $"{slideCountNum}";
                DeBugAddInfo();
            }
        }

        private void DeBugAddInfo()
        {
            // ToDo: Resourceからもって来れる用にする
            RefreshVillegerInfo(debugString, debugSprite, IsNewFound);
        }

        /// <summary>
        /// 更新毎に画像と情報を取ってくる処理
        /// </summary>
        /// <param name="tips">村人からの情報</param>
        /// <param name="sprite">村人の画像</param>
        /// <param name="_isNewFound">村人の新規発見判定</param>
        public void RefreshVillegerInfo(string tips, Sprite sprite, bool _isNewFound)
        {
            charaImageObj.sprite = sprite;
            charaText.text = tips;
            IsCharaFound = _isNewFound;
        }

        //ToDo ここに キャラの新規取得時のアニメーション再生関数を書く
        private void startAnimNewVillage(bool _isNewFound)
        {
             
        }

        /// <summary>
        /// Button用
        /// </summary>
        public void OnTapOKButton()
        {
            slideCountNum--;
            Debug.Log($"残り表示枚数{slideCountNum}");
            restNumText.text = $"{slideCountNum}";
            if (slideCountNum > 0)
            {
                DeBugAddInfo();
            }
            else
            {
                //ToDo リザルトへ行く処理
                miniGameManager.GameState = MiniGameManager.eMiniGameState.Result;
            }
        }

 

    }

}
