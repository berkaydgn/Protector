using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Gun : MonoBehaviour
{
    [SerializeField] private bool _shot;
    private float _internalShotSpeed;
    [SerializeField] private float _externalShotSpeed;
    [SerializeField] private float _range;
    [SerializeField] private Camera _myCam;
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private ParticleSystem _shotEffect;


    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _shot == true && Time.time > _internalShotSpeed)
        {
            Shot();
            _internalShotSpeed = Time.time + _externalShotSpeed;
        }
    }

    public void Shot()
    {
        RaycastHit hit;
        _shotSound.Play();
        _shotEffect.Play();

        if (Physics.Raycast(_myCam.transform.position, transform.forward, out hit, _range))
            print(hit.transform.name);

    }
}
