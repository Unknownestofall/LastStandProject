using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] enum PlayerState { walking, running }
    [SerializeField] PlayerState state;

    float _baseSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;

    [SerializeField] float JumpForce;

    [SerializeField] GameObject[] gunInventory;
    float _gunSwitchTime = 0;
    float _canSwitchGuns = .25f;
    [SerializeField] int curGunSelected;

    Gun _gun;
    Rigidbody _rb;
    PlayerCam _cam;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _cam = GameObject.Find("Main Camera").GetComponent<PlayerCam>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerFunctions();
        stateTracker();
    }
    void FixedUpdate(){
        Movement();
    }
    void PlayerFunctions() {
        if(Input.GetKeyDown(KeyCode.Space) && onGround()) { 
            Jump();
        }if(Input.GetKey(KeyCode.LeftShift)) {
            Run();
        }else{
            state = PlayerState.walking;
        }

        switchWeaponSelected();
        DisplayWeapon();

        if (Input.GetMouseButton(0)) {
            _gun = gunInventory[curGunSelected].GetComponent<Gun>();
            _gun.shoot();
        }if (Input.GetKeyDown(KeyCode.R)) { 
            _gun.Reload();
        }
        if (Input.GetKeyDown(KeyCode.E) && _cam.checkForInteractable()) {
            AmmoCrate ammoCrate = GameObject.Find("ammoCrate").GetComponent<AmmoCrate>();
            ammoCrate.onInteract();
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
    bool onGround() { 
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.1f)) {
            if (hit.transform.CompareTag("floor")) { 
                return true;
            }
        }return false;
    }
    void Run() => state = PlayerState.running;
    void switchWeaponSelected(){
        Vector2 scrollWheelGunSwitch = Input.mouseScrollDelta.normalized;
        if (canSwitchGuns()){ 
            if(scrollWheelGunSwitch.y > 0) {
                curGunSelected++;
            }else if(scrollWheelGunSwitch.y < 0){ 
                curGunSelected--;
            }
        }
        if(curGunSelected < 0) { 
            curGunSelected = 2;
        }else if (curGunSelected > 2) {
            curGunSelected = 0;
        }
    }
    bool canSwitchGuns() { 
        if(Time.time > _canSwitchGuns) { 
            _canSwitchGuns = Time.time + _gunSwitchTime;
            return true;
        }return false;
    }
    void DisplayWeapon() { 
        for(int i = 0;i < gunInventory.Length;i++) { 
            if(i == curGunSelected) {
                gunInventory[curGunSelected].SetActive(true);
            }else {
                gunInventory[i].SetActive(false);
            }
        }
    }
    public void ReceiveAmmo(){
        gunInventory[curGunSelected].GetComponent<Gun>().restockAmmo();
    }
}