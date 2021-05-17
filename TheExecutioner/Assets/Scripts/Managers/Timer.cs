using UnityEngine;

public class Timer 
{
    public float Duration;
    public float CurrentTime { get; set; }

    private void Update()
    {
        
        TimerIsOver();
    }
    public Timer(float duration)
    {
        Duration = duration;
        CurrentTime = 0f;
    }
    /// <summary>
    /// return true if the timer is greater than 0
    /// else return false
    /// </summary>
    /// <returns></returns>
    public bool TimerIsOver()
    {
        if (CurrentTime <= Duration)
        {
            CurrentTime += Time.deltaTime;
            return false;
        }

        CurrentTime = 0f;
        return true;
    }
    /// <summary>
    /// reset the timer to the duration
    /// </summary>
    public void ResetTimer()
    {
        CurrentTime = Duration;
    }
    
  
}
