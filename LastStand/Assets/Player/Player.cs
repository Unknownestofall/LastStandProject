using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float JumpForce;

    Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFunctions();
    }
    void PlayerFunctions() {
        float hInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 movePlayer = Speed * Time.deltaTime * new Vector3(hInput, 0, zInput).normalized;

        transform.Translate(movePlayer);
        if(Input.GetKeyDown(KeyCode.Space) && onGround()) { 
            _rb.velocity = new Vector3(0, JumpForce);
        }
    }
    bool onGround() { 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f)) {
            if (hit.transform.CompareTag("floor")) { 
                return true;
            }
        }
        return false;
    }
}
