using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponDatabase", order = 1)]
public class WeaponDatabaseScriptable : ScriptableObject, IEnumerable<WeaponScriptable>
{
    [SerializeField]private List<WeaponScriptable> table;

    // 搜尋武器 透過武器編號
    public WeaponScriptable Search(int weaponNumber) {
      foreach(WeaponScriptable w in table){
        if(w.weaponNumber == weaponNumber) return w;
      }

      Debug.LogWarning($"can't Search WeaponNumber {weaponNumber}");
      return null;
    }
    
    // 隨機取得一件武器
    public WeaponScriptable Random() {
      return table[UnityEngine.Random.Range(0, table.Count)];
    }

    // 隨機排序 用於抽選升級武器
    public void Shuffle()
    {
        for (int i = table.Count - 1; i > 0; i--)
        {
            int randomIndex = UnityEngine.Random.Range(0, i + 1);
            (table[i], table[randomIndex]) = (table[randomIndex], table[i]); // Swap
        }
    }

    // 用於 foreach 循序
    public IEnumerator<WeaponScriptable> GetEnumerator() => table.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => table.GetEnumerator();
}
