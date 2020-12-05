using juden_suzuki;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace job
{

    public class Result : MonoBehaviour
    {
        [SerializeField]
        private Text hitText = null;
        [SerializeField]
        private Text missTtext = null;
        [SerializeField]
        private Text energyText = null;

        [SerializeField]
        private GameObject miniGameManagerObj = null;     // MiniGameManagerObject

        private MiniGameManager miniGameManager = null;

        private void OnEnable()
        {
            if (miniGameManagerObj.TryGetComponent<MiniGameManager>(out miniGameManager))
            {
                Debug.Log($"result OnEnable hit:{miniGameManager.HitNums}");
                Debug.Log($"result OnEnable miss{miniGameManager.MissNums}");
                Debug.Log($"result OnEnable ene{miniGameManager.eneNums}");
                SetMiniGameResultText(miniGameManager);
            }
        }
        private void SetMiniGameResultText(MiniGameManager manager)
        {
            hitText.text = $"{manager.HitNums}";
            missTtext.text = $"{manager.MissNums}";
            energyText.text = $"{manager.eneNums}";
        }
        public void OnTapRestart()
        {
            //miniGameManager.RestartGame();
            miniGameManager.GameState = MiniGameManager.eMiniGameState.Start;
        }

        public void OnTapMap()
        {
            Debug.Log("go to map");
        }


    }
}