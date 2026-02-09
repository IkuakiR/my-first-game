using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class TitleManager : MonoBehaviour
{
    [SerializeField, Header("決定音")]
    private GameObject _submitSE;

    [SerializeField, Header("タイトルUI一式")]
    private GameObject _titlePanel;

    [SerializeField, Header("ランキングUI一式")]
    private GameObject _rankingPanel;

    private bool _bStart;
    private bool _isRanking;
    private Fade _fade;

    void Start()
    {
        _bStart = false;
        _isRanking = false;

        if (_titlePanel != null) _titlePanel.SetActive(true);
        if (_rankingPanel != null) _rankingPanel.SetActive(false);

        _fade = FindObjectOfType<Fade>();
        if (_fade != null) _fade.FadeStart(_TitleStart);
        else _TitleStart();
    }

    void Update()
    {
        
    }

    private void _TitleStart() {
        _bStart = true;
    }

    private void _ChangeScene() {
        SceneManager.LoadScene("Main");
    }

    public void OnspaceClick(InputAction.CallbackContext context) {
        if (!context.performed) return;
        if (!_bStart) return;
        if (_isRanking) return;

        if (_fade != null) _fade.FadeStart(_ChangeScene);
        else _ChangeScene();

        _bStart = false;
        Instantiate(_submitSE);
    }

    public void OnTabToggle(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!_bStart) return;

        _isRanking = !_isRanking;

        if (_titlePanel != null) _titlePanel.SetActive(!_isRanking);
        if (_rankingPanel != null) _rankingPanel.SetActive(_isRanking);
    }

    public void OnEscape(InputAction.CallbackContext context) {
        if (!context.performed) return;
        Application.Quit();
    }
}
