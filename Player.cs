namespace RockPaperScissors
{
	public class PlayerInfo
	{
		public string name { get; }
		
		public PlayerInfo(string name)
		{
			this.name = name;
		}
	}

	public abstract class Player
	{
		public PlayerInfo info { get; protected set; }

		public Player(PlayerInfo info)
		{
			this.info = info;
		}

		public abstract Move GetNextMove();
	}

	public class HumanPlayer : Player
	{
		private PlayerController controller { get; }

		public HumanPlayer(PlayerInfo info, PlayerController controller) : base(info)
		{
			this.info = info;
			this.controller = controller;
		}

		public override Move GetNextMove()
		{
			Move? move = null;

			while (move == null)
			{
				move = Move.FromConsoleKey(controller.GetKey());
			}

			return move;
		}
	}

	public abstract class AIPlayer : Player {
		public AIPlayer(PlayerInfo info) : base(info)
		{
		}
       	}

	public class PlayerController
	{
		public ConsoleKeyInfo lastPressed { get; set; }

		public virtual ConsoleKey GetKey()
		{
			while (! Console.KeyAvailable) {}

			lastPressed = Console.ReadKey(true);
			
			return lastPressed.Key;
		}
	}

}
