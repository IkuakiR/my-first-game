using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using TMPro;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject _gameOverUI;
    [SerializeField, Header("ゲームクリアUI")]
    private GameObject _gameClearUI;

    [SerializeField, Header("クリアタイム表示")]
    private TMP_Text _clearTimeText;

    [SerializeField, Header("BGM")]
    private AudioSource _bgm;
    [SerializeField, Header("決定音")]
    private GameObject _submitSE;
    [SerializeField, Header("ゲームクリアSE")]
    private GameObject _gameClearSE;
    [SerializeField, Header("ゲームオーバーSE")]
    private GameObject _gameOverSE;

    private GameObject _player;
    private bool _bShowUI;

    void Start() {
        _player = FindObjectOfType<Player>().gameObject;
        _bShowUI = false;

        Fade fade = FindObjectOfType<Fade>();
        if (fade != null) fade.FadeStart(_MainStart);

        _player.GetComponent<Player>().enabled = false;
        foreach (EnemySpawner enemySpawner in FindObjectsOfType<EnemySpawner>()) {
            enemySpawner.enabled = false;
        }
    }

    private void _MainStart() {
        _player.GetComponent<Player>().enabled = true;
        foreach (EnemySpawner enemySpawner in FindObjectsOfType<EnemySpawner>()) {
            enemySpawner.enabled = true;
        }
        if (GameTimer.Instance != null)
            GameTimer.Instance.ResetAndStart();
    }

    void Update() {
        _ShowGameOverUI();
    }

    private void _ShowGameOverUI() {
        if (_player != null || _gameOverUI.activeSelf) return;

        if (GameTimer.Instance != null)
            GameTimer.Instance.Stop();

        _gameOverUI.SetActive(true);
        _bShowUI = true;
        _bgm.Stop();
        Instantiate(_gameOverSE);
    }

    public void ShowGameClearUI() {
        if (_gameClearUI.activeSelf) return;

        if (GameTimer.Instance != null)
        {
            GameTimer.Instance.Stop();

            if (LocalLeaderboard.Instance != null)
                LocalLeaderboard.Instance.AddTimeSeconds(GameTimer.Instance.Elapsed);

            if (_clearTimeText != null)
                _clearTimeText.text = FormatTime(GameTimer.Instance.Elapsed);
        }
        
        _gameClearUI.SetActive(true);
        _bShowUI = true;
        _bgm.Stop();
        Instantiate(_gameClearSE);
    }

    public void OnRestart(InputAction.CallbackContext context) {
        if (!_bShowUI || !context.performed) return;

        if (GameTimer.Instance != null)
            GameTimer.Instance.Stop();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Instantiate(_submitSE);
    }

    public void OnEscape(InputAction.CallbackContext context) {
        if (!context.performed) return;
        Application.Quit();
    }

    private string FormatTime(float t)
    {
        int minutes = (int)(t / 60f);
        float seconds = t % 60f;
        return $"{minutes:00}:{seconds:00.000}";
    }
}
