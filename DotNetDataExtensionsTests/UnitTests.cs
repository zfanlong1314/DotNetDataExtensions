﻿using System;
using System.Linq;
using System.Data;
using NUnit.Framework;
using DataExtensions;

namespace DotNetDataExtensionsTests
{
    public class UnitTests
    {
        private readonly DataTable _dtTest;

        public UnitTests()
        {
            _dtTest = CreateDataTable();
        }

        [TestCase]
        public void DataReader_ReturnsCorrectListCount()
        {
            using (var reader = _dtTest.CreateDataReader())
            {
                var customerList = reader.MapTo<Customer>();
                Assert.That(customerList.Count, Is.EqualTo(3));
            }
        }

        [TestCase]
        public void DataTable_ReturnsCorrectListCount()
        {
            var customerList = _dtTest.MapTo<Customer>();
            Assert.That(customerList.Count, Is.EqualTo(3));
        }

        [TestCase]
        public void DataReader_AreFieldsCorrectInFirstRecord()
        {
            using (var reader = _dtTest.CreateDataReader())
            {
                var customerList = reader.MapTo<Customer>();
                var cust = customerList.First(c => c.CustomerId == 1);
                Assert.That(cust.FirstName, Is.EqualTo("John"));
                Assert.That(cust.LastName, Is.EqualTo("Doe"));
                Assert.That(cust.Email, Is.EqualTo("johnDoe@maxmail.com"));
                Assert.That(cust.PhoneNumber, Is.EqualTo("345-231-9234"));
                Assert.That(cust.Address, Is.EqualTo("312 Brackish Rd"));
                Assert.That(cust.City, Is.EqualTo("Boston"));
                Assert.That(cust.State, Is.EqualTo("MA"));
                Assert.That(cust.Zip, Is.EqualTo(34567));
                Assert.That(cust.RewardsPoints, Is.EqualTo(23.3M));
            }
        }

        [TestCase]
        public void DataTable_AreFieldsCorrectInFirstRecord()
        {
            var customerList = _dtTest.MapTo<Customer>();
            var cust = customerList.First(c => c.CustomerId == 1);
            Assert.That(cust.FirstName, Is.EqualTo("John"));
            Assert.That(cust.LastName, Is.EqualTo("Doe"));
            Assert.That(cust.Email, Is.EqualTo("johnDoe@maxmail.com"));
            Assert.That(cust.PhoneNumber, Is.EqualTo("345-231-9234"));
            Assert.That(cust.Address, Is.EqualTo("312 Brackish Rd"));
            Assert.That(cust.City, Is.EqualTo("Boston"));
            Assert.That(cust.State, Is.EqualTo("MA"));
            Assert.That(cust.Zip, Is.EqualTo(34567));
            Assert.That(cust.RewardsPoints, Is.EqualTo(23.3M));
        }

        [TestCase]
        public void DataRow_AreFieldsCorrectInFirstRecord()
        {
            var dr = _dtTest.Select("CustomerId = 1");
            var cust = dr[0].MapTo<Customer>();
            Assert.That(cust.FirstName, Is.EqualTo("John"));
            Assert.That(cust.LastName, Is.EqualTo("Doe"));
            Assert.That(cust.Email, Is.EqualTo("johnDoe@maxmail.com"));
            Assert.That(cust.PhoneNumber, Is.EqualTo("345-231-9234"));
            Assert.That(cust.Address, Is.EqualTo("312 Brackish Rd"));
            Assert.That(cust.City, Is.EqualTo("Boston"));
            Assert.That(cust.State, Is.EqualTo("MA"));
            Assert.That(cust.Zip, Is.EqualTo(34567));
            Assert.That(cust.RewardsPoints, Is.EqualTo(23.3M));
        }

        [TestCase]
        public void DataReader_DoesNullableTypeSetNullCorrectly()
        {
            using (var reader = _dtTest.CreateDataReader())
            {
                var customerList = reader.MapTo<Customer>();
                var cust = customerList.First(c => c.CustomerId == 2);
                Assert.IsNull(cust.RewardsPoints);
            }
        }

        [TestCase]
        public void DataTable_DoesNullableTypeSetNullCorrectly()
        {
            var customerList = _dtTest.MapTo<Customer>();
            var cust = customerList.First(c => c.CustomerId == 2);
            Assert.IsNull(cust.RewardsPoints);
        }

        [TestCase]
        public void DataRow_DoesNullableTypeSetNullCorrectly()
        {
            var dr = _dtTest.Select("CustomerId = 2");
            var cust = dr[0].MapTo<Customer>();
            Assert.IsNull(cust.RewardsPoints);
        }

        [TestCase]
        public void DataReader_DoesNonNullableTypeSetDefaultCorrectly()
        {
            using (var reader = _dtTest.CreateDataReader())
            {
                var customerList = reader.MapTo<Customer>();
                var cust = customerList.First(c => c.CustomerId == 2);
                Assert.That(cust.Zip, Is.EqualTo(0));
            }
        }

        [TestCase]
        public void DataTable_DoesNonNullableTypeSetDefaultCorrectly()
        {
            var customerList = _dtTest.MapTo<Customer>();
            var cust = customerList.First(c => c.CustomerId == 2);
            Assert.That(cust.Zip, Is.EqualTo(0));
        }

        [TestCase]
        public void DataRow_DoesNonNullableTypeSetDefaultCorrectly()
        {
            var dr = _dtTest.Select("CustomerId = 2");
            var cust = dr[0].MapTo<Customer>();
            Assert.That(cust.Zip, Is.EqualTo(0));
        }

