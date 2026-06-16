using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DIStudy.Mogura
{
    public class MoguraConfig
    {
        [SerializeField]
        private int m_MoguraValue = 1;

        [SerializeField]
        private float m_AutoSaveInterval = 10f;

        [SerializeField]
        private int m_MoguraCount = 0;

        private float m_RespawnDelay = 0.6f;
        private float m_Lifetime = 1f;
        public float Lifetime => m_Lifetime;
        public int MaxMoguraCount => 3;

        private List<int> m_spawnedMoguraIndices = new List<int>();

        public int MoguraValue => m_MoguraValue;
        public float AutoSaveInterval => m_AutoSaveInterval;
        public int MoguraCount => m_MoguraCount;
        public float RespawnDelay => m_RespawnDelay;

        public bool AddMoguraIndex(int moguraIndex)
        {
            if (m_spawnedMoguraIndices.Contains(moguraIndex))
                return false;
            m_spawnedMoguraIndices.Add(moguraIndex);
            m_MoguraCount++;
            return true;
        }
        public void RemoveMoguraIndex(int moguraIndex)
        {
            m_spawnedMoguraIndices.Remove(moguraIndex);
            m_MoguraCount--;
        }

        public void ClearIndices()
        {
            m_spawnedMoguraIndices.Clear();
            m_MoguraCount = 0;
        }
    }
}
