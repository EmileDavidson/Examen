using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable]
public class Timer
{
    [field: SerializeField] private float wantedTime;
    
    private float _currentTime;
    private bool _isFinished = false;
    
    public UnityEvent onTimerFinished = new();
    public UnityEvent onTimerFinishing = new();
    public UnityEvent<float> onTimerUpdate = new();

    public Timer(float timeInSeconds)
    {
        wantedTime = timeInSeconds;
        _currentTime = 0;
    }

    public void Update(float deltaTime)
    {
        if (_isFinished) return;
        
        _currentTime += deltaTime;

        onTimerUpdate.Invoke(_currentTime / wantedTime);
        if (_currentTime >= wantedTime)
        {
            FinishTimer();
        }
    }

    public void ForceFinish()
    {
        if (_isFinished) return;
        FinishTimer();
    }

    private void FinishTimer()
    {
        onTimerFinishing.Invoke();
        _isFinished = true;
        onTimerFinished.Invoke();
    }

    public float WantedTime => wantedTime;

    public bool IsFinished => _isFinished;
}