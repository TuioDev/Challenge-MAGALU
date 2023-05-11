using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private SpikeBehaviour SpikePrefab;
    [SerializeField] private Transform SpikeSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSpikeByClick()
    {
        SpikeBehaviour newSpike = Instantiate(SpikePrefab, SpikeSpawnPosition);
        newSpike.SetDirection(Input.mousePosition);
        Debug.Log("Mouse position: " + Input.mousePosition);
    }
}
