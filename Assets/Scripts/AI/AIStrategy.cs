using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIStrategy
{
    private float timer = 3f;
    protected GameObject enemyTank;
    protected Transform allDirections;
    protected Motor enemyMotor;
    protected Weapon enemyWeapon;
    private bool isPlayerOnRay = false;
    protected Direction currentDirection = Direction.Up;
    public abstract void Algorithm();

    public bool IsHit { get; set; }
    public bool IsBulletNear { get; set; }
    public Vector3 HitPoint { get; set; }

    private void ChoseDirectionToMove()
    {
        int index = Random.Range(0, (int)Direction.Right);
        currentDirection = (Direction)index;
    }

    protected void ChoseDirectionToMove(int blockedIndex)
    {
        int[] indexArr = new int[(int)Direction.Right];
        int counter = 0;

        for (int i = 0; i < (int)Direction.Right+1; i++)
        {
            if(i != blockedIndex)
                {
                    indexArr[counter] = i;
                    counter++;
                }
        }

        currentDirection = (Direction)Random.Range(0, indexArr[indexArr.Length-1]+1);
    }


    protected void GetRandomTimer()
    {
        timer = Random.Range(1, 5f);
    }

    private void RecalculateTimer()
    {
        timer -= Time.deltaTime;
    }

    private void ShotRaysAround()
    {

        if (!isPlayerOnRay)
        {

            Direction? direction =  RaycastAround("Player");
            if (direction.HasValue)
                currentDirection = direction.Value;
        }
        else
        {
            Ray ray = new Ray(allDirections.GetChild(0).position, allDirections.GetChild(0).up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    GetRandomTimer();
                    enemyWeapon.IsCanShot = true;
                    isPlayerOnRay = true;
                }
                else
                {
                    GetRandomTimer();
                    ChoseDirectionToMove();
                    enemyWeapon.IsCanShot = false;
                    isPlayerOnRay = false;
                }

            }
        }
       

    }

    protected Direction? RaycastAround(string tag)
    {
        for (int i = 0; i < allDirections.childCount; i++)
        {
            Ray ray = new Ray(allDirections.GetChild(i).position, allDirections.GetChild(i).up);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag(tag))
                {
                    GetRandomTimer();
                    isPlayerOnRay = true;
                    return (Direction)i;
                }
            }
        }

        return null;
    }


    protected void CheckMoving()
    {
        RecalculateTimer();
       
        if (timer <= 0)
        {
            GetRandomTimer();
            ChoseDirectionToMove();
        }
        else if (IsHit)
        {
            GetRandomTimer();
            ChoseDirectionToMove((int)currentDirection);
            IsHit = false;
        }
        ShotRaysAround();

    }
}
