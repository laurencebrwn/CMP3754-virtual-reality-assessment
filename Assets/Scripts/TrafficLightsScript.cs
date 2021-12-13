using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightsScript : MonoBehaviour
{
  public Transform t1;
  public Transform t2;
  public Transform t3;
  public GameObject t1barrier;
  public GameObject t2barrier;
  public GameObject t3barrier;
  public GameObject t1green;
  public GameObject t1red;
  public GameObject t2green;
  public GameObject t2red;
  public GameObject t3green;
  public GameObject t3red;
  public float stateTimer;
  public int state;


  // Start is called before the first frame update
  void Start()
  {
    t1 = transform.Find("TL1");
    t2 = transform.Find("TL2");
    t3 = transform.Find("TL3");
    t1green = t1.Find("Green light").gameObject;
    t1red = t1.Find("Red light").gameObject;
    t1barrier = t1.Find("Barrier").gameObject;
    t2green = t2.Find("Green light").gameObject;
    t2red = t2.Find("Red light").gameObject;
    t2barrier = t2.Find("Barrier").gameObject;
    t3green = t3.Find("Green light").gameObject;
    t3red = t3.Find("Red light").gameObject;
    t3barrier = t3.Find("Barrier").gameObject;
    stateTimer = 10.0f;
    SetState(1);
  }

  // Update is called once per frame
  void Update()
  {
    stateTimer = stateTimer - Time.deltaTime;
    if (stateTimer < 0)
    {
      if (state == 1) SetState(0);
      else SetState(1);
      stateTimer = 10.0f;
    }
  }

  void SetState(int c)
  {
    state = c;
    if (c == 1)
    {
      t1green.active = true;
      t1red.active = false;
      //t1barrier.active = false;
      t1barrier.transform.localPosition = new Vector3(t1barrier.transform.localPosition.x, t1barrier.transform.localPosition.y, -5);
      t2green.active = false;
      t2red.active = true;
      //t2barrier.active = true;
      t2barrier.transform.localPosition = new Vector3(t2barrier.transform.localPosition.x, t2barrier.transform.localPosition.y, 0);
      t3green.active = false;
      t3red.active = true;
      //t3barrier.active = true;
      t3barrier.transform.localPosition = new Vector3(t3barrier.transform.localPosition.x, t3barrier.transform.localPosition.y, 0);
    }
    else
    {
      t1green.active = false;
      t1red.active = true;
      //t1barrier.active = true;
      t1barrier.transform.localPosition = new Vector3(t1barrier.transform.localPosition.x, t1barrier.transform.localPosition.y, 0);
      t2green.active = true;
      t2red.active = false;
      //t2barrier.active = false;
      t2barrier.transform.localPosition = new Vector3(t2barrier.transform.localPosition.x, t2barrier.transform.localPosition.y, -5);
      t3green.active = true;
      t3red.active = false;
      //t3barrier.active = false;
      t3barrier.transform.localPosition = new Vector3(t3barrier.transform.localPosition.x, t3barrier.transform.localPosition.y, -5);
    }
  }

}
