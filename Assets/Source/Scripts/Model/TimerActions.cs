using System;
using System.Collections.Generic;

public class TimerActions : IUpdateble
{
    public event Action<int> GotReady;

    private Dictionary<int, Timer> _timers = new Dictionary<int, Timer>();
    private int _lastUsedID = -1;

    public int Create(float cooldown)
    {
        _lastUsedID++;

        var newTimer = new Timer(cooldown);
        _timers.Add(_lastUsedID, newTimer);

        return _lastUsedID;
    }

    public void SetCooldown(int id, float cooldown)
    {
        if (_timers.ContainsKey(id))
            _timers[id].SetCooldown(cooldown);
    }

    public bool TryDestroy(int id)
    {
        if (_timers.ContainsKey(id))
            _timers.Remove(id);
        else 
            return false;
        return true;
    }

    public void Update(float deltaTime)
    {
        foreach (var timer in _timers)
        {
            timer.Value.Update(deltaTime);
            if (timer.Value.Ready)
            {
                GotReady?.Invoke(timer.Key);
                timer.Value.ResetReadyCheck();
            }
        }
    }

    private class Timer
    {
        public float Cooldown { get; private set; }
        public float SecondsPassed { get; private set; }
        public bool Ready { get; private set; }

        public Timer(float cooldown)
        {
            Cooldown = cooldown;
            Ready = false;
        }

        public void Update(float deltaTime)
        {
            if (Ready && SecondsPassed == 0)
                return;

            SecondsPassed += deltaTime;

            if (Ready)
            {
                SecondsPassed = 0;
            }
            else if (Cooldown <= SecondsPassed)
            {
                Ready = true;
                SecondsPassed -= Cooldown;
            }
        }

        public void SetCooldown(float cooldown)
        {
            if (cooldown < 0)
                throw new ArgumentOutOfRangeException(nameof(cooldown));

            Cooldown = cooldown;
        }

        public void ResetReadyCheck()
        {
            Ready = false;
        }
    }
}
