using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void InitFromStart()
    {
        base.InitFromStart();

        Debug.Log($"GameManager InitFromStart : {this.name}");
    }

    public void Play(Action action)
    {
        action?.Invoke();
        Debug.Log("GameManager Play Method Call");
    }

    public void PlayCoroutine(float seconds, Action action)
    {
        Debug.Log("GameManager start coroutine");
        StartCoroutine(PlayRoutine(seconds, action));
    }

    IEnumerator PlayRoutine(float seconds, Action action)
    {
        Debug.Log($"wait {seconds} second");
        yield return new WaitForSeconds(seconds);
        action?.Invoke();
        Debug.Log("Action");
    }
}
