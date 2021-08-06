using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishLine : MonoBehaviour
{
    [SerializeField]
    GameObject winScreen,loseSecreen;
    [SerializeField]
    bot[] bots;
    [SerializeField]
    playerMovement movement;
    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "blue")
        {
            winScreen.SetActive(true);
            finshGame();
        }
        else
        {
            loseSecreen.SetActive(true);
            finshGame();
        }
    }
    void finshGame()
    {
        foreach(bot _bot in bots)
        {
            Destroy(_bot);
        }
        Destroy(movement);
    }
    public void tryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
      public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
