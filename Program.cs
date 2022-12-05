using RockPaperScissors;
using RockPaperScissors.AI;
using UI = RockPaperScissors.UI;

PlayerController control = new PlayerController();

/* Player me = new HumanPlayer(new PlayerInfo("You"), control); */
/* Player ai = new AlwaysRock(new PlayerInfo("AI")); */
Player me = new RepeatsYourLastMove(new PlayerInfo("AI 1"));
Player ai = new RepeatsYourLastMove(new PlayerInfo("AI 2"));

bool stop = false;
while (! stop)
{
	Game g = new Game(me, ai, 5, true);

	while (! g.over)
	{
		if (me is HumanPlayer)
		{
			Console.Write($"{g.p1}, choose move: [rps]> ");
		}

		Move myMove = me.GetNextMove();
		Move aiMove = ai.GetNextMove();

		g.Play(myMove, aiMove);
	}

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


