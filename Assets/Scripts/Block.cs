using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int TileIndex;
    public int TileNumber = -1;
    public bool IsOpend = false;
    public Sprite NowImage;

    public Sprite tile0;
    public Sprite tile1;
    public Sprite tile2;
    public Sprite tile3;
    public Sprite tile4;
    public Sprite tile5;
    public Sprite tile6;
    public Sprite tile7;
    public Sprite tile8;
    public Sprite tile9;
    public Sprite tile10;

    public Sprite[] tiles;
    
    void Start()
    {
        tiles = new Sprite[] { tile0, tile1, tile2, tile3, tile4, tile5, tile6, tile7, tile8, tile9, tile10 };
    }
}
