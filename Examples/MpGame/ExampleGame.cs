﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.MpGame;

namespace Examples.MpGame
{
    public sealed class ExampleGame : GameBase<ExamplePlayer>
    {
        // Example fields to keep track of the game state
        private int _turn = 0;
        internal GameState State { get; private set; } = GameState.Setup;

        public ExampleGame(IMessageChannel channel, IEnumerable<ExamplePlayer> players)
            : base(channel, players, setFirstPlayerImmediately: false)
        {
        }

        // Call SetupGame() to do the one-time setup happening prior to a game
        // (think of: shuffling (a) card deck(s))
        public override async Task SetupGame()
        {
            await Channel.SendMessageAsync("Asserting randomized starting parameters.").ConfigureAwait(false);
        }

        // Call StartGame() to do the things that start the game off (think of: dealing cards)
        public override async Task StartGame()
        {
            await Channel.SendMessageAsync("Dealing.").ConfigureAwait(false);
        }

        // Call NextTurn() to do the things happening with a new turn
        public override async Task NextTurn()
        {
            _turn++;
            TurnPlayer = TurnPlayer.Next;
            State = GameState.StartOfTurn;
            await Channel.SendMessageAsync($"It is turn {_turn}, **{TurnPlayer.Value.User.Username}** to play.").ConfigureAwait(false);
        }

        // If you override EndGame() for your own behavior, you MUST call the base implementation
        //public override Task EndGame(string endmsg)
        //    => base.EndGame(endmsg);

        // Create a string that represents the current state of the game
        public override string GetGameState()
        {
            var sb = new StringBuilder($"State of the game at turn {_turn}")
                .AppendLine($"The current turn player is **{TurnPlayer.Value.User.Username}**.")
                .AppendLine($"The current phase is **{State}**");

            return sb.ToString();
        }

        // If you want to get fancy, you can build an embed
        // instead of a string. It's not required to implement both
        // but make sure you don't call the un-implemented one.
        public override Embed GetGameStateEmbed()
        {
            throw new NotImplementedException();
        }
    }

    // Example enum to keep track of the game state
    internal enum GameState
    {
        Setup,
        StartOfTurn,
        MainPhase,
        SpecialPhase,
        EndPhase
    }
}
