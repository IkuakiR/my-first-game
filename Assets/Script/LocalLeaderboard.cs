using System;
using System.IO;
using UnityEngine;

public class LocalLeaderboard : MonoBehaviour
{
    public static LocalLeaderboard Instance { get; private set; }

    private const int MaxRank = 10;
    private LeaderboardData _data = new LeaderboardData();

    private string FilePath =>
        Path.Combine(Application.persistentDataPath, "leaderboard.json");

    public LeaderboardData Data => _data;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        Load();
    }

    public void AddTimeSeconds(float seconds)
    {
        int ms = (int)Math.Round(seconds * 1000.0);
        long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        _data.scores.Add(new ScoreEntry(ms, now));

        _data.scores.Sort((a, b) =>
        {
            int c = a.timeMs.CompareTo(b.timeMs);
            if (c != 0) return c;
            return a.unixTime.CompareTo(b.unixTime);
        });

        if (_data.scores.Count > MaxRank)
            _data.scores.RemoveRange(MaxRank, _data.scores.Count - MaxRank);

        Save();
    }

    private void Save()
    {
        try
        {
            string json = JsonUtility.ToJson(_data, true);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception e)
        {
            Debug.LogError($"Save failed: {e}");
        }
    }

    private void Load()
    {
        try
        {
            if (!File.Exists(FilePath))
            {
                _data = new LeaderboardData();
                return;
            }

            string json = File.ReadAllText(FilePath);
            var loaded = JsonUtility.FromJson<LeaderboardData>(json);
            _data = loaded ?? new LeaderboardData();
        }
        catch (Exception e)
        {
            Debug.LogError($"Load failed: {e}");
            _data = new LeaderboardData();
        }
    }
}
