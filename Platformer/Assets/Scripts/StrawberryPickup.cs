using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryPickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] float timeBeforeDestroying = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Collect());
    }

    IEnumerator Collect()
    {
        GetComponent<Animator>().SetTrigger("Collected");
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        yield return new WaitForSeconds(timeBeforeDestroying);
        Destroy(gameObject);
    }

}
