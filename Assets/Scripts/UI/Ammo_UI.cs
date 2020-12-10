using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_UI : MonoBehaviour
{
    private SuperTextMesh stm;
    private Reload reload;

    [Range(0, 6)]
    [SerializeField]
    private int ammo = 6;

    void Start()
    {
        stm = transform.GetComponent<SuperTextMesh>();
        reload = transform.GetComponentInChildren<Reload>(); 
    }

    public void DecrementAmmo()
    {
        ammo--;
        UpdateText();
    }

    public void NoAmmo()
    {
        reload.Display();
    }

    public void RefillAmmo()
    {
        reload.Stop();
        ammo = 6;
        UpdateText();
    }

    void UpdateText()
    {
        stm.text = ammo + "";
        stm.Rebuild();
    }
}
