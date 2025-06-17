using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EH_Pipe_Puzzle
{

    internal class Program
    {
        const string QuestFile = "EH Pipe Puzzle Quest.json";
        static void Main(string[] args)
        {
            const int rows = 4;
            const int columns = 3;
            int startingNode = 11;
            //int rotationsEncountered = 0;

            // 3x3 , 298 nodes

            //string[,] solutionGrid = new string[rows, columns]
            //{
            //    { "█", "░", "░" },
            //    { "╚", "═", "╗" },
            //    { "░", "░", "█" }
            //};

            //string[,] scrambledSample = new string[rows, columns]
            //{
            //    { "█", "░", "░" },
            //    { "╚", "═", "╗" },
            //    { "░", "░", "█" }
            //};

            // 4x3 , 12298 nodes

            //string[,] solutionGrid = new string[rows, columns]
            //{
            //    { "█", "░", "░", "█" },
            //    { "║", "░", "╔", "╣" },
            //    { "╚", "═", "╝", "█" }
            //};

            //string[,] scrambledSample = new string[rows, columns]
            //{
            //    { "█", "░", "░", "█" },
            //    { "═", "░", "╔", "╦" },
            //    { "╚", "═", "╚", "█" }
            //};

            // 4x3 , 778 nodes

            //string[,] solutionGrid = new string[rows, columns]
            //{
            //    { "█", "░", "░", "█" },
            //    { "╚", "═", "═", "╣" },
            //    { "░", "░", "░", "█" }
            //};

            //string[,] scrambledSample = new string[rows, columns]
            //{
            //    { "█", "░", "░", "█" },
            //    { "╚", "═", "═", "╣" },
            //    { "░", "░", "░", "█" }
            //};

            // 3x4 , 6154 nodes

            //string[,] solutionGrid = new string[rows, columns]
            //{
            //    { "░", "░", "█" },
            //    { "╔", "═", "╝" },
            //    { "║", "░", "░" },
            //    { "╚", "═", "█" }
            //};

            //string[,] scrambledSample = new string[rows, columns]
            //{
            //    { "░", "░", "█" },
            //    { "╔", "═", "╝" },
            //    { "║", "░", "░" },
            //    { "╚", "═", "█" }
            //};

            // 3x4 , 3082 nodes

            string[,] solutionGrid = new string[rows, columns]
            {
                { "░", "░", "█" },
                { "╔", "═", "╝" },
                { "║", "░", "░" },
                { "╚", "█", "░" }
            };

            string[,] scrambledSample = new string[rows, columns]
            {
                { "░", "░", "█" },
                { "╔", "═", "╝" },
                { "║", "░", "░" },
                { "╚", "█", "░" }
            };




            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Console.Write(scrambledSample[i, j]);
                }
                Console.Write("\n");
            }

            int[,] emptyGrid = new int[rows, columns];
            int[,] rotatables = new int[rows, columns];
            rotatables = FindRotatables(scrambledSample, rows, columns);
            int sumRotations = 0;

            // temp code, sums total rotations
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    emptyGrid[i, j] = 0;
                    Console.Write(emptyGrid[i, j]);
                    sumRotations += rotatables[i, j];
                }
                Console.Write("\n");
            }

            string[] pipeCorner = { "╔", "╗", "╝", "╚" };
            string[] pipeStraight = { "║", "═" };
            string[] pipeCross = { "╣", "╩", "╠", "╦" };

            //string nodeTest;


            //for (int i = 0; i < 12; i++)
            //{
            //    nodeTest = PrintNode(scrambledSample, rows, columns, startingNode + i,
            //        ref rotationsEncountered);
            //    Console.WriteLine(nodeTest);
            //}

            //NodeLoop(scrambledSample, rows, columns, startingNode, ref rotationsEncountered,
            //    rotatables, emptyGrid);
            //ModifyGrid(ref scrambledSample, rows, columns, rotatables, ref emptyGrid);
            //rotationsEncountered--;
            //NodeLoop(scrambledSample, rows, columns, startingNode + 12, ref rotationsEncountered,
            //    rotatables, emptyGrid);

            GenerateQuest(QuestFile, solutionGrid, rows, columns, startingNode,
                rotatables, ref emptyGrid);

            //Console.WriteLine(rotationsEncountered);
            Console.WriteLine(sumRotations);
        }

        static int[,] FindRotatables(string[,] grid, int rows, int cols)
        {
            int[,] rotatables;
            rotatables = new int[rows, cols];

            string[] pipeKnob = { "█", "░" };
            string[] pipeCorner = { "╔", "╗", "╝", "╚" };
            string[] pipeStraight = { "║", "═" };
            string[] pipeCross = { "╣", "╩", "╠", "╦" };

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    string tile = grid[i, j];
                    if (pipeKnob.Contains(tile))
                    {
                        rotatables[i, j] = 0;
                    }
                    else if (pipeCorner.Contains(tile))
                    {
                        rotatables[i, j] = 3;
                    }
                    else if (pipeStraight.Contains(tile))
                    {
                        rotatables[i, j] = 1;
                    }
                    else if (pipeCross.Contains(tile))
                    {
                        rotatables[i, j] = 3;
                    }
                }
            }
            return rotatables;
        }

        static int SumRotatables(int[,] rotatables, int rows, int cols)
        {
            int sumRotations = 1;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (rotatables[i, j] != 0)
                    {
                        sumRotations *= (rotatables[i, j] + 1);
                    }
                }
            }
            return sumRotations;
        }

        static void GenerateQuest(string questFile, string[,] grid,
            int rows, int cols, int startingNode,
            int[,] rotatables, ref int[,] emptyGrid)
        {
            string nodes = "";

            string questFileTop =
            "{\r\n" +
            "  \"ItemType\": 15,\r\n" +
            "  \"Id\": 10000,\r\n" +
            "  \"Name\": \"EH Pipe Puzzle\",\r\n" +
            "  \"StartCondition\": 6,\r\n" +
            "  \"Weight\": 0.5,\r\n" +
            "  \"Nodes\": [\r\n";

            string questFileBottom =
                "  ]\r\n" +
                "}";

            using (var gen = new StreamWriter(questFile))
            {
                gen.Write(questFileTop);

                nodes = NodeLoopLoop(ref grid, rows, cols, startingNode,
                rotatables, ref emptyGrid);

                gen.Write(nodes);

                gen.Write(questFileBottom);
            }
        }
        
        /// <summary>
        /// 
        /// This is a cool thing that increments stuff
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="rotatables"></param>
        /// <param name="emptyGrid"></param>
        static void ModifyGrid(ref string[,] grid, int rows, int cols,
            int[,] rotatables, ref int[,] emptyGrid)
        {
            string[] pipeKnob = { "█", "░" };
            string[] pipeCorner = { "╔", "╗", "╝", "╚" };
            string[] pipeStraight = { "║", "═" };
            string[] pipeCross = { "╣", "╩", "╠", "╦" };

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (pipeKnob.Contains(grid[i, j]))
                    {
                        continue;
                    }
                    else if (pipeStraight.Contains(grid[i, j]))
                    {
                        if (grid[i, j] == "═")
                        {
                            if (emptyGrid[i, j] == 0)
                            {
                                emptyGrid[i, j] = 1;
                                grid[i, j] = "║";
                                return;
                            }
                            else if (emptyGrid[i, j] == 1)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "║";
                                continue;
                            }
                        }

                        if (grid[i, j] == "║")
                        {
                            if (emptyGrid[i, j] == 0)
                            {
                                emptyGrid[i, j] = 1;
                                grid[i, j] = "═";
                                return;
                            }
                            else if (emptyGrid[i, j] == 1)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "═";
                                continue;
                            }
                        }

                    }

                    else if (pipeCorner.Contains(grid[i, j]))
                    {
                        if (grid[i, j] == "╔")
                        {
                            if (emptyGrid[i, j] != 3)
                            {
                                emptyGrid[i, j] += 1;
                                grid[i, j] = "╗";
                                return;
                            }
                            else if (emptyGrid[i, j] == 3)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "╗";
                                continue;
                            }
                        }

                        if (grid[i, j] == "╗")
                        {
                            if (emptyGrid[i, j] != 3)
                            {
                                emptyGrid[i, j] += 1;
                                grid[i, j] = "╝";
                                return;
                            }
                            else if (emptyGrid[i, j] == 3)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "╝";
                                continue;
                            }
                        }

                        if (grid[i, j] == "╝")
                        {
                            if (emptyGrid[i, j] != 3)
                            {
                                emptyGrid[i, j] += 1;
                                grid[i, j] = "╚";
                                return;
                            }
                            else if (emptyGrid[i, j] == 3)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "╚";
                                continue;
                            }
                        }

                        if (grid[i, j] == "╚")
                        {
                            if (emptyGrid[i, j] != 3)
                            {
                                emptyGrid[i, j] += 1;
                                grid[i, j] = "╔";
                                return;
                            }
                            else if (emptyGrid[i, j] == 3)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "╔";
                                continue;
                            }
                        }
                    }

                    else if (pipeCross.Contains(grid[i, j]))
                    {
                        if (grid[i, j] == "╣")
                        {
                            if (emptyGrid[i, j] != 3)
                            {
                                emptyGrid[i, j] += 1;
                                grid[i, j] = "╩";
                                return;
                            }
                            else if (emptyGrid[i, j] == 3)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "╩";
                                continue;
                            }
                        }

                        if (grid[i, j] == "╩")
                        {
                            if (emptyGrid[i, j] != 3)
                            {
                                emptyGrid[i, j] += 1;
                                grid[i, j] = "╠";
                                return;
                            }
                            else if (emptyGrid[i, j] == 3)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "╠";
                                continue;
                            }
                        }

                        if (grid[i, j] == "╠")
                        {
                            if (emptyGrid[i, j] != 3)
                            {
                                emptyGrid[i, j] += 1;
                                grid[i, j] = "╦";
                                return;
                            }
                            else if (emptyGrid[i, j] == 3)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "╦";
                                continue;
                            }
                        }

                        if (grid[i, j] == "╦")
                        {
                            if (emptyGrid[i, j] != 3)
                            {
                                emptyGrid[i, j] = 1;
                                grid[i, j] = "╣";
                                return;
                            }
                            else if (emptyGrid[i, j] == 3)
                            {
                                emptyGrid[i, j] = 0;
                                grid[i, j] = "╣";
                                continue;
                            }
                        }
                    }

                }
            }
        }

        static string NodeLoopLoop(ref string[,] grid, int rows, int cols,
            int currentNode,
            int[,] rotatables, ref int[,] emptyGrid)
        {
            int totalLoop;
            totalLoop = rows * cols;
            int totalLoopLoop;
            totalLoopLoop = SumRotatables(rotatables, rows, cols);
            string nodes = "";

            for (int i = 0; i < totalLoopLoop; i++)
            {
                nodes += NodeLoop(ref grid, rows, cols, currentNode + (totalLoop * i),
                    rotatables, emptyGrid);
                ModifyGrid(ref grid, rows, cols, rotatables, ref emptyGrid);
                
            }
            
            return nodes;
        }

        static string NodeLoop(ref string[,] grid, int rows, int cols,
            int currentNode,
            int[,] rotatables, int[,] emptyGrid)
        {
            int rotationsLocal = 1;
            int totalLoop;
            totalLoop = rows * cols;
            string node = "";

            for (int i = 0; i < totalLoop; i++)
            {

                node += PrintNode(grid, rows, cols, currentNode + i,
                    ref rotationsLocal, rotatables, emptyGrid);
                Console.WriteLine(node);
            }
            return node;
        }

        static string PrintNode(string[,] grid, int rows, int cols,
            int currentNode, ref int rotationsLocal,
            int[,] rotatables, int[,] emptyGrid)
        {
            int selectedRow = -1;
            int selectedColumn= -1;

            int nodeUp;
            int nodeLeft;
            int nodeRight;
            int nodeDown;
            int nodeRotate;

            string generatedNode = "";

            generatedNode += string.Format("    {{\r\n");
            generatedNode += string.Format("      \"Id\": {0},\r\n", currentNode);
            generatedNode += string.Format("      \"Type\": 10,\r\n");
            generatedNode += string.Format("      \"Message\": \"");

            generatedNode += string.Format("░▒▓██████████▓▒░\\n");

            FindSelectedTile(currentNode, rows, cols, ref selectedRow, ref selectedColumn);



            // ░ ▒ ▓ █ | │ ┃ █ ▓ ▒ ░

            for (int i = 0; i < rows; i++)
            {
                generatedNode += string.Format("░▒▓█ ");

                for (int j = 0; j < cols; j++)
                {
                    if (((i + 1) == selectedRow) && (j + 1) == selectedColumn)
                    {
                        generatedNode += string.Format(
                            "<color=#00FF00>{0}]</color>", grid[i, j]);
                    }
                    else
                    {
                        generatedNode += string.Format(
                            "{0} ", grid[i, j]);
                    }
                }

                generatedNode += string.Format("█▓▒░");
                generatedNode += string.Format("\\n");
            }
            generatedNode += string.Format("░▒▓██████████▓▒░");
            generatedNode += string.Format("\",\r\n");

            generatedNode += string.Format("      \"Actions\": [\r\n");
            generatedNode += string.Format("        {{\r\n");

            nodeUp = ActionUp(currentNode, rows, cols, selectedRow, selectedColumn);
            nodeLeft = ActionLeft(currentNode, rows, cols, selectedRow, selectedColumn);
            nodeRight = ActionRight(currentNode, rows, cols, selectedRow, selectedColumn);
            nodeDown = ActionDown(currentNode, rows, cols, selectedRow, selectedColumn);
            nodeRotate = ActionRotate(currentNode, rows, cols, selectedRow, selectedColumn,
            grid, ref rotationsLocal, rotatables, emptyGrid);

            generatedNode += string.Format("          \"TargetNode\": {0},\r\n", nodeUp);
            generatedNode += string.Format("          \"ButtonText\": \"Up\"\r\n");
            generatedNode += string.Format("        }},\r\n");
            generatedNode += string.Format("        {{\r\n");
            generatedNode += string.Format("          \"TargetNode\": {0},\r\n", nodeLeft);
            generatedNode += string.Format("          \"ButtonText\": \"Left\"\r\n");
            generatedNode += string.Format("        }},\r\n");
            generatedNode += string.Format("        {{\r\n");
            generatedNode += string.Format("          \"TargetNode\": {0},\r\n", nodeRight);
            generatedNode += string.Format("          \"ButtonText\": \"Right\"\r\n");
            generatedNode += string.Format("        }},\r\n");
            generatedNode += string.Format("        {{\r\n");
            generatedNode += string.Format("          \"TargetNode\": {0},\r\n", nodeDown);
            generatedNode += string.Format("          \"ButtonText\": \"Down\"\r\n");
            generatedNode += string.Format("        }},\r\n");

            generatedNode += string.Format("        {{\r\n");
            generatedNode += string.Format("          \"TargetNode\": {0},\r\n", nodeRotate);
            generatedNode += string.Format("          \"ButtonText\": \"Rotate\"\r\n");
            generatedNode += string.Format("        }},\r\n");

            generatedNode += string.Format("        {{\r\n");
            generatedNode += string.Format("          \"TargetNode\": {0},\r\n", 9);
            generatedNode += string.Format("          \"ButtonText\": \"Exit\"\r\n");
            generatedNode += string.Format("        }}\r\n");
            generatedNode += string.Format("      ]\r\n");
            generatedNode += string.Format("    }},\r\n");

            return generatedNode;
        }

        static void FindSelectedTile(int currentNode, int rows, int cols,
            ref int selectedRow, ref int selectedCol) 
        {
            selectedRow = 1;
            selectedCol = 1;
            int rawTileValue;

            rawTileValue = (currentNode - 10) % (rows * cols);
            if (rawTileValue == 0)
            {
                rawTileValue = rows * cols;
            }

            while (rawTileValue >= cols)
            {
                if (rawTileValue == cols)
                {
                    break;
                }
                rawTileValue -= cols;
                selectedRow++;
            }

            selectedCol = rawTileValue;
        }

        static int ActionUp(int currentNode, int rows, int cols,
            int selectedRow, int selectedCol)
        {
            int nodeUp;

            if (selectedRow != 1)
            {
                nodeUp = currentNode - cols;
            }
            else
            {
                nodeUp = currentNode + (cols * (rows - 1));
            }
            return nodeUp;
        }

        static int ActionLeft(int currentNode, int rows, int cols,
            int selectedRow, int selectedCol)
        {
            int nodeLeft;

            if (selectedCol != 1)
            {
                nodeLeft = currentNode - 1;
            }
            else
            {
                nodeLeft = currentNode + (cols - 1);
            }
            return nodeLeft;
        }

        static int ActionRight(int currentNode, int rows, int cols,
            int selectedRow, int selectedCol)
        {
            int nodeRight;

            if (selectedCol != cols)
            {
                nodeRight = currentNode + 1;
            }
            else
            {
                nodeRight = currentNode - (cols - 1);
            }
            return nodeRight;
        }

        static int ActionDown(int currentNode, int rows, int cols,
            int selectedRow, int selectedCol)
        {
            int nodeDown;

            if (selectedRow != rows)
            {
                nodeDown = currentNode + cols;
            }
            else
            {
                nodeDown = currentNode - (cols * (rows - 1));
            }
            return nodeDown;
        }

        static int ActionRotate(int currentNode, int rows, int cols,
            int selectedRow, int selectedCol, string[,] grid,
            ref int rotationsLocal, int[,] rotatables, int[,] emptyGrid)
        {
            int nodeRotate = currentNode;
            string[] pipeCorner = { "╔", "╗", "╝", "╚" };
            string[] pipeStraight = { "║", "═" };
            string[] pipeCross = { "╣", "╩", "╠", "╦" };
            string tile = grid[selectedRow - 1, selectedCol - 1];
            int emptyTile = emptyGrid[selectedRow - 1, selectedCol - 1];
            int rotTile = rotatables[selectedRow - 1, selectedCol - 1];

            if (pipeStraight.Contains(tile))
            {
                if (emptyTile == 0)
                {
                    nodeRotate = currentNode + (rotationsLocal * rows * cols);
                    rotationsLocal *= (rotTile + 1);
                }
                else if (emptyTile == 1)
                {
                    nodeRotate = currentNode - (rotationsLocal * rows * cols * rotTile);
                    rotationsLocal *= (rotTile + 1);
                }
            }
            else if (pipeCross.Contains(tile) ||
                pipeCorner.Contains(tile))
            {
                if (emptyTile != 3)
                {
                    nodeRotate = currentNode + (rotationsLocal * rows * cols);
                    rotationsLocal *= (rotTile + 1);
                }
                else if (emptyTile == 3)
                {
                    nodeRotate = currentNode - (rotationsLocal * rows * cols * rotTile);
                    rotationsLocal *= (rotTile + 1);
                }
            }
            else
            {
                nodeRotate = currentNode;
            }

            return nodeRotate;
        }
    }
}
