using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AmmoCrate : MonoBehaviour
{
    [SerializeField] Transform PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onInteract() => PlayerPos.GetComponent<Player>().ReceiveAmmo();
}
