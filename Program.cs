using RockPaperScissors;
using RockPaperScissors.AI;

PlayerController control = new PlayerController();

Player me = new HumanPlayer(new PlayerInfo("You"), control);
Player ai = new AlwaysRock(new PlayerInfo("AI"));

bool stop = false;
while (! stop)
{
	Console.WriteLine("Let's play!");

	Game g = new Game();
	
	for (int i = 1; i <= 10; i++)
	{
		Console.Write($"\n\t--- Round {i} ---\n\nChoose move: rps> ");

		Move myMove = me.GetNextMove();

		Console.Write($"{myMove}\n\n");
		g.Play(myMove, ai.GetNextMove());

		Console.WriteLine($"\n{g.lastRound}");
	}

	Console.WriteLine($"\n{g}\n");
	Console.WriteLine($"Play again?\n(y)n> ");

	bool keyOk = false;
	do
	{
		ConsoleKey k = control.GetKey();
		if (k == ConsoleKey.N || k == ConsoleKey.Escape)
		{
			stop = true;
		} else if (k == ConsoleKey.Y || k == ConsoleKey.Enter)
		{
			keyOk = true;
		}
	} while (! (keyOk ||stop));
}


