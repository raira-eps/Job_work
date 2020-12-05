using juden_suzuki;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace job
{
    public class Game : MonoBehaviour
    {
        [SerializeField]
        private GameObject cardLeft = null;
        [SerializeField]
        private GameObject cardRight = null;

        [SerializeField]
        private GameObject cardOpenLeft = null;
        [SerializeField]
        private GameObject cardOpenRight = null;

        [SerializeField]
        private GameObject newCharaImageLeft = null;
        [SerializeField]
        private GameObject newCharaImageRight = null;

        [SerializeField]
        private Text countBoardText = null;
        [SerializeField]
        private Text commentBoardText = null;

        private MiniGameManager miniGameManager;

        [SerializeField]
        private GameObject miniGameManagerObj = null;

        private int hit = 0;
        private int miss = 0;

        private bool isFound = false;

        public bool IsFound
        {
            get => isFound;
            set => isFound = value;
        }

        private bool isHit = false;
        public bool IsHit
        {
            get => isHit;
            set => isHit = value;
        }

        #region Debug
        [SerializeField]
        private bool isDebugMode = false;
        [SerializeField]
        private int debugPlayCount = 10;
        #endregion

        private void OnEnable()
        {
            Init();
            DoorSoundManager.PlayBGM("bgm");
        }

        private void Init()
        {
            if (miniGameManagerObj.TryGetComponent<MiniGameManager>(out miniGameManager))
            {
                IsFound = false;
                IsHit = false;
                ResetGameNums();
                DisplayCountBoard(miniGameManager);
                DisplayCommentBoard();
                gameSoundLoad();
            }

        }

        private void ResetGameNums()
        {
            hit = 0;
            miss = 0;
            cardOpenLeft.SetActive(false);
            cardOpenRight.SetActive(false);
            newCharaImageLeft.SetActive(false);
            newCharaImageRight.SetActive(false);
        }

        private void DisplayCountBoard(MiniGameManager manager)
        {
            countBoardText.text = $"残り{manager.playCount}回";
        }

        private void DisplayCommentBoard()
        {
            commentBoardText.text = $"扉を選んで！";
        }

        private void gameSoundLoad()
        {
            DoorSoundManager.LoadBGM("bgm", "bgm01");
            DoorSoundManager.LoadSE("open", "openSE");
            DoorSoundManager.LoadSE("hit", "hitSE");
            DoorSoundManager.LoadSE("miss", "missSE");
        }

        public void OnTapCard(GameObject card)
        {
            Debug.Log(card.name);
            TapProcess(card);
        }

        private void TapProcess(GameObject card)
        {
            // ToDo Add DebugMode
            if (isDebugMode)
            {

            }

            miniGameManager.SubtractPlayCount();
            DisplayCountBoard(miniGameManager);
            var n = UnityEngine.Random.Range(0, int.MaxValue);
            Debug.Log($"乱数{n}");

            if (n % 2 == 0)
            {
                IsHit = true;
                hit++;

                //ToDo 扉を開ける処理
                DoorOpen(card, IsHit);

                IsHit = false;

                if (!IsFound)
                {
                    IsFound = true;
                }
                Debug.Log($"OnTap hit:{hit}");
                if (miniGameManager.playCount <= 0)
                {
                    //ToDo ゲームを閉じで発見へ行くようMManagerに伝える
                    SetResultScores();
                    SetNextScreen(IsFound);
                }
            }
            else
            {
                //ToDo 扉を開ける処理
                DoorOpen(card, IsHit);

                miss++;

                Debug.Log($"OnTap miss:{miss}");
                if (miniGameManager.playCount <= 0)
                {
                    //todo ゲームを閉じてリザルトへ行くようMMnagerに伝える
                    SetResultScores();
                    SetNextScreen(IsFound);
                }
            }
        }

        private void DoorOpen(GameObject door, bool _isHit)
        {
            //ToDo あとで 右用と左用で 分ける
            if (_isHit)
            {
                if (door == cardLeft)
                {
                    StartCoroutine(DirectionWaitingTime(cardOpenLeft, newCharaImageLeft, _isHit));
                }
                else if (door == cardRight)
                {
                    StartCoroutine(DirectionWaitingTime(cardOpenRight, newCharaImageRight, _isHit));
                }
            }
            else
            {
                if (door == cardLeft)
                {
                    StartCoroutine(DirectionWaitingTime(cardOpenLeft, newCharaImageLeft, _isHit));
                }
                else if (door == cardRight)
                {
                    StartCoroutine(DirectionWaitingTime(cardOpenRight, newCharaImageRight, _isHit));
                }
            }

        }

        private void LayDownCards()
        {
            cardOpenLeft.SetActive(false);
            cardOpenRight.SetActive(false);
            newCharaImageLeft.SetActive(false);
            newCharaImageRight.SetActive(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardObj">left or right CardObj</param>
        /// <param name="newCharaImage">left or right villageImage</param>
        /// <param name="isHit"></param>
        /// <returns></returns>
        private IEnumerator DirectionWaitingTime(GameObject cardObj, GameObject newCharaImage, bool _isHit)
        {
            if (_isHit)
            {
                cardObj.SetActive(true);
                //開閉音 再生
                DoorSoundManager.PlaySE("openSE", 0);
                yield return new WaitForSeconds(1.0f);
                cardObj.SetActive(false);
                newCharaImage.SetActive(true);
                // ここで効果音 "hit" 鳴らす
                DoorSoundManager.PlaySE("hitSE", 0);
                yield return new WaitForSeconds(1.0f);
            }
            else if (!_isHit)
            {
                cardObj.SetActive(true);
                //開閉音 再生
                DoorSoundManager.PlaySE("openSE", 0);
                yield return new WaitForSeconds(1.0f);
                cardObj.SetActive(false);
                newCharaImage.SetActive(false);
                // ここで効果音 "miss" 鳴らす
                DoorSoundManager.PlaySE("missSE", 0);
                yield return new WaitForSeconds(1.0f);
            }

            //ToDo カードを伏せる処理
            LayDownCards();
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetResultScores()
        {
            miniGameManager.ResultSet(in hit, in miss);
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetNextScreen(bool _isfound)
        {
            if (_isfound)
            {
                miniGameManager.GameState = MiniGameManager.eMiniGameState.newChar;
            }
            else
            {
                miniGameManager.GameState = MiniGameManager.eMiniGameState.Result;
            }

        }

    }
}
