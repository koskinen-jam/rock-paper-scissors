namespace RockPaperScissors
{
	namespace AI
	{

		public class AlwaysRock : AIPlayer
		{
			public AlwaysRock(PlayerInfo info) : base(info)
			{
			}

			public override Move GetNextMove()
			{
				Console.WriteLine("\"I'ma play a rock!\"");
				return Move.Rock();
			}
		}
	}
}
