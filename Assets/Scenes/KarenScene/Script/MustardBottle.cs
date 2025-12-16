using UnityEngine;

public class MustardBottle : MonoBehaviour
{
    public ParticleSystem sprayFx;

    void Awake()
    {
        if (sprayFx != null)
            sprayFx.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Nozzle trigger with: " + other.name);

        MustardReceiver receiver = other.GetComponent<MustardReceiver>();
        if (receiver != null)
        {
            Debug.Log("Found MustardReceiver on " + other.name);
            receiver.ApplyMustard();

            if (sprayFx != null)
                sprayFx.Play();
        }
    }
}
