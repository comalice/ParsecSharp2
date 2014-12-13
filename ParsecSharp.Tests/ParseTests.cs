﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PJanssen.ParsecSharp.IO;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class ParseTests
   {
      #region Succeed

      [TestMethod]
      public void Succeed_ReturnsGivenValue()
      {
         var parser = Parse.Succeed('x');
         var result = parser.Parse("");

         ParseAssert.ValueEquals('x', result);
      }

      #endregion

      #region Fail

      [TestMethod]
      public void Fail_ReturnsParseError()
      {
         var parser = Parse.Fail<int>("test");
         var result = parser.Parse("");

         ParseAssert.ErrorEquals("test", result);
      }

      #endregion

      #region Try

      [TestMethod]
      public void Try_Success_ReturnsSuccess()
      {
         var parser = from a in Parse.Try(Parse.Succeed("a"))
                      from b in Parse.Try(Parse.Succeed("b"))
                      select a + b;

         var result = parser.Parse("abc");

         ParseAssert.ValueEquals("ab", result);
      }

      [TestMethod]
      public void Try_Error_ReturnsErrorAndResetsPosition()
      {
         var parser = Parse.Try(from x in Chars.Any()
                                from y in Chars.Any()
                                from f in Parse.Fail<char>("test")
                                select x);

         IInputReader input = InputReader.Create("abc");
         Position expectedPosition = input.GetPosition();

         var result = parser.Parse(input);

         ParseAssert.IsError(result);
         Assert.AreEqual(expectedPosition, input.GetPosition());

      }

      #endregion
   }
}
