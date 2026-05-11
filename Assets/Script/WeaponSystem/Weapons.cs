using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Weapons : MonoBehaviour
{
    [SerializeField] private GameObject _prefabWeapon;
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

    public void AddRandomWeaponAt(Vector2 position, int n)
    {
        Array values = Enum.GetValues(typeof(AmmoSystem.ammoType));
        AmmoSystem.ammoType randomWeaponType = (AmmoSystem.ammoType)values.GetValue(Random.Range(0, values.Length));

        GameObject newWeapon = Instantiate(_prefabWeapon);
        SingleWeapon weaponCompenent = newWeapon.GetComponent<SingleWeapon>();
        weaponCompenent.weaponType = randomWeaponType;
        weaponCompenent.WeaponId = n;

        newWeapon.transform.position = position;
    }
}
