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

    public bool TimerIsOver()
    {
        if (CurrentTime <= Duration)
        {
            CurrentTime += Time.deltaTime;
            return false;
        }
        else
        {
            CurrentTime = 0f;
            return true;
        }
    }

    public void ResetTimer()
    {
        CurrentTime = 0f;
    }
    
  
}
