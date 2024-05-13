using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject[] EnemyExitPoints;
    public GameObject[] DefendPoints;
    public Image _healthBar;
    public float _health;
    public GameObject _gameOverCanvas;

    void Start()
    {
        _health = 100f;
        if (!PlayerPrefs.HasKey("GameStarted"))
        {
            PlayerPrefs.SetInt("ak47_Ammo", 70);
            //PlayerPrefs.SetInt("shotgun_Ammo", 45);
            //PlayerPrefs.SetInt("sniper_Ammo", 30);
            //PlayerPrefs.SetInt("magnum_Ammo", 28);

            PlayerPrefs.SetInt("GameStarted", 1);
        }

        StartCoroutine(EnemyCreate());
    }

    IEnumerator EnemyCreate()
    {
        while(true) 
        {
            yield return new WaitForSeconds(2f);

            int _enemies = Random.RandomRange(0, Enemies.Length);
            int _enemyExitPoint = Random.RandomRange(0, EnemyExitPoints.Length);
            int _defendPoint = Random.RandomRange(0, DefendPoints.Length);

            GameObject obje = Instantiate(Enemies[_enemies], EnemyExitPoints[_enemyExitPoint].transform.position, Quaternion.identity);
            obje.GetComponent<Enemy>().GoalSetting(DefendPoints[_defendPoint]);
        }

    }

    public void HealthBar(float damage)
    {
        _health -= damage;
        _healthBar.fillAmount = _health / 100;

        if (_health <= 0)
        {
            GameOver();
        }

    }

    public void BuyHealth()
    {
        _health = 100;
        _healthBar.fillAmount = _health / 100;
    }

    void GameOver()
    {
       _gameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void SceneReplay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
