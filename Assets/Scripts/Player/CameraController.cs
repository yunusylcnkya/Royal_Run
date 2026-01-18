using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Visual Effects")]
    [SerializeField] ParticleSystem speedupParticleSystem; // Hızlanma sırasında gösterilecek partikül efekti

    [Header("FOV Settings")]
    [SerializeField] float minFOV = 20f;       // Kamera alan görüşünün minimum değeri
    [SerializeField] float maxFOV = 120f;      // Kamera alan görüşünün maksimum değeri
    [SerializeField] float zoomDuration = 1f;  // FOV değişiminin süresi
    [SerializeField] float zoomSpeedModifier = 5f; // Hıza göre FOV değişim katsayısı

    // Cinemachine kamera referansı
    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        // Sahnedeki CinemachineCamera bileşenini al
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    // Kamera alan görüşünü hıza bağlı olarak değiştirir
    public void ChangeCameraFOV(float speedAmount)
    {
        // Önce varsa devam eden FOV değişim rutinini durdur
        StopAllCoroutines();
        // Yeni FOV değişim rutinini başlat
        StartCoroutine(ChangeFOVRoutine(speedAmount));

        // Hız pozitif ise partikül efektini oynat
        if (speedAmount > 0)
        {
            speedupParticleSystem.Play();
        }
    }

    // FOV'u smoothly değiştiren coroutine
    IEnumerator ChangeFOVRoutine(float speedAmount)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView; // Mevcut FOV
        float targetFOV = Mathf.Clamp(
            startFOV + speedAmount * zoomSpeedModifier, // Hıza bağlı hedef FOV
            minFOV,                                    // Minimum sınır
            maxFOV                                     // Maksimum sınır
        );

        float elapsedTime = 0f;

        // zoomDuration boyunca FOV'u yumuşak şekilde değiştir
        while (elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration; // 0-1 arası normalizasyon
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);
            yield return null; // bir sonraki frame'e geç
        }

        // Son değeri kesin olarak uygula
        cinemachineCamera.Lens.FieldOfView = targetFOV;
    }
}
