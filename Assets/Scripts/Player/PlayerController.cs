using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // Oyuncunun hareket hızı
    [SerializeField] private float xClamp = 3f;    // X ekseninde sınırlama
    [SerializeField] private float zClamp = 3f;    // Z ekseninde sınırlama

    // Oyuncu girişlerini saklamak için
    Vector2 movement;

    // Rigidbody referansı, fizik tabanlı hareket için
    Rigidbody rigidBody;

    void Awake()
    {
        // Rigidbody'yi al
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Fizik tabanlı hareketi uygula
        HandleMovement();
    }

    // Input sistemi üzerinden hareket değerlerini alır
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>(); // x ve y değerlerini oku
    }

    private void HandleMovement()
    {
        // Mevcut pozisyonu al
        Vector3 currentPosition = rigidBody.position;

        // Input yönünü 3D vektöre çevir
        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);

        // Yeni pozisyonu hesapla
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);

        // X ve Z eksenlerini sınırla (oyuncunun oyun alanını aşmasını engelle)
        newPosition.x = Mathf.Clamp(newPosition.x, -xClamp, xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClamp, zClamp);

        // Rigidbody ile pozisyonu güncelle
        rigidBody.MovePosition(newPosition);
    }
}
