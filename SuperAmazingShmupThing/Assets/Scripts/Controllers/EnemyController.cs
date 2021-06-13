using UnityEngine;

namespace ShmupProject
{
    public sealed class EnemyController : IUpdateable
    {
        private EnemyWaveState _wave;
        private RegularEnemyWaveState _regularWave;
        private BossWaveState _bossWave;
        private WaveDelayState _delayWave;

        public bool IsEnemyWaveInProgress { get; private set; }

        public int WaveNumber = 0;

        public EnemyController(Transform targetPlayer, EnemyFactory enemyFactory, BossFactory bossFactory)
        {
            _regularWave = new RegularEnemyWaveState(targetPlayer, enemyFactory);
            _bossWave = new BossWaveState(bossFactory, targetPlayer);
            _delayWave = new WaveDelayState();

            _wave = _delayWave;
        }

        public void SetRegularWave()
        {
            _wave = _regularWave;
            IsEnemyWaveInProgress = true;
        }

        public void SetBossWave()
        {
            _wave = _bossWave;
        }

        public void SetWaveDelay()
        {
            _wave = _delayWave;
            IsEnemyWaveInProgress = false;
        }

        public void UpdateRegular(float deltaTime)
        {
            _wave.Operate(this, deltaTime);
        }
    }

    public enum MovementFunction
    { 
        Linear,
        Quadratic,
        Cubic
    }
}