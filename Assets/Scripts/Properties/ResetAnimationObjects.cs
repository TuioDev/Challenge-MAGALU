using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAnimationObjects : MonoBehaviour
{
    [SerializeField] private Transform ObjectTransform;
    [SerializeField] private SpriteRenderer ObjectSprite;
    [SerializeField] private Transform SparkTransform;
    [SerializeField] private SpriteRenderer SparkSprite;

    void OnEnable()
    {
        ObjectTransform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
        ObjectTransform.localScale = new Vector3(5f,5f,1f);

        Color changeAlpha = ObjectSprite.color;
        changeAlpha.a = 1f;
        ObjectSprite.color = changeAlpha;

        SparkTransform.rotation = Quaternion.identity;
        SparkTransform.localScale = Vector3.one;

        changeAlpha = SparkSprite.color;
        changeAlpha.a = 1f;
        SparkSprite.color = changeAlpha;
    }
}
