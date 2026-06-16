using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using VContainer;

namespace DIStudy.Mogura
{
    public class Mogura : MonoBehaviour
    {
        [SerializeField] private AudioClip hitClip;
        private IScoreService m_Score;
        private IAudioService m_Audio;
        private MoguraConfig m_Config;
        private bool collected;

        public event Action<Mogura> Collected;

        public int HoleIndex { get; set; }

        [Inject]
        public void Construct(IScoreService score, IAudioService audio, MoguraConfig config)
        {
            m_Score = score;
            m_Audio = audio;
            m_Config = config;
            AutoDespawn().Forget();
        }

        public void Collect()
        {
            if (collected)
                return;
            if (m_Score == null)
            {
                return;
            }
            collected = true;
            m_Score.Add(m_Config.MoguraValue);
            Debug.Log($"{m_Score.CurrentScore}");
            m_Audio.PlaySoundEffect(hitClip);
            Collected?.Invoke(this);
            Destroy(gameObject);
        }
        private async UniTask AutoDespawn()
        {
            await UniTask.Delay(System.TimeSpan.FromSeconds(m_Config.Lifetime));
            if (collected) return;
            collected = true;
            Collected?.Invoke(this);
            Destroy(gameObject);
        }

    }
}


