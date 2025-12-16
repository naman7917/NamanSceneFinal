using UnityEngine;

public class NPCController : MonoBehaviour
{
    [Header("ZORUNLU AYARLAR")]
    public Transform playerCamera;   // CenterEyeAnchor buraya gelecek
    public GameObject infoCardUI;    // UI Canvas buraya gelecek
    public Animator npcAnimator;     // Karakter Animatoru buraya

    [Header("Mesafe Ayarı")]
    public float detectionDistance = 3.0f; // Algilama mesafesi

    void Start()
    {
        // 1. KONTROL: UI Atanmis mi?
        if (infoCardUI == null)
        {
            Debug.LogError("HATA: 'Info Card UI' kutusu bos! Lutfen UI objesini surukle.");
        }
        else
        {
            infoCardUI.SetActive(false); // Baslangicta kapat
        }

        // 2. KONTROL: Kamera Atanmis mi?
        if (playerCamera == null)
        {
            Debug.LogError("HATA: 'Player Camera' kutusu bos! Lutfen CenterEyeAnchor'i surukle.");
        }

        // 3. KONTROL: Animator var mi?
        if (npcAnimator == null)
        {
            npcAnimator = GetComponent<Animator>();
            if (npcAnimator == null)
                Debug.LogError("HATA: Karakterin uzerinde 'Animator' bileseni yok!");
        }
    }

    void Update()
    {
        if (playerCamera == null) return; // Kamera yoksa calisma

        // Mesafeyi olcuyoruz
        float distance = Vector3.Distance(transform.position, playerCamera.position);

        // Mesafeyi surekli Console'a yazdiralim (Test bitince bu satiri silebilirsin)
        // Debug.Log("Mesafe: " + distance); 

        if (distance <= detectionDistance)
        {
            // Yakindayiz
            if (!infoCardUI.activeSelf) // Eger UI zaten acik degilse ac
            {
                Debug.Log("OYUNCU ALANA GIRDI - UI ACILIYOR");
                infoCardUI.SetActive(true);
                if (npcAnimator) npcAnimator.SetBool("isTalking", true);
            }
        }
        else
        {
            // Uzaktayiz
            if (infoCardUI != null && infoCardUI.activeSelf) // Eger UI aciksa kapat
            {
                Debug.Log("OYUNCU ALANDAN CIKTI - UI KAPANIYOR");
                infoCardUI.SetActive(false);
                if (npcAnimator) npcAnimator.SetBool("isTalking", false);
            }
        }
    }

    // Editor'de sari cember ciz
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }
}