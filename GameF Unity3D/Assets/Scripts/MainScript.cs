using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFLibrary;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainScript : MonoBehaviour
{
    const int size = 4;
    Game game;
    public Text TextMoves;
    public Text TimeText;

    public int iCountDownSec = 60;
    public int iCountDownMin = 9;// Seconds
    public bool bTimeEnd = false;
    public bool bAttempt = false;

    void Start()
    {
        game = new Game(size);
        HideButtons();
    }
    public void OnStart()
    {
        TimeText.text = iCountDownMin.ToString();
        game.Start(20);
        ShowButtons();
        iCountDownMin = 9;
        iCountDownSec = 59;
        bAttempt = false;
        TextMoves.text = "Number of steps: " + game.moves;
    }
    public void OnClick()
    {
        if (game.Solved()) return; //если игра решена кнопки не нажимаются 
        string name = EventSystem.current.currentSelectedGameObject.name;
        int x = int.Parse(name.Substring(0, 1));
        int y = int.Parse(name.Substring(1, 1));
        game.PressAt(x, y);
        ShowButtons();

        if (game.Solved())
        {
            TextMoves.text = "Game finished in " + game.moves + " moves";
        }
    }
    void ShowDigitAt(int digit, int x, int y)
    {
        string name = x + "" + y;
        var button = GameObject.Find(name);
        var text = button.GetComponentInChildren<Text>();
        text.text = ToNumber(digit);
        button.GetComponentInChildren<Image>().color = (digit > 0) ? Color.white : Color.clear;//set Visible
    }
    void HideButtons()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                ShowDigitAt(0, x, y);
            }
        }
    }

    string ToNumber(int digit)
    {
        if(digit == 0)
        {
            return "";
        }
        else
        {
            return digit.ToString();
        }
    }
    void ShowButtons()
    {
        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                ShowDigitAt(game.GetDigitAt(x, y), x, y);
            }
        }
        TextMoves.text = "Number of steps: " + game.moves;
    }
    public void EndGame()
    {
       // _timer.Stop();
        TextMoves.text = "Game was end";
        HideButtons();
    }

    public void TimeWasEnd()
    {
        TextMoves.text = "Time was end";
        if (!bAttempt) // 1 попытка
        {
            bAttempt = true;
            game.Shuffle(11);
            ShowButtons();
            iCountDownMin = 4;
            iCountDownSec = 59;
            TextMoves.text = "Try again";
        }
        if (iCountDownMin == 0 && iCountDownSec == 0) // вермя вышло
        {
            EndGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
