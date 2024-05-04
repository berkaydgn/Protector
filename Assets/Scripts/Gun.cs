using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Gun : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Header("SETTINGS")]
    [SerializeField] private bool _shot;
    private float _internalShotSpeed;
    [SerializeField] private float _externalShotSpeed;
    [SerializeField] private float _range;

    [Header("SOUNDS")]
    [SerializeField] private AudioSource _shotSound;
    [SerializeField] private AudioSource _reload;
    [SerializeField] private AudioSource _finishedBullet;
    [SerializeField] private AudioSource _buyBullets;   

    [Header("EFFECTS")]
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private ParticleSystem _bulletTrace;
    [SerializeField] private ParticleSystem _bloodEffect;

    [Header("WEAPON SETTINGS")]
    private int _totalBullets;
    [SerializeField] private int _magazine;
    private int _remainingBullet;
    [SerializeField] private TextMeshProUGUI _totalBulletsText;
    [SerializeField] private TextMeshProUGUI _remainingBulletText;

    [Header("OTHER")]
    [SerializeField] private Camera _myCam;
    private int _bulletFired;

    [SerializeField] private GameObject _bulletHive;
    [SerializeField] private GameObject _bulletPoint;
    [SerializeField] private GameObject _gunTip;
    [SerializeField] private string _WeaponName;
    public AmmoBoxCreate _ammoBoxCreate;


    private void Start()
    {
        _animator = GetComponent<Animator>();
        _totalBullets = PlayerPrefs.GetInt(_WeaponName + "_Ammo");
        FirstReload();
        MagazineReplacementTechnical("normal");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (_shot && Time.time > _internalShotSpeed && _remainingBullet != 0)
            {
                Shot();
                _internalShotSpeed = Time.time + _externalShotSpeed;
            }
            if(_remainingBullet == 0)
            {
                _finishedBullet.Play();
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            StartCoroutine(Reload());
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            BuyBullets();
        }

    }

    IEnumerator Reload()
    {
        if (_remainingBullet < _totalBullets && _totalBullets != 0)
        {
            _animator.Play("ChangeMagazine");

            if (!_reload.isPlaying)
            {
                _reload.Play();
            }
        }

        yield return new WaitForSeconds(.5f);

        if (_remainingBullet < _totalBullets && _totalBullets != 0)
        {
            if (_remainingBullet != 0)
            {
                MagazineReplacementTechnical("bullet");
            }
            else
            {
                MagazineReplacementTechnical("noBullet");
            }
        }
    }

    public void Shot()
    {
        ShotTechnicalOperations();

        RaycastHit hit;
        if (Physics.Raycast(_gunTip.transform.position, transform.forward, out hit, _range))
        {
            if (hit.transform.gameObject.CompareTag("Enemy"))
            {
                Instantiate(_bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }

            Instantiate(_bulletTrace, hit.point, Quaternion.LookRotation(hit.normal));
            print(hit.transform.name);
        }
    }

    public void MagazineReplacementTechnical(string type)
    {
        switch (type)
        {
            case "bullet":
                if (_totalBullets <= _magazine)
                {
                    int totalValue = _remainingBullet + _totalBullets;
                    if (totalValue > _magazine)
                    {
                        _remainingBullet = _magazine;
                        _totalBullets = totalValue - _magazine;
                        PlayerPrefs.SetInt(_WeaponName + "_Ammo", _totalBullets);
                    }
                    else
                    {
                        _remainingBullet += _totalBullets;
                        _totalBullets = 0;
                        PlayerPrefs.SetInt(_WeaponName + "_Ammo", _totalBullets);
                    }
                }
                else
                {
                    _bulletFired = _magazine - _remainingBullet;
                    _totalBullets -= _bulletFired;
                    _remainingBullet = _magazine;
                    PlayerPrefs.SetInt(_WeaponName + "_Ammo", _totalBullets);
                }
                _totalBulletsText.text = _totalBullets.ToString();
                _remainingBulletText.text = _remainingBullet.ToString();
                break;

            case "noBullet":
                if (_totalBullets <= _magazine)
                {
                    _remainingBullet = _totalBullets;
                    _totalBullets = 0;
                    PlayerPrefs.SetInt(_WeaponName + "_Ammo", _totalBullets);
                }
                else
                {
                    _totalBullets -= _magazine;
                    _remainingBullet = _magazine;
                    PlayerPrefs.SetInt(_WeaponName + "_Ammo", _totalBullets);
                }
                _totalBulletsText.text = _totalBullets.ToString();
                _remainingBulletText.text = _remainingBullet.ToString();
                break;

            case "normal":
                _totalBulletsText.text = _totalBullets.ToString();
                _remainingBulletText.text = _remainingBullet.ToString();
                break;
        }
    }

    public void ShotTechnicalOperations()
    {
        _shotSound.Play();
        _shotEffect.Play();
        _animator.Play("Shot");
        _remainingBullet--;
        _remainingBulletText.text = _remainingBullet.ToString();

        GameObject obje = Instantiate(_bulletHive, _bulletPoint.transform.position, _bulletPoint.transform.rotation);
        Rigidbody rb = obje.GetComponent<Rigidbody>();
        rb.AddRelativeForce(new Vector3(10f, 1, 0) * 20);
    }

    public void BuyBullets()
    {
        RaycastHit hit;
        if (Physics.Raycast(_gunTip.transform.position, transform.forward, out hit, 100))
        {
            if (hit.transform.CompareTag("AmmoBox"))
            {
                SaveBullets(hit.transform.GetComponent<AmmoBox>()._randomWeapon, hit.transform.GetComponent<AmmoBox>()._randomBullet);
                _ammoBoxCreate.DeletePoints(hit.transform.GetComponent<AmmoBox>()._pointValue);
                Destroy(hit.transform.gameObject);
            }
        }
    }

    public void FirstReload()
    {
        if (_totalBullets <= _magazine)
        {
            _remainingBullet = _totalBullets;
            _totalBullets = 0;
            PlayerPrefs.SetInt(_WeaponName + "_Ammo", 0);
        }
        else
        {
            _remainingBullet = _magazine;
            _totalBullets -= _magazine;
            PlayerPrefs.SetInt(_WeaponName + "_Ammo", _totalBullets);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("AmmoBox"))
        {
            SaveBullets(other.transform.GetComponent<AmmoBox>()._randomWeapon, other.transform.GetComponent<AmmoBox>()._randomBullet);
            _ammoBoxCreate.DeletePoints(other.transform.GetComponent<AmmoBox>()._pointValue);
            Destroy(other.transform.gameObject);
        }
    }

    public void SaveBullets(string weapon, int bullet)
    {
        _buyBullets.Play();

        switch (weapon)
        {
            case "ak47":
                _totalBullets += bullet;
                PlayerPrefs.SetInt(_WeaponName + "_Ammo", _totalBullets);
                MagazineReplacementTechnical("normal");
                break;

            case "shotgun":

                PlayerPrefs.SetInt("shotgun_Ammo", PlayerPrefs.GetInt("shotgun_Ammo") + bullet);
                break;

            case "sniper":

                PlayerPrefs.SetInt("sniper_Ammo", PlayerPrefs.GetInt("sniper_Ammo") + bullet);
                break;

            case "magnum":

                PlayerPrefs.SetInt("magnum_Ammo", PlayerPrefs.GetInt("magnum_Ammo") + bullet);
                break;
        }
    }
}
