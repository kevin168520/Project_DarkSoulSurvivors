using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerUtility
{
    private float timeInterval; // 間格
    private float timeCounter; // 計時
    private bool isLoopOn;  // 循環計時
    public bool IsTiming => timeCounter < timeInterval; // 判定計時中
    public bool IsTimeOver => timeCounter >= timeInterval; // 判定計時超過

    public TimerUtility(float timeInterval, bool isLoopOn = false) {
        this.timeInterval = timeInterval;
        this.isLoopOn = isLoopOn;
    }

    public bool Update(float deltaTime) {
        if (IsTiming) {
          timeCounter += deltaTime;
          if (IsTimeOver) {
              if (isLoopOn) Reset();
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
