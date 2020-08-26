using Microsoft.VisualStudio.TestTools.UnitTesting;
using NullVoid_Lib;
using System;

namespace NullVoid_Tests
{
  [TestClass]
  public class UnitTest1
  {
    [TestMethod]
    public void TestMethod1()
    {
      int[] a = new int[4] { 0, 1, 2, 3 };
      int[] b = new int[2] { 0, 3 };
      Assert.IsTrue(Enumerable.Both(b, a, (v1, v2) => v1 == v2));
    }
  }
}
