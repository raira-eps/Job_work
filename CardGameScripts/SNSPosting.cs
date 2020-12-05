using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace juden_suzuki 
{
    public class SNSPosting : MonoBehaviour
    {
        private Coroutine ScreenShotCoroutine = null;
        private int setRewardNumber = 0;
        [SerializeField]
        private string debugSNSMessage = " ";


        /// <summary>
        /// 投稿ボタンにセット
        /// </summary>
        public void StartScreenShot()
        {
            if (ScreenShotCoroutine == null)
            {
                ScreenShotCoroutine = StartCoroutine(ScreeShot());
            }
        }

        IEnumerator ScreeShot()
        {
            //SoundManager.Instance.PlaySe("46Album_Shutter");

            string ScreenShotImagePath = "ScreenShotResult.png";

            yield return null;

            string ScreenShotSavePath = Application.persistentDataPath + "/" + ScreenShotImagePath;

            yield return null;

            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, true);
            texture.Apply();
            byte[] pngData = texture.EncodeToPNG();
            File.WriteAllBytes(ScreenShotSavePath, pngData);

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
                ScreenShotImagePath = Application.persistentDataPath + "/" + ScreenShotImagePath;
#endif

            yield return new WaitForSeconds(1f);

            float latency = 0, latencyLimit = 2;
            while (latency < latencyLimit)
            {
                if (File.Exists(ScreenShotImagePath))
                {
                    break;
                }
                latency += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(1f);

            SNSButton();

            ScreenShotCoroutine = null;
        }

        private void SNSButton()
        {
            string path1 = "ScreenShotResult.png";

#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
                path1 = Application.persistentDataPath + "/" + path1;
#endif

            string message = debugSNSMessage;
            //有料アセットを使用
            //SocialConnector.SocialConnector.Share(message, path1);

            //SendAnalytics.AnalyticsSender.SendActionLogData(Constant.AnalyticsConstant.RESCUE + "016", "result_sns");

            switch (setRewardNumber)
            {
                case 0:
                    if (true)
                    {
                        //Todo リワード処理
                        Debug.Log($"Reward case: 0");
                    }
                    break;
                case 1:
                    if (true)
                    {
                        //Todo リワード処理
                        Debug.Log($"Reward case: 1");
                    }
                    break;
            }
        }
    }
}

