﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Discord.Addons.TriviaGames
{
    /// <summary>
    /// Manages Trivia games.
    /// </summary>
    public sealed class TriviaService
    {
        internal readonly IReadOnlyDictionary<string, string[]> TriviaData;
        internal IReadOnlyDictionary<ulong, TriviaGame> TriviaGames => _triviaGames;

        private readonly ConcurrentDictionary<ulong, TriviaGame> _triviaGames =
            new ConcurrentDictionary<ulong, TriviaGame>();

        /// <summary>
        /// Create the service that will manage Trivia games.
        /// </summary>
        /// <param name="triviaData">A set of questions and answers to use as trivia.</param>
        /// <param name="client">The <see cref="DiscordSocketClient"/> instance.</param>
        public TriviaService(Dictionary<string, string[]> triviaData, DiscordSocketClient client)
        {
            if (triviaData == null) throw new ArgumentNullException(nameof(triviaData));

            TriviaData = triviaData;

            client.MessageReceived += async msg =>
            {
                TriviaGame game;
                if (_triviaGames.TryGetValue(msg.Channel.Id, out game))
                {
                    await game.CheckTrivia(msg);
                }
            };
        }

        /// <summary>
        /// Add a new game to the list of active games.
        /// </summary>
        /// <param name="channelId">Public facing channel of this game.</param>
        /// <param name="game">Instance of the game.</param>
        public void AddNewGame(ulong channelId, TriviaGame game)
        {
            _triviaGames[channelId] = game;
            game.GameEnd += _onGameEnd;
        }

        private Task _onGameEnd(ulong channelId)
        {
            TriviaGame game;
            if (_triviaGames.TryRemove(channelId, out game))
            {
                game.GameEnd -= _onGameEnd;
            }
            return Task.CompletedTask;
        }
    }
}
