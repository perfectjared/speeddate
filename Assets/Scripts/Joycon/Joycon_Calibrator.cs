/*TODO:
- separate this out - have the Calibrator called, not listening always
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joycon_Calibrator : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float threshold;
    [Range(0.0f, 2.0f)]
    public float secondsWait;
    [Range(0.0f, 0.5f)]
    public float changeAmount;

    private Joycon joycon;
    private GameObject crosshair;
    private Joycon_Driftreduce driftReduce;

    private void Start()
    {
        if (JoyconManager.Instance.j.Count > 0) joycon = JoyconManager.Instance.j[0];
        crosshair = GameObject.Find("Crosshair");
        driftReduce = GameObject.Find("Joycon").GetComponent<Joycon_Driftreduce>();
    }

    private void Update()
    {
        //If mouse?
        if (joycon != null && (joycon.GetButtonUp(Joycon.Button.PLUS) || joycon.GetButtonUp(Joycon.Button.MINUS)))
        {
            StartCoroutine(Calibrate());
        }
    }

    public IEnumerator Calibrate()
    {
        //Instruct to set down on flat surface then press DPAD_Left
        Debug.Log("Lay Joycon on flat surface and press DPAD_Left");

        //Wait for input (DPAD_Left)
        bool waitForPress = true;
        while (waitForPress)
        {
            waitForPress &= !joycon.GetButtonUp(Joycon.Button.DPAD_LEFT);
            yield return new WaitForEndOfFrame();
        }

        //Wait one second
        Debug.Log("Waiting one second...");
        yield return new WaitForSeconds(1.0f);
        Debug.Log("calibrating.....");

        //Calibrate
        StartCoroutine(CalibrateX());

        yield return new WaitForEndOfFrame();
        StopCoroutine(Calibrate());
    }

    //lol turns out Joycons only drift in one direction
    private IEnumerator CalibrateX()
    {
        float change = threshold;
        float at = crosshair.transform.position.x;

        yield return new WaitForSeconds(secondsWait);

        while (Mathf.Abs(change) >= threshold)
        {
            float newAt = crosshair.transform.position.x;
            change = newAt - at;
            driftReduce.driftReduceY += (change > 0) ? changeAmount : -changeAmount;
            at = newAt;
            Debug.Log(change + " change in X");
            yield return new WaitForSeconds(secondsWait);

        }

        Debug.Log("X calibrated");
        yield return new WaitForEndOfFrame();
        StopCoroutine(CalibrateX());
    }

}
