using UnityEngine;

public class TeleportRingOnTrigger : MonoBehaviour
{
    public ParticleSystem fireRing;
    public float delayAfterEnter = 0.2f;
    public float visibleDuration = 2f;
    bool isPlaying;

    void Awake()
    {
        if (fireRing == null)
            fireRing = GetComponent<ParticleSystem>();

        if (fireRing != null)
            fireRing.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void OnTriggerEnter(Collider other)
    {
        if (isPlaying) return;

        // Check tag or layer if you want only the player
        if (other.CompareTag("Player"))
            StartCoroutine(PlayRing());
    }

    System.Collections.IEnumerator PlayRing()
    {
        isPlaying = true;

        yield return new WaitForSeconds(delayAfterEnter);

        if (fireRing != null)
            fireRing.Play();

        yield return new WaitForSeconds(visibleDuration);

        if (fireRing != null)
            fireRing.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        isPlaying = false;
    }
}
