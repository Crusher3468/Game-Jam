using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
	

	[SerializeField] AudioSource gameMusic;

	[SerializeField] GameObject playerPrefab;
	[SerializeField] Transform playerStart;

    [Header("Events")]
    [SerializeField] EventRouter startGameEvent;
    [SerializeField] EventRouter stopGameEvent;
    [SerializeField] EventRouter winGameEvent;

    public enum State
	{
		TITLE,
		START_GAME,
		START_LEVEL,
		PLAY_GAME,
		PLAYER_DEAD,
		GAME_OVER,
		VICTORY
	}

	State state = State.TITLE;
	float stateTimer = 0;
	float gameTimer = 0;
	//int lives = 0;

	private void Start()
	{
		state = State.TITLE;
        winGameEvent.onEvent += OnGameWin;
    }

    public void Update()
	{
		switch (state)
		{
			case State.TITLE:
                UIManager.Instance.ShowTitle(true);
                Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
			case State.START_GAME:
				UIManager.Instance.ShowTitle(false);
				Cursor.lockState = CursorLockMode.Locked;
				//lives = 3;
				gameTimer = 90;
				state = State.START_LEVEL;
				break;
			case State.START_LEVEL:
				gameMusic.Play();
				startGameEvent.Notify();
				Instantiate(playerPrefab, playerStart.position, playerStart.rotation);
				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
				gameTimer -= Time.deltaTime;
				UIManager.Instance.SetTime(gameTimer);
				//Debug.Log(gameTimer.ToString());
				if (gameTimer <= 0)
				{
                    UIManager.Instance.ShowGameOver(true);
					stateTimer = 3;
					state = State.GAME_OVER;
                }
				break;
			case State.PLAYER_DEAD:
				stateTimer -= Time.deltaTime;
				if (stateTimer <= 0)
				{
					state = State.START_LEVEL;
				}
				break;
			case State.GAME_OVER:
				stateTimer -= Time.deltaTime;
				if (stateTimer <= 0)
				{
                    state = State.TITLE;
				}
				break;
			case State.VICTORY:
				stateTimer -= Time.deltaTime;
				if (stateTimer <= 0)
				{
					UIManager.Instance.ShowVictory(false);
					state = State.TITLE;
				}
				break;
			default:
				break;
		}
	}

    

    public void SetPlayerDead()
	{
		gameMusic.Stop();
		UIManager.Instance.ShowGameOver(true);
		state = State.GAME_OVER;
		stateTimer = 3;
	}

	public void AddTime(float time)
	{
        gameTimer += time;
		gameTimer = Mathf.Clamp(gameTimer, 0, 120);
		UIManager.Instance.SetTime(gameTimer);
	}

	public void SetGameOver()
	{
		UIManager.Instance.ShowGameOver(true);
		gameMusic.Stop();
		state = State.GAME_OVER;
		stateTimer = 3;
	}

	public void SetVictory()
	{
		UIManager.Instance.ShowVictory(true);
		gameMusic.Stop();
		state = State.VICTORY;
		stateTimer = 3;
	}

	public void StartGame()
	{
		state = State.START_GAME;
	}

    public void OnGameWin()
    {
		Debug.Log("Win");
    }
}
