namespace RockPaperScissors
{
	// A match consisting of multiple games of RPS between two players
	public class Bout
	{
		public Player p1 { get; }
		public Player p2 { get; }

		public List<Game> games { get; }

		public Bout(Player p1, Player p2, int numberOfGames)
		{
			this.p1 = p1;
			this.p2 = p2;

			this.games = new List<Game>();
		}
	}
}
