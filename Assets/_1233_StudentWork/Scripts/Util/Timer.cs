using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour {

    [SerializeField] private float _duration = 1f;
    [SerializeField] private bool _autoReset;
    [SerializeField] private bool _playOnEnable = false;
    [SerializeField] private bool _stopOnDisable = true;
    [SerializeField] private UnityEvent _onElapsed;
    
    private bool _isRunning;
    private float _timeRemaining;

    private void Update() {
        if ( !_isRunning || _timeRemaining <= 0f )
            return;

        _timeRemaining -= Time.deltaTime;

        if ( _timeRemaining > 0f )
            return;

        if (_autoReset)
            _timeRemaining = _duration;

        _onElapsed?.Invoke();
    }

    private void OnEnable() {
        if ( _playOnEnable )
            StartTimer();
    }

    private void OnDisable() {
        if ( _stopOnDisable )
            StopTimer();
    }

    public void PauseTimer() {
        _isRunning = false;
    }

    public void ResumeTimer() {
        _isRunning = true;
    }

    public void ResetTimer() {
        _timeRemaining = _duration;
    }

    public void StartTimer() {
        ResetTimer();
        ResumeTimer();
    }

    public void StopTimer() {
        ResetTimer();
        PauseTimer();
    }

}
