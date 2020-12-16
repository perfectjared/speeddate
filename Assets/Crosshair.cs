using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : Singleton<Crosshair>
{
    [Range(0, 6)]
    public int ammo = 6;
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public AudioSource shootSource;
    public AudioSource noShootSource;
    public AudioSource reloadSource;
    public _UnityEventInt ammoChange;

    private bool shooting = false;
    
    void Start()
    {
        var x = cursorTexture.width / 2;
        var y = cursorTexture.height / 2;
        Cursor.SetCursor(cursorTexture, new Vector2(x, y), cursorMode);
    }

    void Update()
    {  
       if (Input.GetMouseButtonDown(0) && !shooting && ammo > 0) {
           shooting = true;
           ammo--;
           ammoChange.Invoke(ammo);
           shootSource.Play();
       }
       if (Input.GetMouseButtonDown(0) && !shooting && ammo < 1) {
           noShootSource.Play();
       }
       if (!Input.GetMouseButtonDown(0) && shooting) {
           shooting = false;
       }
    }

    void Reload() {
        ammo = 6;
        ammoChange.Invoke(ammo);
        reloadSource.Play();
    }
}
