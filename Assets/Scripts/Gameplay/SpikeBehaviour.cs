using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    [SerializeField] private Transform SpawnPoint;
    [SerializeField] private float Speed;

    private Vector3 DirectionPoint;
    private Rigidbody2D SpikeRB;

    public SpikeBehaviour(Vector3 mousePosition)
    {
        DirectionPoint = Vector3.Normalize(mousePosition - SpawnPoint.position);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpikeRB = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    //Change the behaviour of the travelling spike?
    public void SetDirection(Vector3 direction)
    {
        DirectionPoint = Vector3.Normalize(direction - SpawnPoint.position);
    }

    public void Movement()
    {
        SpikeRB.AddForce(DirectionPoint * Speed, ForceMode2D.Force);
    }
}
