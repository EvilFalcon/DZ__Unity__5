using System;
using System.Collections;
using UnityEngine;

public class SirenActivationScript : MonoBehaviour
{
    [SerializeField] private float _stepVolume;
    [SerializeField] private AudioSource _audioSource;
    private Coroutine _coroutine;
    private const float MaxVolume = 1;
    private const float MinVolume = 0;
    
    private event Action OnPlay;
    private event Action OfPlay;
    
    private void Awake()
    {
        OnPlay += Enable;
        OfPlay += Disable;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            OnPlay?.Invoke();
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            OfPlay?.Invoke();
        }
    }
    
    private void Enable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _audioSource.volume = 0;
        _audioSource.Play();
        Debug.Log(_audioSource.volume);

        _coroutine = StartCoroutine(ChangeVolume(_stepVolume, MaxVolume));
    }

    private void Disable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(_stepVolume, MinVolume));

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }

    private IEnumerator ChangeVolume(float step, float targgetValue)
    {
        while (Math.Abs(_audioSource.volume - targgetValue) > Mathf.Epsilon)
        {
            _audioSource.volume = Mathf.MoveTowards(
                _audioSource.volume,
                targgetValue,
                step * Time.deltaTime
            );

            yield return null;
        }
    }
}