using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public float moveSpeed = 5f;    //���ʳt��

    private Vector2 minBoundary;    // ���ʽd�򪺳̤p�� (���U��)
    private Vector2 maxBoundary;    // ���ʽd�򪺳̤j�� (�k�W��)

    private Rigidbody2D rb;
    private Vector2 movement;
    
    void Start(){

        rb = GetComponent<Rigidbody2D>();

        //��wZ�b����
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        //��l�Ʋ��ʽd��
        UpdateBoundaries();
    }

    
    void Update(){

        //��L
         movement.x = Input.GetAxis("Horizontal");
         movement.y = Input.GetAxis("Vertical");

        //��s���
        UpdateBoundaries();
    }

    void FixedUpdate(){

        //�p�Ⲿ�ʫ᪺��m
        Vector2 newPosition = rb.position +  movement* moveSpeed * Time.deltaTime;

        //����s��m�b��ɽd�� (value, min, max)
        newPosition.x = Mathf.Clamp(newPosition.x, minBoundary.x, maxBoundary.x);
        newPosition.y = Mathf.Clamp(newPosition.y, minBoundary.y, maxBoundary.y);

        //���ʨ���
        rb.MovePosition (newPosition);
    }

    void UpdateBoundaries(){

        //�����v�������
        Camera cam = Camera.main;

        if (cam != null){

            //��v�������� / �e��
            float camHeight = 2f * cam.orthographicSize;
            float camWidth = camHeight * cam.aspect;

            //�]�m��� (��v��������)
            minBoundary = new Vector2((-camWidth / 2) + 0.37f, (-camHeight / 2) + 0.45f);
            maxBoundary = new Vector2((camWidth / 2) - 0.38f, (camHeight / 2) - 2.1f);
        }
    }

    //��s��� (�����j�p����)
    void OnRectTransformDimensionsChange(){ //unity�۱a
        
        UpdateBoundaries();
    }
}
