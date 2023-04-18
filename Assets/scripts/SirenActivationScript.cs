using UnityEngine;

public class SirenActivationScript : MonoBehaviour
{
    [SerializeField] private float _stepVolume;
    [SerializeField] private Volume _volume;

    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            _volume.Enable(_stepVolume);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.TryGetComponent<Player>(out Player player))
        {
            _volume.Disable(_stepVolume);
        }
    }
}