using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_UI : MonoBehaviour
{
    private SuperTextMesh stm;

    void Start()
    {
        stm = transform.GetComponent<SuperTextMesh>();
    }

    public void UpdateText(int ammo)
    {
        stm.text = ammo.ToString();
        stm.Rebuild();
    }
}
