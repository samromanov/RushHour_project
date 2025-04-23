using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RushHour_project.Classes
{
    public class Level : Activity
    {
        public int difficulty { get; } // (eg. 1 = beginner, 2 = intermediate, 5 = grand master...) 
        public int levelNumber { get; }// number of the level
        public char[,] board { get; } // A 6x6 matrix representing the parking lot ('.' = empty, other chars for different vehicles)
        public List<Car> cars { get; }
        public Level()
        {

        }
        public Level(int difficulty, int levelNumber, List<Car> cars)
        {
            this.difficulty = difficulty;
            this.levelNumber = levelNumber;
            this.cars = cars;
            this.board = new char[6, 6];

            // Initialize the empty board
            for (int row = 0; row < this.board.GetLength(0); row++)
                for (int column = 0; column < this.board.GetLength(1); column++)
                    board[row, column] = '.'; // '.' -> empty slot

        }

        public virtual Level Clone()
        {
            List<Car> clonedCars = this.cars.Select(car => car.Clone()).ToList();
            return new Level(this.difficulty, this.levelNumber, clonedCars);
        }

        public void PlaceCarsOnBoard()
        {
            foreach (var car in this.cars)
            {
                int row = car.Row, column = car.Column;

                for (int i = 0; i < car.Length; i++)
                {
                    board[row, column] = car.Id; // places the car Id (for example: X) in the cell. accupied instead of '.'

                    if (car.IsHorizontal)
                        column += 1; // Move right
                    else
                        row += 1; // Move down
                }
            }
        }
    }
}