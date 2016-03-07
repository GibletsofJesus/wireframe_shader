﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

//SOURCE:
//http://wiki.unity3d.com/index.php?title=MouseOrbitZoom

[AddComponentMenu("Camera-Control/3dsMax Camera Style")]
public class cameraOrbitControls : MonoBehaviour
{
    public List<GameObject> models = new List<GameObject>();
    public int currentModel = 1;

    public Transform target;
    public Vector3 targetOffset;
    public float distance = 0;
    float maxDistance = 5;
    float minDistance = 1.5f;
    float xSpeed = 120f;
    float ySpeed = 120;
    int yMinLimit = -5;
    int yMaxLimit = 25;
    int zoomRate = 100;
    float panSpeed = 0.5f;
    float zoomDampening = 5.0f;

    private float xDeg;
    private float yDeg;
    private float currentDistance;
    private float desiredDistance;
    private Quaternion currentRotation;
    private Quaternion desiredRotation;
    private Quaternion rotation;
    private Vector3 position;

    Transform dummyTarget;
    public Vector3 newTargetOffset;

    //UI tings
    public GameObject LeftArrow,RightArrow;

    void Start() { Init(); }
    void OnEnable() { Init(); }
    
    public Vector3 newTarget;

    public void changeTarget(int direction)
    {
        if (direction > 0)
        {
            if (currentModel != models.Count)
            {
                    newTarget = new Vector3(models[currentModel].transform.position.x, 1, models[currentModel].transform.position.z);
                Debug.Log(models[currentModel].name);
                currentModel += direction;
            }
        }
        else
        {
            if (currentModel >1)
            {
                currentModel += direction;
                newTarget = new Vector3(models[currentModel - 1].transform.position.x, 1, models[currentModel - 1].transform.position.z);
            }
        }
    }

    public void Update()
    {
        //Update UI
        if (currentModel > 1)
        {
            //Arrows
            LeftArrow.SetActive(true);
            if (currentModel == models.Count)
                RightArrow.SetActive(false);
            else
                RightArrow.SetActive(true);
        }
        else
        {
                LeftArrow.SetActive(false);
                RightArrow.SetActive(true);
        }
    }

    public void Init()
    {
        changeTarget(0);
        //If there is no target, create a temporary target at 'distance' from the cameras current viewpoint
        if (!target)
        {
            GameObject go = new GameObject("Cam Target");
            go.transform.position = transform.position;
            target = go.transform;
            dummyTarget = go.transform;
            newTarget = go.transform.position;
        }

        distance = Vector3.Distance(transform.position, target.position);
        currentDistance = distance;
        desiredDistance = distance + 10;

        //be sure to grab the current rotations as starting points.
        position = transform.position - (transform.forward * distance); ;
        rotation = transform.rotation;
        currentRotation = transform.rotation;

        xDeg = -Vector3.Angle(Vector3.left, -transform.right);
        yDeg = Vector3.Angle(Vector3.up, transform.up);
        desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
    }

    /*
     * Camera logic on LateUpdate to only update after all character movement logic has been handled. 
     */
    void LateUpdate()
    {
        //Lerp target
        target.position = Vector3.Lerp(target.position, newTarget + newTargetOffset, Time.deltaTime * 5);
        if (target.position == newTarget + newTargetOffset)
            target.position = newTarget+ newTargetOffset;

        // If Control and Alt and Middle button? ZOOM!
        if (Input.GetMouseButton(2))
        {
            dummyTarget.rotation = transform.rotation;
            dummyTarget.position = newTarget + newTargetOffset;
            //grab the rotation of the camera so we can move in a psuedo local XY space
            dummyTarget.rotation = transform.rotation;
            dummyTarget.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
            dummyTarget.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);

            newTargetOffset = dummyTarget.position - newTarget;
        }
        // If middle mouse and left alt are selected? ORBIT
        else if (Input.GetMouseButton(0))
        {
            xDeg += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            yDeg -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            ////////OrbitAngle

            //Clamp the vertical axis for the orbit
            yDeg = ClampAngle(yDeg, yMinLimit, yMaxLimit);
            // set camera rotation f
            desiredRotation = Quaternion.Euler(yDeg, xDeg, 0);
        }
        // otherwise if middle mouse is selected, we pan by way of transforming the target in screenspace
        else if (Input.GetMouseButton(1))
        {
            //grab the rotation of the camera so we can move in a psuedo local XY space
            target.rotation = transform.rotation;
            target.Translate(Vector3.right * -Input.GetAxis("Mouse X") * panSpeed);
            target.Translate(transform.up * -Input.GetAxis("Mouse Y") * panSpeed, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //target.transform.position = Vector3.zero;
            newTargetOffset = Vector3.zero;
        }

        ////////Orbit Position

        // affect the desired Zoom distance if we roll the scrollwheel
        desiredDistance -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomRate * Mathf.Abs(desiredDistance);
        //clamp the zoom min/max
        desiredDistance = Mathf.Clamp(desiredDistance, minDistance, maxDistance);
        // For smoothing of the zoom, lerp distance
        currentDistance = Mathf.Lerp(currentDistance, desiredDistance, Time.deltaTime * zoomDampening);

        //Do the same for rotating
        currentRotation = transform.rotation;
        rotation = Quaternion.Lerp(currentRotation, desiredRotation, Time.deltaTime * zoomDampening);
        transform.rotation = rotation;

        // calculate position based on the new currentDistance 
        position = target.position - (rotation * Vector3.forward * currentDistance + targetOffset);
        transform.position = position;
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}

/*using UnityEngine;
using System.Collections;

//SOURCE:
//http://wiki.unity3d.com/index.php?title=MouseOrbitImproved#Code_C.23

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class cameraOrbitControls : MonoBehaviour
{
    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance -Mathf.Pow(Input.GetAxis("Mouse ScrollWheel")*10,3), distanceMin, distanceMax);

            RaycastHit hit;
            if (Physics.Linecast(target.position, transform.position, out hit))
            {
                distance -= hit.distance;
            }
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}*/
