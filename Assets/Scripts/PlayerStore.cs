using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStore : MonoBehaviour
{
    [SerializeField] int a;
    void Start()
    {
        a = 6;
        print(a);
    }

    
}
