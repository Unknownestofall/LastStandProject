using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    BoxCollider _col;
    void Start()
    {
        _col = GetComponent<BoxCollider>();    
    }
    void Update()
    {
        Destroy(this.gameObject, 5f);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision != null) {
            _col.enabled = true;
            Destroy(this.gameObject);
        }
    }
}
