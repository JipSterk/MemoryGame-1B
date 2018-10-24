using System;
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
        private static readonly Socket Socket = IO.Socket("http://localhost:8000");


        public static bool Online { get; private set; }

        public static event Action<object> OnReceiveSocket;
        public static event Action<object> OnNewMove;

        static SocketIoManager()
        {
            Socket.On("receive-socket", OnReceiveSocket);
            Socket.On("new-move", OnNewMove);
        }

        /// <summary>
        /// Joins a game
        /// </summary>
        /// <param name="name"></param>
        public static void LeaveGame(string name)
        {
            Socket.Emit("leave-game", name);
            Online = false;
        }

        /// <summary>
        /// Joins a game
        /// </summary>
        /// <param name="name"></param>
        public static void JoinGame(string name)
        {
            Socket.Emit("join-game", name);
            Online = true;
        }

        /// <summary>
        /// Makes a move
        /// </summary>
        /// <param name="move"></param>
        public static void FlipCard(Move move)
        {
            var serializeObject = JsonConvert.SerializeObject(move);
            Socket.Emit("flip-card", serializeObject);
        }

    }

    public class Move
    {
        public int X { get; set; }
        public int Y { get; set; }

        public void Deconstruct(out int x, out int y)
        {
            x = X;
            y = Y;
        }
    }
}
