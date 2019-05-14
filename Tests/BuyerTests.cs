using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Livestock_Auction_Tests
{
    [TestClass]
    public class BuyerTests
    {
        //TESTS...
        // * Register a buyer
        //    ** Ensure the buyer is added to the database
        //    ** Ensure the buyer is available from clsBuyers
        // * Add buyers to the database prior to initalizing clsBuyers
        //    ** Ensure load loads all buyers from the database
        //    ** Ensure update does not find any buyers
        // * Add a buyer directly to the database and call update
        //    ** Ensure the buyer is available from clsBuyers
        //    ** Call update a second time and make sure the new buyer is not found again


        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
