using Shared;
using Xunit;

namespace Test
{
    public class ClientTests
    {
        [Fact]
        public void TestCreateDeck()
        {
            var deck = new Deck(32);
            Assert.Equal(true, deck.AddCard(new Card("7", Suit.DIAMONDS, 0, 0)));
            Assert.Equal(true, deck.AddCard(new Card("8", Suit.DIAMONDS, 0, 1)));
            Assert.Equal(true, deck.AddCard(new Card("9", Suit.DIAMONDS, 0, 2)));
            Assert.Equal(true, deck.AddCard(new Card("J", Suit.DIAMONDS, 2, 3)));
            Assert.Equal(true, deck.AddCard(new Card("Q", Suit.DIAMONDS, 3, 4)));
            Assert.Equal(true, deck.AddCard(new Card("K", Suit.DIAMONDS, 4, 5)));
            Assert.Equal(true, deck.AddCard(new Card("10", Suit.DIAMONDS, 10, 6)));
            Assert.Equal(true, deck.AddCard(new Card("A", Suit.DIAMONDS, 11, 7)));
            Assert.Equal(true, deck.AddCard(new Card("7", Suit.CLUBS, 0, 8)));
            Assert.Equal(true, deck.AddCard(new Card("8", Suit.CLUBS, 0, 9)));
            Assert.Equal(true, deck.AddCard(new Card("9", Suit.CLUBS, 0, 10)));
            Assert.Equal(true, deck.AddCard(new Card("J", Suit.CLUBS, 2, 11)));
            Assert.Equal(true, deck.AddCard(new Card("Q", Suit.CLUBS, 3, 12)));
            Assert.Equal(true, deck.AddCard(new Card("K", Suit.CLUBS, 4, 13)));
            Assert.Equal(true, deck.AddCard(new Card("10", Suit.CLUBS, 10, 14)));
            Assert.Equal(true, deck.AddCard(new Card("A", Suit.CLUBS, 11, 15)));
            Assert.Equal(true, deck.AddCard(new Card("7", Suit.HEARTS, 0, 16)));
            Assert.Equal(true, deck.AddCard(new Card("8", Suit.HEARTS, 0, 17)));
            Assert.Equal(true, deck.AddCard(new Card("9", Suit.HEARTS, 0, 18)));
            Assert.Equal(true, deck.AddCard(new Card("J", Suit.HEARTS, 2, 19)));
            Assert.Equal(true, deck.AddCard(new Card("Q", Suit.HEARTS, 3, 20)));
            Assert.Equal(true, deck.AddCard(new Card("K", Suit.HEARTS, 4, 21)));
            Assert.Equal(true, deck.AddCard(new Card("10", Suit.HEARTS, 10, 22)));
            Assert.Equal(true, deck.AddCard(new Card("A", Suit.HEARTS, 11, 23)));
            Assert.Equal(true, deck.AddCard(new Card("7", Suit.SPADES, 0, 24)));
            Assert.Equal(true, deck.AddCard(new Card("8", Suit.SPADES, 0, 25)));
            Assert.Equal(true, deck.AddCard(new Card("9", Suit.SPADES, 0, 26)));
            Assert.Equal(true, deck.AddCard(new Card("J", Suit.SPADES, 2, 27)));
            Assert.Equal(true, deck.AddCard(new Card("Q", Suit.SPADES, 3, 28)));
            Assert.Equal(true, deck.AddCard(new Card("K", Suit.SPADES, 4, 29)));
            Assert.Equal(true, deck.AddCard(new Card("10", Suit.SPADES, 10, 30)));
            Assert.Equal(true, deck.AddCard(new Card("A", Suit.SPADES, 11, 31)));
        }

        [Fact]
        public void TestDeckTooManyCards()
        {
            var deck = new Deck(2);
            Assert.Equal(true, deck.AddCard(new Card("7", Suit.DIAMONDS, 0, 0)));
            Assert.Equal(true, deck.AddCard(new Card("8", Suit.DIAMONDS, 0, 1)));
            Assert.Equal(false, deck.AddCard(new Card("9", Suit.DIAMONDS, 0, 1)));
        }

        [Fact]
        public void TestDeckRemoveCard()
        {
            var deck = new Deck(2);
            Assert.Equal(true, deck.AddCard(new Card("7", Suit.DIAMONDS, 0, 0)));
            Assert.Equal(true, deck.AddCard(new Card("8", Suit.DIAMONDS, 0, 1)));
            Assert.Equal(true, deck.RemoveCard(deck.GetCardById(0)));
            Assert.Equal(null, deck.GetCardById(0));
            Assert.NotEqual(null, deck.GetCardById(1));
        }
    }
}
