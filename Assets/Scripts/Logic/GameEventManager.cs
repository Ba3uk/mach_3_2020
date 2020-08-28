using System;

namespace Logic
{
    public static class GameEventManager
    {

        /// <summary>
        /// Смачились ноды с гравитацией
        /// </summary>
        public static GameEvent MachGravityNode = new GameEvent();

        /// <summary>
        /// Игрок сделал ход
        /// </summary>
        public static GameEvent<Point, Point> PlayerSwipeNode = new GameEvent<Point, Point>();
    }
}
