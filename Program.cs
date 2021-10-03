using System;
using System.Collections.Generic;

namespace snake {
    class Program {
        static void PrintGrid(List<List<int>> grid) {
            // permit to draw the grid in the console
            string grid_limits = new string('*', grid[0].Count*2+2);
            Console.WriteLine(grid_limits);
            foreach (List<int> line in grid) {
                Console.Write("*");
                foreach (int cell in line) {
                    if (cell == 1) {
                        Console.Write("+ ");
                    }
                    else if (cell == 2) {
                        Console.Write("o ");
                    }
                    else {
                        Console.Write("  ");
                    }
                }
                Console.Write("*");
                Console.WriteLine();
            }
            Console.WriteLine(grid_limits);
        }
        static List<List<int>> CreateGrid(int width, int height) {
            // retrun an initial grid, with a width and a height at choice
            List<List<int>> grid = new List<List<int>>{};
            for (int i = 0; i < height; i++) {
                List<int> line = new List<int>{};
                for (int j = 0; j < width; j++) {
                    line.Add(1);
                }
                grid.Add(line);
            }
            return grid;
        }

        static List<List<int>> NextGrid(List<List<int>> grid, List<(int, int)> snake, (int, int) apple) {
            // return a grid with the snake inside
            List<List<int>> nextgrid = new List<List<int>>{};
            for (int i = 0; i < grid.Count; i++) {
                List<int> line = new List<int> { };
                for (int j = 0; j < grid[i].Count; j++) {
                    if (snake.Contains((j, i))) {
                        line.Add(1);
                    }
                    else if (apple == (j, i)) {
                        line.Add(2);
                    }
                    else {
                        line.Add(0);
                    }
                    
                }
                nextgrid.Add(line);
            }
            return nextgrid;
        }

        static List<(int, int)> NextSnake(List<(int, int)> snake, (int, int) apple, int vx, int vy) {
            // return a list of tuple that correspond to the next state of the snake
            int len = snake.Count - 1;
            snake.Add((snake[len].Item1 + vx, snake[len].Item2 + vy));
            if (snake[len] != apple) {
                snake.Remove(snake[0]);
            }
            return snake;
        }
            
        static void Main(string[] args) {
            int score = 0;
            List<(int, int)> snake = new List<(int, int)>{(0, 0), (1, 0), (2, 0)};
            int width = 20;
            int height = 20;
            
            Random rand = new Random();
            (int, int) applePos = (rand.Next(0, width), rand.Next(0, height));
            int xVelocity = 1;
            int yVelocity = 0;
            
            List<List<int>> grid = NextGrid(CreateGrid(width, height), snake, applePos);
            bool Running = true;
            
            // gameplay loop, where every actions are mades
            while (Running) {
                Console.Clear();
                int SnakeLen = snake.Count;
                snake = NextSnake(snake, applePos, xVelocity, yVelocity);
                grid = NextGrid(grid, snake, applePos);
                PrintGrid(grid);
                Console.WriteLine($"Score :{score}");
                DateTime time = DateTime.Now;

                if (snake.Count != SnakeLen) {
                    applePos = (rand.Next(0, width), rand.Next(0, height));
                    score++;
                }
                
                // evenement loop, to get the keyboard input in real time
                while (true) {
                    DateTime time2 = DateTime.Now;
                    if (time2.Subtract(time).TotalMilliseconds > 75) {
                        break;
                    }
                    if (Console.KeyAvailable) {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        switch (key.Key) {
                            case ConsoleKey.UpArrow:
                                yVelocity = -1;
                                xVelocity = 0;
                                break;
                            case ConsoleKey.DownArrow:
                                yVelocity = 1;
                                xVelocity = 0;
                                break;
                            case ConsoleKey.LeftArrow:
                                yVelocity = 0;
                                xVelocity = -1;
                                break;
                            case ConsoleKey.RightArrow:
                                yVelocity = 0;
                                xVelocity = 1;
                                break;
                        }
                    }
                }
            }
        }
    }
}
