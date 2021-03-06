﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SKCore.Collection;

namespace SKCore.Test.Collection
{
    [TestClass]
    public class SplitByTest
    {
        [TestMethod]
        public void SplitBySizeGiven3()
        {
            var source = new List<int> { 1, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 5, 6, 6 };
            var result = source.SplitBySize(3);

            Assert.IsTrue(result.NestedSequenceEqual(new List<List<int>>
            {
                new List<int> { 1, 1, 1 },
                new List<int> { 2, 2, 3 },
                new List<int> { 3, 3, 3 },
                new List<int> { 4, 4, 5 },
                new List<int> { 6, 6 },
            }));
        }

        [TestMethod]
        public void SplitBySizeGivenEmpty()
        {
            var source = new List<int>();
            var result = source.SplitBySize(3);

            Assert.IsTrue(result.NestedSequenceEqual(new List<List<int>>()));
        }

        [TestMethod]
        public void SplitByEquality()
        {
            var source = new List<int> { 1, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 5, 6, 6 };
            var result = source.SplitByEquality();

            Assert.IsTrue(result.NestedSequenceEqual(new List<List<int>>
            {
                new List<int> { 1, 1, 1 },
                new List<int> { 2, 2 },
                new List<int> { 3, 3, 3, 3 },
                new List<int> { 4, 4 },
                new List<int> { 5 },
                new List<int> { 6, 6 },
            }));
        }

        [TestMethod]
        public void SplitByEqualityGivenMaxSize()
        {
            var source = new List<int> { 1, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 5, 6, 6 };
            var result = source.SplitByEquality(3);

            Assert.IsTrue(result.NestedSequenceEqual(new List<List<int>>
            {
                new List<int> { 1, 1, 1 },
                new List<int> { 2, 2 },
                new List<int> { 3, 3, 3 },
                new List<int> { 3 },
                new List<int> { 4, 4 },
                new List<int> { 5 },
                new List<int> { 6, 6 },
            }));
        }

        [TestMethod]
        public void SplitByEqualityGivenEmpty()
        {
            var source = new List<int>();
            var result = source.SplitByEquality();

            Assert.IsTrue(result.NestedSequenceEqual(new List<List<int>>()));
        }

        [TestMethod]
        public void SplitByRegulalityConditionIncrease()
        {
            var source = new List<int> { 1, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 5, 6, 6, 1, 1, 2, 3, 3, 3, 4 };
            var result = source.SplitByRegularity((items, current) => items.Last() <= current);

            Assert.IsTrue(result.NestedSequenceEqual(new List<List<int>>
            {
                new List<int>{ 1, 1, 1, 2, 2, 3, 3, 3, 3, 4, 4, 5, 6, 6,},
                new List<int>{ 1, 1, 2, 3, 3, 3, 4 },
            }));
        }

        [TestMethod]
        public void SplitByRegulalityConditionSerialNumber()
        {
            var source = new List<int> { 1, 2, 3, 5, 6, 5, 6, 7, 8, -2, -1, 0, 2 };
            var result = source.SplitByRegularity((items, current) => current == items.Last() + 1);

            Assert.IsTrue(result.NestedSequenceEqual(new List<List<int>>
            {
                new List<int> { 1, 2, 3 },
                new List<int> { 5, 6 },
                new List<int> { 5, 6, 7, 8 },
                new List<int> { -2, -1, 0 },
                new List<int> { 2 },
            }));
        }
    }
}
