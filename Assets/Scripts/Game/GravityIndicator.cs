using Logic;
using System;
using Unity.Mathematics;
using UnityEngine;

public class GravityIndicator : MonoBehaviour
{
    [SerializeField] private float speed;
    private float targetValue = 0;
    private quaternion startPosition;

    private quaternion to;
    void Start()
    {
        GravityModule.OnGravityChange += GravityModule_OnGravityChange;
        startPosition = transform.rotation; 
        enabled = false;
    }

    private void GravityModule_OnGravityChange(bool inNormalGravity)
    {
        enabled = true;
        targetValue += 180;
        to = startPosition * Quaternion.Euler(0.0f, 0.0f, targetValue);
    }

    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, to, speed * Time.deltaTime);

        if (Quaternion.Angle(transform.rotation, to) < 0.01f)
        {
            enabled = false;
        }
    }
}
