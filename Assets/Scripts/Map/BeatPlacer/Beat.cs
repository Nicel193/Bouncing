using Game.Controllers;
using Game.Player;
using UnityEngine;
using Zenject;

namespace Map
{
    public class Beat : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _hitVFX;
        [SerializeField] private GameObject _beatObject;

        private Player _player;
        private IGameProgressController _gameProgressController;
        
        [Inject]
        private void Construct(Player player, IGameProgressController gameProgressController)
        {
            _player = player;
            _gameProgressController = gameProgressController;
        }
        
        private void OnEnable()
        {
            _hitVFX.Stop();
            _beatObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_player.gameObject != other.gameObject) return;

            _hitVFX.Play();
            _beatObject.SetActive(false);
            _gameProgressController.AddScore();
        }

        public class Factory : PlaceholderFactory<Beat> { };
    }
}