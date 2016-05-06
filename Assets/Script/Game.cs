using UnityEngine;
using UnityEngine.UI;

public enum GameStatus
{
    Start,
    Play,
    Pause,
    GameOver,
}

public class Game : MonoBehaviour
{
    public static int score = 0;
    public SpawnFood sf;
    public Snake snake;
    public Toggle toggle;
    public Text textLose;
    public Flash flash;

    public static GameStatus curStatus = GameStatus.Start;

    private void Awake()
    {
        score = 0;
        curStatus = GameStatus.Start;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (curStatus == GameStatus.Start)
            {
                sf.Continuous = toggle.isOn;

                sf.enabled = true;
                snake.enabled = true;
                snake.gameObject.SetActive(true);

                textLose.gameObject.SetActive(false);
                toggle.gameObject.SetActive(false);
                flash.Stop();

                snake.OnLose += OnLose;
                curStatus = GameStatus.Play;
            }
            if (curStatus == GameStatus.GameOver)
            {
                Application.LoadLevel(0);
            }
        }
    }

    private void OnLose()
    {
        curStatus = GameStatus.GameOver;
        textLose.text = "Game Over & Score is " + score;
        textLose.gameObject.SetActive(true);

        flash.GetComponent<Text>().text = "Press Enter To Restart";
        flash.gameObject.SetActive(true);
        flash.self.Fflash();

        //snake.self.trailGOs.gameObject.SetActive(false);
        sf.self.foodGOs.gameObject.SetActive(false);
    }
}