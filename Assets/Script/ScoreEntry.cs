using System;

[Serializable]
public class ScoreEntry
{
    public int timeMs;
    public long unixTime;

    public ScoreEntry(int timeMs, long unixTime)
    {
        this.timeMs = timeMs;
        this.unixTime = unixTime;
    }
}
