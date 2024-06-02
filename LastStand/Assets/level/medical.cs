using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medical : MonoBehaviour
{
    [SerializeField]GameObject player;
    [SerializeField] float hpToRegen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(dist);
        if(dist < 10) {
            player.GetComponent<Player>().regenHealth(hpToRegen);
        } 
    }

}
