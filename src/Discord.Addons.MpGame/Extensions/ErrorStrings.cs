﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Discord.Addons.MpGame.Collections
{
    internal static class ErrorStrings
    {
        internal static readonly string NoBrowse  = "Not allowed to browse this instance.";
        internal static readonly string NoBrowseAndTake = "Pile needs to allow both Browsing and Taking.";
        internal static readonly string NoBrowseOrPeek  = "Pile needs to allow either Browsing or Peeking.";
        internal static readonly string NoClear   = "Not allowed to clear this instance.";
        internal static readonly string NoCut     = "Not allowed to cut this instance.";
        internal static readonly string NoDraw    = "Not allowed to draw on this instance.";
        internal static readonly string NoInsert  = "Not allowed to insert at arbitrary index on this instance.";
        internal static readonly string NoPeek    = "Not allowed to peek on this instance.";
        internal static readonly string NoPut     = "Not allowed to put items on top of this instance.";
        internal static readonly string NoPutTarget = "Not allowed to put items on top of the target instance.";
        internal static readonly string NoPutBottom = "Not allowed to put items on the bottom of this instance.";
        internal static readonly string NoShuffle = "Not allowed to reshuffle this instance.";
        internal static readonly string NoTake    = "Not allowed to take from an arbitrary index on this instance.";

        internal static readonly string CutAmountNegative  = "Cut amount may not be negative.";
        internal static readonly string CutAmountTooHigh   = "Cut amount may not be greater than the pile's current size.";
        internal static readonly string InsertionNegative  = "Insertion index may not be negative.";
        internal static readonly string InsertionTooHigh   = "Insertion index may not be greater than the pile's current size.";
        internal static readonly string MillTargetSamePile = "Mill target cannot be the same instance as the source pile.";
        internal static readonly string NoSwappingStrategy = "Not allowed to switch buffer strategy after it is used.";
        internal static readonly string NewSequenceNull    = "New sequence may not be null.";
        internal static readonly string PeekAmountNegative = "Peek amount may not be negative.";
        internal static readonly string PeekAmountTooHigh  = "Peek amount may not be greater than the pile's current size.";
        internal static readonly string PileEmpty          = "Cannot draw from an empty pile.";
        internal static readonly string RetrievalNegative  = "Retrieval index may not be negative.";
        internal static readonly string RetrievalTooHighP  = "Retrieval index may not be greater than or equal to the pile's current size.";
        internal static readonly string RetrievalTooHighH  = "Retrieval index may not be greater than or equal to the hand's current size.";
        //internal static readonly string WrapperTypeNotStruct = "This operation is only valid on value types (structs).";
    }
}
