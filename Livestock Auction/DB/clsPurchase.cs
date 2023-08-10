using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Data;

namespace Livestock_Auction.DB
{
    public class clsPurchases : clsAuctionDataCollection<IDAuctionDataKey, clsPurchase, DB.Setup.Purchases>, IEnumerable<clsPurchase>
    {
        Dictionary<int, Dictionary<int, Dictionary<int, clsPurchase>>> dictPurchases;    //Purchases are index by both exhibit and purchase index

        double fGrossSold = 0;

        public clsPurchases(IDbConnection Connection) : base(Connection)
        {
        }

        public override void Load()
        {
            fGrossSold = 0;
            dictPurchases = new Dictionary<int, Dictionary<int, Dictionary<int, clsPurchase>>>();

            m_Setup = new DB.Setup.Purchases();
            IDbCommand dbLoad = m_Setup.SQLLoadDataQuery(m_dbConn);
            //IDbCommand dbLoad = m_dbConn.CreateCommand();
            //dbLoad.CommandText = "SELECT LastUpdated, ExhibitTag, ExhibitItem, PurchaseIndex, BuyerID, RecipientID, SaleCondition, PurchaseBid, Destination, HauledBy, SpecialInstructions, Comments FROM vPurchases_Current ORDER BY ExhibitTag ASC, ExhibitItem ASC, PurchaseIndex ASC";
            IDataReader PurchaseReader = dbLoad.ExecuteReader();

            while (PurchaseReader.Read())
            {
                int iSaleCondition = -1;
                int iDestination = -1;
                int.TryParse(PurchaseReader["SaleCondition"].ToString(), out iSaleCondition);
                int.TryParse(PurchaseReader["Destination"].ToString(), out iDestination);

                if ((Int32)PurchaseReader["ExhibitTag"] == 101)
                {
                    System.Threading.Thread.Sleep(0);
                }

                clsPurchase NewEntry = new clsPurchase((Int32)PurchaseReader["PurchaseIndex"], (Int32)PurchaseReader["ExhibitTag"], (Int32)PurchaseReader["ExhibitItem"], (Int32)PurchaseReader["BuyerID"], (Int32)PurchaseReader["RecipientID"], (clsPurchase.enSaleCondition)iSaleCondition, double.Parse(PurchaseReader["PurchaseBid"].ToString()), (clsPurchase.enAnimalDestination)iDestination, PurchaseReader["HauledBy"].ToString(), PurchaseReader["SpecialInstructions"].ToString());

                this[NewEntry.Exhibit, NewEntry.PurchaseIndex] = NewEntry;
                if (PurchaseReader["LastUpdated"] is DateTime)
                {
                    m_dtLastUpdate = (DateTime)PurchaseReader["LastUpdated"];
                }
                else
                {
                    m_dtLastUpdate = DateTime.Parse(PurchaseReader["LastUpdated"].ToString());
                }

                fGrossSold += NewEntry.TotalBid;
            }
            PurchaseReader.Close();
        }

