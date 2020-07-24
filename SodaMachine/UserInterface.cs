using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace SodaMachine
{
    public static class UserInterface
    {

        private static int heightOfSodaMachineOutline = 20;
        public static int topPadding = (Console.LargestWindowHeight - heightOfSodaMachineOutline) / 2;
        private static int leftPadding = 10;
        private static int paddingForRightHeader = leftPadding + 10;
        private static int leftPadGreenScreen = 74;
        private static int topPadGreenScreen = 13;


        // Hard Coded Situations

        public static void DisplayWelcomeScreen()
        {
            BackgroundCSV background = new BackgroundCSV();
            string welcomeOutput = RunReplacer(background.welcome);
            DrawCSVArt(welcomeOutput);
            WriteLiteralColor("ͰGRBL     Welcome, friend.\n\n    Would you care for\n\n         a Soda?\n\nͰGRGEPress any key to continues...Ω", 26, 9);
            Console.ReadKey(true);
        }
        public static void DisplayMainScreen()
        {
            BackgroundCSV background = new BackgroundCSV();
            string mainBackground = RunReplacer(background.background);
            DrawCSVArt(mainBackground);
        }

        public static string SodaSelectionScreen(List<Can> sodaSelection)
        {
            string sodaChoiceInput;
            Can sodaChoice = null;
            bool decided = false; // Runs the loops.

            WriteLiteralColor("ͰGRBL The old soda machine lets\n\nof a faint humming. In front\n\n of you, a refreshing cold,\n\n     ͰGRDGbeverage awaits.\n\nͰGRGEPress any key to continues...", 25, 8);
            Console.ReadKey(true);
            do
            {
                string message = "";
                message += "ͰGEBLWhich soda would you\nlike to purchase today?\n\n";

                for (int i = 0; i < sodaSelection.Count; i++)
                {
                    message += $"{i + 1}) {(i == 0 ? "ͰDMYE" : (i == 1 ? "ͰWHRE" : "ͰDYWH"))}{sodaSelection[i].name}ͰGEBL\n";
                }
                WriteLiteralColor(message, leftPadGreenScreen + 4, topPadGreenScreen + 2);
                bool valid = false;
                Console.SetCursorPosition(leftPadGreenScreen + 4, topPadGreenScreen + 10);
                sodaChoiceInput = Console.ReadLine();
                for (int i = 0; i < sodaSelection.Count; i++)
                {
                    if (sodaChoiceInput == $"{i + 1}" || sodaChoiceInput.ToLower() == sodaSelection[i].name.ToLower())
                    {
                        valid = true;
                        sodaChoice = sodaSelection[i];
                        ClearGreenScreen();
                        if (GetUserInputYesNo($"ͰGEBL{sodaSelection[i].name} is ${sodaSelection[i].Price:#.00}.\nAre you sure? (Y/N)", leftPadGreenScreen, topPadGreenScreen + 5))
                        {
                            decided = true;
                        }
                        else
                        {
                            ClearGreenScreen();
                        }
                    }
                }
                if (!valid)
                {
                    ClearGrayBox();
                    WriteLiteralColor("ͰGRREInvalid selection, try again.\nͰGRBLPress any key to continue...", 25, 12);
                    Console.ReadKey(true);
                    ClearGrayBox();
                    ClearGreenScreen();
                    continue;
                }
            } while (!decided || sodaChoice == null);
            return sodaChoice.name;



        }


        public static int AskForCoinAmount(string coinName, int amountInWallet)
        {
            bool valid = false;
            int userInput;
            do
            {
                ClearGreenScreen();
                WriteLiteralColor($"ͰGEBLYou have {amountInWallet} {(amountInWallet == 1 ? coinName : coinName + "s")} in your\nwallet .How many would you like to\ninsert into the soda machine?\n", leftPadGreenScreen, topPadGreenScreen + 5);
                Console.SetCursorPosition(leftPadGreenScreen, topPadGreenScreen + 8);
                valid = Int32.TryParse(Console.ReadLine(), out userInput);
                if (valid)
                {
                    if (userInput < 0 || userInput > amountInWallet)
                    {
                        WriteLiteralColor("You do not have that\nmany coins in your wallet.", leftPadGreenScreen, topPadGreenScreen + 10);
                        valid = false;
                        Console.ReadKey(true);
                    }
                }
            } while (!valid);
            DisplayChosenCoinAmount(coinName, userInput);
            ClearGreenScreen();
            WriteLiteralColor("Press Any Key...", leftPadGreenScreen, topPadGreenScreen + 10);
            Console.ReadKey(true);
            return userInput;
        }

        public static void DisplayChosenCoinAmount(string coinName, int userInput)
        {
            switch(coinName)
            {
                case "quarters":
                    WriteLiteralColor($"ͰGRBLQuarters: {userInput}", 25, 8);
                    break;
                case "dimes":
                    WriteLiteralColor($"ͰGRBLDimes: {userInput}", 25, 10);
                    break;
                case "nickels":
                    WriteLiteralColor($"ͰGRBLNickels: {userInput}", 25, 11);
                    break;
                case "pennies":
                    WriteLiteralColor($"ͰGRBLPennies: {userInput}", 25, 12);
                    break;
                default:
                    throw new Exception();
            }
        }

        public static void MachineValidating(string validationStep, string resultMessage, int orderInValidationProcess)
        {
            string validating = $"ͰGEBL{validationStep}";
            WriteLiteralColor(validating, leftPadGreenScreen, topPadGreenScreen + orderInValidationProcess * 2);
            string dots = "";
            for(int i = 0; i < 5; i++)
            {
                dots += ".";
                WriteLiteralColor(dots, leftPadGreenScreen + validating.Length + 1, topPadGreenScreen + orderInValidationProcess * 2);
                Thread.Sleep(400);
            }
            WriteLiteralColor($"ͰGEBL{resultMessage}", leftPadGreenScreen + 10, topPadGreenScreen + 2 + (orderInValidationProcess * 2));

        }

        public static void DisplayCan(string sodaName)
        {

            ClearGreenScreen();
            ClearGrayBox();
            WriteLiteralColor($"ͰGRBLPlease take your soda.", 25, 12);
            SetFullBackgroundColor(ConsoleColor.Gray);






        }








        // Display Messages, Text or List methods.
        /// <summary>
        /// Default Write string to screen. Best for single line strings. No new line at end of string.
        /// </summary>
        /// <param name="text">A single line string</param>
        public static void DisplayList(string text)
        {
            Console.WriteLine(text);
        }
        public static void DisplayList(string[] textLines, bool hasNewLineEscapeCharacter)
        {
            if (hasNewLineEscapeCharacter)
            {
                foreach (string text in textLines)
                {
                    Console.Write(text);
                }
            }
            else
            {
                foreach (string text in textLines)
                {
                    Console.WriteLine(text);
                }
            }
        }
        public static void DisplayList(string text, bool clearScreen)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(text);
        }
        public static void DisplayList(string text, bool clearScreen, bool showPressAnyKey)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(text);
            if (showPressAnyKey)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }
        public static void DisplayList(string text, bool clearScreen, bool showPressAnyKey, bool clearScreenAfterKeyPress)
        {
            if (clearScreen)
            {
                Console.Clear();
            }
            Console.WriteLine(text);
            if (showPressAnyKey)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
            if (clearScreen)
            {
                Console.Clear();
            }
        }

        public static void DisplayListOfCoins(List<Coin> coins)
        {
            int numberOfQuarters = 0;
            int numberOfDimes = 0;
            int numberOfNickels = 0;
            int numberOfPennies = 0;

            foreach (Coin coin in coins)
            {
                switch (coin.name)
                {
                    case "quarter":
                        numberOfQuarters++;
                        break;
                    case "dime":
                        numberOfDimes++;
                        break;

                    case "nickel":
                        numberOfNickels++;
                        break;
                    case "penny":
                        numberOfPennies++;
                        break;
                }
            }
            // Display contents
            string[] coinList = new string[] { $"Quarters: {numberOfQuarters}\n", $"Dimes: {numberOfDimes}\n", $"Nickels: {numberOfNickels}\n", $"Pennies: {numberOfPennies}\n" };
            DisplayList(coinList, true);
        }


        public static void PrintList(List<Coin> coins)
        {
            foreach (Coin coin in coins)
            {
                Console.WriteLine(coin.name);
            }
        }


        /// <summary>
        /// Asks the user for their selection. Once selected it displays the cost and asks for verification. If the change their mind the loop will start over.
        /// </summary>
        /// <returns>The name of the soda.</returns>
        public static string AskForSodaSelection(List<Can> sodaSelection) // TODO: Potentially Refactor to accept strings and not List<Can> unused objects.
        {
            string sodaChoiceInput;
            Can sodaChoice = null;
            bool decided = false; // Runs the loops.
            Console.SetCursorPosition(leftPadding + 25, topPadding - 1);
            TextAndBackgroundColor(ConsoleColor.Green, ConsoleColor.White);
            Console.WriteLine("The Soda Machine glows in front of you, before a cold beverage awaits!");
            do
            {
                Console.ResetColor();
                //DrawSodaMachineOutLine();
                TextAndBackgroundColor(ConsoleColor.White, ConsoleColor.Black);
                string message = "";
                message += "Which soda would you like to purchase today?\n";

                for (int i = 0; i < sodaSelection.Count; i++)
                {
                    message += $"{i + 1}) {sodaSelection[i].name}\n";
                }
                WriteLiteral(message, leftPadding + 35, topPadding + 4);
                bool valid = false;
                sodaChoiceInput = Console.ReadLine();
                for (int i = 0; i < sodaSelection.Count; i++)
                {
                    if (sodaChoiceInput == $"{i + 1}" || sodaChoiceInput.ToLower() == sodaSelection[i].name.ToLower())
                    {
                        valid = true;
                        sodaChoice = sodaSelection[i];
                        if (GetUserInputYesNo($"{sodaSelection[i].name} is ${sodaSelection[i].Price:#.00}. Are you sure? (Y/N)", false, leftPadding + 35, topPadding + 10))
                        {
                            decided = true;
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                }
                if (!valid)
                {
                    Console.SetCursorPosition(leftPadding, topPadding + 11);
                    Console.WriteLine("Invalid selection, try again. Press any key to continue...");
                    Console.ReadKey(true);
                    Console.Clear();
                    continue;
                }
            } while (!decided || sodaChoice == null);
            return sodaChoice.name;
        }


       



        // Positional Based UI Methods

        //public static void WriteLiteral(string msg, int left, int top)
        //{
        //    int pos = 0;
        //    msg = msg.Replace("\n", "¶");

        //    Console.SetCursorPosition(left, top);
        //    foreach (char letter in msg)
        //    {
        //        if (letter == '¶')
        //        {
        //            top += 1;
        //            Console.SetCursorPosition(left, top);
        //        }
        //        else
        //        {
        //            Console.Write(msg[pos]);
        //        }
        //        pos++;
        //    }
        //}

        /*
         * Display a soda machine outline- OutLine X Graphics O
         * Display welcome message either to the right or bottom.
         * 
         * 
         * 
         * Display Thank you and Good Bye
         */




        // Get user input methods, YES/NO, Multiple Choice


        public static bool GetUserInputYesNo(string message, bool clearScreen)
        {
            string userChoice;
            while (true)
            {
                if (clearScreen)
                {
                    Console.Clear();
                }
                Console.Write(message);
                userChoice = Console.ReadLine().ToLower();
                if (userChoice == "y" || userChoice == "yes")
                {
                    return true;
                }
                else if (userChoice == "n" || userChoice == "no")
                {
                    return false;
                }
            }
        }

        public static bool GetUserInputYesNo(string message, int leftPad, int topPad)
        {
            string userChoice;
            while (true)
            {
                ClearGreenScreen();
                ClearGrayBox();
                WriteLiteralColor(message, leftPad, topPad);
                Console.SetCursorPosition(leftPad, Console.CursorTop + 1);
                userChoice = Console.ReadLine().ToLower();
                if (userChoice == "y" || userChoice == "yes")
                {
                    return true;
                }
                else if (userChoice == "n" || userChoice == "no")
                {
                    return false;
                }
            }
        }
        //Depreciated
        public static bool GetUserInputYesNo(string message, bool clearScreen, int leftPad, int topPad)
        {
            string userChoice;
            while (true)
            {
                if (clearScreen)
                {
                    Console.Clear();
                }
                WriteLiteral(message, leftPad, topPad);
                Console.SetCursorPosition(leftPad, topPad + 1);
                userChoice = Console.ReadLine().ToLower();
                if (userChoice == "y" || userChoice == "yes")
                {
                    return true;
                }
                else if (userChoice == "n" || userChoice == "no")
                {
                    return false;
                }
            }
        }


        // Decoration


        private static void TextColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }
        private static void TextAndBackgroundColor(ConsoleColor textColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
        }
        /// <summary>
        /// Draw a
        /// </summary>
        public static void DrawScreenDivider()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(new string('█', Console.WindowWidth));
            Console.ResetColor();
            Console.WriteLine();
        }
        public static void SetFullBackgroundColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.Clear();
            Console.ResetColor();
        }
        public static void WriteLiteral(string msg, int left, int top)
        {
            int pos = 0;
            msg = msg.Replace("\n", "¶");

            Console.SetCursorPosition(left, top);
            foreach (char letter in msg)
            {
                if (letter == '¶')
                {
                    top += 1;
                    Console.SetCursorPosition(left, top);
                }
                else
                {
                    Console.Write(msg[pos]);
                }
                pos++;
            }
        }
        public static void WriteLiteralColor(string msg, int left, int top)
        {
            int pos = 0;
            msg = msg.Replace("\n", "¶");

            Console.SetCursorPosition(left, top);
            for (int i = 0; i < msg.Length; i++)
            {
                if (msg[i] == '¶')
                {
                    top += 1;
                    Console.SetCursorPosition(left, top);
                }
                else if (msg[i] == 'Ͱ')
                {
                    ColorParser(msg.Substring(i + 1, 4));
                    i += 4;
                }
                else if (msg[i] == 'Ω')
                {
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(msg[i]);
                }
                pos++;
            }
        }
        public static void ColorParser(string colorCode)
        {
            //Background color
            switch (colorCode.Substring(0, 2))
            {
                case "BL":
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case "BU":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case "CY":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    break;
                case "DB":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    break;
                case "DC":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    break;
                case "DG":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case "DE":
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
                case "DM":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "DR":
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    break;
                case "DY":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    break;
                case "GR":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case "GE":
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case "MA":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    break;
                case "RE":
                    Console.BackgroundColor = ConsoleColor.Red;
                    break;
                case "WH":
                    Console.BackgroundColor = ConsoleColor.White;
                    break;
                case "YE":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    throw new Exception("Color code incorrect.");
            }

            //Foreground Color/ Text Color
            switch (colorCode.Substring(2, 2))
            {
                case "BL":
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                case "BU":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case "CY":
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case "DB":
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case "DC":
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    break;
                case "DG":
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    break;
                case "DE":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                case "DM":
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    break;
                case "DR":
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case "DY":
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case "GR":
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case "GE":
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case "MA":
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "RE":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "WH":
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case "YE":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    throw new Exception("Color code incorrect.");
            }
        }
        public static void DrawCSVArt(string csvArt)
        {
            string[] splitCSV = csvArt.Split(',');

            for (int i = 0; i < splitCSV.Length; i += 5)
            {
                Console.SetCursorPosition(Convert.ToInt32(splitCSV[i]), Convert.ToInt32(splitCSV[i + 1]));

                switch (splitCSV[i + 4])
                {
                    case "BL":
                        Console.BackgroundColor = ConsoleColor.Black;
                        break;
                    case "BU":
                        Console.BackgroundColor = ConsoleColor.Blue;
                        break;
                    case "CY":
                        Console.BackgroundColor = ConsoleColor.Cyan;
                        break;
                    case "DB":
                        Console.BackgroundColor = ConsoleColor.DarkBlue;
                        break;
                    case "DC":
                        Console.BackgroundColor = ConsoleColor.DarkCyan;
                        break;
                    case "DG":
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        break;
                    case "DE":
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        break;
                    case "DM":
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case "DR":
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        break;
                    case "DY":
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                        break;
                    case "GR":
                        Console.BackgroundColor = ConsoleColor.Gray;
                        break;
                    case "GE":
                        Console.BackgroundColor = ConsoleColor.Green;
                        break;
                    case "MA":
                        Console.BackgroundColor = ConsoleColor.Magenta;
                        break;
                    case "RE":
                        Console.BackgroundColor = ConsoleColor.Red;
                        break;
                    case "WH":
                        Console.BackgroundColor = ConsoleColor.White;
                        break;
                    case "YE":
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        break;
                    default:
                        throw new Exception("Color code incorrect.");
                }

                //Foreground Color/ Text Color
                switch (splitCSV[i + 3])
                {
                    case "BL":
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case "BU":
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case "CY":
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        break;
                    case "DB":
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        break;
                    case "DC":
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        break;
                    case "DG":
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        break;
                    case "DE":
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case "DM":
                        Console.ForegroundColor = ConsoleColor.DarkMagenta;
                        break;
                    case "DR":
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case "DY":
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case "GR":
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case "GE":
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case "MA":
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case "RE":
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case "WH":
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case "YE":
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    default:
                        throw new Exception("Color code incorrect.");
                }
                Console.Write(splitCSV[i + 2]);
            }

        }
        public static string RunReplacer(string design)
        {

            string output = replaceAsciiToUnicode(design);
            output = colorCodes(output);
            output = ReplaceNewLineWithComma(output);
            return output;

        }
        public static string replaceAsciiToUnicode(string design)
        {

            string output = Regex.Replace(design, ",36,#", ",\u0024,#");
            output = Regex.Replace(output, ",83,#", ",\u0053,#");
            output = Regex.Replace(output, ",115,#", ",\u0073,#");
            output = Regex.Replace(output, ",59,#", ",\u003B,#");
            output = Regex.Replace(output, ",46,#", ",\u002E,#");
            output = Regex.Replace(output, ",44,#", ",\u00B8,#");
            output = Regex.Replace(output, ",32,#", ",\u0020,#");
            output = Regex.Replace(output, ",218,#", ",\u250C,#");
            output = Regex.Replace(output, ",219,#", ",\u2588,#");
            output = Regex.Replace(output, ",196,#", ",\u2500,#");
            output = Regex.Replace(output, ",191,#", ",\u2510,#");
            output = Regex.Replace(output, ",179,#", ",\u2502,#");
            output = Regex.Replace(output, ",0,#", ",\u0020,#");
            output = Regex.Replace(output, ",37,#", ",\u0025,#");
            output = Regex.Replace(output, ",33,#", ",\u0021,#");
            output = Regex.Replace(output, ",38,#", ",\u0026,#");
            output = Regex.Replace(output, ",64,#", ",\u0064,#");
            output = Regex.Replace(output, ",35,#", ",\u0023,#");
            output = Regex.Replace(output, ",205,#", ",\u2550,#");
            output = Regex.Replace(output, ",201,#", ",\u2554,#");
            output = Regex.Replace(output, ",187,#", ",\u2557,#");
            output = Regex.Replace(output, ",192,#", ",\u2514,#");
            output = Regex.Replace(output, "217,#", ",\u2518,#");
            output = Regex.Replace(output, ",186,#", ",\u2551,#");
            output = Regex.Replace(output, ",176,#", ",\u2591,#");
            output = Regex.Replace(output, ",178,#", ",\u2593,#");
            output = Regex.Replace(output, ",177,#", ",\u2592,#");
            output = Regex.Replace(output, ",179,#", ",\u2593,#");
            output = Regex.Replace(output, ",79,#", ",\u004f,#");
            output = Regex.Replace(output, ",68,#", ",\u0044,#");
            output = Regex.Replace(output, ",65,#", ",\u0041,#");
            output = Regex.Replace(output, ",204,#", ",\u2560,#");
            output = Regex.Replace(output, ",185,#", ",\u2563,#");
            output = Regex.Replace(output, ",200,#", ",\u255A,#");
            output = Regex.Replace(output, ",188,#", ",\u255D,#");
            output = Regex.Replace(output, ",202,#", ",\u2569,#");
            output = Regex.Replace(output, ",47,#", ",\u002F,#");
            output = Regex.Replace(output, ",92,#", ",\u005C,#");
            output = Regex.Replace(output, ",42,#", ",\u002A,#");
            output = Regex.Replace(output, ",43,#", ",\u002B,#");
            output = Regex.Replace(output, ",15,#", ",\u00A2,#");
            output = Regex.Replace(output, ",134,#", ",\u00E5,#");
            output = Regex.Replace(output, ",135,#", ",\u00E7,#");
            output = Regex.Replace(output, ",136,#", ",\u00EA,#");
            output = Regex.Replace(output, ",0,#", ",\u0020,#");
            output = Regex.Replace(output, ",9,#", ",\u25CB,#");
            return output;

        }
        public static string colorCodes(string design)
        {

            string output = Regex.Replace(design, "#55FFFF", "DG");
            output = Regex.Replace(design, "#000000", "BL");
            output = Regex.Replace(output, "#FFFFFF", "WH");
            output = Regex.Replace(output, "#555555", "DG");
            output = Regex.Replace(output, "#AAAAAA", "GR");
            output = Regex.Replace(output, "#55FFFF", "CY");
            output = Regex.Replace(output, "#00AAAA", "DC");
            output = Regex.Replace(output, "#5555FF", "BU");
            output = Regex.Replace(output, "#AA0000", "DR");
            output = Regex.Replace(output, "#FF5555", "RE");
            output = Regex.Replace(output, "#00AA00", "DE");
            output = Regex.Replace(output, "#55FF55", "GE");
            output = Regex.Replace(output, "#AA5500", "DY");
            output = Regex.Replace(output, "#FFFF55", "YE");
            output = Regex.Replace(output, "#AA00AA", "DM");
            output = Regex.Replace(output, "#FF55FF", "MA");
            output = Regex.Replace(output, "#0000AA", "DB");

            return output;

        }
        public static string ReplaceNewLineWithComma(string design)
        {

            string output = design.Replace("\\r\\n", ",");
            return output;

        }


        public static void ClearGrayBox()
        {
            for (int i = 0; i < 11; i++)
            {
                WriteLiteralColor("ͰGRBL                                 \n", 23, 7 + i);
            }                           
        }
        public static void ClearGreenScreen()
        {
            for (int i = 0; i < 14; i++)
            {
                WriteLiteralColor("ͰGEDE▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒\n", 74, 13 + i);
            }                           
        }










        #region Unused/ Depreciated


        //public static void DisplayListOfCoins(List<Coin> coins, string firstLineText, string lineTextModifier)
        //{
        //    int numberOfQuarters = 0;
        //    int numberOfDimes = 0;
        //    int numberOfNickels = 0;
        //    int numberOfPennies = 0;

        //    foreach (Coin coin in coins)
        //    {
        //        switch (coin.name)
        //        {
        //            case "quarter":
        //                numberOfQuarters++;
        //                break;
        //            case "dime":
        //                numberOfDimes++;
        //                break;

        //            case "nickel":
        //                numberOfNickels++;
        //                break;
        //            case "penny":
        //                numberOfPennies++;
        //                break;
        //        }
        //    }
        //    // Display contents
        //    // Change from singular to plural. Zero will use the plural form given English something rule. 0 coins, not 0 coin.
        //    string[] coinList = new string[]
        //    {
        //        $"{firstLineText}\n",
        //        $"{lineTextModifier} {numberOfQuarters} {(numberOfQuarters == 1 ? "quarter": "quarters" )}.\n",
        //        $"{lineTextModifier} {numberOfDimes} {(numberOfDimes == 1 ? "dime": "dimes" )}.\n",
        //        $"{lineTextModifier} {numberOfNickels} {(numberOfNickels == 1 ? "nickel": "nickels" )}.\n",
        //        $"{lineTextModifier} {numberOfPennies} {(numberOfPennies == 1 ? "penny": "pennies" )}.\n",
        //    };
        //    DisplayList(coinList, true);
        //}

        //public static void DisplayContentsOfCustomerWallet(Customer customer)
        //{
        //    int numberOfQuarters = 0;
        //    int numberOfDimes = 0;
        //    int numberOfNickels = 0;
        //    int numberOfPennies = 0;
        //    List<Coin> coins = customer.CheckCustomerWalletContents();
        //    foreach (Coin coin in coins)
        //    {
        //        switch (coin.name)
        //        {
        //            case "quarter":
        //                numberOfQuarters++;
        //                break;
        //            case "dime":
        //                numberOfDimes++;
        //                break;

        //            case "nickel":
        //                numberOfNickels++;
        //                break;
        //            case "penny":
        //                numberOfPennies++;
        //                break;
        //        }
        //    }
        //    // Display contents
        //}

        //public static void DisplayList(List<Coin> coins)
        //{
        //    List<string> names = new List<string>();
        //    int counter = 0;
        //    Console.WriteLine("This list contains:");
        //    for (int i = 0; i < coins.Count; i++)
        //    {
        //        if (!names.Contains(coins[i].name))
        //        {
        //            counter++;
        //            names.Add(coins[i].name);
        //            Console.WriteLine($"{counter}) {coins[i].name}");
        //        }
        //    }
        //}
        //public static void DisplayList(List<Can> cans)
        //{
        //    List<string> lineText = new List<string>();
        //    int counter = 0;
        //    Console.WriteLine("This Soda Machine offers: ");
        //    for (int i = 0; i < cans.Count; i++)
        //    {
        //        if (!lineText.Contains(cans[i].name))
        //        {
        //            counter++;
        //            lineText.Add(cans[i].name);
        //            Console.WriteLine($"{counter + 1}) {cans[i].name}");
        //        }
        //    }
        //}


        #endregion
    }
}
