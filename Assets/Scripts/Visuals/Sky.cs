using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    [SerializeField] private FloatVariable SkySpeed;
    
    private Material SkyMaterial;
    private float offset = 0f;

    private void Start()
    {
        SkyMaterial = GetComponent<Renderer>().material;
    }

    void Update()
    {
        SetSkyOffset();
    }

    private void SetSkyOffset()
    {
        offset += SkySpeed.Value * Time.deltaTime;
        offset = (offset >= 1) ? 0f : offset;
        SkyMaterial.mainTextureOffset = new Vector2(offset, 0f);
    }
}
