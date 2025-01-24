using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public float moveSpeed = 5f; //���ʳt��

    private Vector2 minBoundary; // ���ʽd�򪺳̤p�� (���U��)
    private Vector2 maxBoundary; // ���ʽd�򪺳̤j�� (�k�W��)

    private Rigidbody2D rb;
    private Vector2 movement;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;// ��w Z �b����

        // ��l�Ʋ��ʽd��
        UpdateBoundaries();
    }

    
    void Update(){
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

        // ��s���
        UpdateBoundaries();
    }

    void FixedUpdate(){
        // �p�Ⲿ�ʫ᪺��m
        Vector2 newPosition = rb.position +  movement* moveSpeed * Time.deltaTime;

        // ����s��m�b��ɽd��
        newPosition.x = Mathf.Clamp(newPosition.x, minBoundary.x, maxBoundary.x); //(value, min, max)
        newPosition.y = Mathf.Clamp(newPosition.y, minBoundary.y, maxBoundary.y);

        // ���ʨ���
        rb.MovePosition (newPosition);
    }

    void UpdateBoundaries()
    {
        // �����v�������
        Camera cam = Camera.main;
        if (cam != null)
        {
            float camHeight = 2f * cam.orthographicSize; // ��v��������
            float camWidth = camHeight * cam.aspect; // ��v�����e��

            // �]�m���
            minBoundary = new Vector2((-camWidth / 2) + 0.37f, (-camHeight / 2) + 0.45f); // ��v��������
            maxBoundary = new Vector2((camWidth / 2) - 0.38f, (camHeight / 2) - 2.1f);
        }
    }

    void OnRectTransformDimensionsChange(){ //unity�۱a
        // ��s��� (�����j�p����)
        UpdateBoundaries();
    }
}
