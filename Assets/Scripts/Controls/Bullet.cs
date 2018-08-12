using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{

    [SerializeField] private int _speed;
    private Vector3 _direction;
    private bool isActive = false;
    private CharacterController character;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
        isActive = true;
        character = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (isActive)
            character.Move(transform.TransformDirection(_direction) * _speed * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.collider.gameObject.GetComponent<Tank>() != null)
        {
            hit.collider.gameObject.GetComponent<Tank>().Health -= 50;
        }
        Destroy(gameObject);
    }

}
