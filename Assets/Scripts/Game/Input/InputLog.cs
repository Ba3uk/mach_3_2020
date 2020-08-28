
using System.Collections.Generic;

namespace Logic
{
    public static class InputLog
    {
        private static List<LogInputData> logInputDatas = new List<LogInputData>();

        public static void Init() {}

        static InputLog()
        {
            GameEventManager.PlayerSwipeNode.Subscribe(AddToLog);
        }

        public static bool HasLog => logInputDatas.Count > 0;

        public static LogInputData DeleteLastLog()
        {
            var lastLog = logInputDatas[logInputDatas.Count - 1];
            logInputDatas.Remove(lastLog);
            return lastLog;
        }

        private static void AddToLog(Point firstPoint, Point secondPoint)
        {
            logInputDatas.Add(new LogInputData(firstPoint, secondPoint));
        }
    }

    public class LogInputData
    {
        public Point firstPoint { private set; get; }
        public Point secondPoint { private set; get; } 

        public LogInputData(Point fPoint, Point sPoint)
        {
            firstPoint = fPoint;
            secondPoint = sPoint;
        }
    }
}
