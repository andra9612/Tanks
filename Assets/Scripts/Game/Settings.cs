using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    private GameObject player;
    private Transform enemies;
    public InputField enemyCountInput;
    private int count = 5;
    public Toggle easyAI;
    public Toggle mediumAI;
    public GameObject UIPanel;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 0;
        player = GameObject.FindWithTag("Player");
        enemies = GameObject.Find("Enemies").transform;
    }

    public void SetEasyLevel()
    {
        mediumAI.isOn = false;

    }

    public void SetMediumLevel()
    {
        easyAI.isOn = false;
    }

    private IEnumerator ChangeEnemyAI(AIStrategy strategy, int count)
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < count; i++)
        {
            GameObject enemy = enemies.GetChild(i).gameObject;
            EnemyTank tank = enemy.GetComponent<EnemyTank>();
            enemy.SetActive(true);
            if (strategy is EasyAIStrategy)
                tank.Strategy = new EasyAIStrategy(enemy, tank.TankMotor, enemy.transform.GetChild(0).GetComponent<Weapon>());
            else
                tank.Strategy = new MediumAiStrategy(enemy, tank.TankMotor, enemy.transform.GetChild(0).GetComponent<Weapon>());
        }
    }

    private void ChangeEnemyCount()
    {
        count = 5;
        try
        {
            count = int.Parse(enemyCountInput.text);
        }
        catch (System.Exception)
        {
            count = 5;
        }

        if (count > enemies.childCount)
            count = enemies.childCount;
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        player.transform.position = Vector3.zero;
        player.transform.rotation = Quaternion.identity;
        for (int i = 0; i < enemies.childCount; i++)
        {
            enemies.GetChild(i).gameObject.SetActive(false);
        }
        ChangeEnemyCount();
        if (easyAI)
            StartCoroutine(ChangeEnemyAI(new MediumAiStrategy(), count));
        else if (mediumAI)
            StartCoroutine(ChangeEnemyAI(new EasyAIStrategy(), count));
        UIPanel.SetActive(false);
    }

    public void Back()
    {
        Time.timeScale = 1;
        player.GetComponent<PlayerTank>().enabled = true;
        UIPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
