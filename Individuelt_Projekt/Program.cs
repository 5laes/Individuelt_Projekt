using System;

namespace Individuelt_Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName;
            bool isLoggedIn;
            string[,] users = new string[5, 2];

            users[0, 0] = "Claes";
            users[0, 1] = "13337";

            users[1, 0] = "Krille";
            users[1, 1] = "42069";

            users[2, 0] = "Linus";
            users[2, 1] = "19999";

            users[3, 0] = "Shaoupa";
            users[3, 1] = "69696";

            users[4, 0] = "Alex";
            users[4, 1] = "12345";

            //Claes pengar
            uint[] claesMoney = new uint[1];
            claesMoney[0] = 3200; //kort

            //Krilles pengar
            uint[] krilleMoney = new uint[2];
            krilleMoney[0] = 1000; //kort
            krilleMoney[1] = 300000; //sparkonto        

            //Linus pengar
            uint[] linusMoney = new uint[3];
            linusMoney[0] = 500; //kort
            linusMoney[1] = 0; //sparkonto
            linusMoney[2] = 870300; //pension

            //Shaoupas pengar
            uint[] shoupaMoney = new uint[4];
            shoupaMoney[0] = 100000; //kort
            shoupaMoney[1] = 1000000; //sparkonto
            shoupaMoney[2] = 200; //pension
            shoupaMoney[3] = 30000; //nödspar


            //Alex pengar
            uint[] alexMoney = new uint[5];
            alexMoney[0] = 200; //kort
            alexMoney[1] = 70; //sparkonto
            alexMoney[2] = 2500; //pension
            alexMoney[3] = 2500; //nödpart
            alexMoney[4] = 2500; //aktiekonto

            userName = ProgramStart();
            isLoggedIn = StartLogIn(users, userName);

            while (isLoggedIn == true)
            {
                Console.Write($"\n\tVad vill du göra?" +
                $"\n\t[1] Se dina konton och saldo" +
                $"\n\t[2] Överför pengar mellan konton" +
                $"\n\t[3] Ta ut pengar" +
                $"\n\t[4] Logga ut" +
                $"\n\t: ");
                int.TryParse(Console.ReadLine(), out int userChoice);
                Console.Clear();

                switch (userChoice)
                {
                    case 1:
                        //ShowUserMoney(user);
                        break;
                    case 2:
                        //TransfereUserMoney(user);
                        break;
                    case 3:
                        //WithdrawUserMoney(user);
                        break;
                    case 4:
                        userName = ProgramStart();
                        isLoggedIn = StartLogIn(users, userName);
                        break;
                    default:
                        Console.Write("\n\tOgiltigt val försök igen!");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }

        static string ProgramStart()
        {
            Console.Clear();
            Console.Write("\n\tVälkommen till banken!" +
                "\n\tTryck enter för att logga in.");
            Console.ReadLine();
            Console.Write("\n\tAnge Användarnamn: ");
            string userName = Console.ReadLine();
            Console.Clear();
            return userName;
        }

        static bool StartLogIn(string[,] users, string userName)
        {


            for (int i = 2; i >= 0; i--)
            {
                Console.Write("\n\tAnge Lösenord: ");
                string password = ReadAndHidePassword();
                Console.Clear();

                if (DoesUserExist(users, userName, password) == true)
                {
                    Console.Write($"\n\tVälkommen {userName}!");
                    return true;
                }
                else
                {
                    if (i == 0)
                    {
                        Console.Write("\n\tDu har slut på inloggnings försök!" +
                            "\n\tProgrammet stängs av!");
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.Write($"\n\tFel lösenord!" +
                            $"\n\tDu har {i} försök kvar!" +
                            $"\n\tTryck enter för att försöka igen!");
                        Console.ReadLine();
                    }
                }
            }
            return false;
        }

        static bool DoesUserExist(string[,] users, string userName, string password)
        {
            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (userName == users[i, 0] && password == users[i, 1])
                {
                    return true;
                }
            }
            return false;
        }

        public static string ReadAndHidePassword()
        {
            string password = "";
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                //Skriver en "*" istället för den angivna karaktären för att gömma texten
                if (key.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += key.KeyChar;
                }
                //Om man trycker backspace
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        //Tar bort den sista karaktären i lösenordet
                        password = password.Substring(0, password.Length - 1);

                        //Tar bort den sista "*" i konsolrutan så man
                        //ser att man har använt backspace
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                key = Console.ReadKey(true);
            }
            return password;
        }
    }
}
