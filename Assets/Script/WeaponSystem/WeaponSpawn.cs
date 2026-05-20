using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    [SerializeField] private int _maxWeapons;
    [SerializeField] private Transform _weapons;
    private Weapons _weaponComponent;
    private Transform[] _spawnpoints;
    private WeaponSpawnPoint[] _spawnPointElements;
    private bool[] _hasAWeapon;
    private int _weaponsNumber;
    
    void Start()
    {
        _spawnpoints = new Transform[transform.childCount];
        _spawnPointElements = new WeaponSpawnPoint[transform.childCount];
        _hasAWeapon = new bool[transform.childCount];
        if (_maxWeapons > transform.childCount) _maxWeapons = transform.childCount;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            _spawnpoints[i] = child;
            _spawnPointElements[i] = child.GetComponent<WeaponSpawnPoint>();
        }

        _weaponComponent = _weapons.GetComponent<Weapons>();
    }
    
    void Update()
    {
        if (_weaponsNumber < _maxWeapons)
        {
            int chosen = Random.Range(0, transform.childCount);
            // while (_hasAWeapon[chosen])
            // {
            //     chosen = Random.Range(0, transform.childCount);
            // }

            _weaponComponent.AddRandomWeaponAt(_spawnpoints[chosen].position, chosen, _spawnPointElements[chosen].GetSpawnableWeapons());
            _hasAWeapon[chosen] = true;

            _weaponsNumber++;
        }
    }

    public void DestroySingleWeapon(int id)
    {
        _hasAWeapon[id] = false;
        _weaponsNumber--;
    }
}
