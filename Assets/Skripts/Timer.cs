using UnityEngine;
using UnityEngine.Events;

public class Timer
{
    private float _time = 0;
    private float startTime = 0;
    private float period;
    public bool isWork { get; private set; }

    public Timer(float period, float startTime=0)
    {
        this.period = period;
        this.startTime = startTime;
        _time = startTime;
        Stop();
    }

    /// <summary>
    /// Запустить таймер
    /// </summary>
    public Timer Start()
    {
        isWork = true;
        return this;
    }

    /// <summary>
    /// Остановить таймер
    /// </summary>
    public void Stop()
    {
        isWork = false;
    }

    /// <summary>
    /// Метод обновления таймера
    /// </summary>
    public void Update()
    {
        if (isWork)
            _time += Time.deltaTime;
    }

    /// <summary>
    /// Сбросить таймер на startTime
    /// </summary>
    public void Refresh()
    {
        _time = startTime;
    }

    /// <summary>
    /// Сбросить таймер на 0
    /// </summary>
    public void Drop()
    {
        _time = 0;
    }

    /// <summary>
    /// Прошло ли время до события
    /// </summary>
    public bool IsTime()
    {
        return _time >= period;
    }
}
