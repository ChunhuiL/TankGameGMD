using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public GameObject[] item;
    private List<Vector3> itemPositionList = new List<Vector3>();
    private void Awake()
    {
        InitMap();
       
    }
    private void InitMap()
        {
            CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
            CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
            CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
            for (int i = -1; i < 2; i++)
            {
                CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
            }
            for (int i = -11; i < 12; i++)
            {
                CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
                CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
            }
            for (int i = -8; i < 9; i++)
            {
                CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
                CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
            }
            //map
            for (int i = 0; i < 45; i++)
            {
                CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
                CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
                CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
                CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
            }
            //player
            GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            //Enemy
            CreateItem(item[3], new Vector3(-10, -8, 0), Quaternion.identity);
            CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
            CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);

            InvokeRepeating("CreateEnemy", 4, 5);
        }
    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                  return createPosition;
            }
        }
    }

    private bool HasThePosition(Vector3 createPos)
    {
        for(int i = 0;i < itemPositionList.Count; i++){
            if(createPos==itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }

    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 EnemyPos = new Vector3();
        if(num==0)
        {
            EnemyPos = new Vector3(-10, 8, 0);
        }
        else if (num == 1)
        {
            EnemyPos = new Vector3(0, 8, 0);
        }
        else
        {
            EnemyPos = new Vector3(10, 8, 0);
        }
        CreateItem(item[3], EnemyPos, Quaternion.identity);
    }
}
