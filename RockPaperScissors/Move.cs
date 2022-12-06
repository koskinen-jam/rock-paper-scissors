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

}
