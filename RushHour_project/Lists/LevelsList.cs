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
        public static List<Car> cars_level3 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',3,1,2,true),
            new Car('B',4,1,2,false),
            new Car('C',5,2,2,true),
            new Car('O',2,3,3,false),
            new Car('P',3,5,3,false)
        };
        public static List<Car> cars_level4 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',3,2,2,false),
            new Car('B',4,5,2,false),
            new Car('O',0,0,3,false),
            new Car('P',0,3,3,false),
            new Car('Q',3,3,3,true),
            new Car('R',5,2,3,true)
        };
        public static List<Car> cars_level5 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,5,2,false),
            new Car('D',4,0,2,false),
            new Car('E',4,4,2,true),
            new Car('F',5,4,2,true),
            new Car('G',2,5,2,false),
            new Car('O',0,3,3,false),
            new Car('P',1,0,3,false),
            new Car('Q',1,4,3,false),
            new Car('R',3,1,3,true)
        };
        public static List<Car> cars_level6 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,3,2,false),
            new Car('C',1,0,2,true),
            new Car('D',3,0,2,true),
            new Car('E',3,2,2,false),
            new Car('F',4,0,2,false),
            new Car('O',1,4,3,false),
            new Car('P',1,5,3,false),
            new Car('Q',2,3,3,false),
            new Car('R',5,3,3,true)
        };
        public static List<Car> cars_level7 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,false),
            new Car('D',0,5,2,false),
            new Car('E',1,3,2,false),
            new Car('F',2,5,2,false),
            new Car('H',4,3,2,false),
            new Car('I',3,2,2,true)
        };
        public static List<Car> cars_level8 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,3,2,true),
            new Car('B',1,2,2,true),
            new Car('C',1,4,2,false),
            new Car('D',2,2,2,false),
            new Car('E',2,3,2,false),
            new Car('F',3,0,2,true),
            new Car('G',3,4,2,true),
            new Car('H',4,0,2,true),
            new Car('I',4,2,2,false),
            new Car('K',5,0,2,true),
            new Car('O',0,5,3,false),
            new Car('P',4,3,3,true),
            new Car('Q',5,3,3,true)
        };
        public static List<Car> cars_level9 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,true),
            new Car('D',1,3,2,false),
            new Car('E',1,4,2,true),
            new Car('F',2,5,2,false),
            new Car('G',4,2,2,false),
            new Car('H',4,5,2,false),
            new Car('O',2,4,3,false),
            new Car('P',3,0,3,false),
            new Car('Q',3,1,3,true)
        };
        public static List<Car> cars_level10 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,false),
            new Car('C',0,4,2,true),
            new Car('D',1,0,2,true),
            new Car('E',4,3,2,false),
            new Car('F',4,4,2,true),
            new Car('G',5,0,2,true),
            new Car('H',5,4,2,true),
            new Car('O',1,5,3,false),
            new Car('P',2,0,3,false),
            new Car('Q',3,1,3,true)
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
        public static List<Car> cars_level12 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,2,2,false),
            new Car('B',1,4,2,true),
            new Car('C',3,1,2,false),
            new Car('D',3,4,2,true),
            new Car('E',4,0,2,false),
            new Car('F',4,2,2,true),
            new Car('G',4,5,2,false),
            new Car('O',0,3,3,true),
            new Car('P',1,3,3,false),
            new Car('Q',5,1,3,true)
        };
        public static List<Car> cars_level13 = new List<Car>
        {
            new Car('X',2,2,2,true,true),
            new Car('A',0,1,2,true),
            new Car('B',0,3,2,true),
            new Car('C',1,0,2,true),
            new Car('D',1,2,2,true),
            new Car('E',3,2,2,false),
            new Car('F',3,3,2,false),
            new Car('G',4,4,2,true),
            new Car('H',5,1,2,true),
            new Car('I',5,3,2,true),
            new Car('O',1,4,3,false),
            new Car('P',1,5,3,false),
            new Car('Q',2,0,3,false),
            new Car('R',2,1,3,false)
        };
        public static List<Car> cars_level14 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,2,2,false),
            new Car('B',1,0,2,true),
            new Car('C',1,4,2,false),
            new Car('D',2,0,2,false),
            new Car('E',3,1,2,false),
            new Car('F',3,2,2,false),
            new Car('G',3,3,2,true),
            new Car('I',4,3,2,true),
            new Car('O',0,3,3,true),
            new Car('P',3,5,3,false),
            new Car('Q',5,1,3,true)
        };
        public static List<Car> cars_level15 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,true),
            new Car('B',3,2,2,false),
            new Car('E',4,5,2,false),
            new Car('O',0,0,3,false),
            new Car('P',0,3,3,false),
            new Car('Q',3,3,3,true),
            new Car('R',5,2,3,true)
        };
        public static List<Car> cars_level16 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',0,1,2,true),
            new Car('B',0,3,2,false),
            new Car('C',0,4,2,true),
            new Car('D',1,1,2,true),
            new Car('E',1,5,2,false),
            new Car('F',3,0,2,true),
            new Car('G',4,0,2,false),
            new Car('H',5,1,2,true),
            new Car('I',5,3,2,true),
            new Car('O',2,2,3,false),
            new Car('P',3,5,3,false)
        };
        public static List<Car> cars_level17 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,true),
            new Car('D',3,1,2,true),
            new Car('E',3,3,2,true),
            new Car('F',4,2,2,false),
            new Car('G',4,3,2,false),
            new Car('H',4,4,2,true),
            new Car('I',5,0,2,true),
            new Car('O',1,0,3,false),
            new Car('P',1,5,3,false)
        };
        public static List<Car> cars_level18 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,4,2,true),
            new Car('C',1,4,2,true),
            new Car('D',3,0,2,false),
            new Car('E',4,1,2,true),
            new Car('F',4,3,2,false),
            new Car('G',5,4,2,true),
            new Car('O',0,2,3,false),
            new Car('P',2,5,3,false),
            new Car('Q',3,1,3,true)
        };
        public static List<Car> cars_level19 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,3,2,false),
            new Car('B',0,4,2,true),
            new Car('C',2,3,2,false),
            new Car('D',2,4,2,false),
            new Car('E',4,1,2,false),
            new Car('F',4,2,2,false),
            new Car('G',4,3,2,true),
            new Car('O',0,0,3,false),
            new Car('P',3,0,3,true),
            new Car('Q',3,5,3,false)
        };
        public static List<Car> cars_level20 = new List<Car>
        {
            new Car('X',2,2,2,true,true),
            new Car('A',0,2,2,false),
            new Car('B',0,4,2,true),
            new Car('C',1,0,2,false),
            new Car('D',1,3,2,true),
            new Car('E',3,0,2,true),
            new Car('F',3,2,2,false),
            new Car('G',3,3,2,false),
            new Car('H',4,4,2,true),
            new Car('I',5,1,2,true),
            new Car('J',5,3,2,true),
            new Car('O',0,1,3,false),
            new Car('P',1,5,3,false)
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
        public static List<Car> cars_level22 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,0,2,false),
            new Car('B',0,1,2,true),
            new Car('C',1,1,2,true),
            new Car('D',2,2,2,false),
            new Car('E',3,3,2,true),
            new Car('F',4,2,2,false),
            new Car('O',0,3,3,false),
            new Car('P',2,5,3,false),
            new Car('R',5,3,3,true)
        };
        public static List<Car> cars_level23 = new List<Car>
        {
            new Car('X',2,2,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,false),
            new Car('C',1,1,2,false),
            new Car('D',4,3,2,false),
            new Car('E',4,4,2,true),
            new Car('F',5,0,2,true),
            new Car('G',5,4,2,true),
            new Car('O',1,0,3,false),
            new Car('P',1,4,3,false),
            new Car('Q',1,5,3,false),
            new Car('R',3,1,3,true)
        };
        public static List<Car> cars_level24 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',0,1,2,true),
            new Car('B',0,3,2,false),
            new Car('C',0,4,2,false),
            new Car('D',1,1,2,false),
            new Car('E',3,3,2,false),
            new Car('F',4,2,2,true),
            new Car('G',4,4,2,true),
            new Car('H',5,0,2,true),
            new Car('I',5,3,2,true),
            new Car('O',0,0,3,false),
            new Car('P',1,5,3,false),
            new Car('Q',3,0,3,true)
        };
        public static List<Car> cars_level25 = new List<Car>
        {
            new Car('X',2,2,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,false),
            new Car('C',0,4,2,true),
            new Car('D',2,4,2,false),
            new Car('E',4,3,2,false),
            new Car('F',4,4,2,true),
            new Car('G',5,0,2,true),
            new Car('O',1,0,3,false),
            new Car('P',1,5,3,false),
            new Car('Q',3,1,3,true)
        };
        public static List<Car> cars_level26 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,2,2,false),
            new Car('B',1,3,2,true),
            new Car('C',1,5,2,false),
            new Car('D',2,3,2,false),
            new Car('E',2,4,2,false),
            new Car('F',3,0,2,true),
            new Car('G',4,3,2,false),
            new Car('H',4,4,2,true),
            new Car('I',5,4,2,true),
            new Car('O',0,0,3,false),
            new Car('P',0,3,3,true)
        };
        public static List<Car> cars_level27 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,false),
            new Car('D',1,2,2,false),
            new Car('E',1,5,2,false),
            new Car('F',3,0,2,true),
            new Car('G',3,2,2,false),
            new Car('H',3,3,2,true),
            new Car('I',4,0,2,false),
            new Car('J',4,3,2,true),
            new Car('O',3,5,3,false),
            new Car('P',5,2,3,true)
        };
        public static List<Car> cars_level28 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,4,2,false),
            new Car('B',0,5,2,false),
            new Car('C',2,5,2,false),
            new Car('D',3,1,2,false),
            new Car('E',4,2,2,false),
            new Car('F',4,3,2,false),
            new Car('G',4,4,2,true),
            new Car('H',5,0,2,true),
            new Car('I',5,4,2,true),
            new Car('O',0,0,3,true),
            new Car('P',2,0,3,false),
            new Car('Q',3,2,3,true)
        };
        public static List<Car> cars_level29 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',1,2,2,false),
            new Car('B',2,5,2,false),
            new Car('C',3,0,2,false),
            new Car('D',3,1,2,true),
            new Car('E',3,3,2,true),
            new Car('F',4,1,2,true),
            new Car('G',4,3,2,false),
            new Car('H',4,5,2,false),
            new Car('O',0,0,3,true),
            new Car('P',0,4,3,false),
            new Car('R',5,0,3,true)
        };
        public static List<Car> cars_level30 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,2,2,false),
            new Car('B',1,0,2,true),
            new Car('C',1,4,2,false),
            new Car('D',2,0,2,false),
            new Car('E',3,4,2,true),
            new Car('F',4,0,2,true),
            new Car('G',4,2,2,true),
            new Car('H',4,5,2,false),
            new Car('I',5,0,2,true),
            new Car('J',5,2,2,true),
            new Car('O',0,3,3,true),
            new Car('P',1,3,3,false)
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
        public static List<Car> cars_level32 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,0,2,false),
            new Car('B',0,2,2,true),
            new Car('C',2,2,2,false),
            new Car('D',2,3,2,false),
            new Car('E',3,4,2,true),
            new Car('F',4,5,2,false),
            new Car('O',0,4,3,false),
            new Car('P',1,1,3,true),
            new Car('Q',4,2,3,true),
            new Car('R',5,2,3,true)
        };
        public static List<Car> cars_level33 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,true),
            new Car('D',1,3,2,false),
            new Car('E',1,4,2,true),
            new Car('F',4,2,2,false),
            new Car('G',4,3,2,true),
            new Car('H',5,0,2,true),
            new Car('O',2,0,3,false),
            new Car('P',3,1,3,true),
            new Car('Q',3,5,3,false)
        };
        public static List<Car> cars_level34 = new List<Car>
        {
            new Car('X',2,0,2,true),
            new Car('A',0,0,2,false),
            new Car('B',0,1,2,true),
            new Car('C',0,3,2,true),
            new Car('D',1,2,2,false),
            new Car('E',2,5,2,false),
            new Car('F',3,0,2,false),
            new Car('G',3,1,2,true),
            new Car('H',3,4,2,false),
            new Car('I',4,1,2,false),
            new Car('J',4,5,2,false),
            new Car('O',1,3,3,true),
            new Car('P',2,3,3,false),
            new Car('Q',5,2,3,true)
        };
        public static List<Car> cars_level35 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,1,2,true),
            new Car('B',0,3,2,true),
            new Car('C',0,5,2,false),
            new Car('D',1,2,2,false),
            new Car('E',1,3,2,false),
            new Car('F',3,2,2,false),
            new Car('G',3,3,2,true),
            new Car('H',4,3,2,true),
            new Car('I',5,1,2,true),
            new Car('O',3,0,3,false),
            new Car('P',2,5,3,false),
            new Car('Q',5,3,3,true)
        };
        public static List<Car> cars_level36 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,3,2,false),
            new Car('B',0,4,2,false),
            new Car('C',1,1,2,true),
            new Car('D',2,3,2,false),
            new Car('E',3,2,2,false),
            new Car('F',3,4,2,true),
            new Car('G',4,3,2,true),
            new Car('H',4,5,2,false),
            new Car('O',0,0,3,true),
            new Car('P',2,0,3,false),
            new Car('Q',5,0,3,true)
        };
        public static List<Car> cars_level37 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',1,3,2,false),
            new Car('C',3,2,2,true),
            new Car('D',4,2,2,false),
            new Car('E',4,3,2,true),
            new Car('F',5,0,2,true),
            new Car('G',5,4,2,true),
            new Car('O',0,0,3,false),
            new Car('P',0,2,3,true),
            new Car('Q',1,4,3,false),
            new Car('R',2,5,3,false)
        };
        public static List<Car> cars_level38 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,false),
            new Car('C',0,4,2,true),
            new Car('D',1,0,2,true),
            new Car('E',2,4,2,false),
            new Car('F',4,3,2,false),
            new Car('G',4,4,2,true),
            new Car('H',5,0,2,true),
            new Car('I',5,4,2,true),
            new Car('O',1,5,3,false),
            new Car('P',2,0,3,false),
            new Car('Q',3,1,3,true)
        };
        public static List<Car> cars_level39 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,0,2,false),
            new Car('B',1,2,2,false),
            new Car('C',1,4,2,true),
            new Car('D',2,5,2,false),
            new Car('E',3,0,2,true),
            new Car('F',3,2,2,false),
            new Car('G',4,1,2,false),
            new Car('H',4,5,2,false),
            new Car('O',0,1,3,true),
            new Car('P',1,3,3,false),
            new Car('Q',5,2,3,true)
        };
        public static List<Car> cars_level40 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',0,4,2,false),
            new Car('B',0,5,2,false),
            new Car('C',1,0,2,false),
            new Car('D',2,2,2,false),
            new Car('E',2,5,2,false),
            new Car('F',3,0,2,true),
            new Car('G',4,4,2,true),
            new Car('O',0,0,3,true),
            new Car('P',1,1,3,true),
            new Car('Q',3,3,3,false),
            new Car('R',5,0,3,true)
        };
        public static List<Car> cars_level41 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,5,2,false),
            new Car('C',1,4,2,false),
            new Car('D',2,5,2,false),
            new Car('E',3,2,2,false),
            new Car('F',3,3,2,true),
            new Car('G',4,3,2,false),
            new Car('H',4,4,2,true),
            new Car('O',0,2,3,true),
            new Car('P',2,0,3,false),
            new Car('Q',5,0,3,true)
        };
        public static List<Car> cars_level42 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,true),
            new Car('B',0,3,2,false),
            new Car('C',0,4,2,true),
            new Car('D',1,1,2,true),
            new Car('E',2,3,2,false),
            new Car('F',3,0,2,true),
            new Car('G',3,2,2,false),
            new Car('H',4,3,2,true),
            new Car('I',4,5,2,false),
            new Car('J',5,1,2,true),
            new Car('K',5,3,2,true),
            new Car('O',0,0,3,false),
            new Car('P',1,4,3,false)
        };
        public static List<Car> cars_level43 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,true),
            new Car('D',1,3,2,false),
            new Car('E',3,0,2,true),
            new Car('F',3,2,2,false),
            new Car('G',4,3,2,true),
            new Car('H',5,1,2,true),
            new Car('I',5,3,2,true),
            new Car('O',0,0,3,false),
            new Car('P',1,4,3,false),
            new Car('Q',3,5,3,false)
        };
        public static List<Car> cars_level44 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,3,2,false),
            new Car('C',0,4,2,true),
            new Car('D',3,0,2,false),
            new Car('E',3,1,2,true),
            new Car('F',3,3,2,true),
            new Car('H',5,0,2,true),
            new Car('K',4,3,2,false),
            new Car('O',0,2,3,false),
            new Car('P',3,5,3,false)
        };
        public static List<Car> cars_level45 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,4,2,true),
            new Car('D',3,1,2,true),
            new Car('E',3,3,2,true),
            new Car('F',4,1,2,true),
            new Car('G',4,3,2,false),
            new Car('H',4,4,2,false),
            new Car('I',3,0,2,false),
            new Car('P',3,5,3,false),
            new Car('Q',5,0,3,true),
            new Car('R',0,2,3,false)
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
        public static List<Car> cars_level47 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,4,2,false),
            new Car('C',3,4,2,true),
            new Car('D',4,0,2,false),
            new Car('E',4,1,2,true),
            new Car('F',5,1,2,true),
            new Car('O',0,2,3,false),
            new Car('P',0,5,3,false),
            new Car('Q',3,3,3,false)
        };
        public static List<Car> cars_level48 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,1,2,true),
            new Car('B',0,3,2,true),
            new Car('C',1,3,2,false),
            new Car('D',3,0,2,true),
            new Car('E',3,3,2,false),
            new Car('F',3,4,2,true),
            new Car('G',4,0,2,false),
            new Car('H',4,1,2,true),
            new Car('I',4,5,2,false),
            new Car('O',1,2,3,false),
            new Car('P',5,1,3,true)
        };
        public static List<Car> cars_level49 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',1,2,2,false),
            new Car('B',1,3,2,true),
            new Car('C',2,0,2,false),
            new Car('D',3,2,2,false),
            new Car('E',3,3,2,false),
            new Car('F',3,4,2,true),
            new Car('G',4,4,2,true),
            new Car('O',0,2,3,true),
            new Car('P',0,5,3,false),
            new Car('Q',5,2,3,true)
        };
        public static List<Car> cars_level50 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,false),
            new Car('D',0,5,2,false),
            new Car('E',1,0,2,true),
            new Car('F',2,5,2,false),
            new Car('G',4,2,2,false),
            new Car('H',4,3,2,false),
            new Car('I',4,4,2,true),
            new Car('J',5,0,2,true),
            new Car('O',2,0,3,false),
            new Car('P',3,2,3,true)
        };
        public static List<Car> cars_level51 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,5,2,false),
            new Car('C',2,3,2,false),
            new Car('D',2,4,2,false),
            new Car('E',3,0,2,true),
            new Car('F',4,0,2,false),
            new Car('G',4,3,2,true),
            new Car('O',0,2,3,true),
            new Car('P',2,5,3,false),
            new Car('Q',3,2,3,false),
            new Car('R',5,3,3,true)
        };
        public static List<Car> cars_level52 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,true),
            new Car('D',1,2,2,true),
            new Car('E',2,3,2,false),
            new Car('F',4,2,2,false),
            new Car('G',4,3,2,true),
            new Car('H',5,3,2,true),
            new Car('O',0,0,3,false),
            new Car('P',1,4,3,false),
            new Car('Q',3,0,3,true),
            new Car('R',3,5,3,false)
        };
        public static List<Car> cars_level53 = new List<Car>
        {
            new Car('X',2,2,2,true,true),
            new Car('A',0,3,2,false),
            new Car('B',0,4,2,true),
            new Car('C',1,1,2,false),
            new Car('D',1,4,2,false),
            new Car('E',3,1,2,true),
            new Car('F',3,3,2,true),
            new Car('G',4,2,2,false),
            new Car('H',4,3,2,true),
            new Car('I',5,0,2,true),
            new Car('J',5,4,2,true),
            new Car('O',1,0,3,false),
            new Car('P',2,5,3,false)
        };
        public static List<Car> cars_level54 = new List<Car>
        {
            new Car('X',2,3,2,true,true),
            new Car('A',1,2,2,false),
            new Car('B',3,0,2,true),
            new Car('C',3,2,2,false),
            new Car('D',3,3,2,false),
            new Car('E',4,0,2,false),
            new Car('F',4,1,2,false),
            new Car('G',4,4,2,true),
            new Car('O',0,2,3,true),
            new Car('P',0,5,3,false),
            new Car('Q',5,2,3,true)
        };
        public static List<Car> cars_level55 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,0,2,false),
            new Car('B',0,1,2,true),
            new Car('C',0,3,2,false),
            new Car('D',0,4,2,true),
            new Car('E',3,3,2,true),
            new Car('F',4,3,2,false),
            new Car('G',4,4,2,true),
            new Car('O',1,2,3,false),
            new Car('P',1,5,3,false),
            new Car('Q',5,0,3,true)
        };
        public static List<Car> cars_level56 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,1,2,false),
            new Car('B',0,2,2,true),
            new Car('C',0,4,2,true),
            new Car('D',1,3,2,false),
            new Car('E',1,4,2,true),
            new Car('F',4,2,2,false),
            new Car('G',5,3,2,true),
            new Car('O',2,4,3,false),
            new Car('P',3,0,3,false),
            new Car('Q',3,1,3,true),
            new Car('R',3,5,3,false)
        };
        public static List<Car> cars_level57 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,0,2,false),
            new Car('B',0,4,2,true),
            new Car('C',1,4,2,false),
            new Car('D',3,1,2,false),
            new Car('E',4,0,2,false),
            new Car('F',4,3,2,false),
            new Car('G',4,4,2,true),
            new Car('H',5,1,2,true),
            new Car('O',0,2,3,false),
            new Car('P',1,5,3,false),
            new Car('Q',3,2,3,true)
        };
        public static List<Car> cars_level58 = new List<Car>
        {
            new Car('X',2,1,2,true,true),
            new Car('A',0,2,2,false),
            new Car('B',1,4,2,false),
            new Car('C',2,3,2,false),
            new Car('D',3,0,2,true),
            new Car('E',3,2,2,false),
            new Car('F',4,1,2,false),
            new Car('G',4,3,2,true),
            new Car('O',0,3,3,true),
            new Car('P',3,5,3,false),
            new Car('Q',5,2,3,true)
        };
        public static List<Car> cars_level59 = new List<Car>
        {
            new Car('X',2,0,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,false),
            new Car('C',0,4,2,true),
            new Car('D',1,4,2,true),
            new Car('E',2,2,2,false),
            new Car('F',2,5,2,false),
            new Car('G',3,3,2,true),
            new Car('H',4,1,2,true),
            new Car('I',4,3,2,false),
            new Car('J',4,5,2,false),
            new Car('K',5,1,2,true),
            new Car('O',3,0,3,false)
        };
        public static List<Car> cars_level60 = new List<Car>
        {
            new Car('X',2,2,2,true,true),
            new Car('A',0,0,2,true),
            new Car('B',0,2,2,false),
            new Car('C',0,4,2,true),
            new Car('D',1,0,2,true),
            new Car('E',3,5,2,false),
            new Car('F',4,3,2,false),
            new Car('G',5,0,2,true),
            new Car('H',5,4,2,true),
            new Car('O',1,4,3,false),
            new Car('P',2,0,3,false),
            new Car('Q',3,1,3,true)
        };

        // THE LIST of all of the LEVELS
        public static List<Level> levelsList = new List<Level>
        {
            new Beginner(1,1,cars_level1),
            new Beginner(1,2,cars_level2),
            new Beginner(1,3,cars_level3),
            new Beginner(1,4,cars_level4),
            new Beginner(1,5,cars_level5),
            new Beginner(1,6,cars_level6),
            new Beginner(1,7,cars_level7),
            new Beginner(1,8,cars_level8),
            new Beginner(1,9,cars_level9),
            new Beginner(1,10,cars_level10),
            new Intermediate(2,11,cars_level11),
            new Intermediate(2,12,cars_level12),
            new Intermediate(2,13,cars_level13),
            new Intermediate(2,14,cars_level14),
            new Intermediate(2,15,cars_level15),
            new Intermediate(2,16,cars_level16),
            new Intermediate(2,17,cars_level17),
            new Intermediate(2,18,cars_level18),
            new Intermediate(2,19,cars_level19),
            new Intermediate(2,20,cars_level20),
            new Advanced(3,21,cars_level21),
            new Advanced(3,22,cars_level22),
            new Advanced(3,23,cars_level23),
            new Advanced(3,24,cars_level24),
            new Advanced(3,25,cars_level25),
            new Advanced(3,26,cars_level26),
            new Advanced(3,27,cars_level27),
            new Advanced(3,28,cars_level28),
            new Advanced(3,29,cars_level29),
            new Advanced(3,30,cars_level30),
            new Expert(4,31,cars_level31),
            new Expert(4,32,cars_level32),
            new Expert(4,33,cars_level33),
            new Expert(4,34,cars_level34),
            new Expert(4,35,cars_level35),
            new Expert(4,36,cars_level36),
            new Expert(4,37,cars_level37),
            new Expert(4,38,cars_level38),
            new Expert(4,39,cars_level39),
            new Expert(4,40,cars_level40),
            new Expert(4,41,cars_level41),
            new Expert(4,42,cars_level42),
            new Expert(4,43,cars_level43),
            new Expert(4,44,cars_level44),
            new Expert(4,45,cars_level45),
            new Grand_Master(5,46,cars_level46),
            new Grand_Master(5,47,cars_level47),
            new Grand_Master(5,48,cars_level48),
            new Grand_Master(5,49,cars_level49),
            new Grand_Master(5,50,cars_level50),
            new Grand_Master(5,51,cars_level51),
            new Grand_Master(5,52,cars_level52),
            new Grand_Master(5,53,cars_level53),
            new Grand_Master(5,54,cars_level54),
            new Grand_Master(5,55,cars_level55),
            new Grand_Master(5,56,cars_level56),
            new Grand_Master(5,57,cars_level57),
            new Grand_Master(5,58,cars_level58),
            new Grand_Master(5,59,cars_level59),
            new Grand_Master(5,60,cars_level60)
        };

        //returns true if the level exists in the list. false otherwise
        public static bool LevelExist(int levelNum)
        {
            foreach (var level in levelsList)
            {
                if (level.levelNumber == levelNum)
                {
                    return true;
                }
            }
            return false;
        }
        public static List<Level> Clone()
        {
            List<Level> clonedCars = levelsList.Select(car => car.Clone()).ToList();
            return clonedCars;
        }
    }
}