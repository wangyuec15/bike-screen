using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateControl : MonoBehaviour
{

    public float longitude, latitude;
    bleLink ble;
    float delta;
    int receiveNum = 0;
    public float saveNum = 0;
    int rotatespeed = 10;
    public GameObject cam2, cam3, cam4;
    public GameObject ball;

    // Use this for initialization
    void Start()
    {
        //longitude = gameObject.transform.rotation.eulerAngles.y;
        //latitude = 0;
        Application.targetFrameRate = -1;
        ble = gameObject.GetComponent<bleLink>();
        delta = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            latitude += 5;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            receiveNum += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            latitude -= 5;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            receiveNum -= 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            longitude += 5;
        }
        if (Input.GetKey(KeyCode.W))
        {
            longitude -= 5;
        }
        gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(longitude, latitude, 0);

        if (Input.GetKey(KeyCode.Q))
        {
            Camera.main.fieldOfView -= 5;
        }
        if (Input.GetKey(KeyCode.E))
        {
            Camera.main.fieldOfView += 5;
        }
        float refAngle = Camera.main.GetComponent<turning>().getRefAngle();
        if ((cam2.GetComponent<Transform>().eulerAngles.y - refAngle) % 360 < 150)
        {
            cam2.GetComponent<Transform>().rotation = Quaternion.Euler(0, refAngle + 150, 0);
        }
        else if ((cam2.GetComponent<Transform>().eulerAngles.y - refAngle) % 360 > 210)
        {
            cam2.GetComponent<Transform>().rotation = Quaternion.Euler(0, refAngle + 210, 0);
        }
        transform.Rotate(Vector3.up * (-receiveNum+saveNum) * delta, Space.World);


        if(  ble.serial.Contains("ang")){
        receiveNum = int.Parse(ble.serial.Substring(3));
        }
        
        if(  ble.serial.Contains("button")){
            receiveNum =0;
            saveNum =0;

        }

        if(Input.GetKey(KeyCode.Space)){
            saveNum = receiveNum;
        }
        /*if (ble.serial != null)
        {
            int.TryParse(ble.serial, out receiveNum);
            transform.Rotate(Vector3.up * receiveNum * delta, Space.World);

            while (true) {
                if (ble.serial == "Switch Pressed!")
                {
                    ball.GetComponent<PlayMovie>().pressed();
                    break;
                }
            }
        }*/

        int rotate = (int)(saveNum-receiveNum)/2;
        cam2.transform.Rotate(Vector3.up * (float)rotate/ rotatespeed, Space.World);

        //根据朝前相机转一下左右相机
        cam3.transform.eulerAngles = new Vector3(0, cam2.transform.eulerAngles.y - 85, 0);
        cam4.transform.eulerAngles = new Vector3(0, cam2.transform.eulerAngles.y + 85, 0);

    }
}
