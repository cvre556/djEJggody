using UnityEngine;
using UnityEngine.UI;


public class BamsongiGenerator : MonoBehaviour
{
    public GameObject gBamsongiPrefab = null;       //프리팹 설계도 전달을 위한 public GameObject 변수 선언
    
    // 파워차징을 위한 게이지 변수
    public Image iChargeGauge = null;               // 차징 게이지 이미지
    public Image iChargeGaugeBg = null;             // 차징 게이지 배경

    private float ChargeSpeed = 1000f;              // 게이지 차징속도
    private float MaxCharge = 2000f;                // 최대 게이지 차징값
    private float CurrentCharge = 0f;               // 현재 차징된 게이지의 값
    private bool Click = true;                      // 마우스 클릭 허용유무 
    private bool Charging = true;                   // 파워게이지 증가/감소 상태 제어변수

    GameObject insBamsongiPrefab = null;            //Istantiate된 밤송이 오브젝트 저장 변수
    Vector3 vBamsongiworldDir = Vector3.zero;       //밤송이 원드 좌표

    void Start()
    {
        // 파워게이지 초기세팅
        // 클릭과 차징 상태를 초기화해줌
        Click = true;
        Charging = true;
        // 차징 UI를 처음에는 비활성화되게함
        iChargeGauge.gameObject.SetActive(false);
        iChargeGaugeBg.gameObject.SetActive(false);
        
    }

    void Update()
    {
        // 줌 상태일 경우 발사 금지
        // 줌 상태를 확인하기 위해 씬 안에서 CameraController 컴포넌트를 가진 객체를 찾음
        CameraController zoomController = FindFirstObjectByType<CameraController>();
        // 줌 동작 중일 경우, 밤송이 발사를 막음
        if (zoomController != null && zoomController.IsZooming)
            return; // 줌 동작 중엔 발사 불가
        // 클릭 불가 상태이면 아래 코드는 실행되지 않게하기 위해서 사용
        if (!Click) return;

        // 마우스 버튼 누름 → 차징 시작
        if (Input.GetMouseButtonDown(0))
        {
            // 현재 차징된 값 초기화
            CurrentCharge = 0f; 
            // 게이지 UI 활성화
            iChargeGauge.gameObject.SetActive(true);
            iChargeGaugeBg.gameObject.SetActive(true);
        }

        // 마우스 버튼 누르고 있음 → 차징 진행
        if (Input.GetMouseButton(0))
        {
            // 차징 중
            if (Charging)
            {
                // 시간 기반으로 증가
                CurrentCharge += ChargeSpeed * Time.deltaTime;
                if (CurrentCharge >= MaxCharge)
                {
                    // 최대값으로 고정
                    CurrentCharge = MaxCharge;
                    // 감소로 전환
                    Charging = false; 
                }
            }
            // 감소 중
            else
            {
                // 시간 기반으로 감소
                CurrentCharge -= ChargeSpeed * Time.deltaTime;
                if (CurrentCharge <= 0f)
                {
                    // 최소값으로 고정
                    CurrentCharge = 0f;
                    // 증가로 전환
                    Charging = true; 
                }
            }
            // 게이지 UI에 현재 진행률 반영되게 사용
            iChargeGauge.fillAmount = CurrentCharge / MaxCharge;
        }

        // 마우스 버튼 뗌 → 밤송이 발사
        if (Input.GetMouseButtonUp(0))
        {
            UiManager.Instance.UpdateGameCount();   // 게임 카운트 증가
            // 게이지 UI 비활성화
            iChargeGauge.gameObject.SetActive(false);
            iChargeGaugeBg.gameObject.SetActive(false);

            // 밤송이 생성 및 발사
            insBamsongiPrefab = Instantiate(gBamsongiPrefab);   // 게임을 실행하는 도중에 밤송이 오브젝트를 생성

            /*
                 * Ray 클래스
                 *  Ray(레이)는 이름 그대로 광선이며, 광원의 좌표(Origin)와 광선의 방향(direction)을 멤버 변수로 갖음
                 *  Ray는 콜라이더가 적용된 오브젝트와 충돌을 감지하는 특징이 있음
                 *  ScreenPointToRay 메서드의 반환값으로 얻을 수 있는 Ray는 Origin이 Main Camera의 좌표고.
                 *  direction 방향으로 밤송이를 날리기 때문에 direction 벡터가 가진 normalized 변수를 사용해 길이가 1인 벡터를 만든 후
                 *  힘을 2000 곱한다. 일단 길이를 1 벡터로 해서 direction 벡터 크기에 관계없이 밤송이에 일정한 힘을 가할 수 있음
            */

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            vBamsongiworldDir = ray.direction;
            //현재 차징된 힘을 곱해서 발사되도록 사용
            insBamsongiPrefab.GetComponent<BamsongiController>().f_TargetShoot(vBamsongiworldDir.normalized * CurrentCharge);
        }
    }
}
