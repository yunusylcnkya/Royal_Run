using Unity.VisualScripting;
using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float adjustChangeMoveSpeedAmount = 3f;
    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>();
    }
    protected override void OnPickup()
    {
        levelGenerator.changeChunkMoveSpeed(adjustChangeMoveSpeedAmount);
    }
}
