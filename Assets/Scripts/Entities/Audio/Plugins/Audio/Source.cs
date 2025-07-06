using UnityEngine;
using UnityEngine.Audio;
using System;
using Random = UnityEngine.Random;

namespace baseSys.Audio.Sources {
    /// <summary>
    /// 音樂播放資料型態
    /// Vol: 設置音量大小跟隨機範圍
    /// Pitch: 設置音階高低跟隨機範圍
    /// </summary>
    [Serializable]
    public class Source {
        /// <summary> 音量控制設定件 </summary>
        [Serializable]
        public class Vol {
            [Range(0, 1)]
            public float Volume = 1;
            public bool IsRandom = false;
            [Range(0, 1)]
            public float Max = 1;
            [Range(0, 1)]
            public float Min = 1;
        }

        /// <summary> 音階控制設定件 </summary>
        [Serializable]
        public class AudioPitch {
            [Range(-3, 3)]
            public float Pitch = 1;
            public bool IsRandom = false;
            [Range(1, 3)]
            public float Max = 1;
            [Range(-3, 1)]
            public float Min = 1;
        }

        /// <summary> 曲名 外部腳本呼叫的名字 </summary>
        public string Name = "";

        /// <summary> 樂曲資源 當存在複數時隨機抽取一首 </summary>
        public AudioClip[] Clip = new AudioClip[1];

        /// <summary> 循環設定 當存在複數 Clip 時播放下一首 </summary>
        public bool Loop = false;

        /// <summary> 重新播放時 重置時間軸 </summary>
        public bool ResetTime = true;

        /// <summary> 音量控制設定 </summary>
        public Vol Volume = new();

        /// <summary> 音頻控制設定 </summary>
        public AudioPitch Pitch = new();

        /// <summary> 混合器資源 </summary>
        public AudioMixerGroup MixerGroup;

        /// <summary> 取得播放音源 </summary>
        public AudioClip GetClip()
        {
            if (Clip == null || Clip.Length == 0)
            {
                Debug.LogError("out of rang! AudioClip...");
                return null;
            }

            return Clip[Random.Range(0, Clip.Length)];
        }

        /// <summary> 取得播放音頻值 </summary>
        public float GetResetTime(AudioSource audio)
        {
            return !ResetTime ? (audio.time + 0.01f) : 0;
        }

        /// <summary> 取得音量值 </summary>
        /// <param name="ratio">音量縮放</param>
        public float GetVolume(float ratio)
        {
            float volume = Volume.IsRandom ? Random.Range(Volume.Max, Volume.Min) : Volume.Volume;
            return volume * ratio;
        }

        /// <summary> 取得音頻值 </summary>
        public float GetPitch()
        {
            float pitch = Pitch.IsRandom ? Random.Range(Pitch.Max, Pitch.Min) : Pitch.Pitch;
            return pitch;
        }

        /// <summary> 是否循環啟用 </summary>
        public bool IsLoop() => Loop;

        /// <summary> 是否循環下一首 </summary>
        public bool IsLoopNext() => Clip.Length > 1 && Loop;

        /// <summary> 取得混合器資源 </summary>
        public AudioMixerGroup GetMixer() => MixerGroup;
    }
}