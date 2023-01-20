using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timerStart;
    public Text textTimer;
    public int sceneIndex;
    public Player player;
    void Start()
    {
        textTimer.text = timerStart.ToString();
    }
    void Update()
    {
        if (timerStart > 0 && player.healthPoint > 0)
        {
            timerStart -= Time.deltaTime;
            textTimer.text = Mathf.Round(timerStart).ToString();
        }
        else if (player.healthPoint <= 0)
        {
            SceneManager.LoadScene(4);
        }
        else if (timerStart < 0 && player.healthPoint > 0)
        {
            Destroy(player);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}