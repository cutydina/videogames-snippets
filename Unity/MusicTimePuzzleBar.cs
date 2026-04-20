using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTimePuzzleBar : MonoBehaviour
{
    public BoxCollider2D SafeZone;
    public float speed;
    public float distance = 1f;
    public static bool youWin;

    private float _startY;
    private float _startTime;
    private bool _insideSafeZone = false;
    private bool _gameEnded = false;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        if (!_gameEnded)
        {
            float time = Time.time - _startTime;
            float y = _startY + Mathf.Sin(time * 2f) * distance;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SafeZone)
        {
            _insideSafeZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == SafeZone)
        {
            _insideSafeZone = false;
        }
    }

    public void OnMouseDown()
    {
        if (!_gameEnded)
        {
            if (_insideSafeZone)
            {
                Debug.Log("You win");
                _gameEnded = true;
                youWin = true;
                speed = 0f;
            }
            else
            {
                Debug.Log("You lose");
                _gameEnded = true;
                youWin = false;
                Invoke("InitializeGame", 2f); // Wait for 2 seconds before restarting the game
            }
        }
    }

    private void InitializeGame()
    {
        Debug.Log("Play Start");
        transform.position = new Vector3(transform.position.x, _startY, transform.position.z); // Reset position
        _startTime = Time.time; // Reset time
        speed = 2f; // Reset speed
        _insideSafeZone = false; // Reset safe zone status
        _gameEnded = false; // Reset game status
        youWin = false; // Reset win status
    }
}
