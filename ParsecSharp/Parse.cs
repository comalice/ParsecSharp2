﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   public static class Parse
   {
      /// <summary>
      /// Always succeeds with the given value, without consuming any input.
      /// </summary>
      public static Parser<TValue> Success<TValue>(TValue value)
      {
         return _ => Either.Success<TValue, string>(value);
      }

      /// <summary>
      /// Always fails with the given error message, without consuming any input.
      /// </summary>
      public static Parser<TValue> Error<TValue>(string message)
      {
         return _ => Either.Error<TValue, string>(message);
      }

      /// <summary>
      /// Always fails with an "unexpected x" error message, without consuming input.
      /// </summary>
      public static Parser<TValue> Unexpected<TValue>(object value)
      {
         return _ => Either.Error<TValue, string>("Unexpected " + value.ToString());
      }

      /// <summary>
      /// Succeeds only at the end of the input.
      /// </summary>
      public static Parser<Unit> Eof()
      {
         return input =>
         {
            if (input.EndOfStream)
               return Either.Success<Unit, string>(Unit.Instance);

            return Either.Error<Unit, string>("Expected end of input");
         };
      }

      /// <summary>
      /// Tries to parse the input using the given parser, only consuming input when the parser succeeds.
      /// </summary>
      public static Parser<TValue> Try<TValue>(Parser<TValue> parser)
      {
         return input =>
         {
            int position = input.Position;

            var result = parser(input);
            if (result.IsError())
               input.Seek(position);

            return result;
         };
      }

      /// <summary>
      /// Only succeeds when the given parser fails.
      /// </summary>
      public static Parser<Unit> NotFollowedBy<TValue>(Parser<TValue> parser)
      {
         var unexpectedParser = from x in Try(parser)
                                from e in Unexpected<Unit>(x)
                                select e;

         return Combine.Or(unexpectedParser, Success(Unit.Instance));
      }
   }
}
