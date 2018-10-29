using System;
using MemoryGame_1B.Models;
using Newtonsoft.Json;
using Quobject.SocketIoClientDotNet.Client;

namespace MemoryGame_1B.Managers
{
    /// <summary>
    /// Handles all socket requests
    /// </summary>
    public static class SocketIoManager
    {
        /// <summary>
        /// The socket
        /// </summary>
        public static readonly Socket Socket = IO.Socket("https://sleepy-falls-91203.herokuapp.com/");

        /// <summary>
        /// The Current room
        /// </summary>
        public static string Room { get; private set; }

        /// <summary>
        /// Are we connected
        /// </summary>
        public static bool Online { get; private set; }

        /// <summary>
        /// The event listener for receiving a socket
        /// </summary>
        public static event Action<object> OnReceiveSocket;

        /// <summary>
        /// The event listener for a new move
        /// </summary>
        public static event Action<object> OnNewMove;

        /// <summary>
        /// Constructor
        /// </summary>
        static SocketIoManager()
        {
            Socket.On("receive-socket", o => { OnReceiveSocket?.Invoke(o); });

            Socket.On("new-move", o => { OnNewMove?.Invoke(o); });
        }

        /// <summary>
        /// Joins a game
        /// </summary>
        /// <param name="name"></param>
        public static void LeaveGame(string name)
        {
            Socket.Emit("leave-game", name);
            Room = string.Empty;
            Online = false;
        }

        /// <summary>
        /// Joins a game
        /// </summary>
        /// <param name="name"></param>
        public static void JoinGame(string name)
        {
            Socket.Emit("join-game", name);
            Room = name;
            Online = true;
        }

        /// <summary>
        /// Makes a move
        /// </summary>
        /// <param name="move"></param>
        public static void FlipCard(Move move)
        {
            var serializeObject = JsonConvert.SerializeObject(move);
            Socket.Emit("new-move", serializeObject);
        }
    }
}