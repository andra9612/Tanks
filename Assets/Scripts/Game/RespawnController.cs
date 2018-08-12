using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnController : MonoBehaviour
{
    private GameObject player;
    private bool isNeedToFindRespawnPoint = false;
    public LayerMask layer;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        PlayerTank.PlayerDisabledObserver += Respawn;

    }

    private void Respawn()
    {
        StartCoroutine(WaitForRespawn());
    }

    private IEnumerator WaitForRespawn()
    {
        yield return new WaitForSeconds(1f);
        isNeedToFindRespawnPoint = true;
    }

    void Update()
    {
        if (isNeedToFindRespawnPoint)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                Collider[] colliderArr = Physics.OverlapSphere(gameObject.transform.GetChild(i).position, 1f, layer);
                if (colliderArr.Length == 0)
                {
                    player.transform.position = new Vector3(
                       gameObject.transform.GetChild(i).position.x,
                       gameObject.transform.GetChild(i).position.y,
                       player.transform.position.z);
                    player.SetActive(true);
                    player.GetComponent<Tank>().Health = 100;
                    isNeedToFindRespawnPoint = false;
                }
            }

        }
    }
}
