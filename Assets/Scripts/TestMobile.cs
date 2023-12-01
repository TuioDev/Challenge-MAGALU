using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMobile : MonoBehaviour
{
    [SerializeField] private GameObject Test;
    [SerializeField] private BoolVariable IsMobile;

    private void Start()
    {
        Test.SetActive(IsMobile.Value);
    }
}
