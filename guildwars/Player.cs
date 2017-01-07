using System.Threading;
using music;

namespace guildwars
{
    public class Player
    {
        private readonly Keyboard _keyboard;
        private readonly EventQueue _queue;
        private readonly Thread _thread;

        public Player(EventQueue queue, Keyboard keyboard)
        {
            _queue = queue;
            _keyboard = keyboard;
            _thread = new Thread(Loop)
            {
                Priority = ThreadPriority.Highest
            };
        }

        public void Start()
        {
            _thread.Start();
        }

        private void Loop()
        {
            while (true)
            {
                var token = _queue.Dequeue();

                if (token == null)
                {
                    Thread.Sleep(1);
                }

                PlayToken(token);
            }
        }

        private void PlayToken(Token token)
        {
            _keyboard.Play(token.Tone);
        }
    }
}