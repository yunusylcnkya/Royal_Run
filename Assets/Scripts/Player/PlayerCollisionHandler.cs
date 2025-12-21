using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;
    [SerializeField] float collisionCooldown = 1f;

    const string hitString = "Hit";
    float cooldownTimer = 1f;
    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer < collisionCooldown) return;

        myAnimator.SetTrigger(hitString);
        cooldownTimer = 0f;
    }
}
