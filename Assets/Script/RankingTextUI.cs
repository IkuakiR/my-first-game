using TMPro;
using UnityEngine;
using System.Text;

public class RankingTextUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void OnEnable()
    {
        if (text == null) return;

        if (LocalLeaderboard.Instance == null)
        {
            text.text = "NO DATA";
            return;
        }

        var scores = LocalLeaderboard.Instance.Data.scores;

        if (scores.Count == 0)
        {
            text.text = "NO RECORD";
            return;
        }

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < scores.Count; i++)
        {
            sb.AppendLine($"{i + 1:00}. {Format(scores[i].timeMs)}");
        }
        text.text = sb.ToString();
    }

    private string Format(int ms)
    {
        int minutes = ms / 60000;
        int secMs = ms % 60000;
        int seconds = secMs / 1000;
        int milli = secMs % 1000;
        return $"{minutes:00}:{seconds:00}.{milli:000}";
    }
}
