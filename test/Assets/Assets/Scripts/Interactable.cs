using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private MoveInWayPoints moveInWayPoints;

    public bool isStatic = false;
    private Transform playerTransform;
    public int range = 10;
    private bool interacts = false;
    public int bulletSpeed = 20;
    public Rigidbody bullet;
    public Transform bulletPos;
    public float fireRate = 1.5f;
    private float lastShot = 0.0f;

    void Awake()
    {
        if (!isStatic)
        {
            moveInWayPoints = GetComponent<MoveInWayPoints>();
        }
        playerTransform = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        bool inRange = Vector3.Distance(transform.position, playerTransform.position) < range;
        if (inRange)
        {
            if (!interacts)
            {
                interacts = true;
                StopWalking();
            }
            if (!isStatic)
            {
                RotateTowards(playerTransform);
                ShootAtPlayer();
            }
        }
        else
        {
            if (interacts)
            {
                Walk();
                interacts = false;
            }
        }
    }

    private void ShootAtPlayer()
    {
        if (Time.time > fireRate + lastShot)
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
            clone.velocity = transform.forward * bulletSpeed;
            lastShot = Time.time;
        }
    }

    private void Walk()
    {
        WalkOrStop(true);
    }

    private void StopWalking()
    {
        WalkOrStop(false);
    }

    private void WalkOrStop(bool walk)
    {
        if (isStatic)
        {
            return;
        }

        if (moveInWayPoints == null)
        {
            return;
        }

        if (walk)
        {
            moveInWayPoints.Walk();
        }
        else
        {
            moveInWayPoints.StopWalking();
        }
    }

    private void RotateTowards(Transform target)
    {

        transform.LookAt(target);
        //Vector3 direction = (target.position - transform.position).normalized*-1;
        //Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));    // flattens the vector3
        //transform.rotation =  Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }


}
