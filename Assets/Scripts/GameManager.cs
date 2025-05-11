using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event Action GameStartEvent;
    
    public void OnGameStart()
    {
        GameStartEvent?.Invoke();
    }
}
