﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class CharsTests
   {
      #region Any

      [TestMethod]
      public void Any_EmptyStream_ReturnsError()
      {
         var parser = Chars.Any();
         var result = parser.Run("");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void Any_NonEmptyStream_ReturnsChar()
      {
         var parser = Chars.Any();
         var result = parser.Run("a");

         ParseAssert.ValueEquals('a', result);
      }

      #endregion

      #region Satisfy

      [TestMethod]
      [ExpectedException(typeof(ArgumentNullException))]
      public void Satisfy_PredicateNull_ThrowsException()
      {
         Chars.Satisfy(null);
      }

      [TestMethod]
      public void Satisfy_PassingPredicate_ReturnsChar()
      {
         var parser = Chars.Satisfy(c => true);
         var result = parser.Run("xyz");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Satisfy_FailingPredicate_ReturnsError()
      {
         var parser = Chars.Satisfy(c => false);
         var result = parser.Run("xyz");

         ParseAssert.IsError(result);
      }

      #endregion

      #region Char

      [TestMethod]
      public void Char_MatchingChar_ReturnsChar()
      {
         var parser = Chars.Char('x');
         var result = parser.Run("xyz");

         ParseAssert.ValueEquals('x', result);
      }

      [TestMethod]
      public void Char_NonMatchingChar_ReturnsChar()
      {
         var parser = Chars.Char('x');
         var result = parser.Run("abc");

         ParseAssert.IsError(result);
      }

      #endregion

      #region OneOf

      [TestMethod]
      public void OneOf_MatchingChar_ReturnsChar()
      {
         var parser = Chars.OneOf("abc");
         var result = parser.Run("b");

         ParseAssert.ValueEquals('b', result);
      }

      [TestMethod]
      public void OneOf_NonMatchingChar_ReturnsError()
      {
         var parser = Chars.OneOf("xyz");
         var result = parser.Run("a");

         ParseAssert.IsError(result);
      }

      #endregion

      #region NoneOf

      [TestMethod]
      public void NoneOf_MatchingChar_ReturnsError()
      {
         var parser = Chars.NoneOf("abc");
         var result = parser.Run("b");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void OneOf_NonMatchingChar_ReturnsChar()
      {
         var parser = Chars.NoneOf("xyz");
         var result = parser.Run("b");

         ParseAssert.ValueEquals('b', result);
      }

      #endregion

      #region EndOfLine

      [TestMethod]
      public void EndOfLine_NoMatch_ReturnsError()
      {
         var parser = Chars.EndOfLine();
         var result = parser.Run("abc");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void EndOfLine_LF_ReturnsLF()
      {
         var parser = Chars.EndOfLine();
         var result = parser.Run("\n");

         ParseAssert.ValueEquals('\n', result);
      }

      [TestMethod]
      public void EndOfLine_CRLF_ReturnsLF()
      {
         var parser = Chars.EndOfLine();
         var result = parser.Run("\r\n");

         ParseAssert.ValueEquals('\n', result);
      }

      #endregion

      #region String

      [TestMethod]
      public void String_NoMatch_ReturnsError()
      {
         Parser<string> parser = Chars.String("xyz");
         var result = parser.Run("abc");

         ParseAssert.IsError(result);
      }

      [TestMethod]
      public void String_Match_ReturnsValue()
      {
         Parser<string> parser = Chars.String("abc");
         var result = parser.Run("abc");

         ParseAssert.ValueEquals("abc", result);
      }

      #endregion
   }
}
