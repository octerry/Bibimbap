using UnityEngine;

public class SingleWeapon : MonoBehaviour
{
    public AmmoSystem.ammoType weaponType;
    public int WeaponId;
    [SerializeField] private WeaponSpawn _weaponSpawn;
}
