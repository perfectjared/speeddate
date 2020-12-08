using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootButtonTest : MonoBehaviour
{
    private Text textComponent;

    void Start() {
        textComponent = GetComponentInChildren<Text>();
    }

    public void ChangeText() {
        textComponent.text = "test";
        Debug.Log("test4");
    }
}
