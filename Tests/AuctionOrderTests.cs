using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Data;
using System.Data.SqlClient;

namespace Livestock_Auction_Tests
{
    [TestClass]
    public class AuctionOrderTests
    {
        //const string TEST_CONNECTION_STRING = "Persist Security Info=True;Data Source=10.22.2.10\\SQLEXPRESS;Initial Catalog=master;Trusted_Connection=True;Pooling=false";
        const string TEST_CONNECTION_STRING = "Persist Security Info=True;Data Source=localhost\\SQLEXPRESS;Initial Catalog=master;Trusted_Connection=True;Pooling=false";

        SqlConnection m_dbConn = null;
        string m_dbName = "";

        //Helper function to generate random market items
        private void GenerateTestData_MarketItems()
        {
            new SqlCommand("ALTER TABLE Market ADD CONSTRAINT commitdate_unique UNIQUE ([CommitDate]);", m_dbConn).ExecuteNonQuery();

            //Generate the typical array of market items
            Livestock_Auction.clsDB.Market.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsMarketItem(1, "Hog", 0.65, "lb", true, true));
            Livestock_Auction.clsDB.Market.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsMarketItem(2, "Dairy Steer", 0.95, "lb", true, true));
            Livestock_Auction.clsDB.Market.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsMarketItem(3, "Beef Steer", 1.05, "lb", true, true));
            Livestock_Auction.clsDB.Market.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsMarketItem(4, "Lamb", 1.20, "lb", true, true));
            Livestock_Auction.clsDB.Market.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsMarketItem(5, "Goat", 1.40, "lb", true, true));
            Livestock_Auction.clsDB.Market.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsMarketItem(6, "Broilers", 0, "Pair", true, true));
            Livestock_Auction.clsDB.Market.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsMarketItem(7, "Ducks", 0, "Pair", true, true));
            Livestock_Auction.clsDB.Market.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsMarketItem(8, "Welded Items", 0, "Item", false, false));

            Assert.AreEqual(9, Livestock_Auction.clsDB.Market.Count, "Failed to generate the expected number of market items in the database");
        }

        //Helper function to generate random exhibtors and exhibits
        private int GenerateTestData_Exhibits(int Count)
        {
            int iExhibitsCount = 0;
            //Randomly generate the specified number of exhibitors...
            Random randBuyerCount = new Random();
            System.Threading.Thread.Sleep(10);
            Random randWeight = new Random();
            System.Threading.Thread.Sleep(10);
            Random randExhibitType = new Random();
            System.Threading.Thread.Sleep(10);
            Random randChampionState = new Random();
            System.Threading.Thread.Sleep(10);
            Random randTakeBack = new Random();

            for (int i = 0; i < Count; i++)
            {
                Livestock_Auction.clsDB.Exhibitors.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsExhibitor(i, string.Format("FN_{0}", i), string.Format("LN_{0}", i), ""));

                int iExhibits = randBuyerCount.Next(1, 4);   //As of 2014, Exhibitors are allowed to show up to three animals
                for (int j = 0; j < iExhibits; j++)
                {
                    int iTag = i * 10 + j;
                    int iWeight = 1;
                    int iType = randExhibitType.Next(1, 9);
                    if (iType > 5)
                    {
                        iWeight = randWeight.Next(10, 2000);
                    }
                    int iChampState = randChampionState.Next(1, 40);
                    if (iChampState > 3)
                    {
                        iChampState = 3;
                    }
                    bool bROG = randChampionState.Next(1, 30) == 1 ? true : false;
                    Livestock_Auction.DB.NoYes nyTakeBack = (Livestock_Auction.DB.NoYes)randTakeBack.Next(1, 3);

                    Livestock_Auction.clsDB.Exhibits.Commit(Livestock_Auction.DB.CommitAction.Modify, new Livestock_Auction.DB.clsExhibit(i * 10 + j, i, iType, iChampState, bROG, iWeight, nyTakeBack, true, ""));
                    System.Threading.Thread.Sleep(1);
                    iExhibitsCount++;
                }
            }

            Assert.AreEqual(Count, Livestock_Auction.clsDB.Exhibitors.Count, "Failed to generate the expected number of exhibitors in the database");
            Assert.AreEqual(iExhibitsCount, Livestock_Auction.clsDB.Exhibits.Count, "Failed to generate the expected number of exhibits in the database");

            return iExhibitsCount;
        }

        private void ValidateAuctionOrder(int iExhibitors, int iExhibits)
        {
            //Check for order requirements...
            //  * All rate of gain animals should go first, followed by Grand Champion, then Reserve Champion, then all other animals
            bool bRog = false;
            int iChampState = -1;
            for (int i = 1; i <= iExhibits; i++)
            {
                Livestock_Auction.DB.clsAuctionIndex Index = Livestock_Auction.clsDB.Auction.Items[i];
                if (iChampState == -1)
                {
                    bRog = Index.Exhibit.RateOfGain;
                    iChampState = Index.Exhibit.ChampionStatus;
                }
                else
                {
                    if (Index.Exhibit.RateOfGain != bRog)
                    {
                        if (bRog)
                        {
                            bRog = false;
                            iChampState = Livestock_Auction.DB.ChampionState.Grand_Champion;
                        }
                        else
                        {
                            Assert.Fail("Expected all Rate of Gain animals before any non-Rate of Gain animals");
                        }
                    }
                    if (Index.Exhibit.ChampionStatus != iChampState)
                    {
                        if (Index.Exhibit.ChampionStatus > iChampState)
                        {
                            iChampState = Index.Exhibit.ChampionStatus;
                        }
                        else
                        {
                            Assert.Fail(string.Format("Expected all {0} animals before any {1} animals", (Livestock_Auction.DB.ChampionState)iChampState, Index.Exhibit.ChampionStatus));
                        }
                    }
                }
            }

            //  * The same exhibitor should never appear twice consecutivley
            int iLastExhibitor = -1;
            for (int i = 1; i <= iExhibits; i++)
            {
                Livestock_Auction.DB.clsAuctionIndex Index = Livestock_Auction.clsDB.Auction.Items[i];
                Assert.AreNotEqual(iLastExhibitor, Index.Exhibit.ExhibitorNumber, string.Format("The same exhibitor appeared twice consecutively on index {0}", i));
                iLastExhibitor = Index.Exhibit.ExhibitorNumber;
            }

            //  * The same animal should never appear twice consecutively
            //  So, as it turns out, this is not explicity forbidden....
            //int iLastExhibit = -1;
            //for (int i = 1; i <= iExhibits; i++)
            //{
            //    Livestock_Auction.DB.clsAuctionIndex Index = Livestock_Auction.clsDB.Auction.Items[i];
            //    Assert.AreNotEqual(iLastExhibit, Index.Exhibit.MarketID, string.Format("The same exhibit appeared twice consecutively on index {0}", i));
            //    iLastExhibit = Index.Exhibit.MarketID;
            //}

            //  * All exhibitors should should have their turn first before any exhibitor gets to go the second time (unless they have multiple animals with a champion status)
            int[] iOccurences = new int[iExhibitors];
            int iCurOccurence = 1;
            for (int i = 1; i <= iExhibits; i++)
            {
                Livestock_Auction.DB.clsAuctionIndex Index = Livestock_Auction.clsDB.Auction.Items[i];

                iOccurences[Index.Exhibit.ExhibitorNumber]++;
                if (iCurOccurence != iOccurences[Index.Exhibit.ExhibitorNumber])
                {
                    if (iOccurences[Index.Exhibit.ExhibitorNumber] > iCurOccurence)
                    {
                        //Don't advance the current occurence count if the exhibits are champion
                        if (!Index.Exhibit.RateOfGain && Index.Exhibit.ChampionStatus == Livestock_Auction.DB.ChampionState.Other)
                        {
                            iCurOccurence = iOccurences[Index.Exhibit.ExhibitorNumber];
                        }
                    }
                    else
                    {
                        Assert.Fail(string.Format("Expected all exhibitors to get {0} chance before one gets {1} on line {2}", iCurOccurence, iOccurences[Index.Exhibit.ExhibitorNumber], i));
                    }
                }
            }
        }

        [TestInitialize]
        public void InitalizeDatabase()
        {
            m_dbConn = new SqlConnection(TEST_CONNECTION_STRING);
            m_dbConn.Open();

            Assert.AreEqual(ConnectionState.Open, m_dbConn.State, "Failed to connect to database server");

            //Create the connection stirng with a random database name
            m_dbName = "LATEST_" + Guid.NewGuid().ToString().Replace("-", "_");
            bool bDBCreated = Livestock_Auction.clsDB.CreateDatabase(m_dbConn, m_dbName);
            Assert.AreEqual(true, bDBCreated, "Failed to create database");

            Livestock_Auction.clsDB.Connect(m_dbConn);
        }

        [TestCleanup]
        public void CleanupDatabase()
        {
            //Disconnect and delete the database
            Livestock_Auction.clsDB.Disconnect();

            System.Threading.Thread.Sleep(500); //Sleep a bit to give the background thread time to cleanup

            SqlCommand sqlUse = new SqlCommand("USE master;", m_dbConn);
            sqlUse.ExecuteNonQuery();
            SqlCommand sqlDrop = new SqlCommand(string.Format("DROP DATABASE {0};", m_dbName), m_dbConn);
            sqlDrop.ExecuteNonQuery();

            m_dbConn.Close();
        }

        // ** Verification for all tests **
        //    ** Ensure there are no duplicates in the order
        //    ** Verify the latest revision number changes as expected
        //    ** Reinitalize the database and rerun all checks

        //TESTS...
        // * Generate the auction order...
        //    ** Ensure that order meets all of the requirements
        //    ** Ensure that all exhibits are in the order
        //    ** Repeat this test x times and verify that the order is different each time
        // WARNING, this test is non-deterministic. There is a lot of random number generation, both
        //  in the test and in the code that is being tested. It is possible for the exhibit
        //  generator to generate a sample set with an insufficent distribution of exhibitors and
        //  exhibit types, making order generation impossible. It is also possible for almost any
        //  given set of exhibits for the auction order to fail due to random selections that reduce
        //  the distribution of the remaining exhibits.
        [TestMethod]
        public void GenerateAuctionOrder()
        {
            int EXHIBITORS = 75;

            //Generate some test data
            GenerateTestData_MarketItems();
            int iExhibits = GenerateTestData_Exhibits(EXHIBITORS);
            System.Collections.Generic.List<int> lstItems1 = new System.Collections.Generic.List<int>(iExhibits + 1);
            System.Collections.Generic.List<int> lstItems2 = new System.Collections.Generic.List<int>(iExhibits + 1);
            for (int i = 0; i <= iExhibits; i++ )
            {
                lstItems1.Add(-1);
                lstItems2.Add(-1);
            }
            
            //Sanity check on the initial state...
            Assert.AreEqual(0, Livestock_Auction.clsDB.Auction.LatestRevision, "Initial revision should be zero");
            Assert.AreEqual(0, Livestock_Auction.clsDB.Auction.Items.Count, "Expected to start off with empty auction order");
            Assert.AreEqual(iExhibits, Livestock_Auction.clsDB.Auction.LoadUnsortedExhibits(0).Count, "Expected all items to be unsorted");

            //Generate an auction order...
            Livestock_Auction.clsDB.Auction.GenerateAuctionOrder();

            //Check the state after order generation
            Assert.AreEqual(1, Livestock_Auction.clsDB.Auction.LatestRevision, "Revision should have been incremented to one");
            Assert.AreEqual(iExhibits, Livestock_Auction.clsDB.Auction.Items.Count, "Not all items entered into first auction order");
            Assert.AreEqual(0, Livestock_Auction.clsDB.Auction.LoadUnsortedExhibits(1).Count, "Expected no items to be unsorted at revision 1");

            //Diagnostic log the order
            System.Diagnostics.Debug.WriteLine("Auction Order 1");
            for (int i = 1; i <= iExhibits; i++)
            {
                Livestock_Auction.DB.clsAuctionIndex Index = Livestock_Auction.clsDB.Auction.Items[i];
                System.Diagnostics.Debug.WriteLine(string.Format("{0,3} {1,4} {2,-20} {3,3} {4} {5}", i, Index.Exhibit.TagNumber, Index.Exhibit.MarketItem.MarketType, Index.Exhibit.ExhibitorNumber, Index.Exhibit.ChampionStatus, Index.Exhibit.RateOfGainText));
            }

            //Check for duplicates
            foreach (Livestock_Auction.DB.clsAuctionIndex Index in Livestock_Auction.clsDB.Auction)
            {
                //Note, In real life, tag numbers are not globally unique, but the algorithm used by GenerateAuctionOrder ensures that they are...
                if (lstItems1.Contains(Index.Exhibit.TagNumber))
                {
                    Assert.Fail("Found duplicate exhibit in list");
                }
                else
                {
                    lstItems1[Index.AuctionIndex] = Index.Exhibit.TagNumber;
                }
            }

            ValidateAuctionOrder(EXHIBITORS, iExhibits);

            //Generate another auction order...
            Livestock_Auction.clsDB.Auction.GenerateAuctionOrder();

            //Check the state after order generation
            Assert.AreEqual(2, Livestock_Auction.clsDB.Auction.LatestRevision, "Revision should have been incremented to two");
            Assert.AreEqual(iExhibits, Livestock_Auction.clsDB.Auction.Items.Count, "Not all items entered into second auction order");
            Assert.AreEqual(0, Livestock_Auction.clsDB.Auction.LoadUnsortedExhibits(2).Count, "Expected no items to be unsorted at revision 2");

            //Diagnostic log the order
            System.Diagnostics.Debug.WriteLine("Auction Order 2");
            for (int i = 1; i <= iExhibits; i++)
            {
                Livestock_Auction.DB.clsAuctionIndex Index = Livestock_Auction.clsDB.Auction.Items[i];
                System.Diagnostics.Debug.WriteLine(string.Format("{0,3} {1,4} {2,-20} {3,3} {4} {5}", i, Index.Exhibit.TagNumber, Index.Exhibit.MarketItem.MarketType, Index.Exhibit.ExhibitorNumber, Index.Exhibit.ChampionStatus, Index.Exhibit.RateOfGainText));
            }

            //Check for duplicates
            foreach (Livestock_Auction.DB.clsAuctionIndex Index in Livestock_Auction.clsDB.Auction)
            {
                if (lstItems2.Contains(Index.Exhibit.TagNumber))
                {
                    Assert.Fail("Found duplicate exhibit in second list");
                }
                else
                {
                    lstItems2[Index.AuctionIndex] = Index.Exhibit.TagNumber;
                }
            }

            //Check for order requirements...
            ValidateAuctionOrder(EXHIBITORS, iExhibits);

            //Make sure lists are different...
            bool bFail = true;
            for (int i = 1; i <= iExhibits; i++)
            {
                if (lstItems1[i] != lstItems2[i])
                {
                    bFail = false;
                }
            }
            Assert.AreEqual(false, bFail, "Expected different auction orders");
        }

        // * Add an item to the end of the order...
        //    ** Verify the added item is in the correct place
        //    ** Verify nothing else changed position
        // * Insert an item in to the middle of the order...
        //    ** Verify the added item is in the correct place
        //    ** Verify nothing else changed position
        // * Remove an item from the order...
        //    ** Verify the added item is no longer in the order
        //    ** Verify nothing changed position
        // * Move an item up in the order...
        //    ** Verify the moved item is in the correct place
        //    ** Verify nothing else changed position
        // * Move an item down in the order...
        //    ** Verify the moved item is in the correct place
        //    ** Verify nothing else changed position
        // * Make changes to the order and revert to a previous revision
        //    ** Verify the order matches previous order
        // * Generate a new order and revert to a previous revision
        //    ** Verify the order matches previous order
    }
}
