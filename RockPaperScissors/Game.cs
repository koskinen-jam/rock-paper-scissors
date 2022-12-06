namespace RockPaperScissors
{
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
			get
			{
				if (rounds.Count < 1)
				{
					return null;
				}
				return rounds[rounds.Count - 1];
			}
		}

		public bool Observed { get; set; } = false;

		// Return true if there is someone in the forest to hear the tree fall
		public bool IsObserved {
			get
			{
				return Observed || p1 is HumanPlayer || p2 is HumanPlayer;
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
				latestPoints = Standings(rounds.Count - 1);
				return;
		}

		// Calculate the players scores after given round.
		public (int p1, int p2) Standings(int round)
		{
			(int p1, int p2) points = (0, 0);

			for (int i = 0; i <= round; i++)
			{
				points.p1 += rounds[i].Points.p1;
				points.p2 += rounds[i].Points.p2;
			}

			return points;
		}

		// Stringified report with outcome of given round and standings after it
		public string RoundReport(int roundNumber)
		{
			Round round = rounds[roundNumber];
			(int p1, int p2) points = Standings(roundNumber);

			return $"{round.outcome, -28}"
				+ $"{round.p1.info.name, 10} {points.p1, 3} (+{round.Points.p1, 2}) - "
				+ $"{points.p2, 3} (+{round.Points.p2, 2}) {round.p2.info.name, -10}";
		}

		// Stringified report with last rounds outcome and standings after it
		public string LastRoundReport()
		{
			return RoundReport(rounds.Count - 1);
		}

		// Create a new game of Rock Paper Scissors
		public Game(Player p1, Player p2, int totalRounds = 3, bool Observed = false)
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

			this.Observed = Observed;

			GameEvents.SubscribeGame(this);
			OnGameStart();
		}

		// Play a round with given moves, if the game is not over yet
		public void Play(Move m1, Move m2)
		{
			if (! over)
			{
				Round r = new Round(p1, p2, m1, m2);
				rounds.Add(r);
				OnRoundEnd(r);
			}

			if (over)
			{
				OnGameEnd();
				GameEvents.UnsubscribeGame(this);
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

		public event EventHandler<RoundEventArgs>? RoundStarted;
		public event EventHandler<RoundEventArgs>? RoundEnded;
		public event EventHandler? GameStarted;
		public event EventHandler? GameEnded;

		public void OnRoundStart(Round r)
		{
			RoundEventArgs e = new RoundEventArgs();
			e.Round = r;
			RoundStarted?.Invoke(this, e);			
		}

		public void OnRoundEnd(Round r)
		{
			RoundEventArgs e = new RoundEventArgs();
			e.Round = r;
			RoundEnded?.Invoke(this, e);
		}

		public void OnGameStart()
		{
			GameStarted?.Invoke(this, new EventArgs());
		}

		public void OnGameEnd()
		{
			GameEnded?.Invoke(this, new EventArgs());
		}
	}

}
