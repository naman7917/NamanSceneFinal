using UnityEngine;

public class MustardReceiver : MonoBehaviour
{
    public GameObject mustardVisual;

    void Awake()
    {
        if (mustardVisual != null)
            mustardVisual.SetActive(false);
    }

    public void ApplyMustard()
    {
        if (mustardVisual != null)
            mustardVisual.SetActive(true);
    }
}
