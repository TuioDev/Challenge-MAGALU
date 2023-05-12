using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    [SerializeField] private float Speed;

    private Rigidbody2D SpikeRB;
    private Vector3 DirectionPoint;

    //public SpikeBehaviour(Vector3 mousePosition)
    //{
    //    DirectionPoint = Vector3.Normalize(mousePosition - SpawnPoint.position);
    //}

    // Start is called before the first frame update
    void Start()
    {
        SpikeRB = GetComponent<Rigidbody2D>();
        SetVelocity();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetVelocity()
    {
        SpikeRB.velocity = Speed * DirectionPoint;
    }

    //Change the behaviour of the travelling spike?
    public void SetDirection(Vector3 direction)
    {
        DirectionPoint = Vector3.Normalize(direction - transform.position);
        transform.up = new Vector2(DirectionPoint.x, DirectionPoint.y);
    }

}
