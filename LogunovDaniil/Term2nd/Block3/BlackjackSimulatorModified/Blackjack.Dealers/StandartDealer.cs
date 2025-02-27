﻿using Blackjack.DealerLibrary;
using Blackjack.GeneralInfo;
using DeckLibrary;

namespace Blackjack.Dealers
{
	public class StandartDealer : AbstractDealer
	{
		private static bool CheckIfFiveCardCharlie(List<Card> hand)
		{
			return hand.Count() >= 5 && Ruleset.EvaluateHandValue(hand) <= 21;
		}

		private static bool CheckIfPlayerAutowin(List<Card> dealerHand, List<Card> playerHand)
		{
			return CheckIfFiveCardCharlie(playerHand)
				   || Ruleset.IsBlackjack(playerHand) && !Ruleset.IsBlackjack(dealerHand);
		}

		public override int GetMinBet()
		{
			return 500;
		}

		public override bool CheckCanPlayerHit(List<Card> playerHand)
		{
			return playerHand.Count < 5
				&& Ruleset.EvaluateHandValue(playerHand) < 21;
		}
		
		public override bool ToGetNextCard(List<Card> dealerHand, List<Card> playerHand)
		{
			return !CheckIfPlayerAutowin(dealerHand, playerHand)
				&& dealerHand.Count < 5
				&& Ruleset.EvaluateHandValue(dealerHand) < 17;
		}

		public override Decisionset MakeDecision(List<Card> dealerHand, List<Card> playerHand)
		{
			if (CheckIfPlayerAutowin(dealerHand, playerHand))
				return Decisionset.PlayerWins;
			if (Ruleset.EvaluateHandValue(playerHand) > 21)
				return Decisionset.DealerWins;
			if (Ruleset.EvaluateHandValue(dealerHand) > 21)
				return Decisionset.PlayerWins;
			if (CheckIfFiveCardCharlie(dealerHand))
				return Decisionset.DealerWins;

			int playerValue = Ruleset.EvaluateHandValue(playerHand);
			int dealerValue = Ruleset.EvaluateHandValue(dealerHand);
			if (playerValue > dealerValue)
				return Decisionset.PlayerWins;
			if (playerValue < dealerValue)
				return Decisionset.DealerWins;
			return Decisionset.Tie;
		}

		public override int CalculateWinning(List<Card> dealerHand, List<Card> playerHand, int bet)
		{
			if (CheckIfPlayerAutowin(dealerHand, playerHand))
				return bet + bet / 2;
			if (MakeDecision(dealerHand, playerHand) != Decisionset.PlayerWins)
				return 0;
			return bet;
		}
	}
}