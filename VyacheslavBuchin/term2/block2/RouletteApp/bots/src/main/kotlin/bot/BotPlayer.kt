package bot

import bot.bet_strategy.BetStrategy
import cash_account.Account
import game.roulette.session.Session
import player.Player

class BotPlayer(
	name: String,
	account: Account,
	private val betStrategy: BetStrategy
) : Player(name, account) {

	override fun makeBet(session: Session) {
		val (bet, amount) = betStrategy.nextBet()
		if (amount <= account.balance()) {
			val betRequest = session.registerBet(bet, account, amount)
			betStrategy.add(betRequest)
		}
	}
}