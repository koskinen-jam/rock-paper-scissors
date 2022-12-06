namespace RockPaperScissors.UI
{

	// Formatters for game event messages
	public class GameMessages
	{
		public static string RoundStarted(Round r)
		{
			return $"New round";
		}

		public static string RoundEnded(Round r)
		{
			return ($"[Round: {r.p1.info.name} vs {r.p2.info.name}: {r.outcome}"
				+ (r.winner == null ? "." : $" - {r.winner.info.name} wins!]"));
		}

		public static string GameStarted(Game g)
		{
			return $"\n-- {g.p1.info.name, 10} vs {g.p2.info.name, -10} --";
		}

		public static string GameEnded(Game g)
		{
			return $"\t{g}";
		}
	}

	// Event handlers for outputting messages for game events
	public class ConsoleOutput
	{

		public static void RoundStartedEventHandler(object? sender, RoundEventArgs e)
		{
			if (((Game)sender!).IsObserved)
			{
				Console.WriteLine(GameMessages.RoundStarted(e.Round!));
			}
		}

		public static void RoundEndedEventHandler(object? sender, RoundEventArgs e)
		{
			if (((Game)sender!).IsObserved)
			{
				Console.WriteLine(GameMessages.RoundEnded(e.Round!));
			}
		}

		public static void GameStartedEventHandler(object? sender, EventArgs e)
		{
			Game g = (Game) sender!;
			if (g.IsObserved)
			{
				Console.WriteLine(GameMessages.GameStarted(g));
			}
		}

		public static void GameEndedEventHandler(object? sender, EventArgs e)
		{
			Game g = (Game) sender!;
			if (g.IsObserved)
			{
				Console.WriteLine(GameMessages.GameEnded(g));
			}
		}
	}
}
