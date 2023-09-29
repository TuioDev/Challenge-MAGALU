using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private FloatVariable ProjectileSpeed;
    [SerializeField] private FloatVariable ProjectileDamage;
    [SerializeField] private FloatVariable ProjectileRotationSpeed;

    private Rigidbody2D ProjectileRB;
    private Vector3 DirectionPoint;

    //Always get components on awake to ensure that OnEnable functions are working properly
    private void Awake()
    {
        ProjectileRB = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        RotateObject();
    }

    private void OnEnable()
    {
        SetVelocity();
    }

    private void RotateObject()
    {
        this.transform.Rotate( new Vector3(0f, 0f, ProjectileRotationSpeed.Value * Time.deltaTime));
        if (this.transform.rotation.eulerAngles.z > 360f)
            this.transform.Rotate(new Vector3(0f, 0f, 0f));
    }

    private void SetVelocity()
    {
        ProjectileRB.velocity = DirectionPoint.normalized * ProjectileSpeed.Value;
    }

    //Sets the direction of the spike, receives the 2D position of the mouse to calculate based on the spawn position
    public void SetDirection(Vector2 direction)
    {
        DirectionPoint = direction - (Vector2)transform.position;
        transform.up = new Vector2(DirectionPoint.x, DirectionPoint.y);
    }

    public void DisableObject()
    {
        this.gameObject.SetActive(false);
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamageOrHeal(ProjectileDamage.Value);
                DisableObject();
            }
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            DisableObject();
        }
    }
}
