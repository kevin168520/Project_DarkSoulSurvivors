using UnityEngine;
using System.Collections.Generic;
using baseSys.Audio.Sources;
using DG.Tweening;

namespace baseSys.Audio.Method
{
    /// <summary>
    /// 使用方式如下：
    /// 1. 事先置入所有曲子 (Source 音樂資料結構)
    /// 2. 從物件池取得帶有 AudioSource 物件 (物件池沒有空閒物件則生成新物件)
    /// 3. 撥放完畢時依據是否循環樂曲：
    ///    a. 循環 同物件重新設置繼續使用
    ///    b. 不循環 回歸物件池
    /// </summary>
    public class PlayMethod
    {
        /// <summary> 播放清單 字典型態 音樂名稱作為 Key </summary>
        Dictionary<string, Source> _sourceDict = new();

        /// <summary> 場景物件池 現在似乎沒作用</summary>
        PlayerMethodPool _pool;

        /// <summary> 音量較正值 </summary>
        [Range(0, 1)]
        float _volume = 0.5f;

        /// <summary> 靜音啟用 </summary>
        bool _mute = false;

        /// <summary> 初始化播放清單 </summary>
        /// <param name="gameObject"></param>
        /// <param name="type">播放器類型名稱</param>
        /// <param name="playlist">播放清單</param>
        /// <param name="fixValue">初始音量較正值</param>
        public PlayMethod(GameObject gameObject, string type, Source[] playlist, float fixValue)
        {
            // 載入所有樂曲
            foreach(var source in playlist)
                _sourceDict.Add(source.Name, source);

            // 儲存音量
            _volume = fixValue;

            // 場景物件分類用物件
            _pool = new PlayerMethodPool(gameObject, type);
        }

        /// <summary> 設置音量較正值 </summary>
        public void ResetValue(float volume)
        {
            foreach(var (_, audio) in _pool.GetNowAudio())
                audio.volume = (audio.volume / _volume) * volume;

            _volume = volume;
        }

        /// <summary> 靜音 </summary>
        public void Mute(bool enable)
        {
            foreach(var (_, audio) in _pool.GetNowAudio())
                audio.mute = _mute;

            _mute = enable;
        }

        /// <summary> 停止所有播放 </summary>
        public void StopAll()
        {
            _pool.RecoverAll();
        }

        /// <summary> 停止特定聲音 </summary>
        public void Stop(string name)
        {
            _pool.Recover(name);
        }

        /// <summary> 一般播放 </summary>
        public void Next(string name)
        {
            if (!_sourceDict.ContainsKey(name))
            {
                Debug.LogError("Not Find Audio");
                return;
            }

            OnNextPlay(name);
        }

        /// <summary> 產生播放器 </summary>
        public void Add(string name)
        {
            if (!_sourceDict.ContainsKey(name))
            {
                Debug.LogError("Not Find Audio");
                return;
            }

            OnPlay(name);
        }

        /// <summary> 播放功能 </summary>
        void OnPlay(string name)
        {
            //取得物件
            GameObject obj = _pool.Create();
            AudioSource aos = obj.GetComponent<AudioSource>();
            obj.name = name;

            bool retrigger = SetAudioPlayer(_sourceDict[name], ref aos);
            obj.SetActive(true);
            aos.Play();

            float life = aos.clip.length;

            //判斷是否循環播放，或者重複觸發
            if (!retrigger)
            {
                if (!_sourceDict[name].Loop)
                {
                    Sequence _delayCallback;
                    _delayCallback = DOTween.Sequence();
                    _delayCallback.InsertCallback(life, delegate
                    {
                        _pool.Recover(obj);
                    });
                }
            }
            else
            {
                Sequence _delayCallback;
                _delayCallback = DOTween.Sequence();
                _delayCallback.InsertCallback(life, delegate
                {
                    OnPlay(name);
                });
            }
        }

        Sequence _delayNextPlay;
        /// <summary> 同個播放器，播放下一首 </summary>
        void OnNextPlay(string name)
        {

            var (audio, audioGb) = _pool.GetAvailableSource();
            //取得物件
            //GameObject obj;
            //AudioSource aos;


            //如果有正在播放
            // int playerCount = _nowPlayer.Count;
            //if (playerCount > 0)
            //{
            //    Debug.Log("_nowPlayer");
            //    obj = _nowPlayer[0];
            //    aos = obj.GetComponent<AudioSource>();
            //}
            //else
            //{
            //    Debug.Log("Create Player");
            //    obj = create();
            //    _nowPlayer.Add(obj);
            //    aos = _nowPlayer[0].GetComponent<AudioSource>();
            //}

            // 移除 Delay
            if (_delayNextPlay != null) _delayNextPlay.Kill();
            
            // 重新取名
            if (audioGb.name != name) audioGb.name = name;


            //是否重置播放時間
            float time = (!_sourceDict[name].ResetTime) ? (audio.time + 0.01f) : 0;

            //載入設定檔&播放
            bool retrigger = SetAudioPlayer(_sourceDict[name], ref audio);

            audio.Play();
            audio.time = time;
            audioGb.SetActive(true);

            if (retrigger && _sourceDict[name].Loop)
            {
                float life =
                audio.clip.length;
                life -= audio.time;

                _delayNextPlay = DOTween.Sequence();
                _delayNextPlay.InsertCallback(life, delegate
                {
                    OnNextPlay(name);
                });
            }
        }

        /// <summary> return reTrigger </summary>
        bool SetAudioPlayer(Source raw, ref AudioSource audio)
        {
            //取得播放歌曲
            audio.clip = raw.GetClip();
            audio.volume = raw.GetVolume(_volume);
            audio.pitch = raw.GetPitch();
            audio.loop = raw.IsLoop();
            audio.outputAudioMixerGroup = raw.GetMixer();
            audio.mute = _mute;

            // 是否循環下一曲
            bool reTrigger = raw.IsLoopNext();
            return reTrigger;
        }
    }
}