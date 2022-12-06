namespace RockPaperScissors
{
	namespace AI
	{
		// AI player that always plays a rock.
		public class AlwaysRock : AIPlayer
		{
			// Create a new instance
			public AlwaysRock(PlayerInfo info) : base(info)	{ }

			// Return the players next move
			public override Move GetNextMove()
			{
				return Move.Rock();
			}
		}

		// AI player that repeats whatever you played last
		public class RepeatsYourLastMove : AIPlayer
		{
			public RepeatsYourLastMove(PlayerInfo info) : base(info) { }

			public override Move GetNextMove()
			{
				if (game == null || game.lastRound == null)
				{
					return RandomMove();
				}
				
				return game.lastRound.OpponentsMove(this).Clone();
			}
		}

		// AI player that picks whatever would beat your previous move
		public class BeatsYourLastMove : AIPlayer
		{
			public BeatsYourLastMove(PlayerInfo info) : base(info) { }

			public override Move GetNextMove()
			{
				if (game == null || game.lastRound == null)
				{
					return RandomMove();
				}

				return game.lastRound.OpponentsMove(this).LosesTo();
			}
		}
	}
}
