using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Init()
    {
        base.Init();

        Debug.Log($"GameManager Init : {this.name}");
    }

    public void Play()
    {
        Debug.Log("GameManager Play Method Call");
    }
}
