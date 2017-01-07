using System.Threading;
using music;

namespace guildwars
{
    public class Player
    {
        private readonly Controller _controller;
        private readonly EventQueue _queue;
        private readonly Thread _thread;

        public Player(EventQueue queue, Controller controller)
        {
            _queue = queue;
            _controller = controller;
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
                else
                {
                    PlayToken(token);
                }
            }
        }

        private void PlayToken(Token token)
        {
            _controller.Play(token.Tone);
        }
    }
}