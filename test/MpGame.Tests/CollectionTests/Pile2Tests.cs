﻿//using System;
//using System.Collections.Generic;
//using System.Collections.Immutable;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Discord.Addons.MpGame.Collections;
//using Xunit;

//namespace MpGame.Tests.CollectionTests
//{
//    public sealed class Pile2Tests
//    {
//        public sealed class GetWrapperAndUpdate
//        {
//            //[Fact]
//            //public void ThrowsOnReferenceTypeWrapper()
//            //{
//            //    var pile = new TestPileCW(withPerms: PilePerms.None, cards: TestCard.Factory(20));
//            //    var priorSize = pile.Count;

//            //    var ex = Assert.Throws<InvalidOperationException>(() => pile.IncrementCounterAt(index: 5));
//            //    Assert.Equal(expected: ErrorStrings.WrapperTypeNotStruct, actual: ex.Message);
//            //    Assert.Equal(expected: priorSize, actual: pile.Count);
//            //}
//        }

//        public sealed class GetWrapperAt
//        {
//            [Fact]
//            public void ThrowsNegativeIndex()
//            {
//                var pile = new TestPile2(withPerms: PilePerms.None, cards: Enumerable.Empty<TestCard>());
//                var priorSize = pile.Count;

//                var ex = Assert.Throws<ArgumentOutOfRangeException>(() => pile.FlipCardAt(index: -1));
//                Assert.StartsWith(expectedStartString: ErrorStrings.RetrievalNegative, actualString: ex.Message);
//                Assert.Equal(expected: "index", actual: ex.ParamName);
//                Assert.Equal(expected: priorSize, actual: pile.Count);
//            }

//            [Fact]
//            public void ThrowsTooHighIndex()
//            {
//                var pile = new TestPile2(withPerms: PilePerms.None, cards: TestCard.Factory(20));
//                var priorSize = pile.Count;

//                var ex = Assert.Throws<ArgumentOutOfRangeException>(() => pile.FlipCardAt(index: pile.Count + 1));
//                Assert.StartsWith(expectedStartString: ErrorStrings.RetrievalTooHighP, actualString: ex.Message);
//                Assert.Equal(expected: "index", actual: ex.ParamName);
//                Assert.Equal(expected: priorSize, actual: pile.Count);
//            }

//            [Fact]
//            public void DoesNotChangePileSize()
//            {
//                var pile = new TestPile2(withPerms: PilePerms.All, cards: TestCard.Factory(20));
//                var priorSize = pile.Count;

//                pile.FlipCardAt(index: 4);
//                var expectedSeq = new[]
//                {
//                     1, 2, 3, 4, -1, 6, 7, 8, 9,10,
//                    11,12,13,14,15,16,17,18,19,20
//                };

//                Assert.Equal(expected: priorSize, actual: pile.Count);
//                Assert.Equal(expected: FaceDownCard.Instance, actual: pile.PeekAt(index: 4));
//                Assert.Equal(expected: expectedSeq, actual: pile.Browse().Select(c => c.Id));
//            }

//            [Fact]
//            public void NodeResetIsReflected()
//            {
//                var sourcePile = new TestPile2(withPerms: PilePerms.All, cards: TestCard.Factory(1));
//                var destPile = new TestPile2(withPerms: PilePerms.All, cards: Enumerable.Empty<TestCard>());

//                Assert.Equal(expected: "Card is face-up", actual: sourcePile.GetStatusAt(0));
//                sourcePile.FlipCardAt(0);
//                Assert.Equal(expected: "Card is face-down", actual: sourcePile.GetStatusAt(0));
//                sourcePile.Mill(destPile);
//                Assert.Equal(expected: "Card is face-up", actual: destPile.GetStatusAt(0));
//            }
//        }
//    }
//}
