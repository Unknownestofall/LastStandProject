using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class uiManager : MonoBehaviour
{
    [SerializeField] Text ammoTracker, waveTracker,interactText;
    [SerializeField] Image health;

    // Start is called before the first frame update
    void Start()
    {
        updateInteractText();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void updateAmmo(int curAmmo, int curMags) {
        ammoTracker.text = $"{curAmmo}/ {curMags}";
    }
    public void updateWave(int curWave) { 
        waveTracker.text = curWave.ToString();
    }
    public void updateHealth(float curhealth) { 
        health.fillAmount = curhealth;
    }
    public void updateInteractText(string msg = null) { 
        interactText.text = msg;
    }
}
