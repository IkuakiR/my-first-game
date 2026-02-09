using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text timeText;

    void Reset()
    {
        timeText = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (GameTimer.Instance == null) return;

        float t = GameTimer.Instance.Elapsed;
        int minutes = (int)(t / 60f);
        float seconds = t % 60f;

        timeText.text = $"{minutes:00}:{seconds:00.000}";
    }
}