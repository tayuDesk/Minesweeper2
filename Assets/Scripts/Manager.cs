using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public bool ClikedFIrst = false;

    public int WIDTH = 10;
    public int HEIGHT = 10;
    public int BOMB_NUM = 20;
    float interval = 4.0f;
    public int[] TilesNumber;
    // -1:初期 0~8:周りの個数 9:爆弾 10:フラッグ


    public GameObject BlockPrefab;
    

    

    int[] dx = { 1, 1, 1, -1, -1, -1, 0, 0 };
    int[] dy = { 1, -1, 0, 1, -1, 0, 1, -1 };

    bool failed = false;

    void Start()
    {
        Init_Stage();
        AssignIndexAndNameToBlock();
        
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            if (!ClikedFIrst && hit2d)
            {

                
               
                hit2d.transform.gameObject.GetComponent<Block>().TileNumber = 0;
                int idx = hit2d.transform.gameObject.GetComponent<Block>().TileIndex;
                
                TilesNumber[idx] = 0;


                AssignBomb(idx);


                AssignNum();


                AssignTileNumberToBlock();

                
                
                ClikedFIrst = true;

            }
            if (hit2d && !failed)
            {
                GameObject tblock = hit2d.transform.gameObject;
                var B_cs = tblock.GetComponent<Block>();
                tblock.GetComponent<SpriteRenderer>().sprite = B_cs.tiles[B_cs.TileNumber];
                if (B_cs.TileNumber == 9)
                {
                    failed = true;
                    GameObject.Find("ReStartButton").transform.position = new Vector3(245, 455, 0);
                }
            }
        }
    }

    void Init_Stage()
    {
        TilesNumber = new int[HEIGHT * WIDTH];
        for (int y = 0; y < HEIGHT; y++)
        {
            for (int x = 0; x < WIDTH; x++)
            {
                TilesNumber[y * WIDTH + x] = -1;

            }
        }

    }

    void AssignBomb(int idx)
    {
        List<int> tls = new List<int>();
        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            if (i == idx)
            {
                continue;
            }
            tls.Add(i);
        }
        for (int i = 0; i < BOMB_NUM; i++)
        {
            int tid = Random.Range(0, tls.Count);
            TilesNumber[tls[tid]] = 9;
            tls.RemoveAt(tid);
        }
    }

    void AssignNum()
    {
        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            if (TilesNumber[i] == 9)
            {
                continue;
            }

            int cntBomb = 0;
            for (int j = 0; j < 8; j++)
            {
                int nx = i % WIDTH + dx[j];
                int ny = i / WIDTH + dy[j];
                if (nx < 0 || nx >= WIDTH || ny < 0 || ny >= HEIGHT)
                {
                    continue;
                }

                if (TilesNumber[ny * WIDTH + nx] == 9)
                {
                    cntBomb++;
                }
            }
            TilesNumber[i] = cntBomb;
        }
    }

    void AssignIndexAndNameToBlock()
    {
        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            GameObject tblock = Instantiate(BlockPrefab) as GameObject;
            tblock.GetComponent<Block>().TileIndex = i;
            tblock.transform.position = new Vector3(i % WIDTH * interval, i / WIDTH * interval, 0);

            tblock.name = "Block" + i.ToString();
        }
    }

    void AssignTileNumberToBlock()
    {
        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            GameObject tblock = GameObject.Find("Block" + i.ToString());
            tblock.GetComponent<Block>().TileNumber = TilesNumber[i];
        }
    }

    
    
    
}
