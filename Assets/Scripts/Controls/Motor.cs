using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor
{
    public int Speed { get; set; }
    public GameObject CurrentTank { get; set; }
    private float globalRotation = 0f;
    private CharacterController character;

    public Motor(GameObject tank, int speed)
    {
        CurrentTank = tank;
        Speed = speed;
        character = tank.GetComponent<CharacterController>();
    }

    public void Move(Direction direction)
    {
        float curRotation = 0f;
        switch (direction)
        {
            case Direction.Up:
                curRotation = 0f;
                break;
            case Direction.Down:
                curRotation = 180f;
                break;
            case Direction.Left:
                curRotation = 90f;
                break;
            case Direction.Right:
                curRotation = 270f;
                break;
        }

        if (globalRotation != curRotation)
        {
            globalRotation = curRotation;
            CurrentTank.transform.rotation = Quaternion.Euler(new Vector3(0, 0, curRotation));
        }
        character.Move(CurrentTank.transform.up * Speed * Time.deltaTime);
    }
}
