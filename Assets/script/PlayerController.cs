using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    public float moveSpeed = 5f; //移動速度

    private Vector2 minBoundary; // 移動範圍的最小值 (左下角)
    private Vector2 maxBoundary; // 移動範圍的最大值 (右上角)

    private Rigidbody2D rb;
    private Vector2 movement;
    
    void Start(){
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;// 鎖定 Z 軸旋轉

        // 初始化移動範圍
        UpdateBoundaries();
    }

    
    void Update(){
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

        // 更新邊界
        UpdateBoundaries();
    }

    void FixedUpdate(){
        // 計算移動後的位置
        Vector2 newPosition = rb.position +  movement* moveSpeed * Time.deltaTime;

        // 限制新位置在邊界範圍內
        newPosition.x = Mathf.Clamp(newPosition.x, minBoundary.x, maxBoundary.x); //(value, min, max)
        newPosition.y = Mathf.Clamp(newPosition.y, minBoundary.y, maxBoundary.y);

        // 移動角色
        rb.MovePosition (newPosition);
    }

    void UpdateBoundaries()
    {
        // 獲取攝影機的邊界
        Camera cam = Camera.main;
        if (cam != null)
        {
            float camHeight = 2f * cam.orthographicSize; // 攝影機的高度
            float camWidth = camHeight * cam.aspect; // 攝影機的寬度

            // 設置邊界
            minBoundary = new Vector2((-camWidth / 2) + 0.37f, (-camHeight / 2) + 0.45f); // 攝影機為中心
            maxBoundary = new Vector2((camWidth / 2) - 0.38f, (camHeight / 2) - 2.1f);
        }
    }

    void OnRectTransformDimensionsChange(){ //unity自帶
        // 更新邊界 (視窗大小改變)
        UpdateBoundaries();
    }
}
