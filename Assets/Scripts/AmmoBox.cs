using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBox : MonoBehaviour
{
    private string[] Weaponry = {"ak47", "shotgun", "sniper", "magnum" };
    private int[] Bullets = {10, 20, 25, 30};
    public List<Sprite> WeaponSprites = new List<Sprite>();

    public string _randomWeapon;
    public int _randomBullet;
    public Image _randomWeaponImage;
    public int _pointValue;


    void Start()
    {
        int _randomWeaponValue = Random.RandomRange(0, Weaponry.Length);
        _randomWeapon = Weaponry[_randomWeaponValue];
        _randomBullet = Bullets[Random.RandomRange(0, Bullets.Length)];
        _randomWeaponImage.sprite = WeaponSprites[_randomWeaponValue];
    }
}   
