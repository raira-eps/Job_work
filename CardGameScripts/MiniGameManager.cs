using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace job
{
    public class MiniGameManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject startPage = null;  //スタート画面
        [SerializeField]
        private GameObject gamePage = null;   //ゲーム画面
        [SerializeField]
        private GameObject newCharPage = null;  //新規キャラ紹介画面
        [SerializeField]
        private GameObject resultPage = null; //リザルト画面

        public enum eMiniGameState
        {
            Start,
            Game,
            newChar,
            Result
        }

        public delegate void OnGameStateChange();
        OnGameStateChange onGameStart;

        public event OnGameStateChange OnGameStart
        {
            add => onGameStart += value;
            remove => onGameStart -= value;
        }

        OnGameStateChange onGamePlay;
        OnGameStateChange onGameFoundCharacter;
        OnGameStateChange onGameShowResult;

        private eMiniGameState gameState;

        public eMiniGameState GameState
        {
            get => gameState;
            set
            {
                gameState = value;
                OnGameStageChange(gameState);
            }
        }

        private void OnGameStageChange(eMiniGameState state)
        {
            switch (state)
            {
                //Start画面
                case eMiniGameState.Start:
                    resultPage.SetActive(false);
                    startPage.SetActive(true);
                    onGameStart?.Invoke();
                    break;
                //Game画面
                case eMiniGameState.Game:
                    startPage.SetActive(false);
                    gamePage.SetActive(true);
                    onGamePlay?.Invoke();
                    break;
                //新規キャラ紹介画面
                case eMiniGameState.newChar:
                    gamePage.SetActive(false);
                    newCharPage.SetActive(true);
                    onGameFoundCharacter?.Invoke();
                    break;
                //Result画面
                case eMiniGameState.Result:
                    gamePage.SetActive(false);
                    newCharPage.SetActive(false);
                    resultPage.SetActive(true);
                    onGameShowResult?.Invoke();
                    break;
            }
        }


        #region game
        private int hitNums = 0;
        public int HitNums
        {
            get => hitNums;
            set => hitNums = value;
        }

        private Action<int> onMiss;

        private int missNums = 0;
        public int MissNums
        {
            get => missNums;
            set
            {
                missNums = value;
                onMiss?.Invoke(missNums);
            }
        }
        public int eneNums = 0;
        public bool resultCheckBool = false;
        #endregion

        #region found
        public static bool foundCheckBool = false;

        public static bool FoundCheckBool
        {
            get => foundCheckBool;
        }

        #endregion

        [SerializeField]
        public int haveSuica = 50;

        public int playCount = 0;
        public bool checkBool = false;

        private void Awake()
        {
            startPage.SetActive(true);
            gamePage.SetActive(false);
            newCharPage.SetActive(false);
            resultPage.SetActive(false);
        }

        private void Start()
        {
            OnGameStart += ResetNumsFunc;

            GameState = eMiniGameState.Start;
        }


        public void UsedSuica(in int useSuica)
        {
            haveSuica -= useSuica;
        }

        public void PlayClick(in int playClick)
        {
            playCount = playClick;
            Debug.Log($"遊べる回数：{playCount}回");
        }

        public void SubtractPlayCount()
        {
            playCount -= 1;
        }

        /// <summary>
        /// リザルトに渡す数値をセットする
        /// </summary>
        /// <param name="_hit">発見回数</param>
        /// <param name="_miss">未発見回数</param>
        /// <param name="_check">ゲーム画面のアクティブ制御</param>
        /// <param name="_result">リザルト画面のアクティブ制御</param>
        public void ResultSet(in int _hit, in int _miss)
        {
            hitNums = _hit;
            missNums = _miss;
            eneNums = (hitNums - missNums) * 100;   //
            if (eneNums < 0)
            {
                eneNums = 0;
            }
        }

        /// <summary>
        /// デバッグ用 初期化
        /// </summary>
        public void ResetNumsFunc()
        {
            hitNums = 0;
            missNums = 0;
            eneNums = 0;
            Debug.Log("Runed ResetNumsFunc");
        }

    }

}