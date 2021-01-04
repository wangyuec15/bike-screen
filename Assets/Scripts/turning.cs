using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System;

public class turning : MonoBehaviour
{
    public VideoPlayer vp;
    public Camera frontCamera;
    public GameObject left, right;
    float refAngle;
    public bool playing = true;

    private ArrayList fm, k, b;

    // Start is called before the first frame update
    void Start()
    {
    //vp.frame=3000;
        vp.playbackSpeed = 1;
        ArrayList framelist = new ArrayList() { 1024,2048};
        ArrayList directionlist = new ArrayList() { 90, 180 };
        for(int i = 0; i < framelist.Count; i++)
        {
            Debug.Log(framelist[i]);
            Debug.Log(directionlist[i]);
        }
        fm = new ArrayList() { 63, 377, 619, 958, 1147, 1301, 1382, 1491, 1625, 1708, 2400, 2531, 2601, 2679, 2900, 2992, 3036, 3097, 3799, 3845, 3931, 4034, 4258, 4910, 5139, 5181, 5219, 5992, 6401, 6459, 6518, 6570, 6617, 7007, 7795, 8223, 8421, 8491, 8563, 8641, 9044, 9519, 10164, 10476, 10639, 11019, 11147, 11235, 11297, 11658, 12102, 12270, 12356, 12418, 12591, 12909, 13118, 13181, 13229, 13304, 14294, 15033, 15189, 15490, 16158, 16462, 16535 };
        k = new ArrayList() {-0.066878981,-0.024793388,0.041297935,0.010582011,0.441558442,-0.12345679,-0.128440367,-0.044776119,-0.554216867,-0.005780347,-0.083969466,-0.757142857,-0.217948718,-0.018099548,0.315217391,0.454545455,0.442622951,-0.004273504,-0.847826087,-0.453488372,-0.126213592,0.03125,-0.009202454,-0.017467249,1.571428571,0.473684211,0,-0.024449878,-0.827586207,-0.389830508,-0.115384615,-0.29787234,-0.025641026,-0.006345178,-0.030373832,-0.085858586,-0.285714286,-0.625,-0.115384615,-0.017369727,-0.023157895,-0.012403101,-0.057692308,0.104294479,0.036842105,0.125,-0.011363636,-0.306451613,-0.038781163,0.004504505,-0.089285714,-0.151162791,-0.064516129,-0.052023121,0.040880503,-0.052631579,-0.476190476,-0.583333333,-0.226666667,-0.009090909,-0.01082544,-0.102564103,-0.03654485,0.005988024,0.052631579,-0.438356164};
        b = new ArrayList() {-62.7866242,-78.65289256,-119.5634218,-90.13756614,-584.4675325,150.617284,157.5045872,32.76119403,860.6024096,-76.12716763,111.5267176,1815.328571,412.8846154,-122.5113122,-1089.130435,-1506,-1469.803279,-85.76495726,3118.891304,1602.662791,316.1456311,-319.0625,-146.8159509,-106.2358079,-8271.571429,-2584.157895,-112,34.50366748,5175.37931,2347.915254,559.0769231,1758.021277,-43.33333333,-178.5393401,8.764018692,465.0151515,2148,5028.875,665.0384615,-181.9081886,-129.56,-231.9348837,228.3846154,-1468.588957,-750.9631579,-1722.375,-202.3295455,3112.983871,89.11080332,-415.5135135,719.5357143,1478.767442,408.1612903,253.0231214,-916.7264151,290.4210526,5846.666667,7258.916667,2540.573333,-354.0545455,-329.2611637,1049.846154,47.07973422,-611.754491,-1365.421053,6717.219178 };
        Debug.Log(fm.Count);
        Debug.Log(k.Count);
        Debug.Log(b.Count);

    }

    public float getRefAngle()
    {
        float frame = vp.frame;
        //Debug.Log(frame);
        if (frame <= (int)fm[0])
        {
            refAngle = -88;
        }
        else
        {
            for (int i = 0; i < k.Count; i++)
            {
               
                if (frame > Convert.ToInt32(fm[i]) && frame < Convert.ToInt32(fm[i + 1]))
                {

                    refAngle = (float)Convert.ToSingle(k[i]) * (float)frame + (float)Convert.ToSingle(b[i]);
                    
                    break;
                }
                //else
                //{
                  //  vp.Stop();
                  //  vp.Play();
               // }
            }
        }
        return refAngle;
    }
    void showArrow()
    {
        if (1300 < vp.frame && vp.frame < 1500)
        {
            //right.SetActive(true);
        }
        else if (1600 < vp.frame && vp.frame < 1750)
        {
            left.SetActive(true);
        }
        else if (2500 < vp.frame && vp.frame < 2600)
        {
            right.SetActive(true);
        }
        else if (2800 < vp.frame && vp.frame < 3000)
        {
            left.SetActive(true);
        }
        else if (3800 < vp.frame && vp.frame < 4000)
        {
            left.SetActive(true);
        }
        else if (4400 < vp.frame && vp.frame < 4500)
        {
            right.SetActive(true);
        }
        else if (5200 < vp.frame && vp.frame < 5400)
        {
            left.SetActive(true);
        }
        else if (5800 < vp.frame && vp.frame < 5950)
        {
            right.SetActive(true);
        }
        else if (7240 < vp.frame && vp.frame < 7400)
        {
            left.SetActive(true);
        }
        else
        {
            left.SetActive(false);
            right.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        float presentAngle = frontCamera.transform.rotation.eulerAngles.y;
        getRefAngle();

         //frontCamera.transform.eulerAngles= new Vector3(0,refAngle,0);
        
       

        //Debug.Log(vp.frame);
        //showArrow();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(playing == false)
            {
                vp.playbackSpeed = 1;
            }
            playing = !playing;
            Debug.Log("Current frame: " + vp.frame);
        }
        if (playing == false)
        {
            vp.playbackSpeed = 0;
        }
       
    }
}
