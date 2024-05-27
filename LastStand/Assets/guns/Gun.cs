using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int curAmmoInMag;
    [SerializeField] int totalAmmoInMag;
    [SerializeField] int maxAmmo;

    [SerializeField] int range;

    [SerializeField] float FireRate;
    float _canFire= .2f;

    [SerializeField] bool isExplosive;

    Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
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
        }
    }
    bool canShoot() { 
        if(Time.time > _canFire) { 
            _canFire = Time.time + FireRate;
            return true;
        }return false;
    }
    public void Reload() { 
        curAmmoInMag = totalAmmoInMag;
    }
}
