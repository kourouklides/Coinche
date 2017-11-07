﻿using System;
using System.Collections.Generic;

namespace Server
{
    public class Game
    {        
        private Team[] _teams = new Team[2];
        private Deck _modelDeck = new Deck(32);
        private Deck _deck;
        private List<Player> _allPlayers;
        private Suit _trump;
        private Trick _trick = new Trick();
        
        public Game(List<Player> allPlayers, Team teamOne, Team teamTwo)
        {
            _teams[0] = teamOne;
            _teams[1] = teamTwo;
            _allPlayers = allPlayers;
        }

        private void CreateModelDeck()
        {
            _modelDeck.AddCard(new Card("7", Suit.DIAMONDS, 0, 0));
            _modelDeck.AddCard(new Card("8", Suit.DIAMONDS, 0, 1));
            _modelDeck.AddCard(new Card("9", Suit.DIAMONDS, 0, 2));
            _modelDeck.AddCard(new Card("10", Suit.DIAMONDS, 10, 3));
            _modelDeck.AddCard(new Card("J", Suit.DIAMONDS, 2, 4));
            _modelDeck.AddCard(new Card("Q", Suit.DIAMONDS, 3, 5));
            _modelDeck.AddCard(new Card("K", Suit.DIAMONDS, 4, 6));
            _modelDeck.AddCard(new Card("A", Suit.DIAMONDS, 11, 7));
            _modelDeck.AddCard(new Card("7", Suit.CLUBS, 0, 8));
            _modelDeck.AddCard(new Card("8", Suit.CLUBS, 0, 9));
            _modelDeck.AddCard(new Card("9", Suit.CLUBS, 0, 10));
            _modelDeck.AddCard(new Card("10", Suit.CLUBS, 10, 11));
            _modelDeck.AddCard(new Card("J", Suit.CLUBS, 2, 12));
            _modelDeck.AddCard(new Card("Q", Suit.CLUBS, 3, 13));
            _modelDeck.AddCard(new Card("K", Suit.CLUBS, 4, 14));
            _modelDeck.AddCard(new Card("A", Suit.CLUBS, 11, 15));
            _modelDeck.AddCard(new Card("7", Suit.HEARTS, 0, 16));
            _modelDeck.AddCard(new Card("8", Suit.HEARTS, 0, 17));
            _modelDeck.AddCard(new Card("9", Suit.HEARTS, 0, 18));
            _modelDeck.AddCard(new Card("10", Suit.HEARTS, 10, 19));
            _modelDeck.AddCard(new Card("J", Suit.HEARTS, 2, 20));
            _modelDeck.AddCard(new Card("Q", Suit.HEARTS, 3, 21));
            _modelDeck.AddCard(new Card("K", Suit.HEARTS, 4, 22));
            _modelDeck.AddCard(new Card("A", Suit.HEARTS, 11, 23));
            _modelDeck.AddCard(new Card("7", Suit.SPADES, 0, 24));
            _modelDeck.AddCard(new Card("8", Suit.SPADES, 0, 25));
            _modelDeck.AddCard(new Card("9", Suit.SPADES, 0, 26));
            _modelDeck.AddCard(new Card("10", Suit.SPADES, 10, 27));
            _modelDeck.AddCard(new Card("J", Suit.SPADES, 2, 28));
            _modelDeck.AddCard(new Card("Q", Suit.SPADES, 3, 29));
            _modelDeck.AddCard(new Card("K", Suit.SPADES, 4, 30));
            _modelDeck.AddCard(new Card("A", Suit.SPADES, 11, 31));
        }

        private void ResetDeck()
        {
            _deck = new Deck(32);
            for (var idx = 0; idx < _modelDeck.Size(); idx += 1)
                _deck.AddCard(_modelDeck.GetDeck()[idx]);
        }
        
        private void DrawCards()
        {
            var rand = new Random();
            ResetDeck();
            int idxPlayer;
            int idxTeam;
            idxPlayer = rand.Next(1, 2);
            idxTeam = rand.Next(1, 2);
            while (_deck.Size() != 0)
            {
                if (idxPlayer >= 2) {
                    idxPlayer = 0;
                    idxTeam += 1;
                }
                if (idxTeam >= 2)
                    idxTeam = 0;
                Console.Write("========== CARDS REMAINING : " + _deck.Size() + " =========\n");
                for (var i = 0; i < 3; i += 1)
                {
                    int randCard;
                    if (_deck.Size() == 8 || _deck.Size() == 6 || _deck.Size() == 4 || _deck.Size() == 2)
                        i = 1;
                    randCard = _deck.Size() > 1 ? rand.Next(1, _deck.Size()) : 0;
                    Console.Write("Giving card - ");
                    _deck.GetDeck()[randCard].PrintCard();
                    Console.Write(" - to " + _teams[idxTeam].GetPlayer(idxPlayer).GetName() +
                                       " of " + _teams[idxTeam].GetName() + "\n");
                    _teams[idxTeam].GetPlayer(idxPlayer).GetDeck().AddCard(_deck.GetDeck()[randCard]);
                    _deck.RemoveCard(_deck.GetDeck()[randCard]);
                }
                idxPlayer += 1;
            }
        }

