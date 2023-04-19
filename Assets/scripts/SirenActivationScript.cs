using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SirenActivationScript : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _stepVolume;
    [SerializeField] private Trigger _trigger;
    private Coroutine _coroutine;
    private AudioSource _audioSource;

    private const float MaxVolume = 1;
    private const float MinVolume = 0;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0;
    }

    private void OnEnable()
    {
        _trigger.Entered += Activate;
        _trigger.Exitred += DeActivate;
    }

    private void OnDisable()
    {
        _trigger.Entered -= Activate;
        _trigger.Exitred -= DeActivate;
    }

    public void Activate()
    {
        StopCoroutine();
        _coroutine = StartCoroutine(ChangeVolume(_stepVolume, MaxVolume));
    }

    public void DeActivate()
    {
        StopCoroutine();
        _coroutine = StartCoroutine(ChangeVolume(_stepVolume, MinVolume));
    }

    private void StopCoroutine()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator ChangeVolume(float step, float targgetValue)
    {
        _audioSource.Play();

        while (Math.Abs(_audioSource.volume - targgetValue) > Mathf.Epsilon)
        {
            _audioSource.volume = Mathf.MoveTowards(
                _audioSource.volume,
                targgetValue,
                step * Time.deltaTime
            );

            yield return null;
        }

        if (_audioSource.volume == 0)
            _audioSource.Stop();
    }
}