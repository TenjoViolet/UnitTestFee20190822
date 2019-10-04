using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestFee20190822.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestFee20190822.Repository;
using Rhino.Mocks;
using NSubstitute;
using Arg = NSubstitute.Arg;

namespace UnitTestFee20190822.Service.Tests
{
    [TestClass()]
    public class PubTests
    {
        [TestMethod()]
        public void CheckInTest_顧客1男2女_應回傳收費人數1人_使用RhinoMocks物件()
        {
            //arrange準備受測物件、參數、預期結果
            ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
            Pub target = new Pub(stubCheckInFee);

            stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

            var customers = new List<Customer>
        {
            new Customer{ IsMale=true},
            new Customer{ IsMale=false},
            new Customer{ IsMale=false},
        };

            decimal expected = 1;

            //act執行受測方法
            var actual = target.CheckIn(customers);

            //assert驗證執行結果與預測結果是否一致
            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void Test_Income_顧客1男2女_應回傳收費人數1人_模擬每一人收費為100元_預期結果門票收入總數為100_使用RhinoMocks物件()
        {
            //arrange
            ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
            Pub target = new Pub(stubCheckInFee);

            stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

            var customers = new List<Customer>
            {
                new Customer{ IsMale=true},
                new Customer{ IsMale=false},
                new Customer{ IsMale=false},
            };

            var inComeBeforeCheckIn = target.GetInCome();
            Assert.AreEqual(0, inComeBeforeCheckIn);

            decimal expectedIncome = 100;

            //act
            var chargeCustomerCount = target.CheckIn(customers);

            var actualIncome = target.GetInCome();

            //assert
            Assert.AreEqual(expectedIncome, actualIncome);
        }

        [TestMethod()]
        public void CheckInTest_顧客1男2女_應回傳收費人數1人_使用NSubstitute物件()
        {
            //arrange準備受測物件、參數、預期結果
            ICheckInFee CheckInFee = Substitute.For<ICheckInFee>();
            Pub pub = new Pub(CheckInFee);
            CheckInFee.GetFee(Arg.Any<Customer>()).Returns(100);
            List<Customer> customers = new List<Customer>
            {
                new Customer{ IsMale=true},
                new Customer{ IsMale=false},
                new Customer{ IsMale=false},
            };

            decimal expected = 1;
            //act執行受測方法
            
            decimal actual = pub.CheckIn(customers);

            //assert驗證執行結果與預測結果是否一致
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Income_顧客1男2女_應回傳收費人數1人_模擬每一人收費為100元_預期結果門票收入總數為100_使用NSubstitute物件()
        {
            //arrange準備受測物件、參數、預期結果
            ICheckInFee CheckInFee = Substitute.For<ICheckInFee>();
            Pub pub = new Pub(CheckInFee);
            CheckInFee.GetFee(Arg.Any<Customer>()).Returns(100);

            List<Customer> customers = new List<Customer>
            {
                new Customer{ IsMale=false},
                new Customer{ IsMale=true},
                new Customer{ IsMale=false},
            };

            var inComeBeforeCheckIn = pub.GetInCome();
            Assert.AreEqual(0, inComeBeforeCheckIn);

            decimal expectedIncome = 100;

            //act執行受測方法
           
            decimal chargeCustomerCount = pub.CheckIn(customers);
            decimal actualIncome = pub.GetInCome();

            //assert驗證執行結果與預測結果是否一致
            Assert.AreEqual(expectedIncome, actualIncome);

        }

    }
}