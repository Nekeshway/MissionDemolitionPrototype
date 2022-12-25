using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Cloud : MonoBehaviour
{
    public GameObject cloudSphere;
    public int numSpheresMin;
    public int numSpheresMax;
    public Vector3 sphereOffsetScale;
    public Vector2 sphereScaleRangeX;
    public Vector2 sphereScaleRangeY;
    public Vector2 sphereScaleRangeZ;
    public float scaleYMin;

    private List<GameObject> spheres;

    private void Start()
    {
        spheres = new List<GameObject>();
        int num = Random.Range(numSpheresMin, numSpheresMax);
        for (int i = 0; i < num; i++)
        {
            GameObject sp = Instantiate<GameObject>(cloudSphere);
            spheres.Add(sp);
            Transform spTrans = sp.transform;
            spTrans.SetParent(this.transform);

            Vector3 offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            Vector3 scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);

            scale.y *= 1 - (Mathf.Abs(offset.x) / sphereOffsetScale.x);
            scale.y = Mathf.Max(scale.y, scaleYMin);
            spTrans.localScale = scale;
        }
    }

  //  private void Update()
   // {
    //    if (Input.GetKeyDown(KeyCode.Space))
       // {
     //       Restart();
     //   }
   // }

    private void Restart()
    {
        foreach (GameObject sp in spheres)
        {
            Destroy(sp);
        }

        Start();
    }
}

