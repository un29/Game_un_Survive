using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMove : MonoBehaviour
{
    public float moveSpeed = 3f; // �ĤH���ʳt��
    public float stoppingDistance = 0.5f; // ����l�ܪ��Z��
    private Transform player; // ���a���� Transform

    void Start(){
        // �d�䪱�a���������� Transform
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update(){
        if (Time.timeScale != 0)
        {
            // �ˬd�ĤH�P���a���Z��.�O�_�ݭn�~�򲾰�
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                // �p���V�V�q
                Vector2 direction = (player.position - transform.position).normalized;

                // ���ʼĤH�µ۪��a
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.unscaledDeltaTime);// �ϥ� unscaledDeltaTime �ө����ɶ��Y��

            }
        }
    }

}
