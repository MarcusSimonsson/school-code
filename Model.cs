using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack
{
    public enum GameState
    {
        Playing, PlayerPassed, Done
    }
    public class Model : Observable
    {

        public int PlayerScore { get; private set; }
        public int ComputerScore { get; private set; }
        public int PlayerLastCard { get; private set; }
        public int ComputerLastCard { get; private set; }
        public GameState State { get; private set; }
        private Random random;

        public Model()
        {
            random = new Random();
            State = GameState.Playing;
        }

        public void Play()
        {
            DoPlayerTurn();
            DoComputerTurn();
            CheckLoss();
            SetChanged();
            NotifyObservers();
        }

        private void CheckLoss()
        {
            if (PlayerScore > 21 || ComputerScore > 21)
            {
                State = GameState.Done;
            }
        }

        private void DoComputerTurn()
        {
            if(ComputerScore < 16)
            {
                ComputerLastCard = DrawCard();
                ComputerScore += ComputerLastCard;
            }
            else
            {
                if (State == GameState.PlayerPassed)
                    State = GameState.Done;
            }
        }

        private void DoPlayerTurn()
        {
            PlayerLastCard = DrawCard();
            PlayerScore += PlayerLastCard;
        }

        private int DrawCard()
        {
            return random.Next(1, 13);
        }

        public void Pass()
        {
            State = GameState.PlayerPassed;
            DoComputerTurn();
            CheckLoss();
            SetChanged();
            NotifyObservers();
        }

        public void Restart()
        {
            PlayerScore = 0;
            PlayerLastCard = 0;
            ComputerScore = 0;
            ComputerLastCard = 0;
            State = GameState.Playing;
            SetChanged();
            NotifyObservers();
        }
    }
}
