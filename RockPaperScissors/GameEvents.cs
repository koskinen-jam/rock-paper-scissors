namespace RockPaperScissors
{
	// Event arguments for round events
	public class RoundEventArgs : EventArgs
	{
		public Round? Round { get; set; }
	}

	// Manager for handling Game event subscriptions
	public class GameEvents
	{
		// Subscribe the UI to a game
		public static void SubscribeGame(Game g)
		{
			g.RoundStarted += UI.ConsoleOutput.RoundStartedEventHandler;
			g.RoundEnded += UI.ConsoleOutput.RoundEndedEventHandler;
			g.GameStarted += UI.ConsoleOutput.GameStartedEventHandler;
			g.GameEnded += UI.ConsoleOutput.GameEndedEventHandler;
		}

		// Unsubscribe the UI from a game
		public static void UnsubscribeGame(Game g)
		{
			g.RoundStarted -= UI.ConsoleOutput.RoundStartedEventHandler;
			g.RoundEnded -= UI.ConsoleOutput.RoundEndedEventHandler;
			g.GameStarted -= UI.ConsoleOutput.GameStartedEventHandler;
			g.GameEnded -= UI.ConsoleOutput.GameEndedEventHandler;
		}
	}
}
