using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DungeonGame
{
    class Map
    {
        int[,] tilemap = new int[100, 100];
        int[,] wallbitwise =  new int [100,100];
        public Map()
        {
            ParseMapText("DungeonOne.txt");
        }
        void ParseMapText(string txtfile)
        {
            string[] lines = System.IO.File.ReadAllLines("DungeonGame/Content/maps/" + txtfile);
            int y = 0;
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                Console.WriteLine("\t" + line);
                string[] splitline = line.Split(',');
                for (int i = 0; i < 100;i++ )
                {
                    tilemap[i, y] = int.Parse(splitline[i]);
                }
                y++;
            }
        }
    }
}
