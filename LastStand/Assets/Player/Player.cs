using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] enum PlayerState {walking, running}
    [SerializeField] PlayerState state;

    float _baseSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

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
        stateTracker();
    }
    void PlayerFunctions() {
        Movement();

        if(Input.GetKeyDown(KeyCode.Space) && onGround()) { 
            Jump();
        }
        if(Input.GetKey(KeyCode.LeftShift)) {
            Run();
        }else{
            state = PlayerState.walking;
        }
    }
    void stateTracker() {
        switch (state) { 
            case PlayerState.walking:
                _baseSpeed = walkSpeed;
                break;
            case PlayerState.running:
                _baseSpeed = runSpeed;
                break;
            default: return;
        }
    }
    void Movement() {
        float hInput = Input.GetAxisRaw("Horizontal");
        float zInput = Input.GetAxisRaw("Vertical");

        Vector3 movePlayer = _baseSpeed * Time.deltaTime * new Vector3(hInput, 0, zInput).normalized;

        transform.Translate(movePlayer);
    }
    void Jump() => _rb.velocity = new Vector3(0, JumpForce);
    void Run() => state = PlayerState.running;
    bool onGround() { 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f)) {
            if (hit.transform.CompareTag("floor")) { 
                return true;
            }
        }return false;
    }
}