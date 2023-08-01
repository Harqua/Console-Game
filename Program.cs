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

                    Level++;
                    Console.WriteLine($"You are now level {Level}");
                    Console.WriteLine($"Attack: {Attack} -> {Attack + 4}");
                    Console.WriteLine($"Defense: {Defense} -> {Defense + 2}");
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
                Console.WriteLine($"A wild {Enemy.Name} appeared");
                bool isBattleOver = false;
                bool isLose = false;

                while (!isBattleOver)
                {
                    
                    DisplayStatus();
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
                        isLose = true;
                        Console.WriteLine("O dem u ded...");
                    }
                    else if (Enemy.Health <= 0)
                    {
                        isBattleOver = true;
                        Console.WriteLine($"Congratulations!!! You defeated The {Enemy.Name} and gained 100 XP Points");
                    }

                }
                if (!isLose)
                {
                    PlayerChar.ExpPoints += 100;
                    PlayerChar.LvlUp();
                }
            }

            private void DisplayStatus()
            {
                Console.WriteLine($"Your Current Health: {PlayerChar.Health}");
                Console.WriteLine($"Enemy Current Health: {Enemy.Health}");
            }
            private void DisplayBattleOptions()
            {
                Console.WriteLine("Choose your action:");
                Console.WriteLine("1. Attack");
                Console.WriteLine("2. Defense");
                Console.WriteLine("3. Run");
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
            private bool PerformRun()
            {
                Console.WriteLine("You ran from the battle!");
                return true;

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


        static void Main()
        {
            //Game Start
            Console.WriteLine("Welcome to WahooLand!");
            Console.Write("Please enter your name: ");
            string heroName = Console.ReadLine();

            //Character Creation
            Character playerCharacter = new Character
            {
                Name = heroName,
                Health = 100,
                Attack = 10,
                Defense = 5
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


            //Chapter 1 gameplay
            chapter1.StartChapter();
            Console.WriteLine($"Level: {playerCharacter.Level}");


            //Game Ends
            Console.WriteLine("Thanks for playing");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();

        }
    }

}