namespace ShmupProject
{
    public class WaveDelayState : EnemyWaveState
    {
        float _maxDelayTime = 1.5f;
        float _currentDelayTime;

        public WaveDelayState()
        {
            _currentDelayTime = _maxDelayTime;
        }

        public override void Operate(EnemyController controller, float deltaTime)
        {
            if (_currentDelayTime > 0)
                _currentDelayTime -= deltaTime;
            else
            {
                _currentDelayTime = _maxDelayTime;

                if (++controller.WaveNumber % 5 == 0)
                    controller.SetBossWave();
                else
                    controller.SetRegularWave();
            }
        }
    }
}