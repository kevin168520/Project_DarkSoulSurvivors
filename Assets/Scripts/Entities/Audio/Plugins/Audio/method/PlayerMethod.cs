using UnityEngine;
using System.Collections.Generic;
using baseSys.Audio.Sources;
using DG.Tweening;

// 目前發現問題
// 1. 當 Source 多首時，隨機播放跟循環設定判定有問題，暫時不要設置多首
// 2. OnNextPlay() 目的是要播放下一首，但是因為上述的原因，導致功能會錯誤，同時 DOTween 計時設置有問題，暫時不要使用這個方法
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
    public class PlayerMethod
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
        /// <param name="gameObject">場景物件池</param>
        /// <param name="playlist">播放清單</param>
        /// <param name="fixValue">初始音量較正值</param>
        public PlayerMethod(GameObject gameObject, Source[] playlist, float fixValue)
        {
            // 載入所有樂曲
            foreach(var source in playlist)
                _sourceDict.Add(source.Name, source);

            // 儲存音量
            _volume = fixValue;

            // 場景物件分類用物件
            _pool = new PlayerMethodPool(gameObject);
        }

        /// <summary> 設置音量較正值 </summary>
        public void ResetValue(float volume)
        {
            _volume = volume;
            foreach (var (_, audio) in _pool.GetNowAudio()) {
                string name = audio.gameObject.name; // 或 clip.name

                if (_sourceDict.TryGetValue(name, out var source))
                    audio.volume = source.GetVolume(_volume); // 原始音量乘上新的 _volume
            }
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

        /// <summary> 連續播放 如果曲目複數音樂時隨機/循序播放 </summary>
        public void NextPlay(string name)
        {
            OnNextPlay(name);
        }

        /// <summary> 一般播放 如果曲目複數音樂時隨機播放 </summary>
        public void Play(string name)
        {
            OnPlay(name);
        }

        /// <summary> 播放功能 </summary>
        void OnPlay(string name)
        {
            if (!_sourceDict.ContainsKey(name))
            {
                Debug.LogError("Not Find Audio");
                return;
            }

            //取得物件
            GameObject obj = _pool.Get(name);
            AudioSource aos = obj.GetComponent<AudioSource>();

            // 設置樂曲資料
            bool loopNext = SetAudioPlayer(_sourceDict[name], ref aos);
            obj.SetActive(true);
            aos.Play();

            // 取得樂曲長度
            float life = aos.clip.length;

            // 樂曲不循環 添加計時器 播放結束時自動回歸物件池
            if(!aos.loop)
            {
                Sequence _delayCallback = DOTween.Sequence();
                _delayCallback.AppendInterval(life);
                _delayCallback.InsertCallback(life, () => _pool.Recover(obj));
            }
        }

        Sequence _delayNextPlay;
        /// <summary> 同個播放器，隨機播放一首 </summary>
        void OnNextPlay(string name)
        {
            if (!_sourceDict.ContainsKey(name))
            {
                Debug.LogError("Not Find Audio");
                return;
            }

            //取得物件
            var (audio, audioGb) = _pool.GetAvailableSource(name);

            // 移除 Delay
            if (_delayNextPlay != null) _delayNextPlay.Kill();
            
            // 重新取名
            if (audioGb.name != name) audioGb.name = name;
            
            //是否重置播放時間
            audio.time = _sourceDict[name].GetResetTime(audio);

            //載入設定檔&播放
            bool retrigger = SetAudioPlayer(_sourceDict[name], ref audio);
            audio.Play();

            // 如果下一首則循環撥放
            if (retrigger)
            {
                // 樂曲時間
                float life = audio.clip.length;
                life -= audio.time;

                _delayNextPlay = DOTween.Sequence();
                _delayNextPlay.AppendInterval(life);
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
            bool loopNext = raw.IsLoopNext();
            return loopNext;
        }
    }
}