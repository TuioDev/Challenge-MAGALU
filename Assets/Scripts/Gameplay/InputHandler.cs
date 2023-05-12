using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private SpikeBehaviour SpikePrefab;
    [SerializeField] private Transform SpikeSpawnPosition;

    private Vector3 ScreenToGamePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSpikeByClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SpikeBehaviour newSpike = Instantiate(SpikePrefab, SpikeSpawnPosition.position, Quaternion.identity);
            ScreenToGamePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            newSpike.SetDirection(ScreenToGamePos);
        }
    }
}
