using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_012 : MonoBehaviour
{                                               
                                                               // SPEAR //
    [SerializeField]
    private Animator animator;
    private float animationCooldown;
    [SerializeField]
    private LayerMask hitableMask;
    private int damage = 10;
    [SerializeField]
    private bool attackedThisRound;   //sa se puna atacu odata pe animatie nu de mai multe ori ca se intampla de multe ori intre intervalu ala

    void Start()
    {
        
    }

    
    void Update()
    {
        AnimationTrigger();
        if (attackedThisRound == false)
        {
            Attack();
        }
    }


    void AnimationTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("playAnimation") == false)
        {
            animator.SetBool("playAnimation", true);
            animationCooldown = 0;
            attackedThisRound = false;
        }

        animationCooldown += Time.deltaTime;

        if (animator.GetBool("playAnimation") == true && animationCooldown >= 0.7f)
            animator.SetBool("playAnimation", false);
    }


    void Attack()
    {
        if (animationCooldown > 0.35f && animationCooldown < 0.4f)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f, hitableMask))
            {
                 if (hit.collider.tag == "Bee")
                    hit.collider.transform.GetComponentInParent<Bee>().health -= damage;
            }

            attackedThisRound = true;
        }
    }
        
}
