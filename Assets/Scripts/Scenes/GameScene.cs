using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    public GameObject player;
    public Transform playerPosition;
    public CinemachineFreeLook freeLookCamera;
    protected override IEnumerator LoadingRoutine()
    {
        progress = 0f;
        
        Debug.Log("랜덤 맵 생성");
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.2f;
        
        Debug.Log("랜덤 몬스터 생성");
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.4f;

        Debug.Log("랜덤 아이템 배치");
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.6f;

        Debug.Log("플레이어 배치");
        player.transform.position = playerPosition.position;
        player.transform.rotation = playerPosition.rotation;
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.8f;

        Debug.Log("카메라 follow");
        
        yield return new WaitForSecondsRealtime(1f);
        progress = 1.0f;

        yield return new WaitForSecondsRealtime(1f);
    }
}
