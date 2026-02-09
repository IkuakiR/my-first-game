using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance { get; private set; }

    public float Elapsed { get; private set; }
    public bool Running { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!Running) return;
        Elapsed += Time.deltaTime; // timeScale=0 で止まる（普通はこれでOK）
    }

    public void ResetAndStart()
    {
        Elapsed = 0f;
        Running = true;
    }

    public void Stop()
    {
        Running = false;
    }

    public void AddPenalty(float seconds)
    {
        Elapsed += seconds;
    }
}
