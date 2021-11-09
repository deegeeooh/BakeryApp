﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Vlaaieboer
{
    internal class Color
    {

        public enum TextColors                              // this enum is for setting a color with Color()
        {                                           
            Input,
            MenuSelect,
            SystemMessage,
            Text,
            DefaultForeGround,
            DefaultBackGround,
            Inactive,
            Title,
            Inverted,
            Defaults
        }

        private static List<Color> userColor = new List<Color>();
        private static string settingsFile = "settings.json";

        public ConsoleColor ForeGroundDefault { get; set; }
        public ConsoleColor BackGroundDefault { get; set; }
        public ConsoleColor MenuSelectDefault { get; set; }
        public ConsoleColor Title             { get; set; }
        public ConsoleColor TextHigh          { get; set; }
        public ConsoleColor InputText         { get; set; }
        public ConsoleColor WarningForeGround { get; set; }
        public ConsoleColor WarningBackGround { get; set; }
        public ConsoleColor ErrorForeGround   { get; set; }
        public ConsoleColor ErrorBackGround   { get; set; }
        public ConsoleColor SystemForeGround  { get; set; }
        public ConsoleColor SystemBackGround  { get; set; }


        public Color(bool setStandard)               // set or reset to standard colors
        {
            ForeGroundDefault = ConsoleColor.Gray;
            BackGroundDefault = ConsoleColor.DarkBlue;
            MenuSelectDefault = ConsoleColor.Cyan;
            Title             = ConsoleColor.Yellow;
            TextHigh          = ConsoleColor.White;
            InputText         = ConsoleColor.Green;
            WarningForeGround = ConsoleColor.White;
            WarningBackGround = ConsoleColor.DarkBlue;
            ErrorForeGround   = ConsoleColor.Red;
            ErrorBackGround   = ConsoleColor.DarkBlue;
            SystemForeGround  = ConsoleColor.White;
            SystemBackGround  = ConsoleColor.DarkBlue;
        }

        public Color()                               // reserved for future color sets
        {

        }

        public static void InitializeColors()
        {
            if (!File.Exists(settingsFile))             // set standard colors and create file
            {
                userColor.Add(new Color(true));
                SetStandardColor();
                SaveColors();
                SetWarningColor(false);
                IO.SystemMessage("Setting standard colors and creating settings file");
            }
            else
            {
                userColor = IO.PopulateList<Color>(settingsFile);
            }
            Console.BackgroundColor = userColor[0].BackGroundDefault;
        }

        public static void SetStandardColor()           // for setting and resetting default color scheme
        {
            userColor[0] = new Color(true);
            Console.BackgroundColor = userColor[0].BackGroundDefault;
        }

        public static void SaveColors()
        {
            IO.WriteToFile<Color>(settingsFile, userColor);
        }

        public static void SetWarningColor (bool aWarning)          // swap between warning and error colors for Color.SystemMessage
        {
           // aWarning ? Color(TextColors.Error) : Color(TextColors.SystemMessage);
            if (aWarning)
            {
                userColor[0].SystemForeGround = userColor[0].ErrorForeGround;
                userColor[0].SystemBackGround = userColor[0].ErrorBackGround;
            }
            else
            {
                userColor[0].SystemForeGround = userColor[0].WarningForeGround;
                userColor[0].SystemBackGround = userColor[0].WarningBackGround;
            }
        }
        

        public static void CycleColors(int aChoice, bool aRndBackground)
        {


            int newColor;
            switch (aChoice)
            {
                case 0:     //Text High
                    newColor = (int)userColor[0].TextHigh;                 // get the int value of enum ConsoleColor Usercolor[0].TextHigh;
                    newColor++; if (newColor == 16) { newColor = 0; }                        // increase with 1 until 16, then reset to 0 (Usercolor has 0-15 value)
                    userColor[0].TextHigh = (ConsoleColor)newColor;            // set userColor[0] to new value;
                    break;

                case 1:     //foreground
                    newColor = (int)userColor[0].ForeGroundDefault;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].ForeGroundDefault = (ConsoleColor)newColor;
                    break;

                case 2:     //background

                    newColor = (int)userColor[0].BackGroundDefault;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].BackGroundDefault = (ConsoleColor)newColor;
                    Console.BackgroundColor        = userColor[0].BackGroundDefault;    // set backgroundcolor here before Console.Clear() in main loop
                    userColor[0].SystemBackGround  = userColor[0].BackGroundDefault;
                    userColor[0].WarningBackGround = userColor[0].BackGroundDefault;
                    userColor[0].ErrorBackGround   = userColor[0].BackGroundDefault;
                    break;

                case 3:     //title

                    newColor = (int)userColor[0].MenuSelectDefault;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].MenuSelectDefault = (ConsoleColor)newColor;
                    break;

                case 4:     //menucolor

                    newColor = (int)userColor[0].Title;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].Title = (ConsoleColor)newColor;
                    break;

                case 5:     //randomize

                    var rand = new Random();
                    if (aRndBackground) { userColor[0].BackGroundDefault = (ConsoleColor)rand.Next(16); }

                    do { userColor[0].TextHigh = (ConsoleColor)rand.Next(16); } while (userColor[0].TextHigh == userColor[0].BackGroundDefault);
                    do { userColor[0].ForeGroundDefault = (ConsoleColor)rand.Next(16); } while (userColor[0].ForeGroundDefault == userColor[0].BackGroundDefault);
                    do { userColor[0].MenuSelectDefault = (ConsoleColor)rand.Next(16); } while (userColor[0].MenuSelectDefault == userColor[0].BackGroundDefault |
                                                                                 userColor[0].MenuSelectDefault == userColor[0].ForeGroundDefault);

                    do { userColor[0].Title = (ConsoleColor)rand.Next(16); } while (userColor[0].Title == userColor[0].BackGroundDefault);
                    do { userColor[0].InputText = (ConsoleColor)rand.Next(16); } while (userColor[0].InputText == userColor[0].BackGroundDefault |
                                                                                 userColor[0].InputText == userColor[0].TextHigh |
                                                                                 userColor[0].InputText == userColor[0].ForeGroundDefault |
                                                                                 userColor[0].InputText == userColor[0].MenuSelectDefault);

                    Console.BackgroundColor = userColor[0].BackGroundDefault;
                    break;

                case 6:     // input text color
                    newColor = (int)userColor[0].InputText;
                    newColor++; if (newColor == 16) { newColor = 0; }
                    userColor[0].InputText = (ConsoleColor)newColor;
                    break;

                default:
                    break;
            }
        }

        public static void SetColor(TextColors textColor)                     // sets font and background color
        {
            // PrintOnConsole(((int)userColor[0].BackGroundDefault).ToString(), 1, 1);
            Console.BackgroundColor = userColor[0].BackGroundDefault;
            
            switch (textColor)
            {
                case TextColors.Input:

                    Console.ForegroundColor = userColor[0].InputText;
                    break;

                case TextColors.MenuSelect:

                    Console.ForegroundColor = userColor[0].MenuSelectDefault;
                    break;

                case TextColors.SystemMessage:
                    Console.ForegroundColor = userColor[0].SystemForeGround;
                    Console.BackgroundColor = userColor[0].SystemBackGround;
                    break;

                case TextColors.Text:

                    Console.ForegroundColor = userColor[0].TextHigh;
                    break;

                case TextColors.DefaultForeGround:                                        // Standard foreground color

                    Console.ForegroundColor = userColor[0].ForeGroundDefault;
                    break;

                case TextColors.DefaultBackGround:
                    Console.BackgroundColor = userColor[0].BackGroundDefault;
                    break;

                case TextColors.Inactive:
                    if (userColor[0].BackGroundDefault != ConsoleColor.DarkGray)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    break;

                case TextColors.Title:
                    Console.ForegroundColor = userColor[0].Title;
                    break;

                case TextColors.Inverted:
                    Console.ForegroundColor = userColor[0].BackGroundDefault;
                    Console.BackgroundColor = userColor[0].ForeGroundDefault;
                    break;

                case TextColors.Defaults:
                    Console.ForegroundColor = userColor[0].ForeGroundDefault;
                    Console.BackgroundColor = userColor[0].BackGroundDefault;

                    break;

                default:
                    break;
            }
        }
    }
}