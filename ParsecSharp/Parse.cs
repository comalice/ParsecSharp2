﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Parse
   {
      /// <summary>
      /// Craetes a parser that always returns the given value, regardless of the input.
      /// </summary>
      public static Parser<TValue> Success<TValue>(TValue value)
      {
         return _ => Either.Success<TValue, string>(value);
      }

      /// <summary>
      /// Creates a parser that always returns the given error message, regardless of the input.
      /// </summary>
      public static Parser<TValue> Error<TValue>(string message)
      {
         return _ => Either.Error<TValue, string>(message);
      }
   }
}