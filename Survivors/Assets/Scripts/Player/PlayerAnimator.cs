using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    PlayerMovement playerMovement;
    SpriteRenderer sr;

    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        if(playerMovement.moveDir.x != 0 || playerMovement.moveDir.y != 0)
        {
            anim.SetBool("Move", true);
            SpriteDirectionChecker();
        }
        else
        {
            anim.SetBool("Move", false);
        }
    }

    void SpriteDirectionChecker()
    {
        if(playerMovement.lastHorizontalVector < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
