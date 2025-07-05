using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    private float _time = 0;
    private float period;
    private UnityAction action;
    public bool isWork { get; private set; }

    public Timer(float period, UnityAction action)
    {
        this.period = period;
        this.action = action;
        Stop();
    }

    public void Start()
    {
        isWork = true;
    }

    public void Stop()
    {
        isWork = false;
    }

    public void Update()
    {
        if (isWork)
            _time += Time.deltaTime;

        if (isWork && _time > period)
        {
            action.Invoke();
            Refresh();
        }
    }

    public void Refresh()
    {
        _time = 0;
    }
}
