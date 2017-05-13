﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Addons.SimplePermissions;
using Discord.Commands;
using Discord.WebSocket;

namespace Examples.SimplePermissions
{
    class Program
    {
        // Keep the config store in a private field, just like the client.
        private readonly IConfigStore<MyJsonConfig> _configStore;

        private readonly DiscordSocketClient _client;

        // Standard 1.0 fare
        private readonly IDependencyMap _map = new DependencyMap();
        private readonly CommandService _commands = new CommandService();

        // Standard 1.0 fare
        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }

        private Program()
        {
            // One neat thing you could do is pass 'args[0]' from Main()
            // into the constructor here and use that as the path so that
            // you can specify where the config is loaded from when the bot starts up.
            _configStore = new JsonConfigStore<MyJsonConfig>("path_to_config.json");

            // If you have some Dictionary of data added to your config,
            // you should add a check to see if is initialized already or not.
            using (var config = _configStore.Load())
            {
                if (config.SomeData == null)
                    config.SomeData = new Dictionary<ulong, string[]>();

                // Remember to call Save() to save any changes
                config.Save();
            }

            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                // Standard 1.0 fare
            });
        }

        private static Task Log(LogMessage message)
        {
            // Your preferred logging implementation here
            return Task.CompletedTask;
        }

        private async Task MainAsync()
        {
            // More standard 1.0 fare here...

            // You can pass your Logging method into the initializer for
            // SimplePermissions, so that you get a consistent looking log:
            await _commands.UseSimplePermissions(_client, _configStore, _map, Log);

            // Load the config, read the token, and pass it into the login method:
            using (var config = _configStore.Load())
            {
                await _client.LoginAsync(TokenType.Bot, config.LoginToken);
            }

            await _client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}