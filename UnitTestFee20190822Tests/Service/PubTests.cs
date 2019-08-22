using Microsoft.VisualStudio.TestTools.UnitTesting;
using UnitTestFee20190822.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestFee20190822.Repository;
using Rhino.Mocks;

namespace UnitTestFee20190822.Service.Tests
{
    [TestClass()]
    public class PubTests
    {
        [TestMethod()]
        public void CheckInTest_顧客1男2女_應回傳收費人數1人_使用RhinoMocks物件()
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

            decimal expected = 1;

            //act
            var actual = target.CheckIn(customers);

            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}