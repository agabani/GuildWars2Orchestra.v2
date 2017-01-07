using System.Collections.Generic;
using music;

namespace guildwars
{
    public class EventQueue
    {
        private readonly Queue<Token> _queue;

        public EventQueue()
        {
            _queue = new Queue<Token>();
        }

        public void Queue(Token token)
        {
            _queue.Enqueue(token);
        }

        public Token Dequeue()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }
    }
}