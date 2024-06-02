using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BREAK : MonoBehaviour
{
    [SerializeField] GameObject[] cells;
    // Start is called before the first frame update
    void Start()
    {
      foreach(var cell in cells) { 
            Rigidbody rb = cell.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddExplosionForce(1000, transform.position, 100);
            }
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
