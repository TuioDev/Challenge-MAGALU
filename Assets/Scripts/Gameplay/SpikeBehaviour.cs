using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private int Power;

    private float TimeToDisable = 2f;
    private Rigidbody2D SpikeRB;
    private Vector3 DirectionPoint;

    //Always get components on awake to ensure that OnEnable functions are working properly
    private void Awake()
    {
        SpikeRB = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        SetVelocity();

        //This function is not doing good...
        Invoke("DisableObject", TimeToDisable);
    }

    private void SetVelocity()
    {
        SpikeRB.velocity = DirectionPoint.normalized * Speed;
    }

    //Sets the direction of the spike, receives the 2D position of the mouse to calculate based on the spawn position
    public void SetDirection(Vector2 direction)
    {
        DirectionPoint = direction - (Vector2)transform.position;
        transform.up = new Vector2(DirectionPoint.x, DirectionPoint.y);
    }

    private void DisableObject()
    {
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamageOrHeal(Power);
                DisableObject();
                CancelInvoke();
            }
        }
    }
}
