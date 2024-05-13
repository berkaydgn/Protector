using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoxCreate : MonoBehaviour
{
    public List<GameObject> HealthBoxPoints = new List<GameObject>();
    public GameObject _healthBox;
    public static bool _isHealthBox;

    void Start()
    {
        _isHealthBox = false;
        StartCoroutine(AmmoBoxCreater());
    }

    IEnumerator AmmoBoxCreater()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            
            if (!_isHealthBox)
            {
                int _randomValue = Random.RandomRange(0, 5);
                Instantiate(_healthBox, HealthBoxPoints[_randomValue].transform.position, HealthBoxPoints[_randomValue].transform.rotation);
                _isHealthBox = true;
            }
        }
    }
}