        private void Broadcast(String message)
        {
            if (message.Equals("DECK"))
            {
                _teams[0].GetPlayer(0).SendDeck();
                _teams[0].GetPlayer(1).SendDeck();
                _teams[1].GetPlayer(0).SendDeck();
                _teams[1].GetPlayer(1).SendDeck();
                return;
            }
            _teams[0].GetPlayer(0).SendMessage(message);
            _teams[0].GetPlayer(1).SendMessage(message);
            _teams[1].GetPlayer(0).SendMessage(message);
            _teams[1].GetPlayer(1).SendMessage(message);
        }

        private void PreBidding() {
            _teams[0].GetPlayer(0).SendMessage("MSG You are " + _teams[0].GetPlayer(0).GetName() + ". Together with your mate "
                                              + _teams[0].GetPlayer(1).GetName() + ", you make " + _teams[0].GetName() + ".");
            _teams[0].GetPlayer(1).SendMessage("MSG You are " + _teams[0].GetPlayer(1).GetName() + ". Together with your mate "
                                              + _teams[0].GetPlayer(0).GetName() + ", you make " + _teams[0].GetName() + ".");
            _teams[1].GetPlayer(0).SendMessage("MSG You are " + _teams[1].GetPlayer(0).GetName() + ". Together with your mate "
                                              + _teams[1].GetPlayer(1).GetName() + ", you make " + _teams[1].GetName() + ".");
            _teams[1].GetPlayer(1).SendMessage("MSG You are " + _teams[1].GetPlayer(1).GetName() + ". Together with your mate "
                                              + _teams[1].GetPlayer(0).GetName() + ", you make " + _teams[1].GetName() + ".");
        }

        private void AnnounceScores()
        {
            Broadcast("MSG " + _teams[0].GetName() + " (" + _teams[0].GetPlayer(0).GetName() + ", "
                      + _teams[0].GetPlayer(1).GetName() + ") has " + _teams[0].GetScore()
                      + (_teams[0].GetContract() == -1 ? "" : "/" + _teams[0].GetContract()) + " points || "
                      + _teams[1].GetName() + " (" + _teams[1].GetPlayer(0).GetName() + ", "
                      + _teams[1].GetPlayer(1).GetName() + ") has " + _teams[1].GetScore()
                      + (_teams[1].GetContract() == -1 ? "" : "/" + _teams[1].GetContract()) + " points. "
                      + "Trump: " + _trump);
        }

        private int GetTrumpChooser()
        {
            foreach (var team in _teams)
            {
                foreach (var player in team.GetPlayers())
                {
                    if (player.IsTrumpChooser())
                        return (player.GetId());
                }
            }
            return (-1);
        }

        private void CalculateScore()
        {
            foreach (var team in _teams)
            {
                foreach (var player in team.GetPlayers())
                {
                    if (player.GetId() == _trick.GetLeadingPlayer().GetId())
                        team.AddScore(_trick.GetValue());
                }
            }
        }

        private void AnnounceWinner()
        {
            if (_teams[0].HasWon(_teams[1].GetScore()))
            {
                Broadcast("MSG " + _teams[0].GetName() + " has " + _teams[0].GetScore() + " points and "
                        + _teams[1].GetName() + " has " + _teams[1].GetScore() + ".");
                Broadcast(_teams[0].GetName() + "won! Congratulations!!");
            }
            else
            {
                Broadcast("MSG " + _teams[0].GetName() + " has " + _teams[0].GetScore() + " points and "
                          + _teams[1].GetName() + " has " + _teams[1].GetScore() + ".");
                Broadcast(_teams[1].GetName() + "won! Congratulations!!");
            }
        }
        
