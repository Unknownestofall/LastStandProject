using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBehaviour : MonoBehaviour
{
    [SerializeField] float bulletDmg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 10f);
    }
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")) {
            other.GetComponent<Player>().Damage(bulletDmg);
            Destroy(this.gameObject);
        }    
    }
}
