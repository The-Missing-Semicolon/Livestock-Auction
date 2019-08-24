using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Linq;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace Livestock_Auction.DB
{
    public class clsAuctionOrder : clsAuctionDataCollection<IDAuctionDataKey, clsAuctionIndex, DB.Setup.AuctionOrder>, IEnumerable<clsAuctionIndex>
    {
        Dictionary<int, clsAuctionIndex> m_dictAuctionOrder;
        Dictionary<int, clsAuctionRevision> m_dictRevisionHistory;

        //Use similarly to dtLastUpdate in the other AuctionDataCollection classes
        int m_iCurrentRevision = 0;

        public clsAuctionOrder(IDbConnection Connection)
            : base(Connection)
        {
            LoadRevisionHistory();
        }

        //Populate dictAuctionOrder with the current auction order
        public override void Load()
        {
            //Load the auction order items
            base.Load();

            m_iCurrentRevision = LatestRevision;

            m_dictAuctionOrder = new Dictionary<int, clsAuctionIndex>();
            RecomputeOrder(null);
        }

        public override void ConnectEvents()
        {
            foreach (clsAuctionIndex Index in this)
            {
                Index.RefreshListItem();
            }

            clsDB.Buyers.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Buyers_Updated);
            clsDB.Exhibitors.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Exhibitors_Updated);
            clsDB.Purchases.Updated += new EventHandler<DatabaseUpdatedEventArgs>(Purchases_Updated);
        }

        protected override void onUpdated(DatabaseUpdatedEventArgs e)
        {
            foreach (clsAuctionIndex index in e.UpdatedItems.Keys)
            {
                if (index.RevisionIndex > m_iCurrentRevision)
                {
                    m_iCurrentRevision = index.RevisionIndex;
                }
            }

            DatabaseUpdatedEventArgs e2 = RecomputeOrder(e);

            //Pass through to the updated handlers...
            base.onUpdated(e2);
        }

        private DatabaseUpdatedEventArgs RecomputeOrder(DatabaseUpdatedEventArgs e)
        {
            DatabaseUpdatedEventArgs e2 = new DatabaseUpdatedEventArgs();

            int iRevIndex = 0;

            //Remove any deleted items from the order before starting
            if (e != null)
            {
                foreach (clsAuctionIndex AuctionIndex in e.UpdatedItems.Keys)
                {
                    if (e.UpdatedItems[AuctionIndex] == CommitAction.Delete)
                    {
                        e2.UpdatedItems.Add(AuctionIndex, CommitAction.Delete);
                    }
                }
            }

            //Put them in order based on the linked list...
            if (m_dictCollection.Count > 0)
            {
                //  Find the starting element
                clsAuctionIndex CurrentIndex = null;
                foreach (clsAuctionIndex AuctionIndex in m_dictCollection.Values)
                {
                    if (AuctionIndex.PrevExhibit < 0 && AuctionIndex.RevisionIndex >= iRevIndex)
                    {
                        CurrentIndex = AuctionIndex;
                        iRevIndex = AuctionIndex.RevisionIndex;
                    }
                }

                int iOrderIndex = 1;
                m_dictAuctionOrder[iOrderIndex] = CurrentIndex;

                //If this item was part of the updated items dictionary, or the index changed, pass it through
                //  The assumption here is that if the item was updated, it's auction index will have been reset to -1;
                if (CurrentIndex.AuctionIndex != iOrderIndex) //(UpdatedItems != null && (UpdatedItems.Contains(CurrentIndex.Key) || CurrentIndex.AuctionIndex != iOrderIndex))
                {
                    e2.UpdatedItems.Add(CurrentIndex, CommitAction.Modify);
                }

                CurrentIndex.AuctionIndex = iOrderIndex;

                //  Walk the list
                int iCurrentIndex = CurrentIndex.NextExhibit;
                while (iCurrentIndex > 0)
                {
                    iOrderIndex++;
                    CurrentIndex = m_dictCollection[iCurrentIndex];
                    m_dictAuctionOrder[iOrderIndex] = CurrentIndex;

                    //If this item was part of the updated items dictionary, or the index changed, pass it through
                    if (CurrentIndex.AuctionIndex != iOrderIndex)
                    {
                        e2.UpdatedItems.Add(CurrentIndex, CommitAction.Modify);
                    }

                    CurrentIndex.AuctionIndex = iOrderIndex;
                    iCurrentIndex = CurrentIndex.NextExhibit;
                }
            }

            //Truncate the auction order...
            while (m_dictAuctionOrder.Count > m_dictCollection.Count)
            {
                m_dictAuctionOrder.Remove(m_dictCollection.Count + 1);
            }

            return e2;
        }
       

        //Updated Buyers handler, keeps the buyers names up to date
        void Buyers_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            foreach (DB.clsAuctionIndex Index in this)
            {
                if (Index.ListView == null)
                {
                    Index.RefreshListItem();
                }
                else
                {
                    Index.ListView.Invoke(new System.Windows.Forms.MethodInvoker(Index.RefreshListItem));
                }
            }
        }

        void Exhibitors_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            lock (this)
            {
                foreach (DB.clsAuctionIndex Index in this)
                {
                    if (Index.ListView == null)
                    {
                        Index.RefreshListItem();
                    }
                    else
                    {
                        Index.ListView.Invoke(new System.Windows.Forms.MethodInvoker(Index.RefreshListItem));
                    }
                }
            }
        }

        //Updated Purchases handler, keeps the checked out column up to date
        void Purchases_Updated(object sender, DatabaseUpdatedEventArgs e)
        {
            foreach (DB.clsAuctionIndex Index in this)
            {
                if (Index.ListView == null)
                {
                    Index.RefreshListItem();
                }
                else
                {
                    Index.ListView.Invoke(new System.Windows.Forms.MethodInvoker(Index.RefreshListItem));
                }
            }
        }

        //Populate dictRevisionHistory with a list of revisions
        public void LoadRevisionHistory()
        {
            m_dictRevisionHistory = new Dictionary<int, clsAuctionRevision>();
            m_dictRevisionHistory.Add(0, new clsAuctionRevision(0, DateTime.MinValue, 0, 0));

            IDbCommand dbCommand = m_dbConn.CreateCommand();
            //dbCommand.CommandText = "SELECT RevisionIndex, MAX(RevisionDate) AS RevisionDate, COUNT(*) AS RecordsAffected, CASE WHEN MAX(RevisionDeleteDate) IS NULL THEN 0 ELSE 1 END AS RevisionReverted FROM AuctionOrder GROUP BY RevisionIndex ORDER BY RevisionIndex";
            dbCommand.CommandText = "SELECT RevisionIndex, MAX(CommitDate) AS RevisionDate, COUNT(*) AS RecordsAffected, 0 AS RevisionReverted FROM AuctionOrder GROUP BY RevisionIndex ORDER BY RevisionIndex";
            IDataReader dbReader = dbCommand.ExecuteReader();
            while (dbReader.Read())
            {
                DateTime RevisionDate;
                if (dbReader["RevisionDate"] is DateTime)
                {
                    RevisionDate = (DateTime)dbReader["RevisionDate"];
                }
                else
                {
                    RevisionDate = DateTime.Parse(dbReader["RevisionDate"].ToString());

                }

                clsAuctionRevision NewRevision = new clsAuctionRevision((Int32)dbReader["RevisionIndex"], RevisionDate, (Int32)dbReader["RecordsAffected"], (Int32)dbReader["RevisionReverted"]);
                m_dictRevisionHistory.Add((Int32)dbReader["RevisionIndex"], NewRevision);
            }
            dbReader.Close();
        }

        //Return a list containing the auction order for the specified revision
        public List<clsAuctionIndex> LoadRevision(int RevisionIndex)
        {
            DatabaseUpdatedEventArgs e2 = new DatabaseUpdatedEventArgs();

            //Compute the differences between the current order and the specified revision...
            IDbCommand dbLoad = m_dbConn.CreateCommand();
            dbLoad.CommandText = "SELECT " +
                                 "   AuctionOrder.CommitDate, " +
                                 "   AuctionOrder.RevisionIndex, " +
                                 "   AuctionOrder.ID, " +
                                 "   AuctionOrder.PrevID, " +
                                 "   AuctionOrder.NextID, " +
                                 "   AuctionOrder.ExhibitTag, " +
                                 "   AuctionOrder.ExhibitItem, " +
                                 "   AuctionOrder.Comments " +
                                 "FROM " +
                                 "    AuctionOrder " +
                                 "    INNER JOIN " +
                                 "        (SELECT ID, MAX(CommitDate) AS CommitDate FROM AuctionOrder WHERE RevisionIndex <= @RevisionIndex GROUP BY ID) AS LastModified " +
                                 "        ON AuctionOrder.CommitDate = LastModified.CommitDate AND AuctionOrder.ID = LastModified.ID " +
                                 "WHERE " +
                                 "    AuctionOrder.CommitAction = 0 " +
                                 "ORDER BY " +
                                 "    RevisionIndex DESC";

            IDbDataParameter param = dbLoad.CreateParameter();
            param.ParameterName = "@RevisionIndex";
            param.Value = RevisionIndex;
            dbLoad.Parameters.Add(param);

            //Apply the changes to m_dictCollection

            //Pass the changes to the updated event


            //List<clsAuctionIndex> lstAuctionOrder = new List<clsAuctionIndex>();
            //
            //IDbCommand dbLoad = m_dbConn.CreateCommand();
            //
            //dbLoad.CommandText = "SELECT AuctionOrder.AuctionIndex, AuctionOrder.SubIndex, AuctionOrder.ExhibitTag, AuctionOrder.ExhibitItem FROM AuctionOrder INNER JOIN (SELECT MAX(RevisionIndex) AS RevisionIndex, AuctionIndex, SubIndex FROM AuctionOrder WHERE RevisionIndex <= @RevisionIndex AND RevisionDeleteDate IS NULL GROUP BY AuctionIndex, SubIndex) AS LatestRevision ON LatestRevision.AuctionIndex = AuctionOrder.AuctionIndex AND LatestRevision.SubIndex = AuctionOrder.SubIndex AND LatestRevision.RevisionIndex = AuctionOrder.RevisionIndex WHERE AuctionOrder.RevisionAction = 0 ORDER BY AuctionOrder.AuctionIndex ASC, AuctionOrder.SubIndex ASC";
            //IDbDataParameter param = dbLoad.CreateParameter();
            //param.ParameterName = "@RevisionIndex";
            //param.Value = RevisionIndex;
            //dbLoad.Parameters.Add(param);
            //
            //IDataReader AuctionReader = dbLoad.ExecuteReader();
            //
            //while (AuctionReader.Read())
            //{
            //    clsAuctionIndex NewEntry = new clsAuctionIndex((Int32)AuctionReader["AuctionIndex"], (Int32)AuctionReader["SubIndex"], (Int32)AuctionReader["ExhibitTag"], (Int32)AuctionReader["ExhibitItem"]);
            //    lstAuctionOrder.Add(NewEntry);
            //}
            //AuctionReader.Close();


            List<clsAuctionIndex> lstAuctionOrder = clsDB.Auction.Items.Values.ToList();
            return lstAuctionOrder;
        }

        //Return a list containing exhibits that were not part of the auction order in the specified revision
        public List<clsExhibit> LoadUnsortedExhibits(int RevisionIndex)
        {
            //Get a copy of the current list of exhibits
            List<clsExhibit> lstUnsortedExhibits = clsDB.Exhibits.ToList<clsExhibit>();

            //Remove all of the exhibits that exist in the auction order
            foreach (clsAuctionIndex AuctionIndex in this.Items.Values)
            {
                lstUnsortedExhibits.Remove(clsDB.Exhibits[AuctionIndex.Exhibit]);
            }

            return lstUnsortedExhibits;
        }

        //Helper function that executes a non-query sql command (the statement
        //  does not return any data). If the query fails due to a lost
        //  connection, this attempts to reconnect and reissue the statement.
        private void _ExecuteQuery(IDbCommand Command)
        {
            try
            {
                Command.ExecuteNonQuery();
            }
            catch (InvalidOperationException ex)
            {
                // Caught an Invalid Operation exception, this can occurs when
                //  the database connection is lost. Attempt to reconnect and
                //  re-execute the query.
                if (m_dbConn.State == System.Data.ConnectionState.Closed)
                {
                    m_dbConn.Open();
                    Command.ExecuteNonQuery();
                }
                else
                {
                    throw ex;
                }
            }

            Update();
            LoadRevisionHistory();
        }

        public void GenerateAuctionOrder()
        {
            //Do some initial work...
            //  Copy the list of exhibits and randomize the order
            List<DB.clsExhibit> lstRandomExhibits = clsDB.Exhibits.ToList<DB.clsExhibit>();
            lstRandomExhibits.OrderBy(x => Guid.NewGuid());

            //  Determine the maximum number of exhibits per exhibitor...
            int iMaxExhibitsPerExhibitor = 0;
            foreach (DB.clsExhibitor Exhibitor in clsDB.Exhibitors)
            {
                if (Exhibitor.ExhibitCount > iMaxExhibitsPerExhibitor)
                {
                    iMaxExhibitsPerExhibitor = Exhibitor.ExhibitCount;
                }
            }

            //Pass #1, Bin the exhibits by champion status and rate of gain, the bins will be randomized already because of the ordering
            const int BIN_ROG_ONLY = 0;     //  Bin 0 is Rate of Gain only
            const int BIN_GRANDCHAMP = 1;   //  Bin 1 is Grand Champion
            const int BIN_RESERVECHAMP = 2; //  Bin 2 is Reserve Champion
            const int BIN_OTHER = 3;        //  Bin 3 is Everything else
            const int BIN_COUNT = BIN_OTHER + 1;

            //Initialize the bins
            List<DB.clsExhibit>[] lstBins = new List<clsExhibit>[BIN_COUNT];
            for (int i = 0; i < BIN_COUNT; i++)
            {
                lstBins[i] = new List<clsExhibit>();
            }
            
            //Fill in the bins
            foreach (DB.clsExhibit Exhibit in lstRandomExhibits)
            {
                if (Exhibit.Include)
                {
                    if (Exhibit.RateOfGain && Exhibit.ChampionStatus == ChampionState.Other)
                    {
                        lstBins[BIN_ROG_ONLY].Add(Exhibit);
                    }
                    else if (Exhibit.ChampionStatus == ChampionState.Grand_Champion)
                    {
                        lstBins[BIN_GRANDCHAMP].Add(Exhibit);
                    }
                    else if (Exhibit.ChampionStatus == ChampionState.Reserve_Champion)
                    {
                        lstBins[BIN_RESERVECHAMP].Add(Exhibit);
                    }
                    else
                    {
                        lstBins[BIN_OTHER].Add(Exhibit);
                    }
                }
            }

            //Pass #2, Bin the exhibits by number of occurences.
            //  Initialize the occurence docutionary to 0
            Dictionary<int, int> dictExhibitorOccurences = new Dictionary<int, int>();
            foreach (DB.clsExhibitor Exhibitor in clsDB.Exhibitors)
            {
                if (Exhibitor.ExhibitCount > 0)
                {
                    dictExhibitorOccurences[Exhibitor.ExhibitorNumber] = 0;
                }
            }

            //  Initialize the sub bins;
            List<DB.clsExhibit>[] lstSubBins = new List<clsExhibit>[BIN_COUNT * iMaxExhibitsPerExhibitor];
            for (int i = 0; i < BIN_COUNT * iMaxExhibitsPerExhibitor; i++)
            {
                lstSubBins[i] = new List<clsExhibit>();
            }

            //   Break up the bins
            for (int iCurBin = 0; iCurBin < BIN_COUNT; iCurBin++)
            {
                foreach (DB.clsExhibit Exhibit in lstBins[iCurBin])
                {
                    int iSubBinIndex = (iCurBin * iMaxExhibitsPerExhibitor) + dictExhibitorOccurences[Exhibit.ExhibitorNumber];
                    lstSubBins[iSubBinIndex].Add(Exhibit);
                    dictExhibitorOccurences[Exhibit.ExhibitorNumber]++;
                }
            }

            //  Sort each sub bin by exhibit type...
            for (int i = 0; i < BIN_COUNT * iMaxExhibitsPerExhibitor; i++)
            {
                //lstSubBins[i].OrderBy(x => lstSubBins[i].Where(y => y.MarketID == x.MarketID).Count());
                lstSubBins[i].OrderBy(x => x.MarketID);
            }

            //Now would be a good time to equalize the distribution of animal types within each sub bin?

            //Pass #3, Randomly fill the order based on the restrictons
            //  Null out the list...
            for (int i = 0; i < lstRandomExhibits.Count(); i++)
            {
                lstRandomExhibits[i] = null;
            }

            //  Fill in the random list...
            DB.clsExhibit LastExhibit = null; //Last exhibit from the previous bin...
            int iStartExhibit = 0;            //Starting exhibit for the current bin...
            Random rand = new Random();
            for (int iCurrentSubBin = 0; iCurrentSubBin < BIN_COUNT * iMaxExhibitsPerExhibitor; iCurrentSubBin++)
            {
                int iSubBinStartIndex = 0;
                while (iSubBinStartIndex < lstSubBins[iCurrentSubBin].Count())
                {
                    //Initialize the slots array by taking all of the slots, then removing the slots that are taken
                    List<int> lstSlots = new List<int>(Enumerable.Range(iStartExhibit, lstSubBins[iCurrentSubBin].Count()));
                    for (int j = iStartExhibit; j < iStartExhibit + lstSubBins[iCurrentSubBin].Count(); j++)
                    {
                        if (lstRandomExhibits[j] != null)
                        {
                            lstSlots.Remove(j);
                        }
                    }
                    //TODO: If the current exhibit type == the last exhibit type of the previous bin, remove it as well...

                    int iSubBinCurrentIndex = iSubBinStartIndex;
                    while (lstSlots.Count() > 0 && iSubBinCurrentIndex < lstSubBins[iCurrentSubBin].Count() && lstSubBins[iCurrentSubBin][iSubBinCurrentIndex].MarketID == lstSubBins[iCurrentSubBin][iSubBinStartIndex].MarketID)
                    {
                        int iRandSlot = lstSlots[rand.Next(lstSlots.Count())];
                        lstRandomExhibits[iRandSlot] = lstSubBins[iCurrentSubBin][iSubBinCurrentIndex];

                        lstSlots.Remove(iRandSlot);
                        lstSlots.Remove(iRandSlot + 1);
                        lstSlots.Remove(iRandSlot - 1);
                        iSubBinCurrentIndex++;
                    }
                    iSubBinStartIndex = iSubBinCurrentIndex;

                }

                iStartExhibit += lstSubBins[iCurrentSubBin].Count();
            }

            //At this point the auction order is determined, now it's time to write the order back to the database

            //Write the order to the database
            int iRevisionIndex = 0;

            using (IDbTransaction dbTrans = m_dbConn.BeginTransaction(IsolationLevel.Serializable))
            {
                //Get the next revision index
                IDbCommand dbCommand = m_dbConn.CreateCommand();
                dbCommand.Transaction = dbTrans;
                dbCommand.CommandText = "SELECT COALESCE(MAX(RevisionIndex), 0) + 1 FROM AuctionOrder";
                iRevisionIndex = (int)dbCommand.ExecuteScalar();

                for (int i = 0; i < lstRandomExhibits.Count; i++)
                {
                    lock (m_dictAuctionOrder)
                    {
                        lock (m_dictCollection)
                        {
                            clsAuctionIndex AuctionIndex = new clsAuctionIndex(i, 0, lstRandomExhibits[i].TagNumber, lstRandomExhibits[i].MarketID);
                            AuctionIndex.RevisionIndex = iRevisionIndex;
                            if (i > 0)
                            {
                                AuctionIndex.PrevExhibit = clsAuctionIndex.ComputeID(lstRandomExhibits[i - 1].TagNumber, lstRandomExhibits[i - 1].MarketID);
                            }
                            if (i < lstRandomExhibits.Count - 1)
                            {
                                AuctionIndex.NextExhibit = clsAuctionIndex.ComputeID(lstRandomExhibits[i + 1].TagNumber, lstRandomExhibits[i + 1].MarketID);
                            }
                            this.Commit(CommitAction.Modify, AuctionIndex, dbTrans);

                            System.Threading.Thread.Sleep(10); //Quick sleep to ensure each entry gets a unique timestamp
                        }
                    }
                }
                dbTrans.Commit();
            }

            Update();
            LoadRevisionHistory();
        }

        public void GenerateAuctionOrderSolanco()
        {
            Console.WriteLine("Starting Solanco Order");
            //Solancos auction order is by market type.  Within each market type champion animals go first, and the rest go by tag number (they use special show tag numbers)

            List<DB.clsExhibit> lstExhibits = clsDB.Exhibits.ToList<DB.clsExhibit>(); //Initalize list, sort by tag number
            lstExhibits.OrderBy(exhibit => exhibit.TagNumber);  //Sort by tag number



            //Will need to check if they use grand and reserve
            Dictionary<int, List<DB.clsExhibit>> dictChamps = new Dictionary<int, List<DB.clsExhibit>>();
            Dictionary<int, List<DB.clsExhibit>> dictOther = new Dictionary<int, List<DB.clsExhibit>>();

            for (int i = 0; i < clsDB.Market.Count; i++)
	        {
                dictChamps[i] = new List<DB.clsExhibit>();
                dictOther[i] = new List<DB.clsExhibit>();
            }


            foreach (DB.clsExhibit Exhibit in lstExhibits)
            {
                if (Exhibit.Include)
                {
                    if (Exhibit.ChampionStatus == ChampionState.Grand_Champion)
                    {
                        dictChamps[Exhibit.MarketID].Add(Exhibit);
                    }
                    else
                    {
                        dictOther[Exhibit.MarketID].Add(Exhibit);
                    }
                }
            }

            lstExhibits = new List<DB.clsExhibit>();  //Or set to null like line 417 in clsAuctionOrder.cs

            //Iterate through each market type and add to final list
            foreach (DB.clsMarketItem mItem in clsDB.Market.ToList<DB.clsMarketItem>())
            {
                if (mItem.MarketType != "Additional Items") //Check for empty market classes?
                {
                    lstExhibits.AddRange(dictChamps[mItem.MarketID]);
                    lstExhibits.AddRange(dictOther[mItem.MarketID]);
                }
            }

            //Write order to database as in clsAuctionOrder at list 463

            //Write the order to the database

            int iRevisionIndex = 0;



            using (IDbTransaction dbTrans = m_dbConn.BeginTransaction(IsolationLevel.Serializable))

            {

                //Get the next revision index

                IDbCommand dbCommand = m_dbConn.CreateCommand();
                dbCommand.Transaction = dbTrans;
                dbCommand.CommandText = "SELECT COALESCE(MAX(RevisionIndex), 0) + 1 FROM AuctionOrder";
                iRevisionIndex = (int)dbCommand.ExecuteScalar();


                for (int i = 0; i < lstExhibits.Count; i++)
                {
                    lock (m_dictAuctionOrder)
                    {
                        lock (m_dictCollection)
                        {
                            clsAuctionIndex AuctionIndex = new clsAuctionIndex(i, 0, lstExhibits[i].TagNumber, lstExhibits[i].MarketID);
                            AuctionIndex.RevisionIndex = iRevisionIndex;

                            if (i > 0)
                            {
                                AuctionIndex.PrevExhibit = clsAuctionIndex.ComputeID(lstExhibits[i - 1].TagNumber, lstExhibits[i - 1].MarketID);
                            }

                            if (i < lstExhibits.Count - 1)
                            {
                                AuctionIndex.NextExhibit = clsAuctionIndex.ComputeID(lstExhibits[i + 1].TagNumber, lstExhibits[i + 1].MarketID);
                            }

                            this.Commit(CommitAction.Modify, AuctionIndex, dbTrans);
                            System.Threading.Thread.Sleep(10); //Quick sleep to ensure each entry gets a unique timestamp
                        }
                    }
                }
                dbTrans.Commit();
            }

            Update();
            LoadRevisionHistory();

        }



        public void RevertToRevision(int TargetRevision)
        {
            SqlCommand dbCommand = new SqlCommand();
            dbCommand.CommandText = "UPDATE AuctionOrder SET RevisionDeleteDate = GETDATE()	WHERE RevisionIndex > '" + TargetRevision.ToString() + "'";

            _ExecuteQuery(dbCommand);
        }

        public void ChangeOrder(clsAuctionIndex Source, int Target)
        {
            int iRevisionIndex = 0;

            using (IDbTransaction dbTrans = m_dbConn.BeginTransaction(IsolationLevel.Serializable))
            {
                //Get the next revision index
                IDbCommand dbCommand = m_dbConn.CreateCommand();
                dbCommand.Transaction = dbTrans;
                dbCommand.CommandText = "SELECT COALESCE(MAX(RevisionIndex), 0) + 1 FROM AuctionOrder";
                iRevisionIndex = (int)dbCommand.ExecuteScalar();

                lock (m_dictAuctionOrder)
                {
                    lock (m_dictCollection)
                    {
                        //Unlink the item from the original order..
                        Source.RevisionIndex = iRevisionIndex;
                        if (Source.PrevExhibit > 0)
                        {
                            m_dictCollection[Source.PrevExhibit].NextExhibit = Source.NextExhibit;
                            m_dictCollection[Source.PrevExhibit].RevisionIndex = iRevisionIndex;
                            if (!this.Items.ContainsKey(Target) || Source.PrevExhibit != this.Items[Target].Key)
                            {
                                this.Commit(CommitAction.Modify, m_dictCollection[Source.PrevExhibit], dbTrans);
                            }
                        }

                        if (Source.NextExhibit > 0)
                        {
                            m_dictCollection[Source.NextExhibit].PrevExhibit = Source.PrevExhibit;
                            m_dictCollection[Source.NextExhibit].RevisionIndex = iRevisionIndex;
                            if (!this.Items.ContainsKey(Target - 1) || Source.NextExhibit != this.Items[Target - 1].Key)
                            {
                                this.Commit(CommitAction.Modify, m_dictCollection[Source.NextExhibit], dbTrans);
                            }
                        }
                        
                        //Link the item into the new order..
                        if (Target > 1)
                        {
                            //This is not the first item on the list
                            //  Set the next pointer on the previous item
                            this.Items[Target - 1].NextExhibit = Source.Key;
                            Source.PrevExhibit = this.Items[Target - 1].Key;

                            this.Items[Target - 1].RevisionIndex = iRevisionIndex;
                            this.Commit(CommitAction.Modify, this.Items[Target - 1], dbTrans);
                        }
                        else
                        {
                            Source.PrevExhibit = -1;
                        }

                        if (Target <= this.Items.Count)
                        {
                            //This is not the last item on the list...
                            //  Set the previous pointer on the next item
                            Source.NextExhibit = this.Items[Target].Key;
                            this.Items[Target].PrevExhibit = Source.Key;

                            this.Items[Target].RevisionIndex = iRevisionIndex;
                            this.Commit(CommitAction.Modify, this.Items[Target], dbTrans);
                        }
                        else
                        {
                            Source.NextExhibit = -1;
                        }

                        this.Commit(CommitAction.Modify, Source, dbTrans);
                    }
                }
                dbTrans.Commit();
            }

            Update();
            LoadRevisionHistory();
        }

        //Note that "Target" is the base 1 index of where the item will be inserted into the list
        public void InsertExhibit(int Target, clsExhibit Animal)
        {
            int iRevisionIndex = 0;

            using (IDbTransaction dbTrans = m_dbConn.BeginTransaction(IsolationLevel.Serializable))
            {
                //Get the next revision index
                IDbCommand dbCommand = m_dbConn.CreateCommand();
                dbCommand.Transaction = dbTrans;
                dbCommand.CommandText = "SELECT COALESCE(MAX(RevisionIndex), 0) + 1 FROM AuctionOrder";
                iRevisionIndex = (int)dbCommand.ExecuteScalar();

                lock (m_dictAuctionOrder)
                {
                    lock (m_dictCollection)
                    {
                        //Is this an append or an insert?
                        if (Target > this.Items.Count)
                        {
                            //Appending... Adjust target to be after the last item
                            Target = this.Items.Count + 1;
                        }

                        //Link the item into the list
                        clsAuctionIndex NewIndex = new clsAuctionIndex(Target, 0, Animal.TagNumber, Animal.MarketID);
                        NewIndex.RevisionIndex = iRevisionIndex;
                        if (Target > 1)
                        {
                            //This is not the first item on the list
                            //  Set the next pointer on the previous item
                            this.Items[Target - 1].NextExhibit = NewIndex.Key;
                            NewIndex.PrevExhibit = this.Items[Target - 1].Key;

                            this.Items[Target - 1].RevisionIndex = iRevisionIndex;
                            this.Commit(CommitAction.Modify, this.Items[Target - 1], dbTrans);
                        }

                        if (Target <= this.Items.Count)
                        {
                            //This is not the last item on the list...
                            //  Set the previous pointer on the next item
                            NewIndex.NextExhibit = this.Items[Target].Key;
                            this.Items[Target].PrevExhibit = NewIndex.Key;

                            this.Items[Target].RevisionIndex = iRevisionIndex;
                            this.Commit(CommitAction.Modify, this.Items[Target], dbTrans);
                        }

                        this.Commit(CommitAction.Modify, NewIndex, dbTrans);
                    }

                    dbTrans.Commit();
                }
            }

            Update();
            LoadRevisionHistory();
        }

        public void AppendExhibit(clsExhibit Animal)
        {
            InsertExhibit(int.MaxValue, Animal);
        }

        public void RemoveExhibit(clsAuctionIndex Target)
        {
            int iRevisionIndex = 0;

            using (IDbTransaction dbTrans = m_dbConn.BeginTransaction(IsolationLevel.Serializable))
            {
                //Get the next revision index
                IDbCommand dbCommand = m_dbConn.CreateCommand();
                dbCommand.Transaction = dbTrans;
                dbCommand.CommandText = "SELECT COALESCE(MAX(RevisionIndex), 0) + 1 FROM AuctionOrder";
                iRevisionIndex = (int)dbCommand.ExecuteScalar();

                lock (m_dictAuctionOrder)
                {
                    lock (m_dictCollection)
                    {
                        if (Target.PrevExhibit > 0)
                        {
                            m_dictCollection[Target.PrevExhibit].NextExhibit = Target.NextExhibit;
                            m_dictCollection[Target.PrevExhibit].RevisionIndex = iRevisionIndex;
                            this.Commit(CommitAction.Modify, m_dictCollection[Target.PrevExhibit], dbTrans);
                        }

                        if (Target.NextExhibit > 0)
                        {
                            m_dictCollection[Target.NextExhibit].PrevExhibit = Target.PrevExhibit;
                            m_dictCollection[Target.NextExhibit].RevisionIndex = iRevisionIndex;
                            this.Commit(CommitAction.Modify, m_dictCollection[Target.NextExhibit], dbTrans);
                        }
                        Target.RevisionIndex = iRevisionIndex;
                        this.Commit(CommitAction.Delete, Target, dbTrans);
                    }
                }

                dbTrans.Commit();
            }

            Update();
            LoadRevisionHistory();
        }

        public int LatestRevision
        {
            get
            {
                IDbCommand dbCommand = m_dbConn.CreateCommand();
                //dbCommand.CommandText = "SELECT COALESCE(MAX(RevisionIndex), 0) AS Revision FROM AuctionOrder WHERE RevisionDeleteDate IS NULL";
                dbCommand.CommandText = "SELECT COALESCE(MAX(RevisionIndex), 0) AS Revision FROM AuctionOrder";

                try
                {
                    return int.Parse(dbCommand.ExecuteScalar().ToString());
                }
                catch (InvalidOperationException ex)
                {
                    if (m_dbConn.State == System.Data.ConnectionState.Closed)
                    {
                        m_dbConn.Open();
                        return int.Parse(dbCommand.ExecuteScalar().ToString());
                    }
                    else
                    {
                        throw ex;
                    }
                }
                
            }
        }

        public clsAuctionIndex this[clsAuctionIndex Index]
        {
            get
            {
                return m_dictAuctionOrder[Index.AuctionIndex];
            }
        }

        public Dictionary<int, clsAuctionRevision> RevisionHistory
        {
            get
            {
                return m_dictRevisionHistory;
            }
        }

        public Dictionary<int, clsAuctionIndex> Items
        {
            get
            {
                return m_dictAuctionOrder;
            }
        }

        #region IEnumerable<clsAuctionIndex> Members

        public IEnumerator<clsAuctionIndex> GetEnumerator()
        {
            return m_dictAuctionOrder.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return m_dictAuctionOrder.Values.GetEnumerator();
        }

        #endregion
    }

    public class clsAuctionRevision
    {
        int m_iIndex;
        DateTime m_dtDate;
        int m_iRecordsAffected;
        bool m_bReverted;

        public clsAuctionRevision(int Index, DateTime Date, int Records, int Reverted)
        {
            m_iIndex = Index;
            m_dtDate = Date;
            m_iRecordsAffected = Records;
            if (Reverted == 0)
            {
                m_bReverted = false;
            }
            else
            {
                m_bReverted = true;
            }
        }

        public int RevisionIndex
        {
            get
            {
                return m_iIndex;
            }
        }
        public DateTime RevisionDate
        {
            get
            {
                return m_dtDate;
            }
        }
        public int RecordsAffected
        {
            get
            {
                return m_iRecordsAffected;
            }
        }
        public bool Reverted
        {
            get
            {
                return m_bReverted;
            }
        }
    }

    public class clsAuctionIndex : AuctionData, IComparable<clsAuctionIndex>
    {
        public static int ComputeID(int iExhibitTag, int iExhibitType)
        {
            return (iExhibitType << 16) | iExhibitTag;
        }

        // Total count of the number of columns in the list view for this auction data class
        const int TOTAL_LV_COLUMNS = 11;
        // Enumeration used to identify list view columns
        public enum AuctionIndexColumns
        {
            Order = 0,
            TagNum = 1,
            ExhibitorNum = 2,
            ExhibitorName = 3,
            ExhibitChampaionStatus = 4,
            ExhibitType = 5,
            ExhibitWeight = 6,
            BuyerNum = 7,
            BuyerName = 8,
            WinningBid = 9,
            CheckedOut = 10
        }

        public class AuctionIndexSorter : IComparer
        {
            AuctionIndexColumns[] eSortColumns = new AuctionIndexColumns[TOTAL_LV_COLUMNS];
            SortOrder[] eSortOrders = new SortOrder[TOTAL_LV_COLUMNS];

            public AuctionIndexSorter()
            {
                //Initalize the current sort state so all of the columns are
                //  sorted ascending in the order they appear on the screen
                //  (this will put buyer number first).
                for (int i = 0; i < TOTAL_LV_COLUMNS; i++)
                {
                    eSortColumns[i] = (AuctionIndexColumns)i;
                    eSortOrders[i] = SortOrder.Ascending;
                }
            }

            public void SetSortColumn(AuctionIndexColumns Column)
            {
                if (eSortColumns[0] == Column)
                {
                    //If the user just re-sorted on the same column, reverse the order
                    if (eSortOrders[0] == SortOrder.Ascending)
                    {
                        eSortOrders[0] = SortOrder.Descending;
                    }
                    else
                    {
                        eSortOrders[0] = SortOrder.Ascending;
                    }
                }
                else
                {
                    //Find the new item's existing place in the list
                    int iOldPos;
                    for (iOldPos = 0; iOldPos < TOTAL_LV_COLUMNS && eSortColumns[iOldPos] != Column; iOldPos++) { }

                    //Move all of the existing sort above the new one down one to make room
                    for (int i = iOldPos - 1; i >= 0; i--)
                    {
                        eSortColumns[i + 1] = eSortColumns[i];
                        eSortOrders[i + 1] = eSortOrders[i];
                    }

                    //Put the new sort at the top of the list
                    eSortColumns[0] = Column;
                    eSortOrders[0] = SortOrder.Ascending;
                }
            }

            public int Compare(object x, object y)
            {
                for (int i = 0; i < TOTAL_LV_COLUMNS; i++)
                {
                    int ans = 0;
                    if (eSortColumns[i] == AuctionIndexColumns.Order)
                    {
                        ans = ((clsAuctionIndex)x).AuctionIndex.CompareTo(((clsAuctionIndex)y).AuctionIndex);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.TagNum)
                    {
                        ans = ((clsAuctionIndex)x).Exhibit.TagNumber.CompareTo(((clsAuctionIndex)y).Exhibit.TagNumber);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.ExhibitorNum)
                    {
                        ans = ((clsAuctionIndex)x).Exhibit.ExhibitorNumber.CompareTo(((clsAuctionIndex)y).Exhibit.ExhibitorNumber);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.ExhibitorName)
                    {
                        ans = ((clsAuctionIndex)x).Exhibit.Exhibitor.Name.CompareTo(((clsAuctionIndex)y).Exhibit.Exhibitor.Name);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.ExhibitChampaionStatus)
                    {
                        ans = ((clsAuctionIndex)x).Exhibit.ChampionStatus.CompareTo(((clsAuctionIndex)y).Exhibit.ChampionStatusText);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.ExhibitType)
                    {
                        ans = ((clsAuctionIndex)x).Exhibit.MarketItem.CompareTo(((clsAuctionIndex)y).Exhibit.MarketItem);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.ExhibitWeight)
                    {
                        ans = ((clsAuctionIndex)x).Exhibit.Weight.CompareTo(((clsAuctionIndex)y).Exhibit.Weight);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.BuyerNum)
                    {
                        ans = ((clsAuctionIndex)x).BuyerNumber.CompareTo(((clsAuctionIndex)y).BuyerNumber);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.BuyerName)
                    {
                        ans = ((clsAuctionIndex)x).BuyerName.CompareTo(((clsAuctionIndex)y).BuyerName);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.WinningBid)
                    {
                        ans = ((clsAuctionIndex)x).WinningBid.CompareTo(((clsAuctionIndex)y).WinningBid);
                    }
                    else if (eSortColumns[i] == AuctionIndexColumns.CheckedOut)
                    {
                        ans = ((clsAuctionIndex)x).CheckedOut.CompareTo(((clsAuctionIndex)y).CheckedOut);
                    }
                    
                    if (ans != 0)
                    {
                        if (eSortOrders[i] == SortOrder.Ascending)
                        {
                            return ans;
                        }
                        else if (eSortOrders[i] == SortOrder.Descending)
                        {
                            return -ans;
                        }
                    }
                }
                return 0;
            }
        }


        int m_iExhibitID;
        clsExhibit m_cExhibit;
        int m_iExhibitType;
        int m_iExhibitTag;

        int m_iNextID;
        int m_iPrevID;
        
        int m_iPrimaryIndex;
        int m_iSubIndex;

        int m_iRevisionIndex;

        public clsAuctionIndex()
        {
            m_iPrimaryIndex = -1;
            m_iSubIndex = 0;

            m_iNextID = -1;
            m_iPrevID = -1;
        }

        public clsAuctionIndex(int AuctionIndex, int SubIndex, int ExhibitTag, int ExhibitType)
        {
            m_iExhibitTag = ExhibitTag;
            m_iExhibitType = ExhibitType;
            m_iExhibitID = ComputeID(m_iExhibitTag, m_iExhibitType);
            m_cExhibit = clsDB.Exhibits[ExhibitTag, ExhibitType];
            
            m_iPrimaryIndex = AuctionIndex;
            m_iSubIndex = SubIndex;

            m_iNextID = -1;
            m_iPrevID = -1;

            RefreshListItem();
        }

        public override void Load(IDataReader dbReader)
        {
            m_iExhibitID = (Int32)dbReader["ID"];
            m_iExhibitTag = (Int32)dbReader["ExhibitTag"];
            m_iExhibitType = (Int32)dbReader["ExhibitItem"];
            m_cExhibit = clsDB.Exhibits[m_iExhibitTag, m_iExhibitType];

            m_iNextID = (Int32)dbReader["NextID"];
            m_iPrevID = (Int32)dbReader["PrevID"];

            RefreshListItem();
        }

        public void RefreshListItem()
        {
            if (base.SubItems.Count <= TOTAL_LV_COLUMNS)
            {
                base.SubItems.Clear();
                for (int i = 0; i < TOTAL_LV_COLUMNS; i++)
                {
                    base.SubItems.Add(new ListViewSubItem());
                }
            }

            base.SubItems[(int)AuctionIndexColumns.Order].Text = this.AuctionIndex.ToString();
            if (this.Exhibit != null)
            {
                base.SubItems[(int)AuctionIndexColumns.TagNum].Text = this.Exhibit.TagNumber.ToString();
                base.SubItems[(int)AuctionIndexColumns.ExhibitorNum].Text = this.Exhibit.ExhibitorNumber.ToString();
                base.SubItems[(int)AuctionIndexColumns.ExhibitorName].Text = this.Exhibit.ExhibitorName;
                base.SubItems[(int)AuctionIndexColumns.ExhibitChampaionStatus].Text = this.Exhibit.ChampionStatusText;
                if (this.Exhibit.MarketItem != null)
                {
                    base.SubItems[(int)AuctionIndexColumns.ExhibitType].Text = this.Exhibit.MarketItem.MarketType;
                }
                else
                {
                    base.SubItems[(int)AuctionIndexColumns.ExhibitorNum].Text = string.Format("ERROR reading market item {0}", this.Exhibit.MarketID.ToString());
                }
                base.SubItems[(int)AuctionIndexColumns.ExhibitWeight].Text = this.Exhibit.WeightString;
            }
            else
            {
                base.SubItems[(int)AuctionIndexColumns.TagNum].Text = m_iExhibitTag.ToString();
                base.SubItems[(int)AuctionIndexColumns.ExhibitorNum].Text = "ERROR reading exhibit";
                base.SubItems[(int)AuctionIndexColumns.ExhibitorName].Text = "ERROR reading exhibit";
                base.SubItems[(int)AuctionIndexColumns.ExhibitChampaionStatus].Text = "ERROR reading exhibit";
                if (clsDB.Market[m_iExhibitType] != null)
                {
                    base.SubItems[(int)AuctionIndexColumns.ExhibitType].Text = clsDB.Market[m_iExhibitType].MarketType;
                }
                else
                {
                    base.SubItems[(int)AuctionIndexColumns.ExhibitType].Text = string.Format("ERROR reading market type {0}", m_iExhibitType);
                }
                base.SubItems[(int)AuctionIndexColumns.ExhibitWeight].Text = "ERROR reading exhibit";
            }
            base.SubItems[(int)AuctionIndexColumns.BuyerNum].Text = this.BuyerNumber;
            base.SubItems[(int)AuctionIndexColumns.BuyerName].Text = this.BuyerName;
            base.SubItems[(int)AuctionIndexColumns.WinningBid].Text = this.WinningBid > 0 ? this.WinningBid.ToString("$#.00/" + this.Exhibit.MarketItem.MarketUnits) : "";
            base.SubItems[(int)AuctionIndexColumns.CheckedOut].Text = this.CheckedOut;
        }

        protected override void DBCommit(DB.CommitAction Action, IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            IDbCommand dbCommand = DatabaseConnection.CreateCommand();
            dbCommand.Transaction = Transaction;
            dbCommand.CommandText = "INSERT INTO AuctionOrder " +
                "		(CommitDate, CommitAction, RevisionIndex, ID, PrevID, NextID, ExhibitTag, ExhibitItem) " +
                "	VALUES " +
                "		(@CommitDate, @CommitAction, @RevisionIndex, @ID, @PrevID, @NextID, @ExhibitTag, @ExhibitItem)";

            //These three parameters will get re-used...
            IDbDataParameter param = dbCommand.CreateParameter();
            param.ParameterName = "@CommitDate";
            if (dbCommand is SqlCommand)
            {
                param.Value = DateTime.Now;
            }
            else
            {
                param.Value = DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fffffff");
            }
            dbCommand.Parameters.Add(param);

            param = dbCommand.CreateParameter();
            param.ParameterName = "@CommitAction";
            param.Value = (int)Action;
            dbCommand.Parameters.Add(param);

            param = dbCommand.CreateParameter();
            param.ParameterName = "@RevisionIndex";
            param.Value = this.m_iRevisionIndex;
            dbCommand.Parameters.Add(param);

            param = dbCommand.CreateParameter();
            param.ParameterName = "@ID";
            param.Value = this.m_iExhibitID;
            dbCommand.Parameters.Add(param);

            param = dbCommand.CreateParameter();
            param.ParameterName = "@PrevID";
            param.Value = this.m_iPrevID;
            dbCommand.Parameters.Add(param);

            param = dbCommand.CreateParameter();
            param.ParameterName = "@NextID";
            param.Value = this.m_iNextID;
            dbCommand.Parameters.Add(param);

            param = dbCommand.CreateParameter();
            param.ParameterName = "@ExhibitTag";
            param.Value = this.m_iExhibitTag;
            dbCommand.Parameters.Add(param);

            param = dbCommand.CreateParameter();
            param.ParameterName = "@ExhibitItem";
            param.Value = this.m_iExhibitType;
            dbCommand.Parameters.Add(param);

            dbCommand.ExecuteNonQuery();
        }

        public static bool operator ==(clsAuctionIndex Index1, clsAuctionIndex Index2)
        {
            if ((object)Index1 == null && (object)Index2 == null)
            {
                return true;
            }
            else if ((object)Index1 == null || (object)Index2 == null)
            {
                return false;
            }
            else
            {
                return (Index1.CompareTo(Index2) == 0);
            }
        }

        public static bool operator !=(clsAuctionIndex Index1, clsAuctionIndex Index2)
        {
            return !(Index1 == Index2);
        }

        #region IComparable<clsAuctionIndex> Members

        public int CompareTo(clsAuctionIndex other)
        {
            if (this.AuctionIndex.CompareTo(other.AuctionIndex) == 0)
            {
                return this.SubIndex.CompareTo(other.SubIndex);
            }
            else
            {
                return this.AuctionIndex.CompareTo(other.AuctionIndex);
            }
        }

        #endregion

        public int Key
        {
            get
            {
                return m_iExhibitID;
            }
        }

        public int RevisionIndex
        {
            get
            {
                return m_iRevisionIndex;
            }
            set
            {
                m_iRevisionIndex = value;
            }
        }
        
        public int AuctionIndex
        {
            get
            {
                return m_iPrimaryIndex;
            }
            set
            {
                m_iPrimaryIndex = value;
                if (this.ListView == null)
                {
                    RefreshListItem();
                }
                else
                {
                    this.ListView.BeginInvoke(new System.Windows.Forms.MethodInvoker(RefreshListItem));
                }
            }
        }

        public int SubIndex
        {
            get
            {
                return m_iSubIndex;
            }
            set
            {
                m_iSubIndex = value;
                RefreshListItem();
            }
        }

        public int NextExhibit
        {
            get
            {
                return m_iNextID;
            }
            set
            {
                m_iNextID = value;
            }
        }

        public int PrevExhibit
        {
            get
            {
                return m_iPrevID;
            }
            set
            {
                m_iPrevID = value;
            }
        }

        public int ExhibitTag
        {
            get
            {
                return m_iExhibitTag;
            }
        }

        public int MarketType
        {
            get
            {
                return m_iExhibitType;
            }
        }

        public clsExhibit Exhibit
        {
            get
            {
                return m_cExhibit;
            }
        }

        public string BuyerNumber
        {
            get
            {
                if (this.Exhibit != null && this.Exhibit.Purchases != null)
                {
                    if (this.Exhibit.Purchases.Count == 1)
                    {
                        return this.Exhibit.Purchases.Values.First().BuyerID.ToString();
                    }
                    else
                    {
                        return this.Exhibit.Purchases.Count.ToString("0 Buyers");
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public string BuyerName
        {
            get
            {
                string sBuyer = "";
                if (this.Exhibit != null && this.Exhibit.Purchases != null)
                {
                    if (this.Exhibit.Purchases.Count == 1)
                    {
                        clsBuyer Buyer = this.Exhibit.Purchases.Values.First().Buyer;
                        if (Buyer != null)
                        {
                            sBuyer = Buyer.Name.First + " " + Buyer.Name.Last;
                            if (Buyer.CompanyName.Length > 0)
                            {
                                sBuyer += " from " + Buyer.CompanyName;
                            }
                            return sBuyer;
                        }
                        else
                        {
                            sBuyer = "Unknown Buyer";
                        }
                        return sBuyer;
                    }
                    else
                    {
                        return this.Exhibit.Purchases.Count.ToString("0 Buyers");
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public double WinningBid
        {
            get
            {
                if (this.Exhibit != null && this.Exhibit.Purchases != null)
                {
                    double fTotalBid = 0;
                    foreach (clsPurchase Purchase in this.Exhibit.Purchases.Values)
                    {
                        fTotalBid += Purchase.FinalBid;
                    }
                    return fTotalBid;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string CheckedOut
        {
            get
            {
                bool bSomeCheckedOut = false;
                bool bSomeNotCheckedOut = false;

                if (this.Exhibit != null && this.Exhibit.Purchases != null)
                {
                    foreach (clsPurchase Purchase in this.Exhibit.Purchases.Values)
                    {
                        if (Purchase.Buyer != null)
                        {
                            if (Purchase.Buyer.CheckedOut && !bSomeCheckedOut)
                            {
                                bSomeCheckedOut = true;
                            }
                            else if (!Purchase.Buyer.CheckedOut && !bSomeNotCheckedOut)
                            {
                                bSomeNotCheckedOut = true;
                            }
                        }
                        else
                        {
                            bSomeNotCheckedOut = true;
                        }
                    }
                }

                if (bSomeCheckedOut && !bSomeNotCheckedOut)
                {
                    return "Yes";
                }
                else if (!bSomeCheckedOut && bSomeNotCheckedOut)
                {
                    return "No";
                }
                else if (bSomeCheckedOut && bSomeNotCheckedOut)
                {
                    return "Partial";
                }
                else
                {
                    return "";
                }
            }
        }

    }

    namespace Setup
    {
        public class AuctionOrder : AuctionDBSetup
        {
            public AuctionOrder()
            {
                TABLE_NAME = "AuctionOrder";

                TABLE_COLUMNS = new List<SQLColumn>() {
                    new SQLColumn("RevisionIndex", "int", "0", false),
                    new SQLColumn("ID", "int", "0", true),
                    new SQLColumn("PrevID", "int", "0", false),
                    new SQLColumn("NextID", "int", "0", false),
                    new SQLColumn("ExhibitTag", "int", "0", true),
                    new SQLColumn("ExhibitItem", "int", "0", true),
                    new SQLColumn("Comments", "nvarchar(1000)", "", false),
                };
            }
            
        }
    }
}