        public override void Update(IDbConnection Connection)
        {
            try
            {
                DatabaseUpdatedEventArgs UpdateArgs = new DatabaseUpdatedEventArgs();

                //IDbCommand dbUpdate = Connection.CreateCommand();
                //dbUpdate.CommandText = "SELECT CommitDate, CommitAction, ExhibitTag, ExhibitItem, PurchaseIndex, BuyerID, RecipientID, SaleCondition, PaymentMethod, CheckNumber, PurchaseBid, Destination, HauledBy, SpecialInstructions FROM vPurchases_Journal WHERE CommitDate > '" + m_dtLastUpdate.ToString("yyyy-MM-dd HH:mm:ss.fffffff") + "' ORDER BY ExhibitTag ASC, ExhibitItem ASC, PurchaseIndex ASC";
                //dbUpdate.CommandText = "SELECT CommitDate, CommitAction, ExhibitTag, ExhibitItem, PurchaseIndex, BuyerID, RecipientID, SaleCondition, PaymentMethod, CheckNumber, PurchaseBid, Destination, HauledBy, SpecialInstructions FROM vPurchases_Journal WHERE CommitDate > @LastUpdate ORDER BY ExhibitTag ASC, ExhibitItem ASC, PurchaseIndex ASC";

                IDbCommand dbUpdate = m_Setup.SQLUpdateDataQuery(Connection);
                IDbDataParameter param = dbUpdate.CreateParameter();
                param.ParameterName = "@LastUpdate";
                if (param is System.Data.SqlServerCe.SqlCeParameter)
                {
                    param.Value = m_dtLastUpdate;
                }
                else
                {
                    param.DbType = DbType.DateTime2;
                    param.Value = m_dtLastUpdate;
                }
                dbUpdate.Parameters.Add(param);


                IDataReader PurchaseReader = dbUpdate.ExecuteReader();

                try
                {
                    while (PurchaseReader.Read())
                    {
                        Int32 iExhibitNumber = -1;
                        Int32 iExhibitItem = -1;
                        Int32 iPurchaseIndex = -1;
                        try
                        {
                            iExhibitNumber = (Int32)PurchaseReader["ExhibitTag"];
                            iExhibitItem = (Int32)PurchaseReader["ExhibitItem"];
                            iPurchaseIndex = (Int32)PurchaseReader["PurchaseIndex"];

                            //Modifications and addtions....
                            clsPurchase Purchase = new clsPurchase((Int32)PurchaseReader["PurchaseIndex"], (Int32)PurchaseReader["ExhibitTag"], (Int32)PurchaseReader["ExhibitItem"], (Int32)PurchaseReader["BuyerID"], (Int32)PurchaseReader["RecipientID"], (clsPurchase.enSaleCondition)PurchaseReader["SaleCondition"], double.Parse(PurchaseReader["PurchaseBid"].ToString()), (clsPurchase.enAnimalDestination)PurchaseReader["Destination"], PurchaseReader["HauledBy"].ToString(), PurchaseReader["SpecialInstructions"].ToString());

                            //Apply the changes to the dictionary
                            if ((DB.CommitAction)((Int32)PurchaseReader["CommitAction"]) == DB.CommitAction.Modify)
                            {
                                if (this[Purchase] != null)
                                {
                                    fGrossSold -= this[Purchase].TotalBid;
                                }
                                this[Purchase] = Purchase;
                                fGrossSold += Purchase.TotalBid;

                                UpdateArgs.UpdatedItems.Add(Purchase, DB.CommitAction.Modify);
                            }
                            else
                            {
                                //Deletes.....
                                //First check to see if there are any purchases for this exhibit
                                if (dictPurchases.ContainsKey(Purchase.Exhibit.TagNumber) && dictPurchases[Purchase.Exhibit.TagNumber].ContainsKey(Purchase.Exhibit.MarketItem.MarketID))
                                {
                                    fGrossSold -= this[Purchase].TotalBid;

                                    //Then, if there are, check to see if this purchase index exists, if it does, remove it
                                    if (dictPurchases[Purchase.Exhibit.TagNumber][Purchase.Exhibit.MarketItem.MarketID].ContainsKey(Purchase.PurchaseIndex))
                                    {
                                        dictPurchases[Purchase.Exhibit.TagNumber][Purchase.Exhibit.MarketItem.MarketID].Remove(Purchase.PurchaseIndex);
                                    }

                                    //Now, if there are no purchases left for this exhibit, we can get rid of the dictionary as well
                                    if (dictPurchases[Purchase.Exhibit.TagNumber][Purchase.Exhibit.MarketItem.MarketID].Keys.Count <= 0)
                                    {
                                        dictPurchases[Purchase.Exhibit.TagNumber].Remove(Purchase.Exhibit.MarketItem.MarketID);
                                    }
                                    if (dictPurchases[Purchase.Exhibit.TagNumber].Keys.Count <= 0)
                                    {
                                        dictPurchases.Remove(Purchase.Exhibit.TagNumber);
                                    }

                                    UpdateArgs.UpdatedItems.Add(Purchase, DB.CommitAction.Delete);
                                }
                            }

                            //Keep track of the last update time
                            if (PurchaseReader.GetDateTime(0) > m_dtLastUpdate)
                            {
                                m_dtLastUpdate = PurchaseReader.GetDateTime(0);
                            }
                        }
                        catch (Exception ex)
                        {
                            clsLogger.WriteLog(string.Format("An error occured processing update for purchase exhibit number {0}, exhibit item {1}, purchase index {2}:\r\nMessage:{3}\r\nStack Trace:\r\n{4}\r\n", iExhibitNumber, iExhibitItem, iPurchaseIndex, ex.Message, ex.StackTrace));
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.WriteLog(string.Format("An error occured updating purchases while the reader was open:\r\nMessage:{0}\r\nStack Trace:\r\n{1}\r\n", ex.Message, ex.StackTrace));
                }
                PurchaseReader.Close();

                //Call the updated event if nessesary
                if (UpdateArgs.UpdatedItems.Count > 0)
                {
                    onUpdated(UpdateArgs);
                }
            }
            catch (Exception ex)
            {
                clsLogger.WriteLog(string.Format("An error occured updating Purchases:\r\nMessage:{0}\r\nStack Trace:\r\n{1}\r\n", ex.Message, ex.StackTrace));
            }
        }

        public List<clsPurchase> GetPurchasesByBuyer(int BuyerNumber)
        {
            List<clsPurchase> Output = new List<clsPurchase>();

            foreach (Dictionary<int, Dictionary<int, clsPurchase>> dictPurchasedTags in dictPurchases.Values)
            {
                foreach (Dictionary<int, clsPurchase> dictPurchasedItems in dictPurchasedTags.Values)
                {
                    foreach (clsPurchase Purchase in dictPurchasedItems.Values)
                    {
                        if (Purchase.BuyerID == BuyerNumber)
                        {
                            Output.Add(Purchase);
                        }
                    }
                }
            }

            return Output;
        }

        public clsPurchase this[clsPurchase Purchase]
        {
            get
            {
                return this[Purchase.Exhibit, Purchase.PurchaseIndex];
            }
            set
            {
                this[Purchase.Exhibit, Purchase.PurchaseIndex] = Purchase;
            }
        }

        public clsPurchase this[clsExhibit Exhibit, int PurchaseIndex]
        {
            get
            {
                return this[Exhibit.TagNumber, Exhibit.MarketItem.MarketID, PurchaseIndex];
            }
            set
            {
                if (Exhibit != null)
                {
                    this[Exhibit.TagNumber, Exhibit.MarketItem.MarketID, PurchaseIndex] = value;
                }
            }
        }

        public clsPurchase this[int ExhibitTag, int ExhibitItem, int PurchaseIndex]
        {
            get
            {
                if (dictPurchases.ContainsKey(ExhibitTag) && dictPurchases[ExhibitTag].ContainsKey(ExhibitItem) && dictPurchases[ExhibitTag][ExhibitItem].ContainsKey(PurchaseIndex))
                {
                    return dictPurchases[ExhibitTag][ExhibitItem][PurchaseIndex];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (!dictPurchases.ContainsKey(ExhibitTag))
                {
                    dictPurchases.Add(ExhibitTag, new Dictionary<int, Dictionary<int, clsPurchase>>()); 
                }
                if (!dictPurchases[ExhibitTag].ContainsKey(ExhibitItem))
                {
                    dictPurchases[ExhibitTag].Add(ExhibitItem, new Dictionary<int, clsPurchase>());
                }
                if (!dictPurchases[ExhibitTag][ExhibitItem].ContainsKey(PurchaseIndex))
                {
                    dictPurchases[ExhibitTag][ExhibitItem].Add(PurchaseIndex, value);
                }
                else
                {
                    dictPurchases[ExhibitTag][ExhibitItem][PurchaseIndex] = value;
                }
            }
        }

        public Dictionary<int, clsPurchase> this[int ExhibitTag, int ExhibitItem]
        {
            get
            {
                if (dictPurchases.ContainsKey(ExhibitTag) && dictPurchases[ExhibitTag].ContainsKey(ExhibitItem))
                {
                    return dictPurchases[ExhibitTag][ExhibitItem];
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (!dictPurchases.ContainsKey(ExhibitTag))
                {
                    dictPurchases.Add(ExhibitTag, new Dictionary<int, Dictionary<int, clsPurchase>>());
                }
                if (!dictPurchases[ExhibitTag].ContainsKey(ExhibitItem))
                {
                    dictPurchases[ExhibitTag].Add(ExhibitItem, value);
                }
                else
                {
                    dictPurchases[ExhibitTag][ExhibitItem] = value;
                }
            }
        }

        public Dictionary<int, clsPurchase> this[clsExhibit Exhibit]
        {
            get
            {
                return this[Exhibit.TagNumber, Exhibit.MarketID];
            }
            set
            {
                this[Exhibit.TagNumber, Exhibit.MarketID] = value;
            }
        }

        public double GrossSold
        {
            get
            {
                return fGrossSold;
            }
        }

        public void ExportToWorkbook(OfficeOpenXml.ExcelPackage outputPackage)
        {
            OfficeOpenXml.ExcelWorksheet sheetPurchases = outputPackage.Workbook.Worksheets.Add("Purchases");

            //Setup the headers
            sheetPurchases.Row(1).Style.Font.Bold = true;

            sheetPurchases.Cells["A1"].Value = "ID";
            sheetPurchases.Cells["B1"].Value = "Buyer Name";
            sheetPurchases.Cells["C1"].Value = "Company Name";
            sheetPurchases.Cells["D1"].Value = "Address";
            sheetPurchases.Cells["E1"].Value = "Phone Number";
            sheetPurchases.Cells["F1"].Value = "Exhibitor Name";
            sheetPurchases.Cells["G1"].Value = "Tag Number";
            sheetPurchases.Cells["H1"].Value = "MarketItem";
            sheetPurchases.Cells["I1"].Value = "Champion/ROG";

            sheetPurchases.Cells["J1"].Value = "Weight / Qty";
            sheetPurchases.Cells["K1"].Value = "TurnedBack";
            sheetPurchases.Cells["L1"].Value = "AmountCharged";
            sheetPurchases.Cells["M1"].Value = "Destination";
            sheetPurchases.Cells["N1"].Value = "Destination Notes";
            

            //Enter Data
            int iCurRow = 2;
            foreach (clsPurchase Purchase in this)
            {
                sheetPurchases.SetValue(iCurRow, 1, Purchase.Buyer.BuyerNumber);
                sheetPurchases.SetValue(iCurRow, 2, Purchase.Buyer.Name.ToString());
                sheetPurchases.SetValue(iCurRow, 3, Purchase.Buyer.CompanyName.ToString());
                sheetPurchases.SetValue(iCurRow, 4, Purchase.Buyer.Address.ToString());
                sheetPurchases.SetValue(iCurRow, 5, Purchase.Buyer.PhoneNumber.ToString());
                sheetPurchases.SetValue(iCurRow, 6, Purchase.Exhibit.Exhibitor.Name.ToString());
                sheetPurchases.SetValue(iCurRow, 7, Purchase.Exhibit.TagNumber);
                sheetPurchases.SetValue(iCurRow, 8, Purchase.Exhibit.MarketItem.MarketType.ToString());
                sheetPurchases.SetValue(iCurRow, 9, Purchase.Exhibit.ChampionStatusText);

                sheetPurchases.SetValue(iCurRow, 10, Purchase.Exhibit.Weight);
                sheetPurchases.Cells[string.Format("I{0}", iCurRow)].Style.Numberformat.Format = "#### \"" + Purchase.Exhibit.MarketItem.MarketUnits + "\"";

                if (Purchase.ConditionOfSale == clsPurchase.enSaleCondition.PayAdvertising)
                {
                    sheetPurchases.SetValue(iCurRow, 11, "Turned Back");
                }
                else if (Purchase.ConditionOfSale == clsPurchase.enSaleCondition.PayFullPrice)
                {
                    sheetPurchases.SetValue(iCurRow, 11, "Kept by Buyer");
                }

                sheetPurchases.SetValue(iCurRow, 12, Purchase.TotalCharged);

                if (Purchase.ConditionOfSale == clsPurchase.enSaleCondition.PayFullPrice)
                {
                    if (Purchase.DestinationOfAnimal == clsPurchase.enAnimalDestination.Fair)
                    {
                        sheetPurchases.SetValue(iCurRow, 13, "Fair");
                    }
                    else if (Purchase.DestinationOfAnimal == clsPurchase.enAnimalDestination.Seller)
                    {
                        sheetPurchases.SetValue(iCurRow, 13, "Hauled By Seller");
                        sheetPurchases.SetValue(iCurRow, 14, Purchase.HauledBy);
                    }
                    else if (Purchase.DestinationOfAnimal == clsPurchase.enAnimalDestination.Buyer)
                    {
                        sheetPurchases.SetValue(iCurRow, 13, "Hauled By Buyer");
                    }
                    else if (Purchase.DestinationOfAnimal == clsPurchase.enAnimalDestination.SpecialInstructions)
                    {
                        sheetPurchases.SetValue(iCurRow, 13, "Special Instructions");
                        sheetPurchases.SetValue(iCurRow, 14, Purchase.HaulSpecialInstructions);
                    }
                }
                else if (Purchase.ConditionOfSale == clsPurchase.enSaleCondition.PayAdvertising)
                {
                    if (Purchase.Exhibit.TakeBack == DB.NoYes.Yes)
                    {
                        sheetPurchases.SetValue(iCurRow, 13, "Hauled by Seller");
                    }
                    else if (Purchase.Exhibit.TakeBack == DB.NoYes.No)
                    {
                        sheetPurchases.SetValue(iCurRow, 13, "New Holland");
                    }
                    
                }
                
                iCurRow++;
            }

            iCurRow--;
            if (iCurRow > 1)
            {
                sheetPurchases.Cells[string.Format("L2:L{0}", iCurRow)].Style.Numberformat.Format = "$#,##0.00";
                //sheetPurchases.Cells[string.Format("K2:K{0}", iCurRow)].Style.Numberformat.Format = "$#,##0.00";
            }

            //Auto fit columns
            sheetPurchases.Column(1).AutoFit();
            sheetPurchases.Column(2).AutoFit();
            sheetPurchases.Column(3).AutoFit();
            sheetPurchases.Column(4).AutoFit();
            sheetPurchases.Column(5).AutoFit();
            sheetPurchases.Column(6).AutoFit();
            sheetPurchases.Column(7).AutoFit();
            sheetPurchases.Column(8).AutoFit();
            sheetPurchases.Column(9).AutoFit();
            sheetPurchases.Column(10).AutoFit();
            sheetPurchases.Column(11).AutoFit();
            sheetPurchases.Column(12).AutoFit();
            sheetPurchases.Column(13).AutoFit();
            sheetPurchases.Column(14).AutoFit();
        }
        public void ExportTurnBackList(OfficeOpenXml.ExcelPackage outputPackage, string marketType)
        {
            OfficeOpenXml.ExcelWorksheet currentSheet = outputPackage.Workbook.Worksheets[marketType];

            //Setup the headers
            currentSheet.Row(1).Style.Font.Bold = true;

            currentSheet.Cells["A1"].Value = "Tag Number";
            currentSheet.Cells["B1"].Value = "Market Item";
            currentSheet.Cells["C1"].Value = "Weight (lbs)";
            currentSheet.Cells["D1"].Value = "Market Price";
            currentSheet.Cells["E1"].Value = "Total";
            
            int iCurRow = 2;
            foreach (clsPurchase Purchase in this)
            {
                if ((Purchase.Exhibit.MarketItem.MarketType == marketType) && (Purchase.ConditionOfSale == clsPurchase.enSaleCondition.PayAdvertising))
                {
                    currentSheet.SetValue(iCurRow, 1, Purchase.Exhibit.TagNumber);
                    currentSheet.SetValue(iCurRow, 2, marketType);
                    currentSheet.SetValue(iCurRow, 3, Purchase.Exhibit.Weight);
                    currentSheet.SetValue(iCurRow, 4, Purchase.Exhibit.MarketItem.MarketValue);
                    currentSheet.SetValue(iCurRow, 5, Purchase.Exhibit.Weight * Purchase.Exhibit.MarketItem.MarketValue);

                    iCurRow++;
                }
            }

            iCurRow--;
            if (iCurRow > 1)
            {
                currentSheet.Cells[string.Format("D2:D{0}", iCurRow)].Style.Numberformat.Format = "$#,##0.00";
                currentSheet.Cells[string.Format("E2:E{0}", iCurRow)].Style.Numberformat.Format = "$#,##0.00";
                currentSheet.Cells[string.Format("A2:E{0}", iCurRow)].Sort();
            }

            //Auto fit columns
            currentSheet.Column(1).AutoFit();
            currentSheet.Column(2).AutoFit();
            currentSheet.Column(3).AutoFit();
            currentSheet.Column(4).AutoFit();
            currentSheet.Column(5).AutoFit();            
        }

        #region IEnumerable<clsPurchase> Members

        public IEnumerator<clsPurchase> GetEnumerator()
        {
            return new clsPurchaseEnumerator(dictPurchases);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new clsPurchaseEnumerator(dictPurchases);
        }

        #endregion
    }

   


    public class clsPurchaseEnumerator : IEnumerator<clsPurchase>
    {
        Dictionary<int, Dictionary<int, Dictionary<int, clsPurchase>>> dictPurchases;
        int iCurTagIndex = 0;
        int iCurMarketIndex = 0;
        int iCurPurchaseIndex = -1;

        public clsPurchaseEnumerator(Dictionary<int, Dictionary<int, Dictionary<int, clsPurchase>>> Purchases)
        {
            dictPurchases = Purchases;
        }


        #region IEnumerator<clsExhibit> Members

        public clsPurchase Current
        {
            get
            {
                return dictPurchases.Values.ToArray()[iCurTagIndex].Values.ToArray()[iCurMarketIndex].Values.ToArray()[iCurPurchaseIndex];
            }
        }

        public void Dispose()
        {
            
        }

        object System.Collections.IEnumerator.Current
        {
            get
            {
                return dictPurchases.Values.ToArray()[iCurTagIndex].Values.ToArray()[iCurMarketIndex].Values.ToArray()[iCurPurchaseIndex];
            }
        }

        public bool MoveNext()
        {
            iCurPurchaseIndex++;
            if (dictPurchases.Keys.Count > iCurTagIndex)
            {
                if (iCurPurchaseIndex >= dictPurchases.Values.ToArray()[iCurTagIndex].Values.ToArray()[iCurMarketIndex].Keys.Count)
                {
                    iCurPurchaseIndex = 0;
                    iCurMarketIndex++;
                }

                if (iCurMarketIndex >= dictPurchases[dictPurchases.Keys.ToArray()[iCurTagIndex]].Keys.Count)
                {
                    iCurMarketIndex = 0;
                    iCurTagIndex++;
                }

                if (iCurTagIndex >= dictPurchases.Keys.Count)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            iCurTagIndex = 0;
            iCurMarketIndex = -1;
        }

        #endregion
    }

    [Serializable()]
    public class clsPurchase : AuctionData, IComparable<clsPurchase>, ISerializable
    {
        public enum enSaleCondition
        {
            NotSet = 0,
            PayFullPrice = 1,
            PayAdvertising = 2
        }

        public enum enPaymentMethod
        {
            NotSet = 0,
            CashNow = 1,
            CheckNow = 2,
            SendBill = 3
        }

        public enum enAnimalDestination
        {
            NotSet = 0,
            Buyer = 1,
            Seller = 2,
            Fair = 3,
            SpecialInstructions = 4
        }

        public enum enTakeBack
        {
            NotSet = 0,
            Yes = 1,
            No = 2
        }

        public enum enRelativeUnits
        {
            NotSet = 0,
            PerUnit = 1,
            Total = 2
        }

        public enum enAdditionalPaymentTo
        {
            NotSet = 0,
            Exhibitor = 1,
            CC_4H = 2,
            CC_Fair = 3,
            Other = 4
        }

        int iPurchaseIndex;
        clsExhibit cExhibit;
        int iBuyerID;

        enSaleCondition eSaleCondition;
        double dFinalBid;
        int iRecipientID;
        enAnimalDestination eDestination;
        string sHauledBy;
        string sHaulSpecialInstructions;

        public clsPurchase()
        {

        }

        public clsPurchase(int PurchaseIndex, int BuyerID)
        {
            iPurchaseIndex = PurchaseIndex;
            cExhibit = null;
            iBuyerID = BuyerID;
            dFinalBid = double.NaN;

            eSaleCondition = enSaleCondition.NotSet;
            eDestination = enAnimalDestination.NotSet;
            sHauledBy = "";
            sHaulSpecialInstructions = "";
        }

        public clsPurchase(int PurchaseIndex, clsExhibit Exhibit, clsBuyer Buyer, clsExhibitor Recipient, double FinalBid)
        {
            iPurchaseIndex = PurchaseIndex;
            cExhibit = Exhibit;
            iBuyerID = Buyer.BuyerNumber;
            iRecipientID = Recipient.ExhibitorNumber;
            dFinalBid = FinalBid;

            eSaleCondition = enSaleCondition.NotSet;
            eDestination = enAnimalDestination.NotSet;
            sHauledBy = "";
            sHaulSpecialInstructions = "";
        }

        public clsPurchase(int PurchaseIndex, int ExhibitTag, int ExhibitItem, int BuyerID, int RecipientID, double FinalBid)
        {
            iPurchaseIndex = PurchaseIndex;
            cExhibit = clsDB.Exhibits[ExhibitTag, ExhibitItem];
            iBuyerID = BuyerID;
            iRecipientID = RecipientID;
            dFinalBid = FinalBid;

            eSaleCondition = enSaleCondition.NotSet;
            eDestination = enAnimalDestination.NotSet;
            sHauledBy = "";
            sHaulSpecialInstructions = "";
        }

        public clsPurchase(int PurchaseIndex, int ExhibitTag, int ExhibitItem, int BuyerID, int RecipientID, enSaleCondition ConditionOfSale, double FinalBid, enAnimalDestination DestinationOfAnimal, string HauledBy, string SpecialInstructions)
        {
            iPurchaseIndex = PurchaseIndex;
            cExhibit = clsDB.Exhibits[ExhibitTag, ExhibitItem];
            iBuyerID = BuyerID;
            iRecipientID = RecipientID;
            dFinalBid = FinalBid;

            eSaleCondition = ConditionOfSale;
            eDestination = DestinationOfAnimal;
            sHauledBy = HauledBy;
            sHaulSpecialInstructions = SpecialInstructions;
        }

        public override void Load(IDataReader dbReader)
        {
            base.Load(dbReader);
        }

        protected override void DBCommit(DB.CommitAction Action, IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            IDbCommand dbCommit = DatabaseConnection.CreateCommand();
            dbCommit.Transaction = Transaction;

            dbCommit.CommandText = "INSERT INTO Purchases (CommitAction, ExhibitTag, ExhibitItem, PurchaseIndex, BuyerID, RecipientID, SaleCondition, PurchaseBid, Destination, HauledBy, SpecialInstructions) VALUES (@CommitAction, @ExhibitTag, @ExhibitItem, @PurchaseIndex, @BuyerID, @RecipientID, @SaleCondition, @PurchaseBid, @Destination, @HauledBy, @SpecialInstructions)";

            IDbDataParameter param = dbCommit.CreateParameter();
            param.ParameterName = "@CommitAction";
            param.Value = (int)Action;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@ExhibitTag";
            param.Value = this.Exhibit.TagNumber;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@ExhibitItem";
            param.Value = this.Exhibit.MarketItem.MarketID;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@PurchaseIndex";
            param.Value = this.PurchaseIndex;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@BuyerID";
            param.Value = this.BuyerID;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@RecipientID";
            param.Value = this.RecipientID;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@SaleCondition";
            param.Value = (int)this.ConditionOfSale;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@PurchaseBid";
            param.Value = this.FinalBid;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Destination";
            param.Value = (int)this.DestinationOfAnimal;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@HauledBy";
            param.Value = this.HauledBy;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@SpecialInstructions";
            param.Value = this.HaulSpecialInstructions;
            dbCommit.Parameters.Add(param);

            dbCommit.ExecuteNonQuery();
        }

        public static bool operator==(clsPurchase Purchase1, clsPurchase Purchase2)
        {
            if ((object)Purchase1 == null && (object)Purchase2 == null)
            {
                return true;
            }
            else if ((object)Purchase1 == null || (object)Purchase2 == null)
            {
                return false;
            }
            else
            {
                return (Purchase1.CompareTo(Purchase2) == 0);
            }
        }
        public static bool operator !=(clsPurchase Purchase1, clsPurchase Purchase2)
        {
            return !(Purchase1 == Purchase2);
        }

        #region IComparable<clsPurchase> Members

        public int CompareTo(clsPurchase other)
        {
            if (this.Exhibit.CompareTo(other.Exhibit) != 0)
            {
                return this.Exhibit.CompareTo(other.Exhibit);
            }
            else
            {
                return this.PurchaseIndex.CompareTo(other.PurchaseIndex);
            }
        }

        #endregion
        
        #region ISerializable Members
        protected clsPurchase(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            iPurchaseIndex = (int)info.GetValue("iPurchaseIndex", typeof(int));
            cExhibit = (clsExhibit)info.GetValue("cExhibit", typeof(clsExhibit));
            iBuyerID = (int)info.GetValue("iBuyerID", typeof(int));
            eSaleCondition = (enSaleCondition)info.GetValue("eSaleCondition", typeof(enSaleCondition));
            dFinalBid = (double)info.GetValue("dFinalBid", typeof(double));
            iRecipientID = (int)info.GetValue("iRecipientID", typeof(int));
            eDestination = (enAnimalDestination)info.GetValue("eDestination", typeof(enAnimalDestination));
            sHauledBy = (string)info.GetValue("sHauledBy", typeof(string));
            sHaulSpecialInstructions = (string)info.GetValue("sHaulSpecialInstructions", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            info.AddValue("iPurchaseIndex", iPurchaseIndex);
            info.AddValue("cExhibit", cExhibit);
            info.AddValue("iBuyerID", iBuyerID);
            info.AddValue("eSaleCondition", eSaleCondition);
            info.AddValue("dFinalBid", dFinalBid);
            info.AddValue("iRecipientID", iRecipientID);
            info.AddValue("eDestination", eDestination);
            info.AddValue("sHauledBy", sHauledBy);
            info.AddValue("sHaulSpecialInstructions", sHaulSpecialInstructions);
        }
        #endregion

        public int PurchaseIndex
        {
            get
            {
                return iPurchaseIndex;
            }
        }

        public clsExhibit Exhibit
        {
            get
            {
                return cExhibit;
            }
        }

        public int BuyerID
        {
            get
            {
                return iBuyerID;
            }
            set
            {
                iBuyerID = value;
            }
        }

        public clsBuyer Buyer
        {
            get
            {
                return clsDB.Buyers[iBuyerID];
            }
        }

        public int RecipientID
        {
            get
            {
                return iRecipientID;
            }
            set
            {
                iRecipientID = value;
            }
        }

        public clsExhibitor Recipient
        {
            get
            {
                return clsDB.Exhibitors[iRecipientID];
            }
        }

        public enSaleCondition ConditionOfSale
        {
            get
            {
                return eSaleCondition;
            }
            set
            {
                eSaleCondition = value;
            }
        }

        public string ConditionOfSale_String
        {
            get
            {
                if (eSaleCondition != enSaleCondition.NotSet)
                {
                    if (eSaleCondition == enSaleCondition.PayAdvertising)
                    {
                        return "Advertising";
                    }
                    else
                    {
                        return "Keep Animal";
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public string BuyerKeepingAnimal
        {
            get
            {
                if (eSaleCondition == enSaleCondition.PayFullPrice)
                {
                    return "Yes";
                }
                else
                {
                    return "No";
                }
            }
        }

        public double FinalBid
        {
            get
            {
                return dFinalBid;
            }
            set
            {
                dFinalBid = value;
            }
        }

        public string FinalBid_String
        {
            get
            {
                if (Exhibit.MarketItem.MarketUnits.Trim().Length <= 0 || !Exhibit.MarketItem.SellByPound)
                {
                    return FinalBid.ToString("$#,##0.00");
                }
                else
                {
                    return (FinalBid * Exhibit.Weight).ToString("$#,##0.00") + " @ " + FinalBid.ToString(String.Format("$#,##0.00/{0}", Exhibit.MarketItem.MarketUnits));
                }
                
            }
        }
        
        public enAnimalDestination DestinationOfAnimal
        {
            get
            {
                return eDestination;
            }
            set
            {
                eDestination = value;
            }
        }

        public string DestinationOfAnimal_String
        {
            get
            {
                if (eDestination != enAnimalDestination.NotSet)
                {
                    if (eDestination == enAnimalDestination.Buyer)
                    {
                        return "Buyer Self Haul";
                    }
                    else if (eDestination == enAnimalDestination.Seller)
                    {
                        //return "Hauled by " + ((sHauledBy.Trim().Length > 0) ? sHauledBy : "(unspecified)");
                        return "Hauled by Seller";
                    }
                    else if (eDestination == enAnimalDestination.Fair)
                    {
                        return "Buyer requests Fair to deliver animal to destination";
                    }
                    else
                    {
                        return "Special Instructions " + ((sHaulSpecialInstructions.Trim().Length > 0) ? sHaulSpecialInstructions : "(unspecified)");
                    }
                }
                else
                {
                    return "";
                }
            }
        }

        public bool DestinationOfAnimal_Valid
        {
            get
            {
                if (!this.Exhibit.MarketItem.ValidDisposition)
                {
                    return true;
                }
                else if (eSaleCondition == enSaleCondition.PayFullPrice)
                {
                    if (eDestination != enAnimalDestination.NotSet)
                    {
                        if (eDestination == enAnimalDestination.SpecialInstructions)
                        {
                            return (sHaulSpecialInstructions.Trim().Length > 0);
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        public string HauledBy
        {
            get
            {
                return sHauledBy;
            }
            set
            {
                sHauledBy = value;
            }
        }

        public string HaulSpecialInstructions
        {
            get
            {
                return sHaulSpecialInstructions;
            }
            set
            {
                sHaulSpecialInstructions = value;
            }
        }

        public bool ExhibitorKept
        {
            get
            {
                if (eSaleCondition == enSaleCondition.PayAdvertising && (cExhibit == null || cExhibit.TakeBack == DB.NoYes.Yes))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public double FairFee
        {
            get
            {
                if (cExhibit != null)
                {
                    return Math.Round(TotalBid * clsDB.Settings.FairFee, 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        public double TotalBid
        {
            get
            {
                if (cExhibit != null)
                {
                    return dFinalBid * (cExhibit.MarketItem.SellByPound ? cExhibit.Weight : 1);
                }
                else
                {
                    return 0;
                }
            }
        }

        public string TotalBid_String
        {
            get
            {
                return TotalBid.ToString("$#,##0.00");
            }
        }

        public double TotalCharged
        {
            get
            {
                if (cExhibit != null)
                {
                    return TotalBid - ((eSaleCondition == enSaleCondition.PayAdvertising ? cExhibit.MarketItem.MarketValue : 0) * cExhibit.Weight);
                }
                else
                {
                    return 0;
                }
            }
        }

        public double TotalPayOut
        {
            get
            {
                if (cExhibit != null)
                {
                    return TotalBid - ((ExhibitorKept ? cExhibit.MarketItem.MarketValue : 0) * cExhibit.Weight) - FairFee;
                }
                else
                {
                    return 0;
                }
            }
        }

        public string TotalPayOut_String
        {
            get
            {
                return TotalPayOut.ToString("$#,##0.00");
            }
        }
    }

    namespace Setup
    {
        public class Purchases : AuctionDBSetup
        {
            public Purchases()
            {
                TABLE_NAME = "Purchases";

                TABLE_COLUMNS = new List<SQLColumn>() {
                    new SQLColumn("ExhibitTag", "int", "0", true),
                    new SQLColumn("ExhibitItem", "int", "0", true),
                    new SQLColumn("PurchaseIndex", "int", "0", true),
                    new SQLColumn("BuyerID", "int", "0", false),
                    new SQLColumn("RecipientID", "int", "0", false),	/*Links to exhibitor who receives money for purchase, most likely the seller but in some cases might a donation to the fair*/
                    new SQLColumn("SaleCondition", "int", "0", false),	/*Buyer Keeps Animal, or turns it back*/
                    new SQLColumn("PaymentMethod", "int", "0", false),	/*Pay now (cash or check) or Mail bill*/
                    new SQLColumn("CheckNumber", "int", "0", false),
                    new SQLColumn("PurchaseBid", "Money", "0", false),
                    new SQLColumn("AdditionalAmount", "Money", "0", false),
                    new SQLColumn("Destination", "int", "0", false),	/*Self Hauled or Hauled by, Galvinell Meat Company, Other/Special Instuctions, */
                    new SQLColumn("HauledBy", "nvarchar(200)", "", false),
                    new SQLColumn("SpecialInstructions", "nvarchar(1000)", "", false),
                    new SQLColumn("Comments", "nvarchar(1000)", "", false)
                };
            }
        }
    }
}
