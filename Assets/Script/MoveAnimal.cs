using UnityEngine;

public class MoveAnimal : MonoBehaviour
{
    public float moveRange = 10.0f;        // 좌우 이동 거리
    public float moveSpeed = 2.0f;        // 이동 속도
    private Vector3 startPos;

    void Start()
    {
        // 장애물의 원래 시작 위치를 저장할 변수
        startPos = transform.position;
    }

    void Update()
    {
        //장애물의 속도를 조절하고 최대 이동 범위를 조절하여 부드러운 움직임을 구사하게 생성
        float offset = Mathf.Sin(Time.time * moveSpeed) * moveRange;
        // y, z 축 위치는 고정 → (x축만 변화되도록,+ offset 추가) 좌우로만 왔다 갔다 함
        transform.position = new Vector3(startPos.x + offset, startPos.y, startPos.z);
    }
}
