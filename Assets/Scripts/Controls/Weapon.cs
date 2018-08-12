using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float _coolDown;
    [SerializeField] private GameObject Bullet;
    private float timer;
    private bool isCanShot = true;

    private void Start()
    {
        timer = _coolDown;
    }

    private void Update()
    {
        if (timer >= 0)
            timer -= Time.deltaTime;
    }

    public bool IsCanShot
    {
        get
        {
            return isCanShot;
        }

        set
        {
            isCanShot = false;
            if (value && timer <= 0)
                isCanShot = true;

        }
    }


    public void Shot()
    {
        if (IsCanShot)
        {
            IsCanShot = false;
            timer = _coolDown;
            GameObject createdBullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            createdBullet.GetComponent<Bullet>().SetDirection(transform.up);
        }
    }

    private IEnumerator GetCoolDown()
    {
        yield return new WaitForSeconds(_coolDown);
        IsCanShot = true;
    }

}
