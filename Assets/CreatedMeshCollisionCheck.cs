using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatedMeshCollisionCheck : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision) {
        Debug.Log(collision.name);
        if (collision.CompareTag("Enemy")) {
            collision.transform.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
