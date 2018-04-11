﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Discord.Addons.MpGame.Collections
{
    /// <summary>
    /// Similar to <see cref="Pile{TCard}"/> but specialized
    /// and optimized for representing a hand of cards.
    /// </summary>
    /// <typeparam name="TCard">The card type.</typeparam>
    /// <remarks><div class="markdown level0 remarks"><div class="CAUTION">
    /// <h5>Caution</h5><p>This class is not thread-safe.</p></div></div></remarks>
    [DebuggerDisplay("Count = {Count}")]
    public sealed class Hand<TCard>
        where TCard : class
    {
        private List<TCard> _hand;

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
        /// <exception cref="ArgumentNullException"><paramref name="cards"/> was <see langword="null"/>.</exception>
        public Hand(IEnumerable<TCard> cards)
        {
            ThrowArgNull(cards, nameof(cards));

            _hand = new List<TCard>(cards);
        }

        /// <summary>
        /// The amount of cards currently in the hand.
        /// </summary>
        public int Count => _hand.Count;

        /// <summary>
        /// The cards inside this hand.
        /// </summary>
        public IReadOnlyCollection<TCard> Cards => (Count == 0)
                ? ImmutableArray<TCard>.Empty
                : _hand.ToImmutableArray();

        /// <summary>
        /// Adds a card to the hand.
        /// </summary>
        /// <param name="card">The card to add.</param>
        /// <exception cref="ArgumentNullException"><paramref name="card"/> was <see langword="null"/>.</exception>
        public void Add(TCard card)
        {
            ThrowArgNull(card, nameof(card));

            _hand.Add(card);
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

        /// <summary>
        /// Orders the cards using the specified function.
        /// </summary>
        /// <param name="orderFunc">A function that produces an
        /// <see cref="IEnumerable{TCard}"/> in a new order.
        /// This function receives the cards currently in
        /// the hand as its argument.</param>
        /// <exception cref="ArgumentNullException"><paramref name="orderFunc"/> was <see langword="null"/>.</exception>
        /// <exception cref="InvalidOperationException">The sequence produced from 
        /// <paramref name="orderFunc"/> was <see langword="null"/>.</exception>
        public void Order(Func<IEnumerable<TCard>, IEnumerable<TCard>> orderFunc)
        {
            ThrowArgNull(orderFunc, nameof(orderFunc));

            var newOrder = orderFunc(_hand.ToImmutableArray());
            ThrowInvalidOp(newOrder != null, ErrorStrings.NullSequence);

            _hand = new List<TCard>(newOrder);
        }

        /// <summary>
        /// Takes a card from the given index.
        /// </summary>
        /// <param name="index">The index of the card to take.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/>
        /// was less than 0 or greater than the pile's current size.</exception>
        public TCard TakeAt(int index)
        {
            ThrowArgOutOfRange(index < 0, ErrorStrings.RetrievalNegative, nameof(index));
            ThrowArgOutOfRange(index > Count, ErrorStrings.RetrievalTooHigh, nameof(index));

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
            ThrowArgNull(predicate, nameof(predicate));

            var tmp = _hand.FirstOrDefault(predicate);
            if (tmp != null)
            {
                _hand.Remove(tmp);
            }
            return tmp;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowInvalidOp(bool check, string msg)
        {
            if (!check)
                throw new InvalidOperationException(message: msg);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowArgNull<T>(T arg, string argname)
            where T : class
        {
            if (arg == null)
                throw new ArgumentNullException(paramName: argname);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void ThrowArgOutOfRange(bool check, string msg, string argname)
        {
            if (!check)
                throw new ArgumentOutOfRangeException(message: msg, paramName: argname);
        }
    }
}
