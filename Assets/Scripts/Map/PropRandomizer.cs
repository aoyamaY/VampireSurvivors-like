using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    // 物品生成点
    public List<GameObject> proSpawnPoints;
    // 物品预制体
    public List<GameObject> probPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        SpawnProps();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 生成物品
    void SpawnProps()
    {
        foreach (GameObject sp in proSpawnPoints)
        {
            int rand = Random.Range(0, probPrefabs.Count);
            // 实例化物品对象，不进行旋转
            GameObject prop = Instantiate(probPrefabs[rand], sp.transform.position, Quaternion.identity);
            // 将生成点作为父变换
            prop.transform.parent = sp.transform;
        }
    }
}
