namespace RockPaperScissors
{
	// Player info wrapper
	public class PlayerInfo
	{
		public string name { get; }
		
		public PlayerInfo(string name)
		{
			this.name = name;
		}
	}

	// Base player class
	public abstract class Player
	{
		public PlayerInfo info { get; protected set; }

		// Create a player with given info
		public Player(PlayerInfo info)
		{
			this.info = info;
		}

		// Return the players next move
		public abstract Move GetNextMove();
	}

	// Human player with a keyboard controller
	public class HumanPlayer : Player
	{
		private PlayerController controller { get; }

		// Create a new human player with given controller
		public HumanPlayer(PlayerInfo info, PlayerController controller) : base(info)
		{
			this.info = info;
			this.controller = controller;
		}

		// Get a move from the player
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

	// Base AI player class
	public abstract class AIPlayer : Player {

		// Create an AI player with given info
		public AIPlayer(PlayerInfo info) : base(info) { }
       	}

	// Player keyboard listener
	public class PlayerController
	{
		public ConsoleKeyInfo lastPressed { get; set; }

		// Wait for a keypress and return the pressed key
		public ConsoleKey GetKey()
		{
			while (! Console.KeyAvailable) {}

			lastPressed = Console.ReadKey(true);
			
			return lastPressed.Key;
		}
	}

}
