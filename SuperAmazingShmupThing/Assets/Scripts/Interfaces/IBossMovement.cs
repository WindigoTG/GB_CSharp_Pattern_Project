namespace ShmupProject
{
    public interface IBossMovement
    {
        public void Move(float widthLeft, float widthRight, float deltaTime);
        public void ResetBoss();
    }
}