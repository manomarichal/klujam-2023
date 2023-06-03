using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void OnGameStartClicked()
    {
        // TODO Add some animated shit here
        SceneManager.LoadSceneAsync("Game");
    }
    
}
