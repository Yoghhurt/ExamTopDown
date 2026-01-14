using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _timeBeforeExit;
    
    [SerializeField]
    private SceneController _sceneController;
    public void OnPlayerDeath()
    {
        Invoke(nameof(EndGame), _timeBeforeExit);
    }

    private void EndGame()
    {
        _sceneController.LoadScene("MainMenu");
        
    }
}
