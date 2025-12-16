using UnityEngine;

public class BreadUITrigger : MonoBehaviour
{
    [Header("UI")]
    public GameObject popupPanel;

    [Header("Bread object")]
    public GameObject breadObject;          // This bread will be hidden

    [Header("FX when bread is added")]
    public ParticleSystem addEffect;        // Optional: sparkle / puff effect
    public AudioSource addSound;            // Optional: sound when adding

    private bool alreadyShown = false;

    private void OnTriggerEnter(Collider other)
    {
        if (alreadyShown) return;

        if (other.gameObject.name.ToLower().Contains("hand") || other.CompareTag("Player"))
        {
            alreadyShown = true;

            if (popupPanel != null)
                popupPanel.SetActive(true);

            Debug.Log("POPUP ACTIVADO POR COLISIÓN");
        }
    }

    public void OnAddToBag()
    {
        Debug.Log("Bread added to bag!");

        // Play visual effect and sound
        if (addEffect != null)
            addEffect.Play();

        if (addSound != null)
            addSound.Play();

        // Hide/remove the bread that was selected
        if (breadObject != null)
            breadObject.SetActive(false);   // or Destroy(breadObject);

        // Close the popup
        if (popupPanel != null)
            popupPanel.SetActive(false);

        // Increase bag count
        if (BagManager.Instance != null)
            BagManager.Instance.AddItems(1);
    }

    public void OnNoThanks()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);

        Debug.Log("No thanks");
        // breadObject is left as-is
    }
}
