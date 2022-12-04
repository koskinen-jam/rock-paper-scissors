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
	}
}
