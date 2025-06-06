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
        public bool TryMoveCar(char carId, int moveAmount)
        {
            // Find the car
            var car = cars.FirstOrDefault(c => c.Id == carId);
            if (car == null || moveAmount == 0) return false;

            // Remove the car from the board temporarily
            int row = car.Row, col = car.Column;
            for (int i = 0; i < car.Length; i++)
            {
                board[row, col] = '.'; // Mark as empty
                if (car.IsHorizontal)
                    col += 1;
                else
                    row += 1;
            }

            // Calculate new position
            int newRow = car.Row, newCol = car.Column;
            if (car.IsHorizontal)
                newCol += moveAmount;
            else
                newRow += moveAmount;

            // Check if the move is within bounds and not colliding
            for (int i = 0; i < car.Length; i++)
            {
                int checkRow = newRow, checkCol = newCol;
                if (car.IsHorizontal)
                    checkCol += i;
                else
                    checkRow += i;

                // Out of bounds?
                if (checkRow < 0 || checkRow >= 6 || checkCol < 0 || checkCol >= 6)
                {
                    // Restore the car to its original position
                    PlaceCarOnBoard(car);
                    return false;
                }

                // Collision?
                if (board[checkRow, checkCol] != '.')
                {
                    PlaceCarOnBoard(car);
                    return false;
                }
            }

            // Move is valid, update car position
            car.Row = newRow;
            car.Column = newCol;

            // Place the car in the new position
            PlaceCarOnBoard(car);

            return true;
        }

        // Helper to place a single car on the board
        private void PlaceCarOnBoard(Car car)
        {
            int row = car.Row, col = car.Column;
            for (int i = 0; i < car.Length; i++)
            {
                board[row, col] = car.Id;
                if (car.IsHorizontal)
                    col += 1;
                else
                    row += 1;
            }
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
        public bool IsWin()
        {
            foreach (var car in this.cars)
            {
                if (car.Id=='X')
                {
                    if (car.Column == 4)//target car arrived at the exit
                    {
                        return true;
                    }
                    break;
                }
            }
            return false;
        }
    }
}