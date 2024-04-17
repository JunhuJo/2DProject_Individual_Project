using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //플레이어를 따라다니는 카메라
    [SerializeField] private float CameraSpeed = 5.0f;
    public float CameraYOffset = 2.0f;

    public GameObject player;
    

    private void Update()
    {
        Vector3 CMR = player.transform.position - this.transform.position;
        Vector3 moveVector = new Vector3(CMR.x * CameraSpeed * Time.deltaTime, CMR.y * CameraSpeed * Time.deltaTime, 0.0f);
        this.transform.Translate(moveVector);
    }
}
