using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    // 
    [SerializeField]
    private PlayerController playerControllerRef;


    /*
     * GameControl 스크립트에서는 게임의 시작과 동시에 진행되어야 하는 일들을 대부분 도맡아 할 것입니다.
     * 다른 오브젝트에 Start 함수가 있다면 순서가 꼬일 수 있으니 유의합니다.
     */
    void Start()
    {
        // Start 함수는 해당 스크립트에서만 사용을 하며 여러 스크립트나 오브젝트들의 초기화를 묶어 진행해줄 것입니다.
        playerControllerRef.InitPlayerController();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
