namespace Logic
{
    public delegate void GravityHandler(bool inNormalGravity);
    public static class GravityModule
    {
        /// <summary>
        /// Событие о смене орентации гравитации
        /// </summary>
        public static event GravityHandler OnGravityChange;
        public static bool IsNormalGravity
        {
            private set;
            get;
        }

        static GravityModule()
        {
            GameEventManager.MachGravityNode.Subscribe(OnMachGravityNode);
        }

        private static void OnMachGravityNode()
        {
            IsNormalGravity = !IsNormalGravity;

            OnGravityChange?.Invoke(IsNormalGravity);
        }
    }
}
