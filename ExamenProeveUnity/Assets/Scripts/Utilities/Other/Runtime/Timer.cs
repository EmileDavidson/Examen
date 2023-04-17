using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

[Serializable]
public class Timer
{
    [field: SerializeField] private float wantedTime;
    private float _currentTime;
    public UnityEvent onTimerFinished = new();
    public UnityEvent<float> onTimerUpdate = new();

    public Timer(float timeInMilliseconds)
    {
        wantedTime = timeInMilliseconds / 1000;
        _currentTime = 0;
    }

    public void Update(float deltaTime)
    {
        _currentTime += deltaTime;
        onTimerUpdate.Invoke(_currentTime / wantedTime);
        if (_currentTime >= wantedTime)
        {
            onTimerFinished.Invoke();
        }
    }

    public float WantedTime => wantedTime;
}