using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScratchManager : MonoBehaviour
{
    #region スクラッチのアタリ関係
    [SerializeField]
    private Image[] scratchCardImages = null;

    private List<Image> scratchCardList = new List<Image>();
    private int selectCardCount = 0;
    public int SelectCardCount
    {
        get => selectCardCount;
        set => selectCardCount = value;
    }
    #endregion

    /// <summary>
    /// 有料アセットを改変し使用
    /// </summary>
    //[SerializeField]
    //private EraseProgress eraseProgress0;
    //[SerializeField]
    //private EraseProgress eraseProgress1;
    //[SerializeField]
    //private EraseProgress eraseProgress2;
    //[SerializeField]
    //private EraseProgress eraseProgress3;
    //[SerializeField]
    //private EraseProgress eraseProgress4;

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        CardsInitialization();
        SetHitMarker();
        SetOnProgress();
        SelectCardCount = 3;

    }

    private void CardsInitialization()
    {
        // 当たりの初期化処理
        foreach (var cards in scratchCardImages)
        {
            cards.gameObject.SetActive(false);
        }
    }
    private void SetHitMarker()
    {
        // スクラッチのアタリをrandomに置く
        
        //リストにスクラッチカードをセット
        for (int i = 0; i < scratchCardImages.Length; ++i)
        {
            scratchCardList.Add(scratchCardImages[i]);
        }

        //リストをシャッフル
        for (int i = 0; i < scratchCardList.Count; i++)
        {
            Image temp = scratchCardList[i];
            var index = Random.Range(0, scratchCardList.Count);
            scratchCardList[i] = scratchCardList[index];
            scratchCardList[index] = temp;
        }
        //先頭から3つ選択
        for (int setCount = 0; setCount < 3; setCount++)
        {
            scratchCardList[setCount].gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// 名前が悪い。SelectCardCheckとSelectCardFittingDetermineWhetherを追加したいだけ
    /// 有料アセットを改変し使用
    /// </summary>
    private void SetOnProgress()
    {
        //eraseProgress0.OnProgress += SelectCardCheck;
        //eraseProgress0.OnSelectCard += SelectCardFittingDetermineWhether;

        //eraseProgress1.OnProgress += SelectCardCheck;
        //eraseProgress1.OnSelectCard += SelectCardFittingDetermineWhether;

        //eraseProgress2.OnProgress += SelectCardCheck;
        //eraseProgress2.OnSelectCard += SelectCardFittingDetermineWhether;

        //eraseProgress3.OnProgress += SelectCardCheck;
        //eraseProgress3.OnSelectCard += SelectCardFittingDetermineWhether;

        //eraseProgress4.OnProgress += SelectCardCheck;
        //eraseProgress4.OnSelectCard += SelectCardFittingDetermineWhether;
    }

    public void SelectCardCheck(GameObject selectCard)
    {
        if (selectCardCount <= 0 && selectCard.activeSelf)
        {
            selectCard.SetActive(false);
            Debug.Log($"削れません");
        }
        selectCardCount--;
        Debug.Log($"残りカウント{selectCardCount}");
    }
    public void SelectCardFittingDetermineWhether(Image selectCardImage, GameObject selectCard)
    {
        if (selectCardImage.isActiveAndEnabled && selectCardCount > 0)
        {
            Debug.Log("Hit");
        }
    }

    public void OnRestart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
