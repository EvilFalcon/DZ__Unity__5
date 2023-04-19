using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class Trigger : MonoBehaviour
    {
        public UnityAction Entered;
        public UnityAction Exitred;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                Entered.Invoke();
            }
        }

        private void OnTriggerExit(Collider collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                Exitred.Invoke();
            }
        }
    }
}