﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Discord.Addons.MpGame
{
    /// <summary>
    /// Base class to manage a game between Discord users.
    /// </summary>
    /// <typeparam name="T">The type of game to manage.</typeparam>
    /// <remarks>Inheriting classes should be marked with
    /// <see cref="Commands.ModuleAttribute"/>.</remarks>
    public abstract class MpGameModuleBase<T> where T : GameBase
    {
        /// <summary>
        /// The <see cref="DiscordSocketClient"/> instance.
        /// </summary>
        protected readonly DiscordSocketClient client;

        /// <summary>
        /// The instance of the game being played.
        /// </summary>
        protected T game;

        /// <summary>
        /// The list of users scheduled to join the game.
        /// </summary>
        protected List<IGuildUser> playerList;

        /// <summary>
        /// Indicates whether the users can join a game about to start.
        /// </summary>
        protected bool openToJoin = false;

        /// <summary>
        /// Indicates whether a game is currently going on.
        /// </summary>
        protected bool gameInProgress = false;

        /// <summary>
        /// Sets up the common logic for a Module that manages a game between Discord users.
        /// </summary>
        protected MpGameModuleBase(DiscordSocketClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));

            this.client = client;
        }

        /// <summary>
        /// Command to open a game for others to join.
        /// </summary>
        public abstract Task OpenGame(IMessage msg);

        /// <summary>
        /// Command to cancel a game before it started.
        /// </summary>
        public abstract Task CancelGame(IMessage msg);

        /// <summary>
        /// Command to join a game that is open.
        /// </summary>
        public abstract Task JoinGame(IMessage msg);

        /// <summary>
        /// Command to leave a game that is not yet started.
        /// </summary>
        public abstract Task LeaveGame(IMessage msg);

        /// <summary>
        /// Command to start a game with the players who joined.
        /// </summary>
        public abstract Task StartGame(IMessage msg);

        /// <summary>
        /// Command to advance to the next turn (if applicable).
        /// </summary>
        public abstract Task NextTurn(IMessage msg);

        /// <summary>
        /// Command to display the current state of the game.
        /// </summary>
        public abstract Task GameState(IMessage msg);

        /// <summary>
        /// Command to end a game in progress early.
        /// </summary>
        public abstract Task EndGame(IMessage msg);
    }
}