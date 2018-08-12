using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tank : MonoBehaviour
{

    [SerializeField] private int _health;
    [SerializeField] private int _speed;
    protected Weapon weapon;

    protected void InitializeTank(int health)
    {
        Health = health;
        TankMotor = new Motor(gameObject, Speed);
        weapon = gameObject.transform.GetChild(0).GetComponent<Weapon>();
    }

    protected void InitializeTank()
    {
        TankMotor = new Motor(gameObject, Speed);
        weapon = gameObject.transform.GetChild(0).GetComponent<Weapon>();
    }

    public Motor TankMotor { get; set; }

    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            if (value <= 0)
            {
                _health = 0;
                gameObject.SetActive(false);
            }
            else
            {
                _health = value;
            }
        }
    }
    public int Speed
    {
        get
        {
            return _speed;
        }
        set
        {
            if (value <= 0)
                _speed = 8;
            else
                _speed = value;
        }
    }

   
}
