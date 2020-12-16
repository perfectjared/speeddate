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
    public _UnityEventInt ammoChange;
    public bool onscreen = false;

    private AudioManager audioManager;

    private bool shooting = false;
    
    void Start()
    {
        var x = cursorTexture.width / 2;
        var y = cursorTexture.height / 2;
        Cursor.SetCursor(cursorTexture, new Vector2(x, y), cursorMode);

        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {  
       if (Input.GetMouseButtonDown(0) && !shooting && ammo > 0) {
           shooting = true;
           audioManager.Play("Shoot");
           audioManager.sounds[0].source.pitch = Random.Range(1f, 1.1f); 
       }
       if (Input.GetMouseButtonDown(0) && !shooting && ammo < 1) {
            audioManager.Play("Empty");
       }
       if (Input.GetMouseButtonUp(0) && shooting) {
           ammo--;
           ammoChange.Invoke(ammo);
           shooting = false;
       }
    }

    void OnMouseEnter() {
        onscreen = true;
    }

    void OnMouseExit() {
        onscreen = false;
    }

    public void Reload() {
        ammo = 6;
        ammoChange.Invoke(ammo);
        audioManager.Play("Reload");
    }
}
