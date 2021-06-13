namespace ShmupProject
{
    public abstract class EnemyWaveState
    {
        public abstract void Operate(EnemyController controller, float deltaTime);
    }
}