﻿using UnityEngine;

public enum PlayerState
{
    Idle = 0,
    Move,
    Jump,
    Damage,
    LightItem,
    ShadowItem,
    Die
}

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMove playerMove;

    [SerializeField]
    private PlayerJump playerJump;

    [SerializeField]
    private PlayerAnimation playerAni;

    [SerializeField]
    private PlayerInteraction playerInteraction;

    private BoxCollider2D m_playerBoxcollider;
  
    public bool IsGround { get { return playerJump.IsGround; } }

    public void Idle()
    {
        playerJump.IsGround = true;
        playerAni.ChangeAni(PlayerState.Idle);
    }

    public void Move(Vector2 direction)
    {
        playerMove.Move(direction);
        
        playerAni.ChangeAni(PlayerState.Move);
    }

    public void Jump()
    {
        playerJump.Jump();

        playerAni.ChangeAni(PlayerState.Jump);
    }

    public void Interaction()
    {
        playerInteraction.Interaction();
    }
}