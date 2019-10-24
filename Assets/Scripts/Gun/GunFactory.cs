using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFactory : MonoBehaviour {
    public static GunFactory Instance;

    private GameObject prefab_AssaultRifle;
    private GameObject prefab_Shotgun;
    private GameObject prefab_WoodenBow;
    private GameObject prefab_WoodenSpear;
    private GameObject prefab_Building;

    int index = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start () {
        PrefabLoad();

    }
	
	private void PrefabLoad()
    {
        prefab_AssaultRifle = Resources.Load<GameObject>("Gun/Prefabs/Assault Rifle");
        prefab_Shotgun = Resources.Load<GameObject>("Gun/Prefabs/Shotgun");
        prefab_WoodenBow = Resources.Load<GameObject>("Gun/Prefabs/Wooden Bow");
        prefab_WoodenSpear = Resources.Load<GameObject>("Gun/Prefabs/Wooden Spear");
        prefab_Building = Resources.Load<GameObject>("Gun/Prefabs/Building Plan");
    }

    public GameObject CreateGun(string gunName , GameObject icon)
    {
        GameObject tempGun = null;
        switch (gunName)
        {
            case "Assault Rifle":
                tempGun = GameObject.Instantiate<GameObject>(prefab_AssaultRifle, transform);
                InitGun(tempGun, 100, 10, GunType.AssaultRifle, icon);
                break;
            case "Shotgun":
                tempGun = GameObject.Instantiate<GameObject>(prefab_Shotgun, transform);
                InitGun(tempGun, 100, 20, GunType.Shotgun , icon);
                break;
            case "Wooden Bow":
                tempGun = GameObject.Instantiate<GameObject>(prefab_WoodenBow, transform);
                InitGun(tempGun, 100, 30, GunType.WoodenBow, icon);
                break;
            case "Wooden Spear":
                tempGun = GameObject.Instantiate<GameObject>(prefab_WoodenSpear, transform);
                InitGun(tempGun, 100, 40, GunType.WoodenSpear , icon);
                break;
            case "Building" +
            "":
                tempGun = GameObject.Instantiate<GameObject>(prefab_Building, transform);
                break;
        }
        return tempGun;
    }

    private void InitGun(GameObject gun,int damage,int durabl,GunType gunType,GameObject icon)
    {
        GunControllerBase gcb = gun.GetComponent<GunControllerBase>();
        gcb.Id = index++;
        gcb.Damage = damage;
        gcb.Durable = durabl;
        gcb.GunWeaponType = gunType;
        gcb.Icon = icon;
    }
        
}
