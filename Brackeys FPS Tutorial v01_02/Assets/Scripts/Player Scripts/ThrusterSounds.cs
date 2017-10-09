using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrusterSounds : MonoBehaviour
{
    private PlayerController plrController;
    private float thrusterFuelAmount;
    private AudioManager audioManager;
    [SerializeField]
    private Sound boosterSoundClip;

    List<AudioSource> boosterAudioSources;


    void Start()
    {

        plrController = gameObject.transform.parent.transform.parent.transform.parent.GetComponent<PlayerController>();
        Debug.Log("plrController = " + plrController.transform.name);
        audioManager = FindObjectOfType<AudioManager>().GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {

        thrusterFuelAmount = plrController.GetThrusterFuelAmount();
        if (Input.GetButtonDown("Jump") && thrusterFuelAmount > .05f)
        {
            audioManager.Play("sndBoosterBoost", gameObject, 5f);
        }
        if (Input.GetButtonUp("Jump") || thrusterFuelAmount <= .05f)
        {
            DestroyBoosterAudioSource();
        }
    }


    public void StartRegularBoosterSound()
    {

        audioManager.Play("sndBoosterRegular", gameObject, 300f);
    }

    void DestroyBoosterAudioSource()
    {
        boosterAudioSources = new List<AudioSource>(FindObjectsOfType<AudioSource>());
        if (boosterAudioSources != null)
        {
            boosterAudioSources.ForEach
           (delegate (AudioSource aSrc)
           {
               if (aSrc.clip.name == boosterSoundClip.clip.name)
               {
                   DestroyImmediate(aSrc);
               }
           }
           );
        }
    }
}
