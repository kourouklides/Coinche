﻿using System;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    public class Player
    {
        private TcpClient channel;
        private Deck deck = new Deck(8);

        public Player(TcpClient channel)
        {
            this.channel = channel;
        }

        public void SendMessage(string message)
        {
            message += "\n";
            var asen = new ASCIIEncoding();
            var toSend = asen.GetBytes(message);
            channel.GetStream().Write(toSend, 0, toSend.Length);
        }

        public Deck GetDeck()
        {
            return deck;
        }

        public void EmptyDeck()
        {
            var count = deck.GetDeck().Count;
            while (count != 0) {
                deck.GetDeck().RemoveAt(0);
                count -= 1;
            }
        }

        public void PutCard(Card card)
        {
            deck.RemoveCard(card);
        }
    }
}
