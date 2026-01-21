using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class Segment : MonoBehaviour
    {
        public Action OnEnterSpawnTrigger;

        private void OnTriggerEnter(Collider other)
        {
            if (gameObject.CompareTag("Segment") && other.TryGetComponent(out DestroyTrigger trigger))
            {
                OnEnterSpawnTrigger?.Invoke();
            }
        }
    }
}
