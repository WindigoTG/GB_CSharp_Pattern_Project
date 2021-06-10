
using UnityEngine.EventSystems;

public interface IScoreMessageReseiver : IEventSystemHandler
{
    public void AddScore(int score);
}
