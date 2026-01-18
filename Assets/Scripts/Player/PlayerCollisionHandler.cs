using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator myAnimator; // Çarpma animasyonunu oynatmak için Animator

    [Header("Collision Settings")]
    [SerializeField] float collisionCooldown = 1f;          // Çarpma sonrası bekleme süresi
    [SerializeField] float adjustChangeMoveSpeedAmount = -2f; // Çarpma sonrası hız değişimi

    const string hitString = "Hit"; // Animator trigger parametre adı
    float cooldownTimer = 1f;       // Çarpma sonrası geçen süreyi takip eden sayaç

    LevelGenerator levelGenerator;   // Oyun seviyesini yöneten sınıf

    void Start()
    {
        // LevelGenerator sahnede varsa al
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }

    void Update()
    {
        // Her frame cooldown timer'ı artır
        cooldownTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Eğer cooldown süresi dolmamışsa çarpmayı yok say
        if (cooldownTimer < collisionCooldown) return;

        // Çarpma gerçekleştiğinde oyun hızını ayarla (chunk move speed)
        levelGenerator.ChangeChunkMoveSpeed(adjustChangeMoveSpeedAmount);

        // Animator'da hit animasyonunu tetikle
        myAnimator.SetTrigger(hitString);

        // Cooldown timer'ı sıfırla
        cooldownTimer = 0f;
    }
}
