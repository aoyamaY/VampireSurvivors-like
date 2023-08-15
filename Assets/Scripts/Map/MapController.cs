using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // 地形快列表
    public List<GameObject> terrainChunks;
    // 玩家
    public GameObject player;
    // 方格半径
    public float checkerRadius;
    // 无地形位置
    Vector3 noTerrainPosition;
    // 地形蒙版
    public LayerMask terrainMask;
    // 玩家移动
    PlayerMovement pm;
    // 当前所处的地形块
    public GameObject currentChunk;
    // Start is called before the first frame update

    [Header("Optimization")]
    // 已经生成的地形块的列表
    public List<GameObject> spawnedChunks;
    // 最后一个地块
    GameObject latestChunk;
    // 最大优化距离，必须大于地块的边长*根号2*1.5，防止最近的8个地块被禁用，导致在现有的地块上面再次生成新的地块
    public float maxOpDist;
    // 玩家与地块的距离
    float opDist;
    // 倒计时和优化器的冷却时间
    float optimizerCooldown;
    public float optimizerCooldownDur;

    void Start()
    {
        // 返回一个已加载的tpye类型的对象
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
        if (pm.moveDir.x > 0 && pm.moveDir.y == 0)  // 玩家向右移动
        {
            // 返回圆形区域内的碰撞体，检查某碰撞体是否位于一个圆形区域内。
            // transform.Find 在当前对象的子项目中寻找名字为Right的变换（只能查找1个层级）
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y == 0)  // 玩家向左移动
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y > 0)  // 玩家向上移动
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x == 0 && pm.moveDir.y < 0)  // 玩家向下移动
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y > 0)  // 玩家向右上移动
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x > 0 && pm.moveDir.y < 0)  // 玩家向右下移动
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y > 0)  // 玩家向左上移动
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                SpawnChunk();
            }
        }
        else if (pm.moveDir.x < 0 && pm.moveDir.y < 0)  // 玩家向左下移动
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
        // 实例化地形块
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
