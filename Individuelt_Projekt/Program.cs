using System;

namespace Individuelt_Projekt
{
    class Program
    {
        static void Main(string[] args)
        {
            string userName;
            bool isLoggedIn;
            //Sträng array med alla användare och deras lösenord
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

            //float array för penga kontona
            float[,] userMoney = new float[5, 5];

            //Claes Konton
            userMoney[0, 0] = 1000.25f;

            //Krilles konton
            userMoney[1, 0] = 1337.42f;
            userMoney[1, 1] = 420.69f;

            //Linus konton
            userMoney[2, 0] = 50.50f;
            userMoney[2, 1] = 3.50f;
            userMoney[2, 2] = 9876.54f;

            //shaoupas konton
            userMoney[3, 0] = 100000.20f;
            userMoney[3, 1] = 8000.50f;
            userMoney[3, 2] = 9856.32f;
            userMoney[3, 3] = 845.75f;

            //Alex konton
            userMoney[4, 0] = 895.50f;
            userMoney[4, 1] = 1354.85f;
            userMoney[4, 2] = 55.55f;
            userMoney[4, 3] = 654.78f;
            userMoney[4, 4] = 13.37f;


            //sträng och bool som sparar användarnamn och om användaren
            //lyckades logga in
            userName = ProgramStart();
            isLoggedIn = StartLogIn(users, userName);

            //huvudmenyn som visas när användaren loggas in
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

                //switch som kallar på olika metoder
                switch (userChoice)
                {
                    case 1:
                        ShowUserMoney(userMoney, users, userName);
                        break;
                    case 2:
                        TransfereUserMoney(userMoney, users, userName);
                        break;
                    case 3:
                        WithdrawUserMoney(userMoney, users, userName);
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

        //metod för att ta ut pengar
        static void WithdrawUserMoney(float[,] userMoney, string[,] users, string userName)
        {
            string[] accountType = new string[5];
            accountType[0] = "Kort";
            accountType[1] = "Sparkonto";
            accountType[2] = "Pensionsparkonto";
            accountType[3] = "Semestersparkonto";
            accountType[4] = "Nödsparkonto";

            //får ett index beroende på användaren
            int userIndex = WhatUserIsLoggedIn(users, userName);

            //loop som skriver ut alla konton
            for (int i = 0; i < userMoney.GetLength(0); i++)
            {
                Console.Write($"\n\t[{i + 1}]{accountType[i]}: {userMoney[userIndex, i]}kr");
            }

            //frågar användaren vilket konto och hur mycket penfar som ska tas ut
            Console.Write("\n\tVilket konto vill du ta ut pengar från?" +
                "\n\t: ");
            int.TryParse(Console.ReadLine(), out int withdrawl);
            Console.Write("\n\tHur mycket pengar vill du ta ut?" +
                "\n\t: ");
            float.TryParse(Console.ReadLine(), out float ammount);

            //kollar så att mängden pengar finns
            if (ammount > userMoney[userIndex, withdrawl - 1])
            {
                Console.Write("\n\tFinns inte så mycket pengar på det angiva kontot" +
                    "\n\tFörsök igen!");
                Console.ReadLine();
                Console.Clear();
                WithdrawUserMoney(userMoney, users, userName);
            }
            //kollar så att kontot finns
            if (withdrawl - 1 > 4)
            {
                Console.Write("\n\tDe angiva kontonen finns inte" +
                    "\n\tFörsök igen!");
                Console.ReadLine();
                Console.Clear();
                WithdrawUserMoney(userMoney, users, userName);
            }
            //frågar användaren efter lösenord sen tar ut pengarna
            else
            {
                Console.Write($"\n\tAnge ditt lösenord för att bekräfta uttaget!" +
                    $"\n\t:");
                string password = Console.ReadLine();
                bool correctPassword = DoesUserExist(users, userName, password);

                if (correctPassword == false)
                {
                    Console.Write($"\n\tFel lösenord bekreftandet misslyckades" +
                        $"\n\tFörsök igen!");
                    Console.ReadLine();
                    WithdrawUserMoney(userMoney, users, userName);
                }
                else
                {
                    userMoney[userIndex, withdrawl - 1] = userMoney[userIndex, withdrawl - 1] - ammount;
                    Console.Write($"\n\tDu har nu tagit ut {ammount}kr från {accountType[withdrawl-1]}" +
                        $"\n\t{accountType[withdrawl - 1]} har nu {userMoney[userIndex, withdrawl-1]}kr i sig");
                }
            }
            Console.ReadLine();
            Console.Clear();
        }

        //metod för att visa pengarna på kontona
        static void ShowUserMoney(float[,] userMoney, string[,] users, string userName)
        {
            string[] accountType = new string[5];
            accountType[0] = "Kort";
            accountType[1] = "Sparkonto";
            accountType[2] = "Pensionsparkonto";
            accountType[3] = "Semestersparkonto";
            accountType[4] = "Nödsparkonto";

            //får ett index beroende på användaren
            int userIndex = WhatUserIsLoggedIn(users, userName);

            //en loop som skriver ut alla konton med pengar på
            for (int i = 0; i < userMoney.GetLength(0); i++)
            {
                if (userMoney[userIndex, i] != 0)
                {
                    Console.Write($"\n\tDu har {userMoney[userIndex, i]}kr på ditt {accountType[i]}");
                }
            }
            Console.ReadLine();
            Console.Clear();
        }

        //metod för att föra över pengar mellan konton
        static void TransfereUserMoney(float[,] userMoney, string[,] users, string userName)
        {
            string[] accountType = new string[5];
            accountType[0] = "Kort";
            accountType[1] = "Sparkonto";
            accountType[2] = "Pensionsparkonto";
            accountType[3] = "Semestersparkonto";
            accountType[4] = "Nödsparkonto";

            //får ett index beroende på användaren
            int userIndex = WhatUserIsLoggedIn(users, userName);

            //loop som skriver ut alla konton
            for (int i = 0; i < userMoney.GetLength(0); i++)
            {
                    Console.Write($"\n\t[{i+1}]{accountType[i]}: {userMoney[userIndex, i]}kr");
            }

            //frågar användaren vilka konton och summa som ska föras över
            Console.Write("\n\tVilket konto vill du flytta pengar från?" +
                "\n\t: ");
            int.TryParse(Console.ReadLine(), out int withdrawl);
            Console.Write("\n\tVilket konto vill du flytta pengar till?" +
                "\n\t: ");
            int.TryParse(Console.ReadLine(), out int depossit);
            Console.Write("\n\tHur mycket pengar vill du flytta?" +
                "\n\t: ");
            float.TryParse(Console.ReadLine(), out float ammount);

            //kollar så att summan finns
            if (ammount > userMoney[userIndex, withdrawl-1])
            {
                Console.Write("\n\tFinns inte så mycket pengar på det angiva kontot" +
                    "\n\tFörsök igen!");
                Console.ReadLine();
                Console.Clear();
                TransfereUserMoney(userMoney, users, userName);
            }
            //kollar så att kontona finns
            if (withdrawl-1 > 4 || depossit-1 > 4)
            {
                Console.Write("\n\tNågot av de angiva kontonen finns inte" +
                    "\n\tFörsök igen!");
                Console.ReadLine();
                Console.Clear();
                TransfereUserMoney(userMoney, users, userName);
            }
            //för över pengarn
            else
            {
                userMoney[userIndex, withdrawl-1] = userMoney[userIndex, withdrawl-1] - ammount;
                userMoney[userIndex, depossit-1] = userMoney[userIndex, depossit - 1] + ammount;
                Console.Write($"\n\t{ammount}kr har nu flyttats från ditt {accountType[withdrawl - 1]} till ditt {accountType[depossit - 1]}" +
                    $"\n\t{accountType[withdrawl - 1]} har nu {userMoney[userIndex, withdrawl - 1]}kr i sig" +
                    $"\n\t{accountType[depossit - 1]} har nu {userMoney[userIndex, depossit - 1]}kr i sig");
            }
            
            Console.ReadLine();
            Console.Clear();
        }

        //metod som skickar ut en int beroende på vilken användare som är inne
        static int WhatUserIsLoggedIn(string[,] users, string userName)
        {
            int userIndex = 10;
            for (int i = 0; i < users.GetLength(0); i++)
            {
                if (users[i, 0] == userName)
                {
                    userIndex = i;
                }
            }
            return userIndex;
        }

        //metod som startar programmet
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

        //metod för att logga in, körs max 3 gånger
        static bool StartLogIn(string[,] users, string userName)
        {
            for (int i = 2; i >= 0; i--)
            {
                Console.Write("\n\tAnge Lösenord: ");
                string password = Console.ReadLine();
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

        //Metod som kollar om användarnamn och lösenord stämmer med varandra
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


        //Metod som gömmer lösenordet när man skriver in det
        //denna är kopierad från google
        //ändra Console.Readline(); vid password strängen i StartLogIn metoden
        //till denna metod för att köra den (rad 291)
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
