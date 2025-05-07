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
using RushHour_project.Classes;

namespace RushHour_project.Lists
{
    internal static class LevelsList
    {
        public static List<Car> cars_level1 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,0,2,true),
            new Car('P',1,0,3,false),
            new Car('B',4,0,2,false),
            new Car('Q',1,3,3,false),
            new Car('o',0,5,3,false),
            new Car('R',5,2,3,true),
            new Car('C',4,4,2,true)
        };
        public static List<Car> cars_level2 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,0,2,false),
            new Car('O',0,3,3,true),
            new Car('B',1,3,2,false),
            new Car('D',4,2,2,false),
            new Car('C',2,4,2,false),
            new Car('F',5,0,2,true),
            new Car('G',5,3,2,true),  
            new Car('E',4,4,2,true),
            new Car('P',1,5,3,false),
            new Car('Q',3,0,3,true)
        };
        public static List<Car> cars_level11 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,false),
            new Car('D',1,0,2,false),
            new Car('E',1,2,2,true),
            new Car('F',2,1,2,false),
            new Car('G',5,0,2,true),
            new Car('O',0,5,3,false),
            new Car('P',2,2,3,false),
            new Car('Q',3,3,3,true)
        };
        public static List<Car> cars_level21 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,false),
            new Car('C',0,4,2,true),
            new Car('D',1,0,2,true),
            new Car('E',2,4,2,false),
            new Car('F',4,1,2,false),
            new Car('G',4,3,2,false),
            new Car('H',4,4,2,true),
            new Car('I',5,4,2,true),
            new Car('O',1,5,3,false),
            new Car('P',2,0,3,false),
            new Car('Q',3,1,3,true)
        };
        public static List<Car> cars_level31 = new List<Car>
        {
            new Car('X',2,2,2,true,true),
            new Car('A',0,0,2,false),
            new Car('B',0,1,2,true),
            new Car('C',0,3,2,false),
            new Car('D',1,1,2,false),
            new Car('E',2,4,2,false),
            new Car('F',3,0,2,true),
            new Car('G',3,3,2,false),
            new Car('H',4,4,2,true),
            new Car('I',5,0,2,true),
            new Car('J',5,3,2,true),
            new Car('O',0,5,3,false),
            new Car('P',3,2,3,false)
        };
        public static List<Car> cars_level46 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',0,3,2,false),
            new Car('B',1,0,2,false),
            new Car('C',1,1,2,true),
            new Car('D',3,3,2,false),
            new Car('E',3,4,2,true),
            new Car('F',5,1,2,true),
            new Car('G',5,3,2,true),
            new Car('O',0,0,3,true),
            new Car('P',0,5,3,false),
            new Car('Q',2,2,3,false)
        };

        // THE LIST of all of the LEVELS
        public static List<Level> levelsList = new List<Level>
        {
            new Beginner(1,1,cars_level1),
            new Beginner(1,2,cars_level2),
            new Intermediate(2,11,cars_level11),
            new Advanced(3,21,cars_level21),
            new Expert(4,31,cars_level31),
            new Grand_Master(5,46,cars_level46)
        };
    }
}