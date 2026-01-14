using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeExit;
    
    public void OnPlayerDeath()
    {
        Invoke(nameof(EndGame), _timeBeforeExit);
    }

    private void EndGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
