using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject _gameOverUI;
    [SerializeField, Header("ゲームクリアUI")]
    private GameObject _gameClearUI;

    private GameObject _player;
    private bool _bShowUI;

    void Start() {
        _player = FindObjectOfType<Player>().gameObject;
        _bShowUI = false;
    }

    void Update() {
        _ShowGameOverUI();
    }

    private void _ShowGameOverUI() {
        if (_player != null) return;

        _gameOverUI.SetActive(true);
        _bShowUI = true;
    }

    public void ShowGameClearUI() {
        _gameClearUI.SetActive(true);
        _bShowUI = true;
    }

    public void OnRestart(InputAction.CallbackContext context) {
        if (!_bShowUI || !context.performed) return;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
