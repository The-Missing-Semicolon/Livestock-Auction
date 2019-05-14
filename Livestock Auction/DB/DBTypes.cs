using System;
using System.Runtime.Serialization;
namespace Livestock_Auction.DB.Types
{
    //Represents an address with a Street, City, State, and Zip code.
    //  IComparable for sorting and ISerializable for compatability for with
    //  reporting.
    [Serializable]
    public class Address : IComparable<Address>, ISerializable 
    {
        private string sStreet = "";
        private string sCity = "";
        private string sState = "";
        private int iZipCode = 0;

        public Address()
        {
        }

        protected Address(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            sStreet = (string)info.GetValue("sStreet", typeof(string));
            sCity = (string)info.GetValue("sCity", typeof(string));
            sState = (string)info.GetValue("sState", typeof(string));
            iZipCode = (int)info.GetValue("iZipCode", typeof(int));
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, Flags = System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter)]
        public virtual void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            info.AddValue("sStreet", sStreet);
            info.AddValue("sCity", sCity);
            info.AddValue("sState", sState);
            info.AddValue("iZipCode", iZipCode);
        }

        public static bool operator ==(Address Addr1, Address Addr2)
        {
            
            if (ReferenceEquals(Addr1, null) && ReferenceEquals(Addr2, null))
            {
                return true;
            }
            else if (ReferenceEquals(Addr1, null) != ReferenceEquals(Addr2, null))
            {
                return false;
            }
            else
            {
                return Addr1.sStreet == Addr2.sStreet && Addr1.sCity == Addr2.sCity && Addr1.sState == Addr2.sState && Addr1.iZipCode == Addr2.iZipCode;
            }
        }

        public static bool operator !=(Address Addr1, Address Addr2)
        {
            if (ReferenceEquals(Addr1, null) && ReferenceEquals(Addr2, null))
            {
                return false;
            }
            else if (ReferenceEquals(Addr1, null) != ReferenceEquals(Addr2, null))
            {
                return true;
            }
            else
            {
                return Addr1.sStreet != Addr2.sStreet || Addr1.sCity != Addr2.sCity || Addr1.sState != Addr2.sState || Addr1.iZipCode != Addr2.iZipCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Address)
            {
                return (Address)obj == this;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(Address other)
        {
            int Ans = this.State.CompareTo(other.State);
            if (Ans == 0)
            {
                Ans = this.City.CompareTo(other.City);
                if (Ans == 0)
                {
                    return this.Street.CompareTo(other.Street);
                }
            }
            return Ans;
        }

        public override string ToString()
        {
            return this.Street + (this.Street.Trim().Length > 0 && this.City.Trim().Length > 0 ? ", " : "") + this.City + (this.City.Trim().Length > 0 && this.State.Trim().Length > 0 ? ", " : "") + this.State + " " + (this.Zip > 0 ? this.Zip.ToString("00000") : "");
        }

        public string Street
        {
            get
            {
                return sStreet;
            }
            set
            {
                sStreet = value;
            }
        }
        public string City
        {
            get
            {
                return sCity;
            }
            set
            {
                sCity = value;
            }
        }
        public string State
        {
            get
            {
                return sState;
            }
            set
            {
                sState = value;
            }
        }
        public int Zip
        {
            get
            {
                return iZipCode;
            }
            set
            {
                iZipCode = value;
            }
        }
    }

    //Represents a person's first/last name with an optional "nick" name.
    //  IComparable for sorting and ISerializable for compatability for with
    //  reporting.
    [Serializable]
    public class Name : IComparable<Name>, ISerializable
    {
        public string sFirst = "";
        public string sLast = "";
        public string sNick = "";

        public Name()
        {
        }

        protected Name(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            sFirst = (string)info.GetValue("sFirst", typeof(string));
            sLast = (string)info.GetValue("sLast", typeof(string));
            sNick = (string)info.GetValue("sNick", typeof(string));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new System.ArgumentNullException("info");
            }
            info.AddValue("sFirst", sFirst);
            info.AddValue("sLast", sLast);
            info.AddValue("sNick", sNick);
        }

        public static bool operator ==(Name Name1, Name Name2)
        {
            if (ReferenceEquals(Name1, null) && ReferenceEquals(Name2, null))
            {
                return true;
            }
            else if (ReferenceEquals(Name1, null) != ReferenceEquals(Name2, null))
            {
                return false;
            }
            else
            {
                return Name1.sFirst == Name2.sFirst && Name1.sLast == Name2.sLast;
            }
        }

        public static bool operator !=(Name Name1, Name Name2)
        {
            if (ReferenceEquals(Name1, null) && ReferenceEquals(Name2, null))
            {
                return false;
            }
            else if (ReferenceEquals(Name1, null) != ReferenceEquals(Name2, null))
            {
                return true;
            }
            else
            {
                return Name1.sFirst != Name2.sFirst || Name1.sLast != Name2.sLast;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Name)
            {
                return (Name)obj == this;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(Name other)
        {
            /*int Ans = this.First.CompareTo(other.First);
            if (Ans == 0)
            {
                Ans = this.Last.CompareTo(other.Last);
                if (Ans == 0)
                {
                    return this.Nick.CompareTo(other.Nick);
                }
            }*/
            int Ans = this.Last.CompareTo(other.Last);
            if (Ans == 0)
            {
                Ans = this.Nick.CompareTo(other.Nick);
                if (Ans == 0)
                {
                    return this.First.CompareTo(other.First);
                }
            }
            return Ans;
        }

        public override string ToString()
        {
            return string.Format(sNick != null && sNick.Trim().Length > 0 ? "{0} \"{2}\" {1}" : "{0} {1}", sFirst, sLast, sNick);
        }

        public string First
        {
            get
            {
                return sFirst;
            }
            set
            {
                sFirst = value;
            }
        }

        public string Last
        {
            get
            {
                return sLast;
            }
            set
            {
                sLast = value;
            }
        }

        public string Nick
        {
            get
            {
                return sNick;
            }
            set
            {
                sNick = value;
            }
        }
    }
}