using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSoundManager
{
    private const int SE_CHANNEL = 10;

    public enum eType
    {
        BGM,
        SE,
    }

    private static DoorSoundManager _singleton = null;

    public static DoorSoundManager GetInstance()
    {
        return _singleton ?? (_singleton = new DoorSoundManager());
    }

    //サウンド制作のためのゲームオブジェクト
    private GameObject soundPlayObj = null;

    //サウンドリソース
    private AudioSource sourceBgm = null;
    private AudioSource sourceSeDefault = null;
    private AudioSource[] sourceSeChannel;

    //BGMにアクセスするためのテーブル
    Dictionary<string, _Data> poolBgm = new Dictionary<string, _Data>();
    //SEにアクセスするためのテーブル
    Dictionary<string, _Data> poolSe = new Dictionary<string, _Data>();
    
    #region 保存するデータ
    class _Data
    {
        // AccessKey
        public string Key = "";
        // ResourceName
        public string ResName = "";

        public AudioClip Clip;

        // Constructor
        public _Data(string key, string res)
        {
            Key = key;
            ResName = "Sounds/DoorGameSounds/" + res;
            //AudioClipの取得
            Clip = Resources.Load(ResName) as AudioClip;
        }
    }
    #endregion

    public DoorSoundManager()
    {
        //channel確保
        sourceSeChannel = new AudioSource[SE_CHANNEL];
    }

    AudioSource GetAudioSource(eType type, int channel = -1)
    {
        if (soundPlayObj == null)
        {
            //なければ作る
            soundPlayObj = new GameObject("Sound");
            //破棄しない
            GameObject.DontDestroyOnLoad(soundPlayObj);

            sourceBgm = soundPlayObj.AddComponent<AudioSource>();
            sourceSeDefault = soundPlayObj.AddComponent<AudioSource>();
            for (int i = 0; i < SE_CHANNEL; i++)
            {
                sourceSeChannel[i] = soundPlayObj.AddComponent<AudioSource>();
            }
        }

        if (type ==  eType.BGM)
        {
            return sourceBgm;
        }
        else
        {
            //SE
            if (channel >= 0 && channel < SE_CHANNEL)
            {
                //channel指定
                return sourceSeChannel[channel];
            }
            else
            {
                return sourceSeDefault;
            }
        }
    }

    //Sound Load
    // Resources/Sounds
    #region Load BGM
    public static void LoadBGM(string key, string resName)
    {
        GetInstance().loadBgm(key, resName);
    }

    private void loadBgm(string key, string resName)
    {
        if (poolBgm.ContainsKey(key))
        {
            poolBgm.Remove(key);
        }
        poolBgm.Add(key, new _Data(key, resName));
    }
    #endregion

    #region Load SE
    public static void LoadSE(string key, string resName)
    {
        GetInstance().loadSe(key, resName);
    }

    private void loadSe(string key, string resName)
    {
        if (poolSe.ContainsKey(key))
        {
            poolSe.Remove(key);
        }
        poolSe.Add(key, new _Data(key, resName));
    }
    #endregion

    //事前にLoadBGMでロードしておく
    #region BGM Play and Stop
    public static bool PlayBGM(string key)
    {
        return GetInstance().playBgm(key);
    }

    private bool playBgm(string key)
    {
        if (poolBgm.ContainsKey(key) == false)
        {
            return false;
        }

        StopBGM();

        var data = poolBgm[key];

        var source = GetAudioSource(eType.BGM);
        source.loop = true;
        source.clip = data.Clip;
        source.Play();

        return true;
    }

    public static bool StopBGM()
    {
        return GetInstance().stopBgm();
    }

    private bool stopBgm()
    {
        GetAudioSource(eType.BGM).Stop();
        return true;
    }
    #endregion

    #region SE Play and Stop
    public static bool PlaySE(string key, int channel = -1)
    {
        return GetInstance().playSe(key, channel);
    }

    private bool playSe(string key, int channel = -1)
    {
        if (poolSe.ContainsKey(key) == false)
        {
            //対応するキーがない
            return false;
        }

        //Resourceの取得
        var data = poolSe[key];

        if (channel >= 0 && channel < SE_CHANNEL)
        {
            //channel指定
            var source = GetAudioSource(eType.SE, channel);
            source.clip = data.Clip;
            source.Play();
        }
        else
        {
            //defaultで再生
            var source = GetAudioSource(eType.SE);
            source.PlayOneShot(data.Clip);
        }

        return true;
    }
    #endregion
}
