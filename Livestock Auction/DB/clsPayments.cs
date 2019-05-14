using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Runtime.Serialization;
using System.Data;

namespace Livestock_Auction.DB
{

    public class clsPayments : clsAuctionDataCollection<IDAuctionDataKey, clsPayment, DB.Setup.Payments>
    {
        Dictionary<int, Dictionary<int, clsPayment>> dictPayments;

        public clsPayments(IDbConnection Connection)
            : base(Connection)
        {
        }

        public override void Load()
        {
            dictPayments = new Dictionary<int, Dictionary<int, clsPayment>>();

            m_Setup = new DB.Setup.Payments();
            IDbCommand dbLoad = m_Setup.SQLLoadDataQuery(m_dbConn);
            //IDbCommand dbLoad = m_dbConn.CreateCommand();
            //dbLoad.CommandText = "SELECT LastUpdated, BuyerID, PaymentIndex, PaymentMethod, Amount, CheckNumber, Comments FROM vPayments_Current ORDER BY BuyerID ASC, PaymentIndex ASC";

            IDataReader PaymentReader = dbLoad.ExecuteReader();

            while (PaymentReader.Read())
            {
                clsPayment Payment = new clsPayment((int)PaymentReader["BuyerID"], (int)PaymentReader["PaymentIndex"], (clsPayment.PaymentMethod)PaymentReader["PaymentMethod"], (int)PaymentReader["CheckNumber"], double.Parse(PaymentReader["Amount"].ToString()), PaymentReader["Comments"].ToString());

                if (!dictPayments.ContainsKey(Payment.BuyerNumber))
                {
                    dictPayments.Add(Payment.BuyerNumber, new Dictionary<int, clsPayment>());
                }
                dictPayments[Payment.BuyerNumber].Add(Payment.Index, Payment);

                if (PaymentReader["LastUpdated"] is DateTime)
                {
                    m_dtLastUpdate = (DateTime)PaymentReader["LastUpdated"];
                }
                else
                {
                    m_dtLastUpdate = DateTime.Parse(PaymentReader["LastUpdated"].ToString());
                }
            }
            PaymentReader.Close();
        }

