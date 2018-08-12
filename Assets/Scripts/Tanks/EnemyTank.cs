using UnityEngine;
using System.Collections;

public class EnemyTank : Tank
{
    public AIStrategy Strategy { get; set; }

    private void OnEnable()
    {
        InitializeTank();
    }

    private void Update()
    {
        Strategy.Algorithm();
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Strategy.IsHit = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Strategy.HitPoint = other.transform.position;
            Strategy.IsBulletNear = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bullet"))
            Strategy.IsBulletNear = false;
    }
}
