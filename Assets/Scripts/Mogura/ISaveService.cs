using System;

namespace DIStudy.Mogura
{
    public interface ISaveService
    {
        event Action<int> Saved;

        int LoadScore();

        void SaveScore(int score);
    }
}