        public override void Update(IDbConnection Connection)
        {
            try
            {
                DatabaseUpdatedEventArgs UpdateArgs = new DatabaseUpdatedEventArgs();

                //IDbCommand dbUpdate = Connection.CreateCommand();
                //dbUpdate.CommandText = "SELECT CommitDate, CommitAction, BuyerID, PaymentIndex, PaymentMethod, Amount, CheckNumber, Comments FROM vPayments_Journal WHERE CommitDate > '" + m_dtLastUpdate.ToString("yyyy-MM-dd HH:mm:ss.fffffff") + "' ORDER BY BuyerID ASC, PaymentIndex ASC";
                //dbUpdate.CommandText = "SELECT CommitDate, CommitAction, BuyerID, PaymentIndex, PaymentMethod, Amount, CheckNumber, Comments FROM vPayments_Journal WHERE CommitDate > @LastUpdate ORDER BY BuyerID ASC, PaymentIndex ASC";

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

                IDataReader PaymentReader = dbUpdate.ExecuteReader();

                try
                {
                    while (PaymentReader.Read())
                    {
                        Int32 iBuyerNumber = -1;
                        Int32 iPaymentIndex = -1;
                        try
                        {
                            iBuyerNumber = (Int32)PaymentReader["BuyerID"];
                            iPaymentIndex = (Int32)PaymentReader["PaymentIndex"];

                            //Apply the changes to the dictionary
                            clsPayment Payment = new clsPayment(iBuyerNumber, iPaymentIndex, (clsPayment.PaymentMethod)PaymentReader["PaymentMethod"], (int)PaymentReader["CheckNumber"], double.Parse(PaymentReader["Amount"].ToString()), PaymentReader["Comments"].ToString());
                            if ((DB.CommitAction)int.Parse(PaymentReader["CommitAction"].ToString()) == DB.CommitAction.Modify)
                            {
                                //Modifications and addtions....
                                if (!dictPayments.ContainsKey(Payment.BuyerNumber))
                                {
                                    dictPayments.Add(Payment.BuyerNumber, new Dictionary<int, clsPayment>());
                                }
                                if (!dictPayments[Payment.BuyerNumber].ContainsKey(Payment.Index))
                                {
                                    dictPayments[Payment.BuyerNumber].Add(Payment.Index, Payment);
                                }
                                else
                                {
                                    dictPayments[Payment.BuyerNumber][Payment.Index] = Payment;
                                }
                            }
                            else
                            {
                                //Deletes.....
                                if (dictPayments.ContainsKey(Payment.BuyerNumber))
                                {
                                    dictPayments[Payment.BuyerNumber].Remove(Payment.Index);
                                    if (dictPayments[Payment.BuyerNumber].Count <= 0)
                                    {
                                        dictPayments.Remove(Payment.BuyerNumber);
                                    }
                                }
                            }

                            //Keep Track up what was changed for the updated event
                            UpdateArgs.UpdatedItems.Add(Payment, (DB.CommitAction)int.Parse(PaymentReader["CommitAction"].ToString()));

                            //Keep track of the last update time
                            DateTime dtLastUpdate;
                            if (PaymentReader["CommitDate"] is DateTime)
                            {
                                dtLastUpdate = (DateTime)PaymentReader["CommitDate"];
                            }
                            else
                            {
                                dtLastUpdate = DateTime.Parse(PaymentReader["CommitDate"].ToString());
                            }

                            if (dtLastUpdate > m_dtLastUpdate)
                            {
                                m_dtLastUpdate = dtLastUpdate;
                            }
                        }
                        catch (Exception ex)
                        {
                            clsLogger.WriteLog(string.Format("An error occured processing update for payment buyer number {0}, payment index {1}:\r\nMessage:{2}\r\nStack Trace:\r\n{3}\r\n", iBuyerNumber, iPaymentIndex, ex.Message, ex.StackTrace));
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.WriteLog(string.Format("An error occured updating payments while the reader was open:\r\nMessage:{0}\r\nStack Trace:\r\n{1}\r\n", ex.Message, ex.StackTrace));
                }
                PaymentReader.Close();

                //Call the updated event if nessesary
                if (UpdateArgs.UpdatedItems.Count > 0)
                {
                    onUpdated(UpdateArgs);
                }
            }
            catch (Exception ex)
            {
                clsLogger.WriteLog(string.Format("An error occured updating Payments:\r\nMessage:{0}\r\nStack Trace:\r\n{1}\r\n", ex.Message, ex.StackTrace));
            }
        }

        public clsPayment this[int BuyerNumber, int PaymentIndex]
        {
            get
            {
                if (dictPayments.ContainsKey(BuyerNumber) && dictPayments[BuyerNumber].ContainsKey(PaymentIndex))
                {
                    return dictPayments[BuyerNumber][PaymentIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        public Dictionary<int, clsPayment> this[int BuyerNumber]
        {
            get
            {
                if (dictPayments.ContainsKey(BuyerNumber))
                {
                    return dictPayments[BuyerNumber];
                }
                else
                {
                    return null;
                }
            }
        }

        public Dictionary<int, clsPayment> this[clsBuyer Buyer]
        {
            get
            {
                if (dictPayments.ContainsKey(Buyer.BuyerNumber))
                {
                    return dictPayments[Buyer.BuyerNumber];
                }
                else
                {
                    return null;
                }
            }
        }
    }

    public class clsPayment : AuctionData, IComparable<clsPayment>
    {
        //Square gets 2.75% of the total purchase. The surcharge is
        //  calculated such that the total amount owed by the buyer
        //  gets the fair the full purchase price and the square
        //  their 2.75%.
        public double CREDIT_CARD_FEE = Math.Round((1 / (1 - 0.0275)) - 1, 5);

        public enum PaymentMethod
        {
            NotSet = 0,
            Cash = 1,
            Check = 2,
            Credit = 3
        }

        int iBuyerNum = 0;
        clsBuyer cBuyer = null;
        int iPaymentIndex = -1;
        PaymentMethod eMethod = PaymentMethod.NotSet;
        int iCheckNumber = 0;
        double fAmount = 0.0;
        string sComments = "";

        public clsPayment()
        {

        }

        public clsPayment(int BuyerNumber, PaymentMethod Method, int CheckNumber, double Amount, string Comments)
        {
            iBuyerNum = BuyerNumber;
            cBuyer = clsDB.Buyers[iBuyerNum];
            iPaymentIndex = 0;
            if (clsDB.Payments[BuyerNumber] != null)
            {
                foreach (clsPayment Payment in clsDB.Payments[BuyerNumber].Values)
                {
                    if (Payment.Index >= iPaymentIndex)
                    {
                        iPaymentIndex = Payment.Index + 1;
                    }
                }
            }
            
            eMethod = Method;
            iCheckNumber = CheckNumber;
            fAmount = Amount;
            sComments = Comments;
        }

        public clsPayment(int BuyerNumber, int PaymentIndex, PaymentMethod Method, int CheckNumber, double Amount, string Comments)
        {
            iBuyerNum = BuyerNumber;
            iPaymentIndex = PaymentIndex;
            eMethod = Method;
            iCheckNumber = CheckNumber;
            fAmount = Amount;
            sComments = Comments;
        }

        public override void Load(IDataReader dbReader)
        {
            base.Load(dbReader);
        }

        protected override void DBCommit(DB.CommitAction Action, IDbConnection DatabaseConnection, IDbTransaction Transaction)
        {
            IDbCommand dbCommit = DatabaseConnection.CreateCommand();
            dbCommit.Transaction = Transaction;
            dbCommit.CommandText = "INSERT INTO Payments (CommitAction, BuyerID, PaymentIndex, PaymentMethod, CheckNumber, Amount, Comments) VALUES (@CommitAction, @BuyerID, @PaymentIndex, @PaymentMethod, @CheckNumber, @Amount, @Comments)";

            IDbDataParameter param = dbCommit.CreateParameter();
            param.ParameterName = "@CommitAction";
            param.Value = (int)Action;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@BuyerID";
            param.Value = this.BuyerNumber;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@PaymentIndex";
            param.Value = this.Index;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@PaymentMethod";
            param.Value = this.Method;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@CheckNumber";
            param.Value = this.CheckNumber;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Amount";
            param.Value = this.Amount;
            dbCommit.Parameters.Add(param);

            param = dbCommit.CreateParameter();
            param.ParameterName = "@Comments";
            param.Value = this.Comments;
            dbCommit.Parameters.Add(param);

            dbCommit.ExecuteNonQuery();
        }

        public static bool operator ==(clsPayment Payment1, clsPayment Payment2)
        {
            if ((object)Payment1 == null && (object)Payment2 == null)
            {
                return true;
            }
            else if ((object)Payment1 == null || (object)Payment2 == null)
            {
                return false;
            }
            else
            {
                return (Payment1.CompareTo(Payment2) == 0);
            }
        }
        public static bool operator !=(clsPayment Payment1, clsPayment Payment2)
        {
            return !(Payment1 == Payment2);
        }

        #region IComparable<clsExhibitor> Members

        public int CompareTo(clsPayment other)
        {
            return this.BuyerNumber.CompareTo(other.BuyerNumber);
        }

        #endregion

        #region ISerializable Members
        protected clsPayment(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            iBuyerNum = (int)info.GetValue("iBuyerNum", typeof(int));
            cBuyer = (clsBuyer)info.GetValue("cBuyer", typeof(clsBuyer));
            iPaymentIndex = (int)info.GetValue("iPaymentIndex", typeof(int));
            eMethod = (PaymentMethod)info.GetValue("eMethod", typeof(PaymentMethod));
            iCheckNumber = (int)info.GetValue("iCheckNumber", typeof(int));
            fAmount = (double)info.GetValue("fAmount", typeof(double));
            sComments = (string)info.GetValue("sComments", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            info.AddValue("iBuyerNum", iBuyerNum);
            info.AddValue("cBuyer", cBuyer);
            info.AddValue("iPaymentIndex", iPaymentIndex);
            info.AddValue("eMethod", eMethod);
            info.AddValue("iCheckNumber", iCheckNumber);
            info.AddValue("fAmount", fAmount);
            info.AddValue("sComments", sComments);
        }
        #endregion

        public int BuyerNumber
        {
            get
            {
                return iBuyerNum;
            }
        }

        public clsBuyer Buyer
        {
            get
            {
                if (clsDB.Buyers != null)
                {
                    return clsDB.Buyers[iBuyerNum];
                }
                else
                {
                    return cBuyer;
                }
            }
        }

        public int Index
        {
            get
            {
                return iPaymentIndex;
            }
        }

        public PaymentMethod Method
        {
            get
            {
                return eMethod;
            }
            set
            {
                eMethod = value;
            }
        }
        public string Method_String
        {
            get
            {
                if (eMethod == PaymentMethod.Cash)
                {
                    return "Cash";
                }
                else if (eMethod == PaymentMethod.Check)
                {
					return string.Format("Check (#{0})", iCheckNumber);
                }
                else if (eMethod == PaymentMethod.Credit)
                {
                    //return string.Format("Credit (${0:0.00} + ${1:0.00})", fAmount, this.Surcharge);
                    return "Credit Card";
                }
                else
                {
                    return "";
                }
            }
        }
        public string Fee_String
        {
            get
            {
                if (eMethod == PaymentMethod.Credit)
                {
                    return String.Format("Credit Card Fee ({0:0.00}% of ${1:#,##0.00})", CREDIT_CARD_FEE * 100, fAmount);
                }
                else
                {
                    return "";
                }
            }
        }
        public int CheckNumber
        {
            get
            {
                return iCheckNumber;
            }
            set
            {
                iCheckNumber = value;
            }
        }

        public double Amount
        {
            get
            {
                return fAmount;
            }
            set
            {
                fAmount = value;
            }
        }

        public double Surcharge
        {
            get
            {
                if (eMethod == PaymentMethod.Credit)
                {

                    return Math.Round(fAmount * CREDIT_CARD_FEE, 2);// Math.Round((fAmount * (1 / (1 - 0.0275))) - fAmount, 2);
                }
                else
                {
                    return 0;
                }
            }
        }

        public string Comments
        {
            get
            {
                return sComments;
            }
            set
            {
                sComments = value;
            }
        }
    }

    namespace Setup
    {
        public class Payments : AuctionDBSetup
        {
            public Payments()
            {
                TABLE_NAME = "Payments";

                TABLE_COLUMNS = new List<SQLColumn>() {
                    new SQLColumn("BuyerID", "int", "0", true),
	                new SQLColumn("PaymentIndex", "int", "0", true),
	                new SQLColumn("PaymentMethod", "int", "0", false),	/*Pay now (cash or check) or Mail bill*/
	                new SQLColumn("CheckNumber", "int", "0", false),
	                new SQLColumn("Amount", "Money", "0", false),
	                new SQLColumn("Comments", "nvarchar(1000)", "0", false)
                };
            }
        }
    }
}
