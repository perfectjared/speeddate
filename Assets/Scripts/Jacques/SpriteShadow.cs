using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteShadow : MonoBehaviour
{

    public Vector2 offset = new Vector2(0,-0.2f);
    [SerializeField]
    private Material shadowMat;
    private SpriteRenderer casterRenderer;
    private SpriteRenderer shadowRenderer;
    private Transform casterTransform;
    private Transform shadowTransform;


    private void Awake()
    {
        shadowMat = Resources.Load<Material>("Shadow");
    }

    void Start()
    {
        casterTransform = transform;
        shadowTransform = new GameObject().transform;
        shadowTransform.parent = casterTransform;
        shadowTransform.gameObject.name = (casterTransform.gameObject.name + "'s shadow");
        shadowTransform.localScale = new Vector3(1, 1, 0);
        shadowTransform.localRotation = Quaternion.identity;
        


        casterRenderer = GetComponent<SpriteRenderer>();
        shadowRenderer = shadowTransform.gameObject.AddComponent<SpriteRenderer>();

        shadowRenderer.sortingLayerName = casterRenderer.sortingLayerName;
        shadowRenderer.sortingOrder = casterRenderer.sortingOrder - 1;
    }

    void LateUpdate()
    {
        shadowTransform.position = new Vector2(casterTransform.position.x + offset.x, casterTransform.position.y + offset.y);
        shadowRenderer.sprite = casterRenderer.sprite;
        shadowRenderer.material = shadowMat;

        shadowRenderer.color = new Color(shadowRenderer.color.r, shadowRenderer.color.g, shadowRenderer.color.b, casterRenderer.color.a);
    }
}
