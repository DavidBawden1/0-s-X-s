using NaughtsNCrosses.Players;
using System.Collections.Generic;
using System.Linq;

namespace NaughtsNCrosses.Rules
{
    public class Referee
    {

        public List<Player> DetermineCurrentPlayersTurn(List<Player> players)
        {
            var expiredPlayersTurn = players.FirstOrDefault(player => player.IsTurn == true);
            var currentPlayersTurn = players.FirstOrDefault(player=>player.IsTurn == false);
            currentPlayersTurn.IsTurn = true;
            expiredPlayersTurn.IsTurn = false;
            return players;
        }
    }
}
