using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;

namespace DIStudy.Mogura
{
    public class MoguraSpawner : MonoBehaviour
    {
        [SerializeField] private Mogura moguraPrefab;
        [SerializeField] private List<Transform> spawnPosition;
        [SerializeField] private float yUnderInterval;
        [SerializeField] private float moveAmount;
        private IObjectResolver _resolver;
        private MoguraConfig _config;
        private IGameService _game;
        private float spawnTimer;
        private readonly List<Mogura> _activeMoguras = new List<Mogura>();

        [Inject]
        public void Construct(IObjectResolver resolver, MoguraConfig config, IGameService game)
        {
            _resolver = resolver;
            _config = config;
            _game = game;
            _game.GameRestarted += OnGameRestarted;
        }

        private void Update()
        {
            if (!_game.IsRunning) return;

            spawnTimer += Time.deltaTime;
            if (spawnTimer >= _config.RespawnDelay)
            {
                if (_config.MoguraCount < _config.MaxMoguraCount)
                {
                    Spawn();
                    spawnTimer = 0f;
                }
            }
        }

        private void Spawn()
        {
            int index;
            while (true)
            {
                index = Random.Range(0, spawnPosition.Count);
                if (_config.AddMoguraIndex(index))
                    break;
            }
            Vector3 position = spawnPosition[index].position;
            position.y = position.y - yUnderInterval;
            Mogura mogura = _resolver.Instantiate(moguraPrefab, position, Quaternion.identity);
            mogura.HoleIndex = index;
            mogura.Collected += OnMoguraCollected;
            _activeMoguras.Add(mogura);
            SpawnAnimation(mogura).Forget();
        }

        private void OnMoguraCollected(Mogura mogura)
        {
            mogura.Collected -= OnMoguraCollected;
            _activeMoguras.Remove(mogura);
            _config.RemoveMoguraIndex(mogura.HoleIndex);
        }

        private void OnGameRestarted()
        {
            foreach (Mogura m in _activeMoguras)
                if (m != null) Destroy(m.gameObject);
            _activeMoguras.Clear();
            _config.ClearIndices();
            spawnTimer = 0f;
        }

        private async UniTask SpawnAnimation(Mogura mogura)
        {
            Vector3 start = mogura.transform.position;
            Vector3 end = start + Vector3.up * moveAmount;
            float elapsed = 0f;
            float duration = 0.3f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                mogura.transform.position = Vector3.Lerp(start, end, elapsed / duration);
                await UniTask.Yield(mogura.destroyCancellationToken);
            }

            transform.position = end;
        }
    }
}
