using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponUi : MonoBehaviour
{

    TextMeshProUGUI tmpui;
    // Start is called before the first frame update
    void Start()
    {
        tmpui = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        Weapon weaponScript = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>();
        string reloading = weaponScript.getIsReload() ? "Reloading " : "";
        tmpui.SetText(reloading + weaponScript.getCurrentBullets().ToString() + "/" + weaponScript.maxBullets.ToString());
    }
}
