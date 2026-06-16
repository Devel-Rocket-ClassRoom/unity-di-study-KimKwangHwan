using System;
using UnityEngine;

namespace DIStudy.Mogura
{
    public class MoguraPlayerSaveService : ISaveService
    {
        private const string ScoreKey = "DIStudy.Mogura.Score";

        public event Action<int> Saved;

        public int LoadScore()
        {
            return PlayerPrefs.GetInt(ScoreKey, 0);
        }

        public void SaveScore(int score)
        {
            PlayerPrefs.SetInt(ScoreKey, score);
            PlayerPrefs.Save();
            Saved?.Invoke(score);
        }
    }
}


