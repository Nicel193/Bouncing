using UnityEngine;
using Zenject;

namespace Map
{
    public class Beat : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitVFX;
        [SerializeField] private GameObject _beatObject;

        private void OnEnable()
        {
            _hitVFX.Stop();
            _beatObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            _hitVFX.Play();
            _beatObject.SetActive(false);
        }

        public class Factory : PlaceholderFactory<Beat> { };
    }
}