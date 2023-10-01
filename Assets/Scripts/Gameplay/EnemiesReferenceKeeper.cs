using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesReferenceKeeper : MonoBehaviour
{
    private readonly List<Enemy> EnemiesOnPath = new();
    private Transform SpriteTransform;
    private SpriteRenderer Sprite;
    
    public List<Enemy> GetEnemiesOnPath() => EnemiesOnPath;
    public void AddEnemyOnPath(Enemy enemy) => EnemiesOnPath.Add(enemy);
    public void RemoveEnemyFromPath(Enemy enemy)
    {
        EnemiesOnPath.Remove(enemy);

        if (EnemiesOnPath.Count == 0)
        {
            this.gameObject.SetActive(false);
            ResetSprite();
        }
    }

    void Awake()
    {
        SpriteTransform = this.gameObject.transform.GetChild(0);
        Sprite = this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void SetSpriteTransform(Vector3 initialPosition, Vector3 finalPosition, float angle)
    {
        SpriteTransform.localPosition = initialPosition;
        SpriteTransform.eulerAngles = Vector3.forward * angle;

        float horizontalSize = Sprite.localBounds.size.x;
        float totalSize = Vector2.Distance(initialPosition, finalPosition);
        float desiredScale = totalSize / horizontalSize;

        SpriteTransform.localScale = new Vector2(desiredScale, SpriteTransform.localScale.y);        
    }

    private void ResetSprite()
    {
        SpriteTransform.position = Vector3.zero;
        SpriteTransform.rotation = Quaternion.identity;
    }
}
