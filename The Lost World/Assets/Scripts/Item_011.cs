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
    void Start()
    {
        
    }

    
    void Update()
    {
        AnimationTrigger();

        if(animationCooldown > 0.28f && animationCooldown < 0.35f)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5f, hitableMask))
            {
                //if(hit.collider.tag == "Bee")
                  //  hit.collider.GetComponent<>
            }
        }
    }


    void AnimationTrigger()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && animator.GetBool("playAnimation") == false)
        {
            animator.SetBool("playAnimation", true);
            animationCooldown = 0;
        }

        animationCooldown += Time.deltaTime;

        if (animator.GetBool("playAnimation") == true && animationCooldown >= 0.7f)
            animator.SetBool("playAnimation", false);
    }
        
}
