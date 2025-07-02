using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    [SerializeField, Header("ゲームオーバーUI")]
    private GameObject _gameOverUI;

    private GameObject _player;

    void Start() {
        _player = FindObjectOfType<Player>().gameObject;
    }

    void Update() {
        _ShowGameOverUI();
    }

    private void _ShowGameOverUI() {
        if (_player != null) return;

        _gameOverUI.SetActive(true);
    }
}
