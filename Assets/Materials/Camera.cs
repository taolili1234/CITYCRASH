using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] public Transform y_Axis;
    //public Transform y_Axis;

    public Transform x_Axis;

    public Transform z_Axis;
   
    public Transform zoom_Axis;

    public Transform Player;
    
    public float roSpeed = 180;
   
    public float scSpeed = 50;
  
    public float limitAngle = 45;

    private float hor, ver, scrollView;
    float x = 0, sc = 10;
   
    public bool followFlag;
    
    public bool turnFlag;

    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    
    private void Update()
    {
        hor = Input.GetAxis("Mouse X");
        ver = Input.GetAxis("Mouse Y");

        scrollView = Input.GetAxis("Mouse ScrollWheel");

        if (hor != 0)
        {
            y_Axis.Rotate(Vector3.up*roSpeed*hor*Time.deltaTime); ;
        }

        if (ver != 0)
        {
            x += -ver * Time.deltaTime * 5000;
            x = Mathf.Clamp(x, -limitAngle, limitAngle);
            Quaternion q = Quaternion.identity;
            q = Quaternion.Euler(x, x_Axis.eulerAngles.y, x_Axis.eulerAngles.z);
            x_Axis.rotation = Quaternion.Lerp(x_Axis.rotation, q, Time.deltaTime);
        }

        if (scrollView != 0)
        {
            sc -= scrollView * scSpeed;
            sc = Mathf.Clamp(sc, 3, 10);
            zoom_Axis.transform.localPosition = new Vector3(0, 0, -sc);
        }

        if (followFlag && Player != null)
        {
            y_Axis.position = Vector3.Lerp(y_Axis.position, Player.position + Vector3.up, Time.deltaTime * 10f);
        }
        //
        if(turnFlag && Player != null)
        {
            Player.transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        }
    }
}
