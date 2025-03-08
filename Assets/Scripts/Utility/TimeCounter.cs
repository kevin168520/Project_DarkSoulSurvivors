using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter
{
    private float timeInterval; // 間格
    private float timeCounter; // 計時
    private bool loopEnabled;  // 啟用循環計時
    
    public bool IsTiming => timeCounter < timeInterval; // 判定計時中
    public bool IsTimeOver => timeCounter >= timeInterval; // 判定計時超過

    public TimeCounter(float timeInterval, bool loopEnabled = false) {
        this.timeInterval = timeInterval;
        this.loopEnabled = loopEnabled;
    }

    public void SetTimeInterval(float f) {
        timeInterval = f;
    }

    public void SetLoopEnabled(bool b) {
        loopEnabled = b;
    }

    public bool Update(float deltaTime) {
        if (IsTiming) {
          timeCounter += deltaTime;
          if (IsTimeOver) {
              if (loopEnabled) Reset();
              return true;
          }
        }

        return false;
    }

    public bool UpdateDelta() => Update(Time.deltaTime);

    public bool UpdateFrame() => Update(1f);

    public void Reset() {
        timeCounter = 0;
    }
}
