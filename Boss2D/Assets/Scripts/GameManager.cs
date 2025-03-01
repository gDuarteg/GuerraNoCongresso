﻿public class GameManager {

    private static GameManager _instance;

    public PlayerController player1 { get; set; }
    public PlayerController player2 { get; set; }

    public enum PlayerTurn {
        PLAYER1, PLAYER2
    }
    public PlayerTurn currentTurn {
        get; private set;
    }

    public enum GameState { MENU, GAME, PAUSE, ENDGAME };

    public GameState gameState { get; private set; }

    public delegate void ChangeStateDelegate();
    public static ChangeStateDelegate changeStateDelegate;

    private GameManager() {
        currentTurn = PlayerTurn.PLAYER1;
        gameState = GameState.MENU;
    }

    public static GameManager GetInstance() {
        if (_instance == null) {
            _instance = new GameManager();
        }

        return _instance;
    }

    public void changeTurn() {
        if (currentTurn == PlayerTurn.PLAYER1) {
            currentTurn = PlayerTurn.PLAYER2;
            player2.gameObject.GetComponent<PlayerController>().enabled = true;
            player2.stepCounter = 25;
 

            player1.gameObject.GetComponent<PlayerController>().enabled = false;
        }
        else {
            currentTurn = PlayerTurn.PLAYER1;
            player1.gameObject.GetComponent<PlayerController>().enabled = true;
            player1.stepCounter = 25;

            player2.gameObject.GetComponent<PlayerController>().enabled = false;
        }
    }

    public void DisableBothPlayers() {
        player1.gameObject.GetComponent<PlayerController>().enabled = false;
        player2.gameObject.GetComponent<PlayerController>().enabled = false;
    }

    public void changeState(GameState nextState) {
        if (gameState != GameState.PAUSE && nextState == GameState.GAME) Reset();
        gameState = nextState;
        changeStateDelegate();

        //if (gameState == GameState.PAUSE || gameState == GameState.MENU || gameState == GameState.ENDGAME) {
        //    audioMgr.PauseAllMusic();
        //}

        //if (gameState == GameState.GAME) {
        //    if (currentTurn == PlayerTurn.PLAYER1) {
        //        audioMgr.SetLulaMusic();
        //    }
        //    else if (currentTurn == PlayerTurn.PLAYER2) {
        //        audioMgr.SetBolsonoaroMusic();
        //    }
        //}
    }

    private void Reset() {
        player1.Vida = 5;
        player1.transform.position = new UnityEngine.Vector3(-22.4f, 8.5f, 0);
        
        player2.Vida = 5;
        player2.transform.position = new UnityEngine.Vector3(19.5f, 8.5f, 0);
    }
}