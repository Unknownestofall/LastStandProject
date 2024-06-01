using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] int DMG;
    void Start()
    {
         
    }
    void Update()
    {
        Destroy(this.gameObject, 5f);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) {
            other.GetComponent<Enemy>().Damage(DMG);
            Destroy(this.gameObject);
        }
    }
}
