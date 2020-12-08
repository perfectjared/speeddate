using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joycon_Driftreduce : MonoBehaviour
{
    [Range(-5.0f, 5.0f)]
    public float driftReduceX;
    [Range(-5.0f, 5.0f)]
    public float driftReduceY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(-driftReduceX, -driftReduceY, 0) * Time.deltaTime, Space.World);
    }
}
