using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{

    public bool ClikedFIrst = false;
    public bool GameIsOver = false;

    public int WIDTH = 10;
    public int HEIGHT = 10;
    public int BOMB_NUM = 20;
    float interval = 4.0f;
    public int[] TilesNumber;
    // -1:初期 0~8:周りの個数 9:爆弾 10:フラッグ


    public GameObject BlockPrefab;


    List<int> around = new List<int>();
    bool[] visited;
    public int CountOpendBlockNum = 0;

    int[] dx = { 1, 1, 1, -1, -1, -1, 0, 0 };
    int[] dy = { 1, -1, 0, 1, -1, 0, 1, -1 };

    bool failed = false;

    void Start()
    {
        visited = new bool[HEIGHT * WIDTH];
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
            if (hit2d && !failed && hit2d.transform.gameObject.GetComponent<Block>().TileNumber != 10)
            {
                GameObject tblock = hit2d.transform.gameObject;
                var B_cs = tblock.GetComponent<Block>();
                



                if (0 < B_cs.TileNumber && B_cs.TileNumber < 9 && !B_cs.IsOpend)
                {
                    Open(tblock);
                }



                if (B_cs.TileNumber == 9)
                {
                    failed = true;
                    tblock.GetComponent<SpriteRenderer>().sprite = B_cs.tiles[B_cs.TileNumber];
                    GameObject.Find("ReStartButton").transform.position = new Vector3(1000, 455, 0);
                    GameObject.Find("ReturnButton").transform.position = new Vector3(1000, 600, 0);

                    GameIsOver = true;

                }
                if (B_cs.TileNumber == 0 && !B_cs.IsOpend)
                {
                    int idx = hit2d.transform.gameObject.GetComponent<Block>().TileIndex;
                    Open(tblock);
                    OpenAround(idx);
                }
            }

            if (CountOpendBlockNum == WIDTH * HEIGHT - BOMB_NUM)
            {
                GameClear();

                GameIsOver = true;
            }

            /*
            if (CountOpenedBlock() == WIDTH * HEIGHT - BOMB_NUM)
            {
                GameClear();
            }
            */
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit2d = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction);

            var B_cs = hit2d.transform.gameObject.GetComponent<Block>();

            if (B_cs.TileNumber < 100)
            {
                RaiseFlag(hit2d.transform.gameObject);
            }
            else
            {
                if (!B_cs.IsOpend)
                {
                    hit2d.transform.gameObject.GetComponent<SpriteRenderer>().sprite = B_cs.NowImage;
                    B_cs.TileNumber -= 100;
                }
                else
                {
                    B_cs.TileNumber -= 100;
                    hit2d.transform.gameObject.GetComponent<SpriteRenderer>().sprite = B_cs.tiles[B_cs.TileNumber];
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
            if (i == idx || AroundIdx(idx, i))
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

    bool AroundIdx(int idx, int i)
    {
        List<int> koho = new List<int>();
        for (int j = 0; j < 8; j++)
        {
            int nx = idx % WIDTH + dx[j];
            int ny = idx / WIDTH + dy[j];
            if (0 <= nx && nx < WIDTH && 0 <= ny && ny < HEIGHT)
            {
                koho.Add(ny * WIDTH + nx);
            }
        }
        for (int j = 0; j < koho.Count; j++)
        {
            if (koho[j] == i)
            {
                return true;
            }
        }
        return false;
    }
    
    void OpenAround(int idx)
    {
        GameObject.Find("Block" + idx.ToString()).GetComponent<Block>().IsOpend = true;
        for (int i = 0; i < 8; i++)
        {

            int nx = idx % WIDTH + dx[i];
            int ny = idx / WIDTH + dy[i];
            if (nx < 0 || WIDTH <= nx || ny < 0 || HEIGHT <= ny)
            {
                continue;
            }
            GameObject tblock = GameObject.Find("Block" + (ny * WIDTH + nx).ToString());
            var B_cs = tblock.GetComponent<Block>();


            if (B_cs.IsOpend)
            {
                continue;
            }

            Open(tblock);


            if (B_cs.TileNumber == 0)
            {
                
                OpenAround(ny * WIDTH + nx);
            }
           
        }
    }

    void Open(GameObject tblock)
    {
        var B_cs = tblock.GetComponent<Block>();
        tblock.GetComponent<SpriteRenderer>().sprite = B_cs.tiles[B_cs.TileNumber];
        B_cs.IsOpend = true;
        CountOpendBlockNum++;
    }

    int CountOpenedBlock()
    {
        int ret = 0;
        for (int i = 0; i < WIDTH * HEIGHT; i++)
        {
            if (GameObject.Find("Block" + i.ToString()).GetComponent<Block>().IsOpend)
            {
                ret++;
            }
        }
        return ret;
    }

    void GameClear()
    {
        GameObject.Find("ReStartButton").transform.position = new Vector3(500, 455, 0);
        GameObject.Find("GameClearText").transform.position = new Vector3(600, 300, 0);
        GameObject.Find("ReturnButton").transform.position = new Vector3(500, 455, 0);
    }

    void RaiseFlag(GameObject tblock)
    {
        var B_cs = tblock.GetComponent<Block>();
        B_cs.TileNumber += 100;
        tblock.GetComponent<SpriteRenderer>().sprite = B_cs.tiles[10];
    }
}
