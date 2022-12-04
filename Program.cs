using RockPaperScissors;
using RockPaperScissors.AI;

PlayerController control = new PlayerController();

Player me = new HumanPlayer(new PlayerInfo("You"), control);
/* Player ai = new AlwaysRock(new PlayerInfo("AI")); */
Player ai = new RepeatsYourLastMove(new PlayerInfo("AI"));

bool stop = false;
while (! stop)
{
	Console.WriteLine("Let's play!");

	Game g = new Game(me, ai, 5);
	
	while (! g.over)
	{
		Console.WriteLine($"\t--- Round {g.currentRound} ---");

		Console.Write($"{g.p1}, choose move: [rps]> ");

		Move myMove = me.GetNextMove();

		Console.WriteLine($"{myMove}");
		
		Move aiMove = ai.GetNextMove();

		Console.WriteLine($"{ai.info.name} chose {aiMove}");

		g.Play(myMove, aiMove);

		Console.WriteLine($"{g.lastRound}");
	}

	Console.WriteLine($"\n{g}\n");
	Console.WriteLine($"Play again? [(y)n]> ");

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


