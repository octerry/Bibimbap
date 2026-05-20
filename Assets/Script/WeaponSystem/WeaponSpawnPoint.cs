using UnityEngine;

public class WeaponSpawnPoint : MonoBehaviour
{
    [SerializeField] private AmmoSystem.ammoType[] _spawnableWeapons;
    
    void Start()
    {
        
    }

    public AmmoSystem.ammoType[] GetSpawnableWeapons()
    {
        return _spawnableWeapons;
    }
}
