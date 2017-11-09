﻿using System;

namespace Server
{
    public class Team
    {
        private Player[] _players = new Player[2];
        private string _name;
        private int _score;
        private int _contract;
        private int _nbOfTrick = 0;

        public Team(Player playerOne, Player playerTwo, string name)
        {
            _players[0] = playerOne;
            _players[1] = playerTwo;
            _name = name;
            _contract = -1;
            _score = 0;
        }
        
        public void AddATrick()
        {
            _nbOfTrick += 1;
        }
        
        public int GetNbOfTrick()
        {
            return _nbOfTrick;
        }
        
        public void SetContract(int contract)
        {
            _contract = contract;
        }

        public int GetContract()
        {
            return (_contract);
        }
        
        public int GetScore()
        {
            return (_score);
        }
        
        public void AddScore(int toAdd)
        {
            _score += toAdd;
        }
        
        public Player[] GetPlayers()
        {
            return _players;
        }
        
        public Player GetPlayer(int id)
        {
            if (id >= 2 || id < 0)
                return (null);
            return _players[id];
        }

        public string GetName()
        {
            return _name;
        }

        public void SetScore(int score)
        {
            _score = score;
        }
        
        public Boolean HasWon(int otherTeamScore)
        {
            if (_score >= _contract && _score > otherTeamScore)
            {
                _score += _contract;
                return (true);
            }
            _score = 0;
            return (false);
        }
    }
}
