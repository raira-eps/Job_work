using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace job
{
    public class Start : MonoBehaviour
    {
        [SerializeField]
        private GameObject playOneButton = null;
        [SerializeField]
        private GameObject playElevenButton = null;

        private MiniGameManager miniGameManager;

        [SerializeField]
        private GameObject miniGameManagerObj = null;

        #region start
        /// <summary>
        /// One Play
        /// </summary>
        public static int one = 1;
        public static int costOne = 1;
        /// <summary>
        /// Eleven Play
        /// </summary>
        public static int eleven = 11;
        public static int costTen = 10;
        #endregion

        private void OnEnable()
        {
            if (miniGameManagerObj.TryGetComponent<MiniGameManager>(out miniGameManager))
            {
                miniGameManager.ResetNumsFunc();
            }

            startSoundLoad();
            DoorSoundManager.PlayBGM("bgm");

        }

        public void OnTapPlayOne()
        {
            DoorSoundManager.PlaySE("tap", 0);

            Debug.Log("click one");
            if (miniGameManager.haveSuica < costOne)
            {
                Debug.Log("ポイントが足りません");
                return;
            }
            else
            {
                miniGameManager.UsedSuica(in costOne);
                Debug.Log($"ポイント残り{miniGameManager.haveSuica}P");
                miniGameManager.PlayClick(in one);
                DoorSoundManager.StopBGM();
                GoToGameScreen();
            }
        }
        public void OnTapPlayEleven()
        {
            DoorSoundManager.PlaySE("tap", 0);
            Debug.Log("click eleven");
            if (miniGameManager.haveSuica < costTen)
            {
                Debug.Log("ポイントが足りません");
                return;
            }
            else
            {
                miniGameManager.UsedSuica(in costTen);
                Debug.Log($"ポイント残り{miniGameManager.haveSuica}P");
                miniGameManager.PlayClick(in eleven);
                DoorSoundManager.StopBGM();
                GoToGameScreen();
            }
        }

        //アニメーション イベントで使う
        public void GoToGameScreen()
        {
            miniGameManager.GameState = MiniGameManager.eMiniGameState.Game;
        }

        #region Sound 関係
        private void startSoundLoad()
        {
            DoorSoundManager.LoadBGM("bgm", "bgm01");
            DoorSoundManager.LoadSE("open", "openSE");
            DoorSoundManager.LoadSE("ok", "tapSE");
            DoorSoundManager.LoadSE("cansel", "canselSE");
        }
        #endregion
    }

}


