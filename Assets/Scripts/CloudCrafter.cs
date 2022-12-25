using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class CloudCrafter : MonoBehaviour
{
  public int numClouds;
  public GameObject cloudPrefab;
  public GameObject anchor;
  public Vector3 cloudPosMin;
  public Vector3 cloudPosMax;
  public float cloudScaleMin;
  public float cloudScaleMax;
  public float cloudSpeedMult;
  private float fixedSize = 90f;
  private float minSize = 100;

  private GameObject[] cloudInstances;

  private void Awake()
  {
    cloudInstances = new GameObject[numClouds];
    GameObject cloud;
    for (int i = 0; i < numClouds; i++)
    {
      cloud = Instantiate<GameObject>(cloudPrefab);
      Vector3 cPos = Vector3.zero;
      cPos.x = Random.Range(cloudPosMin.x, cloudPosMax.x);
      cPos.y = Random.Range(cloudPosMin.y, cloudPosMax.y);
      float scaleU = Random.value;
      float scaleVal = Mathf.Lerp(cloudScaleMin,cloudScaleMax,scaleU);
      cPos.y = Mathf.Lerp(cloudPosMin.y, cPos.y, scaleU);
      cPos.x = minSize - fixedSize * scaleU;
      cloud.transform.position = cPos;
      cloud.transform.localScale = Vector3.one * scaleVal;
      cloud.transform.SetParent(anchor.transform);
      cloudInstances[i] = cloud;
    }
  }

  private void Update()
  {
    foreach (GameObject cloud in cloudInstances)
    {
      float scaleVal = cloud.transform.localScale.x;
      Vector3 cPos = cloud.transform.position;
      cPos.x -= scaleVal * Time.deltaTime * cloudSpeedMult;
      if (cPos.x <= cloudPosMin.x)
      {
        cPos.x = cloudPosMax.x;
      }
      cloud.transform.position = cPos;
    }
  }
}
