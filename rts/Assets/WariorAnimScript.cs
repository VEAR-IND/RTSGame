using UnityEngine;
using System.Collections;

public class WariorAnimScript : MonoBehaviour {

    Animator anim;
    GameObject player;
    CharacterController characterController;
    UnitPath unit;
    Vector3 vel;
    float speed;
    int persentOffMaxSpeed;
    int randNum;
    Vector3 lastPosition;
    void Start()
    {
        player = GameObject.Find("Player").gameObject;
        anim = GetComponent<Animator>();
        characterController = player.gameObject.GetComponent<CharacterController>();
        lastPosition = player.transform.position;
        unit = player.GetComponent<UnitPath>();
    }
    void FixedUpdate()
    {
        System.Random rnd = new System.Random();
        randNum = rnd.Next(1, 4);        
        vel = characterController.velocity;
        speed = Vector3.Distance(lastPosition, player.transform.position) / Time.deltaTime;
        anim.SetInteger("random", randNum);
        persentOffMaxSpeed = unit.persentOffMaxSpeed;
        anim.SetFloat("Speed", persentOffMaxSpeed);
        lastPosition = player.transform.position;

        

        //if (persentOffMaxSpeed == 0)//idle
        //{
        //    anim.speed = 1;
        //}
        //else if(persentOffMaxSpeed < 20)//walk
        //{
        //    anim.speed = (persentOffMaxSpeed * 5)/100;
        //}
        //else//run
        //{
        //    anim.speed = (persentOffMaxSpeed)/100;
        //}
        
    }
}
