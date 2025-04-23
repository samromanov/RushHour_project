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
    public class Car
    {

        //Rush Hour Car Id's:
        //Red car: X
        //Light green car: A
        //Orange car: B
        //Light blue car: C
        //Pink car: D
        //Purple car: E
        //Dark green car: F
        //Gray car: G
        //Beige car: H
        //Yellow car: I
        //Brown car: J
        //Green car: K
        //Yellow lorry: O
        //Purple lorry: P
        //Blue lorry: Q
        //Turquoise lorry: R

        // Unique identifier (e.g., 'A', 'B', 'R'. 'X' for red car!)
        public char Id { get; }

        // Position of the car (top-left corner)
        public int Row { get; set; }  // the row in the rows (up-down)
        public int Column { get; set; }  // the column in the columns (left-right)

        // Size & movement direction
        public int Length { get; }  // How many spaces the car occupies
        public bool IsHorizontal { get; }  // True = left/right, False = up/down
        public bool IsRedCar { get; }  // If it's the target red car


        public Car(char id, int row, int column, int length, bool isHorizontal, bool isRedCar = false)
        {
            this.Id = id;
            this.Row = row;
            this.Column = column;
            this.Length = length;
            this.IsHorizontal = isHorizontal;
            this.IsRedCar = isRedCar;
        }

        public bool CanMove(int dRows, int dColumns, char[,] board)
        {
            int newRow = this.Row + dRows;
            int newColumn = this.Column + dColumns;

            if (this.IsHorizontal)
            {
                if (newColumn < 0 || (newColumn + Length) > 6) return false;
                for (int i = 0; i < Length; i++)
                    if (board[newRow, newColumn + i] != '.') return false;

            }
            else // if isHorizontal == false
            {
                if (newRow < 0 || (newRow + Length) > 6) return false;
                for (int i = 0; i < Length; i++)
                    if (board[newRow + i, newColumn] != '.') return false;
            }
            return true;
        }

        public void Move(int dRows, int dColumns, char[,] board)
        {
            if (CanMove(dRows, dColumns, board))
            {
                // Clear old position
                for (int i = 0; i < Length; i++)
                {
                    int oldColumn = IsHorizontal ? Column + i : Column; // if isHorizontal -> oldColumn = column + i, if !isHorizontal -> oldColumn = column
                    int oldRow = IsHorizontal ? Row : Row + i;
                    board[oldRow, oldColumn] = '.'; // Mark as empty
                }

                // Update position
                Row += dRows;
                Column += dColumns;

                // Place car in new position
                for (int i = 0; i < Length; i++)
                {
                    int newColumn = IsHorizontal ? Column + i : Column;
                    int newRow = IsHorizontal ? Row : Row + i;
                    board[newRow, newColumn] = Id; // Mark car's new position
                }
            }
        }

        public Car Clone()
        {
            return new Car(this.Id, this.Row, this.Column, this.Length, this.IsHorizontal,this.IsRedCar);
        }

        public bool HasExited()
        {
            return IsRedCar && (Column + Length >= 6); // If red car reaches right edge
        }




    }
}