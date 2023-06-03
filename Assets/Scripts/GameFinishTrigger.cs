using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private GameWonPanel gameWonPanel;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameBert"))
        {
            PlayerPrefs.SetInt("GameFinished", 1);
            PlayerPrefs.Save();
            gameWonPanel.OpenAnimated();
        }
    }
}