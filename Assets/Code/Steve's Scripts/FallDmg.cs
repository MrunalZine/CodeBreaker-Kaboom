using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDmg : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            collider.gameObject.SetActive(false);
        }

    }
}
