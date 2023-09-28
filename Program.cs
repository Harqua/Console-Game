using System;

namespace WahooRPGGame
{
    class Program
    {
        class Character
        {
            public required string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public int Defense { get; set; }
            public int Level { get; set; }
            public int ExpPoints { get; set; }
            public int ExpToNextLvl()
            {
                return Level * 100;
            }
            public void LvlUp()
            {
                int nextLvlExp = ExpToNextLvl();
                if (ExpPoints >= nextLvlExp)
                {
                    Console.WriteLine("Congratulations! You leveled up!!!");
                    Console.WriteLine($"Level: {Level} -> {Level + 1}");
                    Console.WriteLine($"Attack: {Attack} -> {Attack + 4}");
                    Console.WriteLine($"Defense: {Defense} -> {Defense + 2}");
                    Level++;
                    Attack += 4;
                    Defense += 2;
                    ExpPoints -= nextLvlExp;

                }
            }
        }
        class Chapter
        {
            public int ChapterNumber { get; set; }
            public required string Title { get; set; }
            public required string Description { get; set; }
            public required Character PlayerChar { get; set; }
            public required Character Enemy { get; set; }

            public void StartChapter()
            {
                Console.WriteLine(Title);
                Console.WriteLine(Description);

                StartBattle();
            }
            private void StartBattle()
            {
                Console.Clear();
                bool isBattleOver = false;

                while (!isBattleOver)
                {
                    Console.WriteLine("===" + Title + "===");
                    Console.WriteLine(Description);
                    Console.WriteLine($"\nA wild {Enemy.Name} appeared");
                    DisplayChars();
                    DisplayBattleOptions();
                    string action = Console.ReadLine();

                    switch (action)
                    {
                        case "1":
                            PerformAttack();
                            break;
                        case "2":
                            PerformDefense();
                            break;
                        case "3":
                            PerformStatCheck();
                            break;
                        case "4":
                            isBattleOver = true;
                            PerformRun();
                            break;
                        default:
                            Console.WriteLine("Invalid action. Try Again.");
                            break;
                    }

                    //Check Battle Over
                    if (PlayerChar.Health <= 0)
                    {
                        isBattleOver = true;
                        Console.WriteLine("O dem u ded...");
                    }
                    else if (Enemy.Health <= 0)
                    {
                        isBattleOver = true;
                        Console.WriteLine($"Congratulations!!! You defeated The {Enemy.Name} and gained 100 XP Points");
                        PlayerChar.ExpPoints += 100;
                        PlayerChar.LvlUp();
                    }

                }


            }

            private void DisplayChars()
            {
                Console.WriteLine($"{PlayerChar.Name} Current Health: {PlayerChar.Health}");
                Console.WriteLine($"{Enemy.Name} Current Health: {Enemy.Health}");
            }
            
            private void DisplayBattleOptions()
            {
                Console.WriteLine("Choose your action:");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Defense");
                Console.WriteLine("3. Stats");
                Console.WriteLine("4. Run");
            }
            private void PerformAttack()
            {
                int damageDealt = PlayerChar.Attack - Enemy.Defense;
                if (damageDealt > 0)
                {
                    Enemy.Health -= damageDealt;
                    Console.WriteLine($"You dealt {damageDealt} Damage to the {Enemy.Name}");
                }
                else
                {
                    Console.WriteLine($"You dealt no damage to the {Enemy.Name}");
                }
                EnemyAttack();
            }
            private void PerformDefense()
            {
                PlayerChar.Defense += 5;
                Console.WriteLine("You take defensive stance!");
                EnemyAttack();
                PlayerChar.Defense -= 5;
            }
            private void PerformStatCheck()
            {
                int nextLevel = PlayerChar.Level *100; 
                Console.WriteLine($"==={PlayerChar.Name}===");
                Console.WriteLine($"Level: {PlayerChar.Level}");
                Console.WriteLine($"Attack: {PlayerChar.Attack}");
                Console.WriteLine($"Defense: {PlayerChar.Defense}");
                Console.WriteLine($"EXP Points: {PlayerChar.ExpPoints}/{PlayerChar.Level*100}");

                Console.WriteLine("\n");

                Console.WriteLine($"==={Enemy.Name}===");
                Console.WriteLine($"Level: {Enemy.Level}");
                Console.WriteLine($"Attack: {Enemy.Attack}");
                Console.WriteLine($"Defense: {Enemy.Defense}");
            }
            private void PerformRun()
            {
                Console.WriteLine("You ran from the battle!");

            }
            private void EnemyAttack()
            {
                int damageReceived = Enemy.Attack - PlayerChar.Defense;
                if (damageReceived > 0)
                {
                    PlayerChar.Health -= damageReceived;
                    Console.WriteLine($"Wahoo!! The {Enemy.Name} dealt {damageReceived} damage to you");

                }
                else
                {
                    Console.WriteLine($"The {Enemy.Name} dealt no damage");
                }

            }
        }

        static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("===Main Menu===");
            Console.WriteLine("1. New Game");
            Console.WriteLine("2. Load Game (Not Implemented Yet)");
            Console.WriteLine("3. Exit");
            Console.Write("Select an option: ");
            string input = Console.ReadLine();

            if (input == "1")
            {
                Console.WriteLine("Starting new game...");
            }
            else if (input == "2")
            {
                Console.WriteLine("Not Implemented Just Yet");
                DisplayMainMenu();
            }
            else if (input == "3")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Invalid Input. Try Again.");
                DisplayMainMenu();
            }
        }

        static void Main()
        {
            Console.Clear();
            //Game Start
            Console.WriteLine("===Welcome to WahooLand!===");
            Console.Write("Please enter your name: ");
            string heroName = Console.ReadLine();

            //Character Creation
            Character playerCharacter = new Character
            {
                Name = heroName,
                Health = 100,
                Attack = 10,
                Defense = 5,
                Level = 1
            };
            Character enemyChaptOne = new Character
            {
                Name = "Wahoo Fish",
                Health = 30,
                Attack = 8,
                Defense = 2
            };

            //Chapter 1 Creation
            Chapter chapter1 = new Chapter
            {
                ChapterNumber = 1,
                Title = "Chapter 1: WahooLand Destruction",
                Description = "This is the beginning",
                PlayerChar = playerCharacter,
                Enemy = enemyChaptOne
            };

            //Main Menu
            DisplayMainMenu();


            //Chapter 1 gameplay
            chapter1.StartChapter();



            //Game Ends
            Console.WriteLine("Thanks for playing");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }

}