using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomizer : MonoBehaviour
{
    // ��Ʒ���ɵ�
    public List<GameObject> proSpawnPoints;
    // ��ƷԤ����
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

    // ������Ʒ
    void SpawnProps()
    {
        foreach (GameObject sp in proSpawnPoints)
        {
            int rand = Random.Range(0, probPrefabs.Count);
            // ʵ������Ʒ���󣬲�������ת
            GameObject prop = Instantiate(probPrefabs[rand], sp.transform.position, Quaternion.identity);
            // �����ɵ���Ϊ���任
            prop.transform.parent = sp.transform;
        }
    }
}
