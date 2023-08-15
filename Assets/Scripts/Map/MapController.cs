using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // ���ο��б�
    public List<GameObject> terrainChunks;
    // ���
    public GameObject player;
    // ����뾶
    public float checkerRadius;
    // �޵���λ��
    Vector3 noTerrainPosition;
    // �����ɰ�
    public LayerMask terrainMask;
    // ����ƶ�
    PlayerMovement pm;
    // ��ǰ�����ĵ��ο�
    public GameObject currentChunk;
    // Start is called before the first frame update

    [Header("Optimization")]
    // �Ѿ����ɵĵ��ο���б�
    public List<GameObject> spawnedChunks;
    // ���һ���ؿ�
    GameObject latestChunk;
    // ����Ż����룬������ڵؿ�ı߳�*����2*1.5����ֹ�����8���ؿ鱻���ã����������еĵؿ������ٴ������µĵؿ�
    public float maxOpDist;
    // �����ؿ�ľ���
    float opDist;
    // ����ʱ���Ż�������ȴʱ��
    float optimizerCooldown;
    public float optimizerCooldownDur;

    void Start()
    {
        // ����һ���Ѽ��ص�tpye���͵Ķ���
        pm = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }
    void ChunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }
        if (pm.moveDir.x > 0 && pm.moveDir.y == 0)  // ��������ƶ�
        {
            // ����Բ�������ڵ���ײ�壬���ĳ��ײ���Ƿ�λ��һ��Բ�������ڡ�
            // transform.Find �ڵ�ǰ���������Ŀ��Ѱ������ΪRight�ı任��ֻ�ܲ���1���㼶��
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0)  // ��������ƶ�
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0)  // ��������ƶ�
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0)  // ��������ƶ�
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0)  // ����������ƶ�
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0)  // ����������ƶ�
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0)  // ����������ƶ�
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0)  // ����������ƶ�
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                SpawnChunk();
            }
        }
    }
    void SpawnChunk()
    {
        int rand = Random.Range(0, terrainChunks.Count);
        // ʵ�������ο�
        latestChunk = Instantiate(terrainChunks[rand], noTerrainPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }
    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;
        if (optimizerCooldown < 0)
        {
            optimizerCooldown = optimizerCooldownDur;
        }
        else
        {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
