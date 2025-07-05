using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    private float _time = 0;
    private float period;
    public bool isWork { get; private set; }

    public Timer(float period, float startTime=0)
    {
        this.period = period;
        _time = startTime;
        Stop();
    }

    public Timer Start()
    {
        isWork = true;
        return this;
    }

    public void Stop()
    {
        isWork = false;
    }

    public void Update()
    {
        if (isWork)
            _time += Time.deltaTime;
    }

    public void Refresh()
    {
        _time = 0;
    }
    public bool IsTime()
    {
        return _time >= period;
    }
}
