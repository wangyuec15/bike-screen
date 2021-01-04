using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;

public class datasave : MonoBehaviour
{
    public GameObject player;
    public GameObject cam2;
    string filename;
    ArrayList dataContent;
    // Start is called before the first frame update
    void Start()
    {
        filename = getTime()+'C';
        dataContent = new ArrayList { "time,frame,camRotate,videoSpeed" };
    }

    // Update is called once per frame
    void Update()
    {
        string time = "";
        time += Time.time;
        string frame = "";
        frame += player.GetComponent<VideoPlayer>().frame;
        string camRotate = "";
        camRotate += cam2.GetComponent<Transform>().rotation.eulerAngles.y;
        string videoSpeed = "";
        videoSpeed += player.GetComponent<VideoPlayer>().playbackSpeed;
        //Debug.Log(videoSpeed);
        dataContent.Add(time + ',' + frame + ',' + camRotate + ',' + videoSpeed);
    }

    public void WriteCsv(ArrayList strs,string path)
    {
        if (!File.Exists(path))
        {
            File.Create(path).Dispose();
        }
        using (StreamWriter stream = new StreamWriter(path))
        {
            for(int i = 0; i < strs.Count; i++)
            {
                if (strs[i] != null)
                {
                    stream.WriteLine(strs[i]);
                }
            }
        }
    }
    void OnDestroy()
    {
        //ArrayList strs;
        //strs = new ArrayList { "123", "123,456", "123,456,789" };
        WriteCsv(dataContent, "E:\\data\\"+filename+".csv");
    }
    public string getTime()
    {
        string nametime = "";
        int day =  DateTime.Now.Day;
        int hour = DateTime.Now.Hour;
        int minute = DateTime.Now.Minute;
        int second = DateTime.Now.Second;

        nametime = nametime + day+hour+minute+second;

        return nametime;
    }
}


