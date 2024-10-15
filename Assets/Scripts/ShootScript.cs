using UnityEngine;
using UnityEngine.UIElements;

/*
총을 발사하는 스크립트 
*/
public class ShootScript : MonoBehaviour
{
    public GameObject arCamera;
    public GameObject smoke;

    public void Shoot()
    {
        RaycastHit hit;


        // 카메라 위치에서 카메라 앞쪽으로 Ray 발사
        if (Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit))
        {
            // Ray를 쐈을때 빔을 맞은 물체가 Target이면?
            if (hit.transform.gameObject.tag.Equals("Target"))
            {
                // 해당 오브젝트 삭제
                Destroy(hit.transform.gameObject);
                
                // 발사 효과(연기) 생성 
                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }


}
