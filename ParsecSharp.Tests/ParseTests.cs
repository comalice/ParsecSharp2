﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PJanssen.ParsecSharp
{
   [TestClass]
   public class ParseTests
   {
      #region Success

      [TestMethod]
      public void Success_ReturnsGivenValue()
      {
         Parser<char> parser = Parse.Success('x');
         var result = parser.Run("");

         ParseAssert.ValueEquals('x', result);
      }

      #endregion

      #region Error

      [TestMethod]
      public void Error_ReturnsErrorMessage()
      {
         Parser<char> parser = Parse.Error<char>("test");
         var result = parser.Run("");

         ParseAssert.ErrorEquals("test", result);
      }

      #endregion

      #region Eof

      [TestMethod]
      public void Eof_EndOfInput_ReturnsSuccess()
      {
         Parser<Unit> parser = Parse.Eof();
         var result = parser.Run("");

         ParseAssert.IsSuccess(result);
      }

      [TestMethod]
      public void Eof_RemainingInput_ReturnsError()
      {
         Parser<Unit> parser = Parse.Eof();
         var result = parser.Run("abc");

         ParseAssert.IsError(result);
      }

      #endregion
   }
}
