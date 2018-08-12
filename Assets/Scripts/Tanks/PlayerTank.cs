using UnityEngine;
using System.Collections;

public class PlayerTank : Tank
{
    public delegate void OnPlayewrDisabled();
    public static event OnPlayewrDisabled PlayerDisabledObserver;

    private void Start()
    {
        InitializeTank();
    }
    private void Update()
    {
        PlayerMoving();
        PlayerAttack();
    }

    private void PlayerMoving()
    {
        if (Input.GetKey(KeyCode.W))
            TankMotor.Move(Direction.Up);
        else
        if (Input.GetKey(KeyCode.S))
            TankMotor.Move(Direction.Down);
        else
        if (Input.GetKey(KeyCode.A))
            TankMotor.Move(Direction.Left);
        else
        if (Input.GetKey(KeyCode.D))
            TankMotor.Move(Direction.Right);
    }

    private void PlayerAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weapon.IsCanShot = true;
            weapon.Shot();
        }
    }

    private void OnDisable()
    {
        PlayerDisabledObserver();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            PlayerDisabledObserver();
        }
    }
}
