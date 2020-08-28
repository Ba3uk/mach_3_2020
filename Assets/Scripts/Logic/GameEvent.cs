
namespace Logic
{
    public class GameEvent
    {
        private event System.Action action;

        public void Subscribe(System.Action callback)
        {
            action += callback;
        }

        public void UnSubscribe(System.Action callback)
        {
            action -= callback;
        }

        public void Invoke()
        {
            action?.Invoke();
        }

        public System.Delegate[] GetInvocationList()
        {
            return action.GetInvocationList();
        }
    }

    public class GameEvent<T>
    {
        private event System.Action<T> action;

        public void Subscribe(System.Action<T> callback)
        {
            action += callback;
        }

        public void UnSubscribe(System.Action<T> callback)
        {
            action -= callback;
        }

        public void Invoke(T p)
        {
            action?.Invoke(p);
        }

        public System.Delegate[] GetInvocationList()
        {
            return action.GetInvocationList();
        }
    }

    public class GameEvent<T, L>
    {
        private event System.Action<T, L> action;

        public void Subscribe(System.Action<T, L> callback)
        {
            action += callback;
        }

        public void UnSubscribe(System.Action<T, L> callback)
        {
            action -= callback;
        }

        public void Invoke(T p, L k)
        {
            action?.Invoke(p, k);
        }

        public System.Delegate[] GetInvocationList()
        {
            return action.GetInvocationList();
        }
    }
}
