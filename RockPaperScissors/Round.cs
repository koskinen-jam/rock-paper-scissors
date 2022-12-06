namespace RockPaperScissors
{
	// A round of Rock Paper Scissors, including extra scoring rules invented
	// by elves.
	public class Round
	{
		public Player p1 { get; }
		public Player p2 { get; }
		public Move m1 { get; }
		public Move m2 { get; }

		public string outcome { get; }
		public Player? winner { get; }

		public (int p1, int p2) Points { get; }

		// Create and resolve a round of Rock Paper Scissors
		public Round(Player p1, Player p2, Move m1, Move m2)
		{
			this.p1 = p1;
			this.p2 = p2;
			this.m1 = m1;
			this.m2 = m2;

			switch (m1.CompareTo(m2))
			{
				case -1:
					outcome = $"{m1} loses to {m2}";
					Points = ((int)m1, (int)m2 + 6);
					winner = p2;
					Outcome = new Outcome(p2, (int) m1, 6 + (int) m2);
					break;
				case 0:
					outcome = $"{m1} ties with {m2}";
					Points = ((int)m1 + 3, (int)m2 + 3);
					winner = null;
					Outcome = new Outcome(null, 3 + (int) m1, 3 + (int) m2);
					break;
				case 1:
					outcome = $"{m1} beats {m2}";
					Points = ((int)m1 + 6, (int)m2);
					winner = p1;
					Outcome = new Outcome(p1, 6 + (int) m1, (int) m2);
					break;
				default:
					throw new Exception($"Bad comparison result for {m1} vs {m2}: {m1.CompareTo(m2)}");
			}		
		}

		// Convert this round into a string
		public override string ToString()
		{
			return $"{outcome}! {p1} (+{Points.p1}), {p2} (+{Points.p2})";
		}

		// Return the players move this round
		public Move MyMove(Player me)
		{
			return p1 == me ? m1 : m2;
		}

		// Return the opponents move this round
		public Move OpponentsMove(Player me)
		{
			return p1 == me ? m2 : m1;
		}
	}
}
