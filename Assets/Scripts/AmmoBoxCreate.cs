using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxCreate : MonoBehaviour
{
    public List<GameObject> AmmoBoxPoints = new List<GameObject>();
    private List<int> Points = new List<int>();
    public GameObject _ammoBox;

    void Start()
    {
        StartCoroutine(AmmoBoxCreater());
    }

    IEnumerator AmmoBoxCreater()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            int _randomValue = Random.RandomRange(0, 5);

            if (!Points.Contains(_randomValue))
            {
                Points.Add(_randomValue);
            }

            else
            {
                _randomValue = Random.Range(0, 5);
                continue;
            }

            GameObject obje = Instantiate(_ammoBox, AmmoBoxPoints[_randomValue].transform.position, AmmoBoxPoints[_randomValue].transform.rotation);
            obje.transform.gameObject.GetComponent<AmmoBox>()._pointValue = _randomValue;
        }
    }

    public void DeletePoints(int value)
    {
        Points.Remove(value);
    }
}
