using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    [SerializeField] private AudioSource _reload;
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private ParticleSystem _bulletTrace;
    [SerializeField] private ParticleSystem _bloodEffect;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && _shot == true && Time.time > _internalShotSpeed)
        {
            Shot();
            _internalShotSpeed = Time.time + _externalShotSpeed;
        }

        if (Input.GetKey(KeyCode.R))
        {
            _animator.Play("ChangeMagazine");

            if (!_reload.isPlaying)
            {
                _reload.Play();
            }
        }
    }

    public void Shot()
    {
        _shotSound.Play();
        _shotEffect.Play();
        _animator.Play("Shot");

        RaycastHit hit;
        if (Physics.Raycast(_myCam.transform.position, transform.forward, out hit, _range))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(_bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            Instantiate(_bulletTrace, hit.point, Quaternion.LookRotation(hit.normal));
            print(hit.transform.name);
        }
    }
}
