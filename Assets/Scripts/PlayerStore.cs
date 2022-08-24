using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStore : MonoBehaviour
{
    [SerializeField] int a;
    void Start()
    {
        a = 0;
    }
    public void Test() {
        a = 1;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