        [TestCase]
        public void DataReader_DoesDBNullOverrideCorrectly()
        {
            using (var reader = _dtTest.CreateDataReader())
            {
                var customerList = reader.MapTo(new Customer { Zip = 0 });
                var cust = customerList.First(c => c.CustomerId == 2);
                Assert.That(cust.Zip, Is.EqualTo(0));
            }
        }

        [TestCase]
        public void DataTable_DoesDBNullOverrideCorrectly()
        {
            var customerList = _dtTest.MapTo(new Customer { Zip = 0 });
            var cust = customerList.First(c => c.CustomerId == 2);
            Assert.That(cust.Zip, Is.EqualTo(0));
        }

        [TestCase]
        public void DataRow_DoesDBNullOverrideCorrectly()
        {
            var dr = _dtTest.Select("CustomerId = 2");
            var cust = dr[0].MapTo(new Customer { Zip = 0 });
            Assert.That(cust.Zip, Is.EqualTo(0));
        }

        [TestCase]
        public void DataReader_DoesDBNullReturnCorrectlyForString()
        {
            using (var reader = _dtTest.CreateDataReader())
            {
                var customerList = reader.MapTo<Customer>();
                var cust = customerList.First(c => c.CustomerId == 3);
                Assert.That(cust.PhoneNumber, Is.EqualTo(string.Empty));
            }
        }

        [TestCase]
        public void DataTable_DoesDBNullReturnCorrectlyForString()
        {
            var customerList = _dtTest.MapTo<Customer>();
            var cust = customerList.First(c => c.CustomerId == 3);
            Assert.That(cust.PhoneNumber, Is.EqualTo(string.Empty));
        }

        [TestCase]
        public void DataRow_DoesDBNullReturnCorrectlyString()
        {
            var dr = _dtTest.Select("CustomerId = 3");
            var cust = dr[0].MapTo<Customer>();
            Assert.That(cust.PhoneNumber, Is.EqualTo(string.Empty));
        }

        [TestCase]
        public void DataReader_DoesDBNullReturnCorrectlyForStringWithNullOverrideTurnedOff()
        {
            using (var reader = _dtTest.CreateDataReader())
            {
                var customerList = reader.MapTo<Customer>(false);
                var cust = customerList.First(c => c.CustomerId == 3);
                Assert.IsNull(cust.PhoneNumber);
            }
        }

        [TestCase]
        public void DataTable_DoesDBNullReturnCorrectlyForStringWithNullOverrideTurnedOff()
        {
            var customerList = _dtTest.MapTo<Customer>(false);
            var cust = customerList.First(c => c.CustomerId == 3);
            Assert.IsNull(cust.PhoneNumber);
        }

        [TestCase]
        public void DataRow_DoesDBNullReturnCorrectlyForStringWithNullOverrideTurnedOff()
        {
            var dr = _dtTest.Select("CustomerId = 3");
            var cust = dr[0].MapTo<Customer>(false);
            Assert.IsNull(cust.PhoneNumber);
        }

        private static DataTable CreateDataTable()
        {
            Customer c;
            var dt = new DataTable();

            dt.Columns.Add(nameof(c.CustomerId), typeof(long));
            dt.Columns.Add(nameof(c.FirstName), typeof(string));
            dt.Columns.Add(nameof(c.LastName), typeof(string));
            dt.Columns.Add(nameof(c.Email), typeof(string));
            dt.Columns.Add(nameof(c.PhoneNumber), typeof(string));
            dt.Columns.Add(nameof(c.Address), typeof(string));
            dt.Columns.Add(nameof(c.City), typeof(string));
            dt.Columns.Add(nameof(c.State), typeof(string));
            dt.Columns.Add(nameof(c.Zip), typeof(int));
            dt.Columns.Add(nameof(c.RewardsPoints), typeof(decimal));

            var dr = dt.NewRow();
            dr[nameof(c.CustomerId)] = 1;
            dr[nameof(c.FirstName)] = "John";
            dr[nameof(c.LastName)] = "Doe";
            dr[nameof(c.Email)] = "johnDoe@maxmail.com";
            dr[nameof(c.PhoneNumber)] = "345-231-9234";
            dr[nameof(c.Address)] = "312 Brackish Rd";
            dr[nameof(c.City)] = "Boston";
            dr[nameof(c.State)] = "MA";
            dr[nameof(c.Zip)] = "34567";
            dr[nameof(c.RewardsPoints)] = 23.3M;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[nameof(c.CustomerId)] = 2;
            dr[nameof(c.FirstName)] = "Jake";
            dr[nameof(c.LastName)] = "McPhelson";
            dr[nameof(c.Email)] = "Jake123@mail.com";
            dr[nameof(c.PhoneNumber)] = DBNull.Value;
            dr[nameof(c.Address)] = "64 Back Road Drive";
            dr[nameof(c.City)] = "Houston";
            dr[nameof(c.State)] = "TX";
            dr[nameof(c.Zip)] = DBNull.Value;
            dr[nameof(c.RewardsPoints)] = DBNull.Value;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[nameof(c.CustomerId)] = 3;
            dr[nameof(c.FirstName)] = "Bob";
            dr[nameof(c.LastName)] = "Jackson";
            dr[nameof(c.Email)] = "Jake123@vixmix.com";
            dr[nameof(c.PhoneNumber)] = DBNull.Value;
            dr[nameof(c.Address)] = "2345 Cumberland St.";
            dr[nameof(c.City)] = "Nashville";
            dr[nameof(c.State)] = "TN";
            dr[nameof(c.Zip)] = 37210;
            dr[nameof(c.RewardsPoints)] = 0M;
            dt.Rows.Add(dr);
            return dt;
        }
    }

    public class Customer
    {
        public long CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public decimal? RewardsPoints { get; set; }
    }
}
