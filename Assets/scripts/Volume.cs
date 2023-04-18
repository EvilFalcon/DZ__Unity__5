using System;
using System.Collections;
using UnityEngine;

public class Volume : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource = new AudioSource();
    private Coroutine _coroutine;

    public void Enable(float increaseStepVolume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _audioSource.volume = 0;
        _audioSource.Play();
        Debug.Log(_audioSource.volume);

        _coroutine = StartCoroutine(ChangeVolume(increaseStepVolume, 1));
    }

    public void Disable(float decreaseStepVolume)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ChangeVolume(decreaseStepVolume, 0));

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