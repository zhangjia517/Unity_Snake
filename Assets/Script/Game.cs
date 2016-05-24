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

    private void Start()
    {
        string str = "";
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            str = "Press Enter To Start";
        else
            str = "Swipe UpperRightDiagonal To Start";

        flash.GetComponent<Text>().text = str;
    }

    private void Update()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                EnterGame();
            }
        }
        else
        {

        }

    }

    private void EnterGame()
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

    void OnSwipe(SwipeGesture gesture)
    {
        GameObject selection = gesture.StartSelection;
        string str = "Swiped " + gesture.Direction + " with finger " + gesture.Fingers[0] +
            " (velocity:" + gesture.Velocity + ", distance: " + gesture.Move.magnitude + " )";
        Debug.Log(str);

        if (gesture.Direction == FingerGestures.SwipeDirection.UpperRightDiagonal)
            EnterGame();
    }

    private void OnLose()
    {
        curStatus = GameStatus.GameOver;
        textLose.text = "Game Over & Score is " + score;
        textLose.gameObject.SetActive(true);

        string str = "";
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
            str = "Press Enter To Restart";
        else
            str = "Swipe UpperRightDiagonal To Restart";

        flash.GetComponent<Text>().text = str;
        flash.gameObject.SetActive(true);
        flash.self.Fflash();

        //snake.self.trailGOs.gameObject.SetActive(false);
        sf.self.foodGOs.gameObject.SetActive(false);
    }
}