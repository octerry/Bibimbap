using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapons : MonoBehaviour
{
    [SerializeField] private GameObject _prefabWeapon;
    public Sprite[] weaponSprites;
    private GameObject[] _actualWeapons;
    
    void Start()
    {
        _actualWeapons = new GameObject[transform.childCount];

        for (int i=0; i<transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            _actualWeapons[i] = child;
        }
    }

    public void AddRandomWeaponAt(Transform spawnpoint, int n, AmmoSystem.ammoType[] ammoList)
    {
        AmmoSystem.ammoType randomWeaponType = (AmmoSystem.ammoType)ammoList.GetValue(Random.Range(0, ammoList.Length));

        GameObject newWeapon = Instantiate(_prefabWeapon);
        SingleWeapon weaponCompenent = newWeapon.GetComponent<SingleWeapon>();
        weaponCompenent.weaponType = randomWeaponType;
        weaponCompenent.WeaponId = n;

        newWeapon.transform.position = spawnpoint.position;
        newWeapon.GetComponent<SpriteRenderer>().sprite = weaponSprites[(int)randomWeaponType];
    }
}
