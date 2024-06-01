using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] int curAmmoInMag;
    [SerializeField] int totalAmmoInMag;
    [SerializeField] int maxAmmo;
    int _baseAmmo;

    [SerializeField] AudioClip shootSFX;
    [SerializeField] GameObject shootFX;
    [SerializeField] AudioClip reloadSFX;

    [SerializeField] int range;

    [SerializeField] float FireRate;
    float _canFire= .2f;
    [SerializeField] int bulletDMG;

    [SerializeField] bool isExplosive;
    [SerializeField] GameObject rocketOBJ;
    [SerializeField] Transform RocketPos;

    Camera _camera;
    Player _player;
    uiManager ui;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        ui = GameObject.Find("UI").GetComponent<uiManager>();

        _baseAmmo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        ui.updateAmmo(curAmmoInMag, maxAmmo);
    }
    public void shoot() {
        if (!isExplosive && canShoot()){ 
            if(curAmmoInMag > 0) { 
                RaycastHit hit;
                if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out hit, range)) {
                    Enemy en = hit.transform.GetComponent<Enemy>();
                    if (en != null) {
                        en.Damage(bulletDMG);
                    }
                }
                if(shootSFX != null) AudioSource.PlayClipAtPoint(shootSFX, transform.position,.3f);
                shootFX.SetActive(true);
                ui.updateAmmo(curAmmoInMag, maxAmmo);
                curAmmoInMag--;
            }
        }else if(isExplosive && canShoot()) {
            if (curAmmoInMag > 0) {
                GameObject rocket = Instantiate(rocketOBJ, RocketPos.transform.position ,_camera.transform.rotation);
                rocket.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, 0, 1000));
                curAmmoInMag--;
                ui.updateAmmo(curAmmoInMag, maxAmmo);
                shootFX.SetActive(true);
            }
        }
    }
    bool canShoot() { 
        if(Time.time > _canFire) { 
            _canFire = Time.time + FireRate;
            return true;
        }return false;
    }
    public void onStopShooting() => shootFX.SetActive(false);
    public void Reload() { 
        int bulletsToReload = totalAmmoInMag - curAmmoInMag;
        if (maxAmmo >= bulletsToReload) {
            curAmmoInMag += bulletsToReload;
            maxAmmo -= bulletsToReload;
        }else {
            curAmmoInMag = maxAmmo;
            maxAmmo -= maxAmmo;
        }
        AudioSource.PlayClipAtPoint(reloadSFX, transform.position);
    }
    public void restockAmmo() =>maxAmmo = _baseAmmo;
}
