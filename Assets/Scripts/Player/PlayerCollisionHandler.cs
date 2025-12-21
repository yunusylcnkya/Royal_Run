using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Animator myAnimator;
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float adjustChangeMoveSpeedAmount = -2f;

    const string hitString = "Hit";
    float cooldownTimer = 1f;

    LevelGenerator levelGenerator;
    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();

    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (cooldownTimer < collisionCooldown) return;
        levelGenerator.changeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
        myAnimator.SetTrigger(hitString);
        cooldownTimer = 0f;
    }
}