        public void StartPlaying()
        {
            var playerId = GetTrumpChooser();
            while (_allPlayers[0].GetDeck().Size() != 0)
            {
                Broadcast("MSG " + _teams[0].GetName() + " (" + _teams[0].GetPlayer(0).GetName() + ", "
                          + _teams[0].GetPlayer(1).GetName() + ") has " + _teams[0].GetScore()
                          + (_teams[0].GetContract() == -1 ? "" : "/" + _teams[0].GetContract()) + " points || "
                          + _teams[1].GetName() + " (" + _teams[1].GetPlayer(0).GetName() + ", "
                          + _teams[1].GetPlayer(1).GetName() + ") has " + _teams[1].GetScore()
                          + (_teams[1].GetContract() == -1 ? "" : "/" + _teams[1].GetContract()) + " points. "
                          + "Trump: " + _trump);
                AnnounceScores();
                while (_trick.Size() != 4)
                {
                    _allPlayers[playerId].SendDeck();
                    _allPlayers[playerId].SendMessage("PLAY");
                    Broadcast("MSG " + _allPlayers[playerId].GetName() + "'s turn...");
                    var msg = _allPlayers[playerId].GetNextMessage().Split();
                    if (msg.Length != 2 && msg[0].Equals("PLAY") &&
                        Int32.TryParse(msg[1], out var cardId))
                    {
                        if (_allPlayers[playerId].PutCard(_trick, cardId, _trump))
                        {
                            Broadcast("MSG " + _allPlayers[playerId].GetName() + " put a "
                                      + _modelDeck.GetDeck()[cardId].GetName() + " of " +
                                      _modelDeck.GetDeck()[cardId].GetSuit()
                                      + ". " + _trick.GetLeadingPlayer().GetName() + " is leading this turn.");
                            _allPlayers[playerId].SendMessage("PLAY OK");
                            playerId += 1;
                            playerId = (playerId >= 4 ? 0 : playerId);
                        }
                        else
                            _allPlayers[playerId].SendMessage("PLAY KO");
                    }
                    else
                        _allPlayers[playerId].SendMessage("PLAY KO");
                    playerId = _trick.GetLeadingPlayer().GetId();
                    CalculateScore();
                    _trick.ResetDeck();
                }
                AnnounceWinner();
                Broadcast("END");
            }
        }

        private Boolean RecursiveBidding(int idPlayer, int maxIterations)
        {
            var iterations = 0;
            while (iterations < maxIterations)
            {
                if (idPlayer >= 4)
                    idPlayer = idPlayer % 4;
                var player = _allPlayers[idPlayer];
                Broadcast("MSG " + player.GetName() + " is making a decision...");
                player.SendMessage("BID");
                while (true)
                {
                    var msg = player.GetNextMessage();
                    Console.Write("[SG] Received client answer: " + msg + "\n");
                    if (msg.Equals("BID N"))
                        break;
                    var msgTab = msg.Split();
                    if (!msgTab[0].Equals("BID") || !msgTab[1].Equals("Y") || msgTab.Length != 4)
                    {
                        player.SendMessage("BID KO");
                        continue;
                    }
                    if (!Int32.TryParse(msgTab[2], out var contract))
                    {
                        player.SendMessage("BID KO");
                        continue;
                    }
                    var suit = msgTab[3];
                    if (contract <= _teams[0].GetContract()
                            || contract <= _teams[1].GetContract()
                            || contract < 80
                            || contract % 10 != 0)
                    {
                        player.SendMessage("BID KO");
                        continue;
                    }
                    if (!(suit.ToUpper().Equals("HEARTS")
                            || suit.ToUpper().Equals("DIAMONDS")
                            || suit.ToUpper().Equals("CLUBS")
                            || suit.ToUpper().Equals("SPADES")))
                    {
                        player.SendMessage("BID KO");
                        continue;
                    }
                    _trump = (Suit)Enum.Parse(typeof(Suit), suit.ToUpper());
                    _teams[(idPlayer == 0 || idPlayer == 2) ? 0 : 1].SetContract(contract);
                    _teams[(idPlayer == 0 || idPlayer == 2) ? 1 : 0].SetContract(-1);
                    Broadcast("MSG " + player.GetName() + " from " + _teams[(idPlayer == 0 || idPlayer == 2) ? 0 : 1].GetName()
                    + " bid " + contract + " on " + suit.ToUpper());
                    if (!RecursiveBidding(idPlayer + 1, 3))
                        _allPlayers[idPlayer].SetTrumpChooser(true);
                    return (true);
                }
                ++idPlayer;
                ++iterations;
            }
            return (false);
        }

        private void StartBidding()
        {
            var rand = new Random();
            int idPlayer;
            var playerName = "Player1";
            var teamName = "Team2";
            var contract = 0;
            idPlayer = rand.Next(0, 4);
            var contractTaken = false;
            while (!contractTaken)
            {
                DrawCards();
                Broadcast("DECK");
                if (RecursiveBidding(idPlayer, 4))
                    contractTaken = true;
                if (!contractTaken)
                    Broadcast("BID RESET");
            }
            Broadcast("BID STOP");
            foreach (var team in _teams)
            {
                foreach (var player in team.GetPlayers())
                {
                    if (player.IsTrumpChooser())
                    {
                        playerName = player.GetName();
                        teamName = team.GetName();
                        contract = team.GetContract();
                    }
                }
            }
            Broadcast("MSG " + playerName + " from " + teamName + " made the final bid! The chosen trump is " + _trump + ". "
                      + teamName + "'s contract is " + contract + ".");
        }
        
        public void StartGame()
        {
            Console.Write("Starting game...\n");
            CreateModelDeck();
            DrawCards();
            PreBidding();
            Broadcast("DECK");
            StartBidding();
//            StartPlaying();
            Broadcast("END");
        }
    }
}