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
        
        Debug.Log("���� �� ����");
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.2f;
        
        Debug.Log("���� ���� ����");
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.4f;

        Debug.Log("���� ������ ��ġ");
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.6f;

        Debug.Log("�÷��̾� ��ġ");
        player.transform.position = playerPosition.position;
        player.transform.rotation = playerPosition.rotation;
        yield return new WaitForSecondsRealtime(1f);
        progress = 0.8f;

        Debug.Log("ī�޶� follow");
        
        yield return new WaitForSecondsRealtime(1f);
        progress = 1.0f;

        yield return new WaitForSecondsRealtime(1f);
    }
}
