using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossenemyMove : MonoBehaviour{

    public float moveSpeed = 5f;            //�ĤH���ʳt��
    public float stoppingDistance = 0.5f;   //����l�ܪ��Z��

    private Transform player;               //���aTransform

    void Start(){

        //�d�䪱�a���Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        if (Time.timeScale != 0){
             
            //�ˬd�ĤH�P���a���Z�� �O�_�ݭn�~�򲾰�
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance){
                //�p���V�V�q
                Vector2 direction = (player.position - transform.position).normalized;

                //���ʼĤH�µ۪��a (unscaledDeltaTime�����ɶ��Y��)
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.unscaledDeltaTime);
            }
        }
    }
}
