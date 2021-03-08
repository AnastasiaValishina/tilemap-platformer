using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryPickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSFX;
    [SerializeField] float timeBeforeDestroying = 1f;
    [SerializeField] int pickupValue = 1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision is BoxCollider2D) { return; }
        StartCoroutine(Collect());
    }

    IEnumerator Collect()
    {
        GetComponent<Animator>().SetTrigger("Collected");
        AudioSource.PlayClipAtPoint(pickupSFX, Camera.main.transform.position);
        FindObjectOfType<GameSession>().AddToScore(pickupValue);
        yield return new WaitForSeconds(timeBeforeDestroying);
        Destroy(gameObject);
    }
}
