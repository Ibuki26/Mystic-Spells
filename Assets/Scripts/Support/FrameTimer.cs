using UnityEngine;

public class FrameTimer : MonoBehaviour
{
    private int _elapsed;
    private int _duration;
    private bool _isRunnung = false;

    public bool IsRunning => _isRunnung;

    public void StartTimer(int frame)
    {
        _elapsed = 0;
        _duration = frame;
        _isRunnung = true;
    }

    public bool UpdateTimer()
    {
        if (!_isRunnung)
            return false;

        _elapsed++;

        if(_elapsed >= _duration)
        {
            _isRunnung = false;
            return true;
        }

        return false;
    }

    public void Reset()
    {
        _elapsed = 0;
        _duration = 0;
        _isRunnung = false;
    }
}
