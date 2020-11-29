using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ThirdPersonController_S : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;
    
    public float rotsenstitvity;
    float referenceVar;

    float walk = 0.0f;
    public float speed = 0.1f;

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0f, z).normalized;
        if (direction.magnitude >= 0.1f)
        {
            float rotAngle = Mathf.Atan2(x, z)*Mathf.Rad2Deg+cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotAngle, ref referenceVar, rotsenstitvity);
            this.transform.rotation = Quaternion.Euler(0f,angle,0f);
            if (walk > 1.0f)
            {
                walk=1.0f;
                anim.SetFloat("mov_speed", 1.0f);
            }else if(walk == 1.0f){

            }else{
                walk += speed;
                if (anim.GetBool("run") != true) anim.SetBool("run", true);
                anim.SetFloat("mov_speed", walk);
            }
            direction = Quaternion.Euler(0f, rotAngle, 0f) * Vector3.forward;
            controller.Move(direction * speed * Time.deltaTime*100);
        }
        else
        {
            if(anim.GetBool("run") == true)
            {
                walk -= 4 * speed;
                anim.SetFloat("mov_speed", walk);
                if (walk <= 0)
                {
                    walk = 0.0f;
                    anim.SetBool("run", false);
                }
            }
        }
    }
}
