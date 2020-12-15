using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastShadows : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> shadowCasters = new List<GameObject>();
    private SpriteShadow _spriteShadow;


    void Awake()
    {
        _spriteShadow = FindObjectOfType<SpriteShadow>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("ShadowCaster"))
        {
            go.AddComponent<SpriteShadow>();
        }
    }
}
