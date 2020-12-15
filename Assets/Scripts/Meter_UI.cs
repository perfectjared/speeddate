using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter_UI : MonoBehaviour
{
    [SerializeField]
    private float value = 0;
    private SuperTextMesh stm;

    void Start() {
        stm = GetComponent<SuperTextMesh>();
    }

    public void ReceiveValue(float value) {
        this.value = value;
    }
}
