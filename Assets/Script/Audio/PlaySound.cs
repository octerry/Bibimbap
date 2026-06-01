using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class PlaySound : MonoBehaviour
{
    public enum SoundType
    {
        Explosion,
        Throw,
        Walk,
        Jump,
        Hit,
        Curtain
    }
    
    [Serializable]
    public class AudioLink
    {
        public SoundType Type;
        public AudioClip[] Audios;
    }
    
    [SerializeField] private List<AudioLink> _sounds;
    private GameObject _soundSourceElement;
    private List<AudioSource> _toCheck = new();

    public static PlaySound instance;
 
    
    private void Awake()
    {
        instance = this;
    }
    
    void Start()
    {
        _soundSourceElement = transform.Find("AudioSource").gameObject;
    }

    private void Update()
    {
        if (_toCheck.Any())
        {
            foreach (var source in _toCheck.ToList())
            {
                if (!source.isPlaying)
                {
                    _toCheck.Remove(source);
                    Destroy(source.gameObject);
                }
            }
        }  
    }

    public void PlayByType(SoundType type, Vector3 position, int value = -1)
    {
        // On mets l'objet à la bonne position
        GameObject source = Instantiate(_soundSourceElement);
        source.transform.position = position;

        // On récup son AudioSource
        AudioSource sourceSound = source.GetComponent<AudioSource>();
        
        // On prends l'index dans _sounds
        int soundFxIndex = _sounds.FindIndex(item => item.Type == type);
        int index;
        if (value >= 0)
        {
            index = value;
        }
        // On prends l'index d'un son aléatoire parmis _sounds.Audios
        else
        {
            index = (int)UnityEngine.Random.Range(0,_sounds[soundFxIndex].Audios.Length-1);
            Debug.Log(index);
        }
        
        // On le mets en lecture dans le AudioSource
        sourceSound.clip = _sounds[soundFxIndex].Audios[index];
        sourceSound.Play();

        sourceSound.volume = GlobalSettings.SoundFxVolume * GlobalSettings.GlobalVolume;
        
        _toCheck.Add(sourceSound);
    }
}
