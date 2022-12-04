namespace RockPaperScissors
{
	// Possible player moves in RPS
	public enum MoveType
	{
		Rock = 1,
		Paper = 2,
		Scissors = 3
	}

	// Single player choice for a round, comparable to another
	// players move to determine which is better.
	public class Move
	{
		public MoveType type { get; }

		// Parse a Move
		public Move(string m)
		{
			if (m == "A" || m == "X")
			{
				this.type = MoveType.Rock;
			}
			else if (m == "B" || m == "Y")
			{
				this.type = MoveType.Paper;
			}
			else if (m == "C" || m == "Z")
			{
				this.type = MoveType.Scissors;
			}
			else
			{
				throw new Exception($"Cannot parse \"{m}\" as a Move");
			}
		}

		// Create a Move
		public Move(MoveType m)
		{
			this.type = m;
		}

		// Create a move from console key press.
		public static Move? FromConsoleKey(ConsoleKey key)
		{
			switch (key)
			{
				case ConsoleKey.R:
					return new Move(MoveType.Rock);
				case ConsoleKey.P:
					return new Move(MoveType.Paper);
				case ConsoleKey.S:
					return new Move(MoveType.Scissors);
				default:
					return null;		
			}
		}

		// Stringify this move
		public override string ToString()
		{
			return $"{type}";
		}

		// Convert Move to the value of its shape
		public static implicit operator int(Move m)
		{
			return (int)m.type;
		}

		// Compare this move to another to determine which one is greater,
		// i.e. which one wins.
		public int CompareTo(Move m)
		{
			if (this.type == m.type)
			{
				return 0;
			}
			else if (this.type == MoveType.Rock)
			{
				return m.type == MoveType.Paper ? -1 : 1;
			}
			else if (this.type == MoveType.Paper)
			{
				return m.type == MoveType.Scissors ? -1 : 1;
			}
			else // if (this.type == MoveType.Scissors)
			{
				return m.type == MoveType.Rock ? -1 : 1;
			}
		}

		// Who has time to type "new Move(MoveType.Rock)"?
		public static Move Rock()
		{
			return new Move(MoveType.Rock);
		}

		// Not this guy
		public static Move Paper()
		{
			return new Move(MoveType.Paper);
		}

		// No sirree
		public static Move Scissors()
		{
			return new Move(MoveType.Scissors);
		}

		// Return the move this move will win
		// (e.g. if this is rock, return scissors)
		public Move Wins()
		{
			return this.type == MoveType.Rock
				? Scissors()
				: this.type == MoveType.Paper
					? Rock()
					: Paper();
		}

		// Return the move this move ties with
		public Move TiesWith()
		{
			return new Move(this.type);
		}

		// Return the move this move will lose to
		public Move LosesTo()
		{
			return this.type == MoveType.Rock
				? Paper()
				: this.type == MoveType.Paper
					? Scissors()
					: Rock();
		}

		// Return a clone of this move
		public Move Clone()
		{
			return new Move(type);
		}
	}

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
					break;
				case 0:
					outcome = $"{m1} ties with {m2}";
					Points = ((int)m1 + 3, (int)m2 + 3);
					winner = null;
					break;
				case 1:
					outcome = $"{m1} beats {m2}";
					Points = ((int)m1 + 6, (int)m2);
					winner = p1;
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

	// A game of Rock Paper Scissors consisting of many rounds
	public class Game
	{
		public Player p1 { get; }
		public Player p2 { get; }
		public int totalRounds { get; }
		public List<Round> rounds { get; }
		
		// Returns true if enough rounds have been played
		public bool over {
			get
			{
				return rounds.Count >= totalRounds;
			}
		}

		// Return the current round number
		public int currentRound {
			get
			{
				return over ? rounds.Count : rounds.Count + 1;
			}
		}

		// Return the most recently played round
		public Round? lastRound {
			get {
				if (rounds.Count < 1)
				{
					return null;
				}
				return rounds[rounds.Count - 1];
			}
		}

		// Which round the scores were last calculated at
		private int scoredAt = 0;

		// Backing variable for points
		private (int p1, int p2) latestPoints = (0, 0);

		// Current points as readable public property. I guess.
		public (int p1, int p2) Points {
			get
			{
				if (rounds.Count == scoredAt)
				{
					return latestPoints;
				}

				UpdateScores();
				return latestPoints;
			}
		}

		// Recalculate current standings
		private void UpdateScores()
		{
				scoredAt = rounds.Count;
				latestPoints = (0, 0);

				foreach (Round r in rounds)
				{
					latestPoints.p1 += r.Points.p1;
					latestPoints.p2 += r.Points.p2;
				}
		}

		// Create a new game of Rock Paper Scissors
		public Game(Player p1, Player p2, int totalRounds = 3)
		{
			this.p1 = p1;
			if (p1 is AIPlayer)
			{
				p1.SetGame(this);
			}

			this.p2 = p2;
			if (p2 is AIPlayer)
			{
				p2.SetGame(this);
			}

			this.totalRounds = totalRounds;
			this.rounds = new List<Round>();
		}

		// Play a round with given moves, if the game is not over yet
		public void Play(Move m1, Move m2)
		{
			if (! over)
			{
				rounds.Add(new Round(p1, p2, m1, m2));
			}
		}

		// Return current standings
		public override string ToString()
		{
			return $"{rounds.Count} rounds played. {p1.info.name} {Points.p1} -  {Points.p2} {p2.info.name}. {State()}";
		}

		// Return description of which player is winning
		public string State()
		{
			if (Points.p1 == Points.p2)
			{
				return "Tie";
			}
			else
			{
				return $"{(Points.p1 > Points.p2 ?  p1.info.name : p2.info.name)} {(over ? "wins" : "is winning")}";
			}
		}
	}

	// Parsers for elves' secret Rock Paper Scissors strategy guide
	public class Strategy
	{
		// Initial, naive understanding of strategy, where second move is
		// interpreted as an explicit move.
		public static (Move p1, Move p2) ParseNaive(string s)
		{
			string[] moves = s.Split(" ");
			return (new Move(moves[0]), new Move(moves[1]));
		}

		// Full explanation of strategy, where second value means whether
		// you should lose, tie or win the round.
		public static (Move p1, Move p2) ParseCunning(string s)
		{
			string[] moves = s.Split(" ");
			Move p1 = new Move(moves[0]);

			Move p2;
			switch (moves[1])
			{
				case "X":
					p2 = p1.Wins();
					break;
				case "Y":
					p2 = p1.TiesWith();
					break;
				case "Z":
					p2 = p1.LosesTo();
					break;
				default:
					throw new Exception($"Whatever this {moves[1]} means...");
			}

			return (p1, p2);
		}
	}
}
