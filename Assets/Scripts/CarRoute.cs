using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRoute : MonoBehaviour
{
  public List<Transform> wps;
  public List<Transform> route;
  public int routeNumber = 0;
  public int targetWP = 0;
  public Rigidbody rb;
  public bool go = false;
  public float initialDelay;

  // Start is called before the first frame update
  void Start()
  {
    wps = new List<Transform>();
    GameObject wp;
    rb = GetComponent<Rigidbody>();

    wp = GameObject.Find("CWP1");
    wps.Add(wp.transform);

    wp = GameObject.Find("CWP2");
    wps.Add(wp.transform);

    wp = GameObject.Find("CWP3");
    wps.Add(wp.transform);

    wp = GameObject.Find("CWP4");
    wps.Add(wp.transform);

    wp = GameObject.Find("CWP5");
    wps.Add(wp.transform);

    wp = GameObject.Find("CWP6");
    wps.Add(wp.transform);

    wp = GameObject.Find("CWP7");
    wps.Add(wp.transform);

    wp = GameObject.Find("CWP8");
    wps.Add(wp.transform);

    SetRoute();

    initialDelay = Random.Range(2.0f, 12.0f);
    transform.position = new Vector3(0.0f, -5.0f, 0.0f);
  }

  void FixedUpdate()
  {
    Vector3 displacement = route[targetWP].position - transform.position;
    displacement.y = 0;
    float dist = displacement.magnitude;
    if (dist < 0.1f)
    {
      targetWP++;
      if (targetWP >= route.Count)
      {
        SetRoute();
        return;
      }
    }
    //calculate velocity for this frame
    Vector3 velocity = displacement;
    velocity.Normalize();
    velocity *= 10f;

    if (go)
    {
      //apply velocity
      Vector3 newPosition = transform.position;
      newPosition += velocity * Time.deltaTime;
      rb.MovePosition(newPosition);

    }
    //align to velocity
    Vector3 desiredForward = Vector3.RotateTowards(transform.forward, velocity,
    10.0f * Time.deltaTime, 0f);
    Quaternion rotation = Quaternion.LookRotation(desiredForward);
    rb.MoveRotation(rotation);
  }

  void SetRoute()
  {
    if (!go)
    {
      initialDelay -= Time.deltaTime;
      if (initialDelay <= 0.0f)
      {
        go = true;
        SetRoute();
      }
      else return;
    }

    //randomise the next route
    routeNumber = Random.Range(0, 4);
    //set the route waypoints
    if (routeNumber == 0) route = new List<Transform> { wps[0], wps[1] };
    else if (routeNumber == 1) route = new List<Transform> { wps[2], wps[3]};
    else if (routeNumber == 2) route = new List<Transform> { wps[6], wps[4], wps[1] };
    else if (routeNumber == 3) route = new List<Transform> { wps[2], wps[5], wps[7] };

     //initialise position and waypoint counter
    transform.position = new Vector3(route[0].position.x, 0.55f, route[0].position.z);
    targetWP = 1;
  }

  void OnTriggerEnter(Collider other)
  {
    if(other.gameObject.CompareTag("Pedestrian") || other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("VR Player"))
    {
      go = false;
    }
  }

  void OnTriggerExit(Collider other)
  {
    if(other.gameObject.CompareTag("Pedestrian") || other.gameObject.CompareTag("Car") || other.gameObject.CompareTag("VR Player"))
    {
      go = true;
    }
  }
}
