using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int curAmmoInMag;
    [SerializeField] int totalAmmoInMag;
    [SerializeField] int maxAmmo;
    int _baseAmmo;

    [SerializeField] int range;

    [SerializeField] float FireRate;
    float _canFire= .2f;

    [SerializeField] bool isExplosive;
    [SerializeField] GameObject rocketOBJ;
    [SerializeField] Transform RocketPos;

    Camera _camera;
    Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _player = GameObject.Find("Player").GetComponent<Player>();

        _baseAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void shoot() {
        if (!isExplosive && canShoot()){ 
            if(curAmmoInMag > 0) { 
                RaycastHit hit;
                if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range)) {
                    Debug.Log(hit.transform.name);
                }
                curAmmoInMag--;
            }
        }else if(isExplosive && canShoot()) {
            if (curAmmoInMag > 0) {
                GameObject rocket = Instantiate(rocketOBJ, RocketPos.transform.position ,_camera.transform.rotation);
                rocket.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 3000));
                curAmmoInMag--;
            }
        }

    }
    bool canShoot() { 
        if(Time.time > _canFire) { 
            _canFire = Time.time + FireRate;
            return true;
        }return false;
    }
    public void Reload() { 
        int bulletsToReload = totalAmmoInMag - curAmmoInMag;
        if (maxAmmo >= bulletsToReload) {
            curAmmoInMag += bulletsToReload;
            maxAmmo -= bulletsToReload;
        }
        else {
            curAmmoInMag = maxAmmo;
            maxAmmo -= maxAmmo;
        }
    }
    public void restockAmmo() =>maxAmmo = _baseAmmo;
}
