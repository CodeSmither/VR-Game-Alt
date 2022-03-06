using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowRotate : MonoBehaviour
{
    private int Vertical;
    private int Horizontal;
    [SerializeField]private float Frequency;
    [SerializeField] private float magnitude;
    [SerializeField] private float rotationSpeed;
    private Vector3 pos;

    private void Awake()
    {
        pos = transform.position;
    }

    public void FixedUpdate()
    {
        transform.Rotate(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
        transform.position = pos + transform.up * Mathf.Sin(Time.time * Frequency) * magnitude;
    }
}
