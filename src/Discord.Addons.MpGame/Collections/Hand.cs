﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;

namespace Discord.Addons.MpGame.Collections
{
    [DebuggerDisplay("Count = {Count}")]
    /// <summary>
    /// Similar to <see cref="Pile{TCard}"/> but specialized
    /// and optimized for representing a hand of cards.
    /// </summary>
    /// <typeparam name="TCard">The card type.</typeparam>
    public sealed class Hand<TCard>
        where TCard : class
    {
        private readonly List<TCard> _hand;

        /// <summary>
        /// The amount of cards currently in the hand.
        /// </summary>
        public int Count => _hand.Count;

        /// <summary>
        /// The cards inside this hand.
        /// </summary>
        public IReadOnlyCollection<TCard> Cards => _hand.ToImmutableArray();

        /// <summary>
        /// Initializes a new <see cref="Hand{TCard}"/> to an empty state.
        /// </summary>
        public Hand()
        {
            _hand = new List<TCard>();
        }

        /// <summary>
        /// Initializes a new <see cref="Hand{TCard}"/> with the specified cards.
        /// </summary>
        /// <param name="cards">The cards to put in the hand.</param>
        public Hand(IEnumerable<TCard> cards)
        {
            _hand = new List<TCard>(cards);
        }

        /// <summary>
        /// Adds a card to the hand.
        /// </summary>
        /// <param name="card">The card to add.</param>
        public void Add(TCard card) => _hand.Add(card);

        /// <summary>
        /// Takes a card from the given index.
        /// </summary>
        /// <param name="index">The index of the card to take.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>
        /// was less than 0 or greater than the pile's current size.</exception>
        public TCard TakeAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(message: "Retrieval index may not be negative.", paramName: nameof(index));
            if (index > Count)
                throw new ArgumentOutOfRangeException(message: "Retrieval index may not be greater than the hand's current size.", paramName: nameof(index));

            var tmp = _hand[index];
            _hand.RemoveAt(index);
            return tmp;
        }

        /// <summary>
        /// Takes the first card that matches a given predicate.
        /// </summary>
        /// <param name="predicate">The predicate to match.</param>
        /// <returns>The card, or <see langword="null"/> if no match found.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="predicate"/> was <see langword="null"/>.</exception>
        public TCard TakeFirstOrDefault(Func<TCard, bool> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var tmp = _hand.FirstOrDefault(predicate);
            if (tmp != null)
            {
                _hand.Remove(tmp);
            }
            return tmp;
        }

        /// <summary>
        /// Clears the entire hand and returns the cards that were in it.
        /// </summary>
        /// <returns>The collection as it was before it is cleared.</returns>
        public IReadOnlyCollection<TCard> Clear()
        {
            var result = _hand.ToImmutableArray();
            _hand.Clear();
            return result;
        }
    }
}
