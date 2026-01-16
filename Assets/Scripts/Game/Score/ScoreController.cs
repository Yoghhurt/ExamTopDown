using UnityEngine;
using UnityEngine.Events;

public class ScoreController : MonoBehaviour
{
    public UnityEvent OnScoreChange = new UnityEvent();
    public int Score {get; private set;}

    public void AddScore(int amount)
    {
        Score += amount;
        OnScoreChange.Invoke();
    }
}
