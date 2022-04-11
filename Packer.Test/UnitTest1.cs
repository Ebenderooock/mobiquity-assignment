using NUnit.Framework;
using com.mobiquity.packer;
using com.mobiquity.packer.models;
using System;

namespace com.mobiquity.packer.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InvalidPackageWeightLimit()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
           try { Packer.Pack("./Resources/InvalidWeightLimit.txt");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void InvalidItemWeightLimit()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
            try
            {
                Packer.Pack("./Resources/InvalidItemWeightLimit.txt");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void NonExistentFile()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
            try
            {
                Packer.Pack(@"C:\randomfilethatshouldnotexist");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void InvalidItemPriceLimit()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
            try
            {
                Packer.Pack("./Resources/InvalidPriceLimit.txt");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void InvalidItemCount()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
            try
            {
                Packer.Pack("./Resources/InvalidItemCount.txt");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void InvalidPriceInput()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
            try
            {
                Packer.Pack("./Resources/InvalidPriceInput.txt");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void InvalidItemWeightInput()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
            try
            {
                Packer.Pack("./Resources/InvalidItemWeightInput.txt");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void InvalidItemIndex()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
            try
            {
                Packer.Pack("./Resources/InvalidItemIndex.txt");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test]
        public void InvalidPackageWeight()
        {
            // This should throw an API Exception due to the weight limit for the package being exceeded
            try
            {
                Packer.Pack("./Resources/InvalidPackageWeight.txt");
                Assert.Fail("Expected an exception");
            }
            catch (APIException ex)
            {
                Assert.Pass();
            }

        }

        [Test(ExpectedResult = "4\r\n-\r\n2,7\r\n8,9")]
        public string ValidResultSet1()
        {
            // This should not fail.
            try
            {
                return Packer.Pack("./Resources/ValidResultSet1.txt");
            }
            catch (Exception ex)
            {
                Assert.Fail("Did not expect an exception");
                return ex.Message;
            }

        }
        [Test(ExpectedResult = "1\r\n-\r\n2")]
        public string ValidResultSet2()
        {
            // This should not fail.
            try
            {
                return Packer.Pack("./Resources/ValidResultSet2.txt");
            }
            catch (Exception ex)
            {
                Assert.Fail("Did not expect an exception - " + ex.Message);
                return ex.Message;
            }

        }
    }
}