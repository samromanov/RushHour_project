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
        public char Id { get; set; }  // Unique identifier for each car (
        public int X { get; set; }   // Top-left X coordinate on the board (0-5)
        public int Y { get; set; }   // Top-left Y coordinate on the board (0-5)
        public int Length { get; set; }  // Car length (either 2 or 3 cells)
        public bool IsHorizontal { get; set; }  // True = horizontal, False = vertical
        public bool IsRedCar { get; set; }  // Identifies the target car

        public Car(char id, int x, int y, int length, bool isHorizontal, bool isRedCar = false)
        {
            Id = id;
            X = x;
            Y = y;
            Length = length;
            IsHorizontal = isHorizontal;
            IsRedCar = isRedCar;
        }

        public bool CanMove(int steps, char[,] board) // steps can be positive or negative
        {
            int newX = X, newY = Y;

            if (IsHorizontal)
                newX += steps;
            else
                newY += steps;

            // Ensure the new position is within the board boundaries
            if (newX < 0 || newY < 0 || (IsHorizontal && newX + Length > 6) || (!IsHorizontal && newY + Length > 6))
                return false;

            // Check if new position collides with another car
            for (int i = 0; i < Length; i++)
            {
                int checkX = IsHorizontal ? newX + i : X; // If the car is horizontal, the X coordinate changes while Y remains the same
                int checkY = !IsHorizontal ? Y : newY + i; // If the car is vertical, the Y coordinate changes while X remains the same.

                if (board[checkX, checkY] != '0' && board[checkX, checkY] != Id)
                    return false;
            }

            return true;
        }





    }
}