using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject dinoPrefab;

    /*
     *  지정된 dinoPrefab 을 특정 위치에 만듭니다.
     *  여기에서 만든다는 것은 인스턴스를 하나 생성한다는 것입니다.
     */
    public void SpawnDinoPrefab()
    {
        Instantiate(dinoPrefab, new Vector3(0, 2.5f, 0), Quaternion.identity);
    }
}
