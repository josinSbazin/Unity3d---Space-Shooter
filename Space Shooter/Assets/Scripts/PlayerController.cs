﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public SimpleTouchPad touchPad;
    public SimpleTouchAreaButtom areaButton;

    private Rigidbody rb;
    private AudioSource au;
    private float nextFire;
    private Quaternion calibrationQuaternion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        au = GetComponent<AudioSource>();
        CalibrateAccelerometer();
    }

    void Update()
    {
        if (areaButton.CanFire && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            au.Play();
        }
    }

    void CalibrateAccelerometer()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }

    Vector3 FixAcceleration(Vector3 acceleration)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
        return fixedAcceleration;
    }

    void FixedUpdate()
    {
        //        float moveHorizontal = Input.GetAxis("Horizontal");
        //        float moveVertical = Input.GetAxis("Vertical");

        //        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        //        Vector3 accelerationRaw = Input.acceleration;
        //        Vector3 acceleration = FixAcceleration(accelerationRaw);

        Vector2 direction = touchPad.Direction;
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
        rb.velocity = movement * speed;
        rb.position = new Vector3
            (
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

    
        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }

}
