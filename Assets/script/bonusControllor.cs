using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bonusControllor : MonoBehaviour{
    public GameObject bonusPrefab;// �ĤH�w�s��
    public int bonusCount = 2;
    public Vector2 Min;   // �ͦ��d�򪺥��U��
    public Vector2 Max;   // �ͦ��d�򪺥k�W��
    public Transform player; // ���a��m
    public float minDistance = 3f; // �P���a���̤p�Z��

    public float spawnTime = 10f; // �ͦ����j�ɶ��]��^
    public float initialDelay = 5f; // ��l����ɶ��]��)


    void Start(){
        // �}�l�w���ͦ�
        InvokeRepeating("SpawnBonus", initialDelay, spawnTime);//(�ͦ��禡,��l����ɶ�,���j�ɶ�)
    }

    void SpawnBonus(){
        int spawnedBonus = 0; // �w���\�ͦ����ƶq

        while (spawnedBonus < bonusCount)
        {
            Vector2 spawnPosition;
            int attempts = 0;     // ���զ���
            int maxAttempts = 100;// �̤j���զ��ƭ��� �קK�j��

            do{
                // �H���ͦ���m
                spawnPosition = new Vector2(Random.Range(Min.x, Max.x),
                Random.Range(Min.y, Max.y));

                attempts++;
            }
            while (Vector2.Distance(spawnPosition, player.position) < minDistance && attempts < maxAttempts);

            if (attempts < maxAttempts){
                Instantiate(bonusPrefab, spawnPosition, Quaternion.identity);// �Ы�
                //(Prefab,�ͦ�����m,�ͦ����ਤ��"�L����")
                spawnedBonus++;
            }
            else{
                Debug.LogWarning("�L�k�b�w���Z���~�ͦ�");
                break;
            }
        }
    }
}
