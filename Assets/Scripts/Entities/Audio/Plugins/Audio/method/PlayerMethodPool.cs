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
        GameObject playerObject;

        /// <summary> 物件池 </summary>
        GameObject PoolObject;
        
        /// <summary> 初始化播放清單 </summary>
        /// <param name="thisObject"></param>
        /// <param name="type">播放器類型名稱</param>
        /// <param name="playlist">播放清單</param>
        /// <param name="fixValue">初始音量較正值</param>
        public PlayerMethodPool(GameObject thisObject, string type)
        {
            // 場景物件分類用物件
            #region [產生物件]
            playerObject = new GameObject();
            playerObject.transform.SetParent(thisObject.transform, false);
            playerObject.name = type;

            PoolObject = new GameObject();
            PoolObject.transform.SetParent(playerObject.transform, false);
            PoolObject.name = "Pool";
            #endregion
        }
        
        /// <summary> 檢查物件池是否有可用物件，無則產生一個，並返回物件(減少創物件)。 </summary>
        public GameObject Create()
        {
            Transform tsf = PoolObject.transform;
            GameObject obj;

            if (tsf.childCount > 0)
            {
                obj = tsf.GetChild(0).gameObject;
            }
            else
            {
                obj = new GameObject();
                obj.AddComponent<AudioSource>();
                obj.AddComponent<AudioAutoDestroyScript>();
            }

            if (obj.GetComponent<AudioSource>() == null)
                obj.AddComponent<AudioSource>();

            if (obj.GetComponent<AudioAutoDestroyScript>() == null)
                obj.AddComponent<AudioAutoDestroyScript>();

            obj.transform.SetParent(playerObject.transform, false);
            
            _nowPlayer.Add(obj);
            _nowAudio.Add(obj, obj.GetComponent<AudioSource>());

            return obj;
        }

        /// <summary> 回收所有物件 </summary>
        public void RecoverAll()
        {
            foreach(var gb in _nowPlayer)
            {
                //丟進物件池並關閉
                gb.transform.SetParent(PoolObject.transform, false);
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
            obj.transform.SetParent(PoolObject.transform, false);
            obj.SetActive(false);
        }

        /// <summary> 取得 AudioSource 不存在則新建 </summary>
        public (AudioSource src, GameObject obj) GetAvailableSource()
        {
            // foreach (var _obj in _nowPlayer)
            // {
            //    AudioSource _src = _obj.GetComponent<AudioSource>();
            //    if (!_src.isPlaying)
            //    {
            //        return (_src, _obj);
            //    }
            // }

            // 不存在則新建
            GameObject obj = Create();
            AudioSource src = obj.GetComponent<AudioSource>();

            return (src, obj);
        }
    }
}
