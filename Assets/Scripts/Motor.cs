﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Motor : MonoBehaviour
{
    public static readonly float MAX_ANGULAR_SPEED = 10.0f;
    public static readonly float MIN_ANGULAR_SPEED = -10.0f;

    private float m_AngularSpeed;

    public float AngularSpeedAbsolute
    {
        get => m_AngularSpeed;
        set => m_AngularSpeed = Mathf.Clamp(value, MIN_ANGULAR_SPEED, MAX_ANGULAR_SPEED);
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = float.MaxValue;
        
        InvokeRepeating("CheckWheelSpeed", 5.0f, 1.0f);
    }

    void CheckWheelSpeed()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float currentAngularSpeed = Vector3.Project(rb.angularVelocity, transform.forward).magnitude;
        if (Mathf.Abs(currentAngularSpeed - Mathf.Abs(m_AngularSpeed)) > 1.0f)
        {
            Debug.LogWarning($"Wheel speed above / below from target: {currentAngularSpeed}");
        }
    }

    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        //Debug.Log(transform.forward);
        //Debug.Log(rb.angularVelocity);
        //Debug.Log(Vector3.Cross(rb.angularVelocity.normalized, transform.forward.normalized).magnitude);
        float currentAngularSpeed = Vector3.Project(rb.angularVelocity, transform.forward).magnitude;
        //switch (name)
        //{
        //    //Debug.Log(currentAngularSpeed);
        //    //rb.AddTorque(new Vector3(0.0f, 0.0f, 100.0f), ForceMode.Force);
        //    case "WheelFL":
        //    case "WheelRL":
        //    {
        //        //Debug.Log(transform.localEulerAngles);
        //        Vector3 angles = transform.localEulerAngles;
        //        angles.x = 90.0f;
        //        transform.localEulerAngles = angles;
        //        break;
        //    }
        //    case "WheelFR":
        //    case "WheelRR":
        //    {
        //        //Debug.Log(transform.localEulerAngles);
        //        Vector3 angles = transform.localEulerAngles;
        //        angles.x = -90.0f;
        //        transform.localEulerAngles = angles;
        //        break;
        //    }
        //    default:
        //    {
        //        Debug.Break();
        //        break;
        //    }
        //}
        if (m_AngularSpeed > 0.0f)
        {
            if (currentAngularSpeed < m_AngularSpeed)
            {
                if (gameObject.name == "WheelRR" || gameObject.name == "WheelFR")
                {
                    rb.AddRelativeTorque(new Vector3(0.0f, 0.0f, -20.0f), ForceMode.Force);
                }
                else
                {
                    rb.AddRelativeTorque(new Vector3(0.0f, 0.0f, 20.0f), ForceMode.Force);
                }
            }
        }
    }

}
