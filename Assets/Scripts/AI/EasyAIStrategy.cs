using UnityEngine;
using System.Collections;

public class EasyAIStrategy : AIStrategy
{
    public override void Algorithm()
    {
        CheckMoving();
        enemyMotor.Move(currentDirection);
        enemyWeapon.Shot();
    }

    public EasyAIStrategy()
    {
        enemyTank = null;
        enemyMotor = null;
        enemyWeapon = null;
        allDirections = null;
    }

    public EasyAIStrategy(GameObject tank, Motor motor, Weapon weapon)
    {
        enemyTank = tank;
        enemyMotor = motor;
        enemyWeapon = weapon;
        allDirections = tank.transform.Find("Directions");
    }
}
