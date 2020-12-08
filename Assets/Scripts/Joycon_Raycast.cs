using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joycon_Raycast : MonoBehaviour
{
    private List<Joycon> joycons;
    private Joycon joycon;

    public Vector3 gyro;
    public Vector3 accel;
    public Quaternion orientation;

    void Start()
    {
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        joycons = JoyconManager.Instance.j;
        


        if (joycons.Count > 0)
        {
            joycon = joycons[0];
            StartCoroutine(JoyconUpdate());
        }
    }

    IEnumerator JoyconUpdate() {
        gyro = joycon.GetGyro();
        accel = joycon.GetAccel();
        orientation = joycon.GetVector();
        orientation = new Quaternion(orientation.x, orientation.z, orientation.y, orientation.w);
        gameObject.transform.localRotation = Quaternion.Inverse(orientation);

        if (joycon.GetButtonUp(Joycon.Button.DPAD_LEFT))
        {
            joycon.Recenter();
            transform.parent.transform.localEulerAngles = new Vector3(0, 180, 0);
        }
        yield return new WaitForEndOfFrame();
        JoyconUpdate();
    }
}
