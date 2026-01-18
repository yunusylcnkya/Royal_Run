using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    // Çarpınca çıkacak toz/taş parçaları efekti
    [SerializeField] ParticleSystem collisionParticleSystem;
    // Çarpma sesi
    [SerializeField] AudioSource boulderSmashAudioSource;
    // Kamerayı sallama gücü
    [SerializeField] float shakeModifier = 10f;
    // Bir çarpma efekti çalıştıktan sonra tekrar çalışabilmesi için bekleme süresi
    [SerializeField] float collisionCooldown = 1f;

    // Kamerayı sallamak için gerekli bileşen
    CinemachineImpulseSource cinemachineImpulseSource;
    // Cooldown zamanlayıcı
    float collisionTimer = 1f;

    void Awake()
    {
        // Kamerayı sallamak için bileşeni bul
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Update()
    {
        // Zamanlayıcıyı sürekli artır, böylece cooldown kontrolü yapılabilir
        collisionTimer += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Eğer cooldown süresi dolmadıysa hiçbir şey yapma
        if (collisionTimer < collisionCooldown) return;

        // Kamera sarsıntısını ve çarpma efektlerini çalıştır
        FireImpulse();
        CollisionFX(collision);
        // Cooldown için zamanlayıcıyı sıfırla
        collisionTimer = 0f;
    }

    private void FireImpulse()
    {
        // Kameraya olan uzaklığı hesapla
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        // Sarsıntı yoğunluğunu ayarla, uzaksa daha az sarsın
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);

        // Kamerayı salla
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    private void CollisionFX(Collision collision)
    {
        // Çarpma noktasını al
        ContactPoint contactPoint = collision.contacts[0];
        // Partikül sistemini çarpma noktasına taşı ve patlat
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        // Çarpma sesini çal
        boulderSmashAudioSource.Play();
    }
}
