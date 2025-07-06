using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace baseSys.Audio.Method
{
    public class PlayerMethodPool
    {
        /// <summary> 現在播放中 </summary>
        List<GameObject> _nowPlayer = new();
        public List<GameObject> GetNowPlayer() => new(_nowPlayer);
        Dictionary<GameObject, AudioSource> _nowAudio = new();
        public Dictionary<GameObject, AudioSource> GetNowAudio() => new(_nowAudio);

        /// <summary> 播放父物件 </summary>
        GameObject playerRoot;

        /// <summary> 物件池 </summary>
        GameObject PoolRoot;
        
        /// <summary> 初始化播放清單 </summary>
        /// <param name="root">場景中存根處</param>
        /// <param name="type">播放器類型名稱</param>
        /// <param name="playlist">播放清單</param>
        /// <param name="fixValue">初始音量較正值</param>
        public PlayerMethodPool(GameObject root)
        {
            // 場景物件分類用物件
            #region [產生物件]
            playerRoot = root;

            PoolRoot = new GameObject();
            PoolRoot.transform.SetParent(playerRoot.transform, false);
            PoolRoot.name = "Pool";
            #endregion
        }
        
        /// <summary> 檢查物件池是否有可用物件，無則產生一個，並返回物件(減少創物件)。 </summary>
        public GameObject Get(string name)
        {
            // 檢查物件池是否有空閒物件
            Transform tsf = PoolRoot.transform;
            GameObject gb = (tsf.childCount > 0) ? tsf.GetChild(0).gameObject : Create();
            // 重新命名
            gb.name = name;
            // 移出物件池
            gb.transform.SetParent(playerRoot.transform, false);
            // 添加到使用中柱列
            _nowPlayer.Add(gb);
            _nowAudio.Add(gb, gb.GetComponent<AudioSource>());

            return gb;
        }
        
        
        /// <summary> 創建一個物件 </summary>
        GameObject Create()
        {
            GameObject gb = new GameObject();
            gb.AddComponent<AudioSource>();
            return gb;
        }

        /// <summary> 回收所有物件 </summary>
        public void RecoverAll()
        {
            foreach(var gb in _nowPlayer)
            {
                //丟進物件池並關閉
                gb.transform.SetParent(PoolRoot.transform, false);
                gb.SetActive(false);
            }
            //從使用中移除
            _nowPlayer.Clear();
            _nowAudio.Clear();
        }

        /// <summary> 回收指定名稱物件 </summary>
        public void Recover(string name)
        {
            var gbs = _nowPlayer.Where(gb => gb.name == name ).ToArray();
            foreach(var gb in gbs) Recover(gb);
        }

        /// <summary> 回收指定物件 </summary>
        public void Recover(GameObject obj)
        {
            //從使用中移除
            _nowPlayer.Remove(obj);
            _nowAudio.Remove(obj);
            //丟進物件池並關閉
            obj.transform.SetParent(PoolRoot.transform, false);
            obj.SetActive(false);
        }

        /// <summary> 從使用中的去取得物件 不存在則新建 </summary>
        public (AudioSource src, GameObject obj) GetAvailableSource(string name)
        {
            // 從使用中的去取得物件
            foreach (var _obj in _nowPlayer)
            {
                AudioSource _src = _obj.GetComponent<AudioSource>();
                Debug.Log(_obj.name + ".isPlaying = " + _src.isPlaying);
                if (_src.isPlaying) return (_src, _obj);
            }

            // 不存在則新建
            GameObject obj = Get(name);
            AudioSource src = obj.GetComponent<AudioSource>();

            return (src, obj);
        }
    }
}
