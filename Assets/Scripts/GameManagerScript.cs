using UnityEngine;
using System.Collections;

public enum GameState
{
    Running,
    Paused,
    GameOver
}

public class GameManagerScript : MonoBehaviour
{

    private static GameState _state;

    void Start()
    {
        _state = GameState.Running;
    }

    public static void PauseGame()
    {
        _state = GameState.Paused;
    }

    public static void UnPauseGame()
    {
        _state = GameState.Running;
    }

    public static void GameOver()
    {
        _state = GameState.GameOver;
    }

    public static GameState State
    {
        get { return _state; }
    }
}
