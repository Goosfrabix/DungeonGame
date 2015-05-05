using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace DungeonGame
{
    class Map
    {
        int mapwidth = 100;
        int mapheight = 100;
        int tilewidth = 50;

        int[,] tilemap = new int[100, 100];
        int[,] bitwisemap =  new int [100,100];
        
        public Map()
        {
            tilemap = new int[mapwidth, mapheight];
            bitwisemap = new int[mapwidth, mapheight];
            ParseMapText("DungeonOne.txt");
            BitwiseMap();
            //PrintMap();
           // PrintBitwiseMap();

        }
        void ParseMapText(string txtfile)
        {
            string[] lines = System.IO.File.ReadAllLines("..\\..\\..\\Content\\maps\\" + txtfile);
            int y = 0;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                string[] splitline = line.Split(',');
                for (int i = 0; i < splitline.Length; i++)
                {
                    tilemap[i, y] = int.Parse(splitline[i]);
                }
                y++;
            }
        }
        void PrintMap()
        {
            for (int y = 0; y < mapheight; y++)
            {
                string line = "";
                for (int x = 0; x < mapwidth; x++)
                {
                    if (tilemap[x, y]!=null)
                        line += tilemap[x,y].ToString()+",";
                }
                Console.WriteLine(line);
            }
        }
        void PrintBitwiseMap()
        {
            for (int y = 0; y < mapheight; y++)
            {
                string line = "";
                for (int x = 0; x < mapwidth; x++)
                {
                    if (bitwisemap[x, y] != null)
                        line += bitwisemap[x, y].ToString() + ",";
                }
                Console.WriteLine(line);
            }
        }
        void BitwiseMap()
        {
            for (int y = 0; y < mapheight; y++)
            {
                for (int x = 0; x < mapwidth; x++)
                {
                    if (tilemap[x, y] != null)
                    {
                        bitwisemap[x, y] = GetBitValue(x, y);
                    }
                        
                }
            }
        }
        int GetBitValue(int x,int y)
        { 
            int value =0;
            int center = tilemap[x, y];
            int up = 0;
            int right = 0;
            int down = 0;
            int left = 0;
            if (y - 1 >= 0)
            {
                if (tilemap[x, y - 1] != null)
                    up = tilemap[x, y - 1];
            }
            if (x+1 < mapwidth)
            {
                if (tilemap[x + 1, y] != null)
                    right = tilemap[x + 1, y];
            }
            if (y + 1 < mapheight)
            {
                if (tilemap[x, y + 1] != null)
                    down = tilemap[x, y + 1];
            }
            if (x - 1 >= 0)
            {
                if (tilemap[x - 1, y] != null)
                    left = tilemap[x - 1, y];
            }

            value = 
                (GetLocationValue("Center") * GetTypeValue(center)
                + (GetLocationValue("Up") * GetTypeValue(up))
                + (GetLocationValue("Right") * GetTypeValue(right))
                + (GetLocationValue("Down") * GetTypeValue(down))
                + (GetLocationValue("Left") * GetTypeValue(left)));
            return value;
           
        }
        int GetLocationValue(string location)
        {
            int value = 0;
            switch (location)
            {
                case "Center": value = 81; break;
                case "Up": value = 1; break;
                case "Right": value = 3; break;
                case "Down": value = 9; break;
                case "Left": value = 27; break;
            }
            return value;
        }
        int GetTypeValue(int type)
        {
            int value = 0;
            switch (type)
            {
                case 0: value = 2; break;
                case 1: value = 0; break;
                case 2: value = 1; break;
            }
            return value;
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tilemap)
        {
            spriteBatch.Begin();
            for (int y = 0; y < mapheight; y++)
            {
                for (int x = 0; x < mapwidth; x++)
                {
                    int value = bitwisemap[x, y];
                    int _frameCol = value % (900 / tilewidth);
                    int _frameRow = (value - _frameCol) / (900 / tilewidth);
                    _frameCol *= tilewidth;
                    _frameRow *= tilewidth;
                    spriteBatch.Draw(tilemap, new Rectangle(x * (tilewidth), y * (tilewidth), tilewidth, tilewidth), new Rectangle(_frameCol, _frameRow, tilewidth, tilewidth), Color.White);
                }
            }
            spriteBatch.End();
        }
    }
}
