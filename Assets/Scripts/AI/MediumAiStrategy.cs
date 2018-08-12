using UnityEngine;
using System.Collections;

public class MediumAiStrategy : AIStrategy
{
    public override void Algorithm()
    {

        CheckMoving();
        if (IsBulletNear)
        {
            ChoseDirectionToMove(CheckWhereIsBullet());
            GetRandomTimer();
            IsBulletNear = false;
        }
        enemyMotor.Move(currentDirection);
        enemyWeapon.Shot();
    }

    public MediumAiStrategy()
    {
        enemyTank = null;
        enemyMotor = null;
        enemyWeapon = null;
        allDirections = null;
    }

    public MediumAiStrategy(GameObject tank, Motor motor, Weapon weapon)
    {
        enemyTank = tank;
        enemyMotor = motor;
        enemyWeapon = weapon;
        allDirections = tank.transform.Find("Directions");
    }

    private int CheckWhereIsBullet()
    {
        float minimum = float.MaxValue;
        float currentValue;
        int minIndex = 0;

        for (int i = 0; i < allDirections.childCount; i++)
        {
            currentValue = Vector3.Distance(allDirections.GetChild(i).position, HitPoint);
            if (currentValue < minimum)
            {
                minimum = currentValue;
                minIndex = i;
            }
        }
        Debug.Log((Direction)minIndex);
        return minIndex;
    }


}
