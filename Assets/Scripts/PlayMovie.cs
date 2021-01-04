using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayMovie : MonoBehaviour
{

    public string pth;
    public GameObject maincamera;
    VideoPlayer vp;
    AudioSource wind;
    bool high,lasthigh = true;
    public float l_max = 30;
    public float l_min = 10;
    public float timer = 0f;
    public float lasttime = 6f;

    bleLink ble;
    void Start()
    {
        vp = gameObject.GetComponent<VideoPlayer>();
        vp.playbackSpeed = 0f;
        wind = gameObject.GetComponent<AudioSource>();
        

            vp.source = VideoSource.Url;
            vp.url = pth;
        
        ble = maincamera.GetComponent<bleLink>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer>lasttime&&maincamera.GetComponent<turning>().playing&&timer>1f){
            vp.playbackSpeed =Mathf.Min(2.5f,  0.5f/timer);

            vp.playbackSpeed =Mathf.Max(0.8f,  0.5f/timer);
        }
        //Debug.Log(ble.serial);
        if(ble.serial.Contains("light")){
            //Debug.Log("get delta!!!!");
            float light = float.Parse( ble.serial.Substring(5));
            lighton(light);
            
            if(4/(timer+Time.deltaTime*2)>vp.playbackSpeed&&timer>1f){
                vp.playbackSpeed =Mathf.Min(2.5f,   0.5f/(timer+Time.deltaTime*2));
            }
            /*else{
                vp.playbackSpeed =Mathf.Min(2f,  0.7f/timer);
            }*/
            if(high!=lasthigh){

                if(timer>0.2){

                    vp.playbackSpeed = Mathf.Max(0.5f / lasttime, 0.5f / timer);
                    lasttime = timer;
                    timer = 0;
                }
                else
                {
                    lasttime = 0.2f;
                    timer = 0;
                }
            }
            lasthigh = high;
        }
              
        if(timer>4){
                vp.playbackSpeed = 0;
            //maincamera.GetComponent<turning>().playing = false;
        }
        //print(vp.frame);

        /*if (Input.GetKey(KeyCode.Escape))
        {
            GameObject.Destroy(s1);
            SceneManager.LoadScene("ChooseFile");
        }*/

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            vp.playbackSpeed += 0.2f;
            wind.volume += 0.05f;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            vp.playbackSpeed -= 0.2f;
            wind.volume -= 0.05f;
        }
        //timer = 0;
    }

    public void lighton(float i){
    if(i>l_max){
        high = true;
    }
    else if (i<l_min){
        high = false;
    }
    }
    public void pressed()
    {
        vp.playbackSpeed = Mathf.Min(5, timer);
        Debug.Log(timer);
        timer = 0;
    }

    private GameObject[] getDontDestroyOnLoadGameObjects()
    {
        var allGameObjects = new List<GameObject>();
        allGameObjects.AddRange(FindObjectsOfType<GameObject>());

        for (var i = 0; i < SceneManager.sceneCount; i++)
        {
            var scene = SceneManager.GetSceneAt(i);
            var objs = scene.GetRootGameObjects();
            for (var j = 0; j < objs.Length; j++)
            {
                allGameObjects.Remove(objs[j]);
            }
        }

        int k = allGameObjects.Count;
        while (--k >= 0)
        {
            if (allGameObjects[k].transform.parent != null)
            {
                allGameObjects.RemoveAt(k);
            }
        }
        Debug.LogError(allGameObjects.Count);
        Debug.LogError(allGameObjects[0].name);
        return allGameObjects.ToArray();
    }
}

