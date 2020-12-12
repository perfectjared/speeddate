/* TODO:
- Split this out BIGTIME
*/

//https://answers.unity.com/questions/820599/simulate-button-presses-through-code-unity-46-gui.html
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Crosshair_Follow : MonoBehaviour
{
    public Joycon_Crosshair JoyconCrosshair;
    public Ammo_UI ammoUi;
    public _UnityEventInt onAmmo;
    public _UnityEventGameObject onShoot;

    private Joycon joycon;
    private bool shooting = false;
    private int ammo = 6;

    public AudioSource shootSource;
    public AudioSource reloadSource;

    private Vector3 mousePos;
    private Vector3 joyconPos;

    private bool joyconControl = false;

    void Start()
    {
        if (JoyconManager.Instance.j.Count > 0) joycon = JoyconManager.Instance.j[0];
        shootSource = GameObject.Find("Shoot(AudioSource)").transform.GetComponent<AudioSource>();
        reloadSource = GameObject.Find("Reload(AudioSource)").transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        Cursor.visible = false;

        if (joycon != null) {
            if (joycon.GetButtonDown(Joycon.Button.SHOULDER_2) && !shooting)
                {
                    Shoot();
                }

            if (joycon.GetButtonUp(Joycon.Button.SHOULDER_2) && shooting)
                {
                    shooting = false;
                }
            if (JoyconCrosshair.onScreen && JoyconCrosshair.pointOnScreen != joyconPos) {
                joyconControl = true;
                transform.position = JoyconCrosshair.pointOnScreen; 
            }
            else transform.position = new Vector3(800, 800, 0); //TODO: this is a temp way to keep the crosshair off the screen
        }

        var mouseIn = Input.mousePosition;
        if (mousePos != mouseIn) {
            joyconControl = false;
            //mousePos = mouseIn;
            transform.position = Camera.main.ScreenToWorldPoint(mouseIn);
            //transform.position = mouseIn;
        }
        if (Input.GetMouseButtonDown(0) && !shooting) {
            // Jacques
        }
        if (!Input.GetMouseButtonDown(0) && shooting && !joyconControl) {
            shooting=false;
        }

        if (Input.GetKeyDown("space") && ammo != 6) {
            RefillAmmo();
        }
    }




    void Shoot()
    {
        shooting = true;
        Debug.Log("Shoot");
        if (onScreen() && ammo > 0)
        {            GameObject shotObject;
            
            if (joyconControl) {
                shotObject = JoyconCrosshair.pointingObject;
                Debug.Log("shot " + shotObject + ", " + ammo + "/6");
                onShoot.Invoke(shotObject);
                if (shotObject && shotObject.GetComponent<Button>()) PressButton(shotObject);
            }
            else {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if(Physics.Raycast (ray, out hit)) {
                    shotObject = hit.transform.gameObject;
                    Debug.Log("shot " + shotObject + ", " + ammo + "/6");
                    onShoot.Invoke(shotObject);
                }
            }

            shootSource.Play();
            DecrementAmmo();
        } else if (!onScreen() && ammo != 6) 
        {
            RefillAmmo();
        } else if (ammo == 0)
        {
            //no ammo sound
        }
    }

    bool onScreen() {
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
        return new Rect(0, 0, 1, 1).Contains(screenPoint);
    }

    void PressButton(GameObject button) {
        PointerEventData pointer = new PointerEventData(EventSystem.current);
        ExecuteEvents.Execute(button, pointer, ExecuteEvents.pointerClickHandler);
    }

    void DecrementAmmo()
    {
        ammo--;
        ammoUi.DecrementAmmo();
        if (ammo == 0) NoAmmo();
        onAmmo.Invoke(ammo);
    }

    void NoAmmo()
    {
        ammoUi.NoAmmo();
    }

    void RefillAmmo()
    {
        ammo = 6;
        ammoUi.RefillAmmo();
        reloadSource.Play();
        onAmmo.Invoke(ammo);
    }
}
