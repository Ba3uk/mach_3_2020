using GameCore;
using Logic;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameMaker: MonoBehaviour
    {

        public static GameMaker Instance { get; private set; }

        [SerializeField] private GridView gridView;

        public Logic.Grid Grid { private set; get; }
        public StateMachine Machine { private set; get; }

        public void Awake()
        {
            Instance = this;

            InputLog.Init();
        }

        public void Start()
        {
            Grid = new Logic.Grid(10, 10);
            gridView.InitGrid(Grid);

            Machine = gameObject.AddComponent<StateMachine>();
            Machine.SetGrid(Grid);
            Machine.SetState(new FindMach(), 1f);
        }
    }
}
