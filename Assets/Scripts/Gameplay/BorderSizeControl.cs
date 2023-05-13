using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSizeControl : MonoBehaviour
{
    [SerializeField] private BoxCollider2D BorderCollider;
    [SerializeField] private SpriteRenderer SpriteReference;

    private void Awake()
    {
        BorderCollider.size = SpriteReference.bounds.size;
    }
}
