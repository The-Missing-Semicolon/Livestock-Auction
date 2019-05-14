using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using System.Runtime.InteropServices;

namespace Livestock_Auction.Helpers.Paradox
{
    namespace RawStruct
    {
        public static class Constants
        {
            public const int SIZEOF_FILEHEADER = 120;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct FileHeader
        {
            [FieldOffset(0x00)] public UInt16 RecordSize;
            [FieldOffset(0x02)] public UInt16 HeaderSize;
            [FieldOffset(0x04)] public Byte FileType;
            [FieldOffset(0x05)] public Byte MaxTableSize;
            [FieldOffset(0x08)] public UInt32 RecordCount;
            [FieldOffset(0x0A)] public UInt16 NextBlock;
            [FieldOffset(0x0C)] public UInt16 FileBlocks;
            [FieldOffset(0x0E)] public UInt16 FirstBlock;
            [FieldOffset(0x10)] public UInt16 LastBlock;
            [FieldOffset(0x21)] public Byte FieldCount;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct ColumnHeader
        {
            [FieldOffset(0x00)] public Byte Type;
            [FieldOffset(0x01)] public Byte Length;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        public struct BlockHeader
        {
            [FieldOffset(0x02)] public UInt16 BlockNumber;
            [FieldOffset(0x04)] public UInt16 AddDataSize;
        }
    }

    namespace Types
    {
        public class ParadoxField
        {
            protected string Value_String;
            protected DateTime Value_Date;
            protected Int16 Value_Short;
            protected Int32 Value_Long;
            protected double Value_Currency;
            protected bool Value_Boolean;

            protected bool ValueNull = false;

            public ParadoxField(Byte[] RowBytes, UInt16 FieldOffset, UInt16 FieldLength)
            {
                ValueNull = true;
            }

            public virtual object Value()
            {
                return null;
            }
            public virtual string ValueAsString()
            {
                throw new InvalidCastException();
            }
            public virtual DateTime ValueAsDate()
            {
                throw new InvalidCastException();
            }
            public virtual Int16 ValueAsShort()
            {
                throw new InvalidCastException();
            }
            public virtual Int32 ValueAsLong()
            {
                throw new InvalidCastException();
            }
            public virtual double ValueAsCurrency()
            {
                throw new InvalidCastException();
            }
            public virtual bool ValueAsBoolean()
            {
                throw new InvalidCastException();
            }
            public bool ValueIsNull()
            {
                return ValueNull;
            }

            public override string ToString()
            {
                return this.Value().ToString();
            }

            public static ParadoxField DecodeField(Byte ColumnType, Byte[] RowBytes, UInt16 Offset, UInt16 Length)
            {
                switch (ColumnType)
                {
                    case 0x1:
                        return new ParadoxString(RowBytes, Offset, Length);
                    case 0x2:
                        return new ParadoxDate(RowBytes, Offset, Length);
                    case 0x3:
                        return new ParadoxShort(RowBytes, Offset, Length);
                    case 0x4:
                        return new ParadoxLong(RowBytes, Offset, Length);
                    case 0x5:
                        return new ParadoxCurrency(RowBytes, Offset, Length);
                    case 0x9:
                        return new ParadoxBoolean(RowBytes, Offset, Length);
                    case 0x0C: // Memo blob
                        return new ParadoxField(RowBytes, Offset, Length);
                    case 0x15: // Timestamp
                        return new ParadoxField(RowBytes, Offset, Length);
                    case 0x16: // Auto-increment
                        return new ParadoxLong(RowBytes, Offset, Length);
                    default:
                        throw new InvalidDataException("Unsupported column type");
                }
            }
        }

        public class ParadoxString : ParadoxField
        {
            public ParadoxString(Byte[] RowBytes, UInt16 FieldOffset, UInt16 FieldLength) : base(RowBytes, FieldOffset, FieldLength)
            {
                //Decode the string and strip off null characters
                Value_String = Encoding.ASCII.GetString(RowBytes, FieldOffset, FieldLength);
                Value_String = Value_String.TrimEnd('\x00');
            }

            public override object Value()
            {
                return Value_String;
            }
            public override string ValueAsString()
            {
                return Value_String;
            }
        }

        public class ParadoxDate : ParadoxField
        {
            public ParadoxDate(Byte[] RowBytes, UInt16 FieldOffset, UInt16 FieldLength) : base(RowBytes, FieldOffset, FieldLength)
            {
                if (RowBytes[FieldOffset] == 0 && RowBytes[FieldOffset + 1] == 0 && RowBytes[FieldOffset + 2] == 0 && RowBytes[FieldOffset + 3] == 0)
                {
                    ValueNull = true;
                    Value_Date = DateTime.MinValue;
                }
                else
                {
                    Byte[] FieldBytes = new Byte[FieldLength];
                    Array.Copy(RowBytes, FieldOffset, FieldBytes, 0, FieldLength);
                    FieldBytes[0] = (Byte)(FieldBytes[0] & 0x7F);
                    Array.Reverse(FieldBytes);  //Reverse Endienness
                    UInt32 iDateOffset = BitConverter.ToUInt32(FieldBytes, FieldOffset);

                    Value_Date = new DateTime(1, 1, 1);
                    Value_Date.AddDays(iDateOffset);
                }
            }

            public override object Value()
            {
                return Value_Date;
            }
            public DateTime ValueAsDate()
            {
                return Value_Date;
            }
        }

        public class ParadoxShort : ParadoxField
        {
            public ParadoxShort(Byte[] RowBytes, UInt16 FieldOffset, UInt16 FieldLength) : base(RowBytes, FieldOffset, FieldLength)
            {
                if (RowBytes[FieldOffset] == 0 && RowBytes[FieldOffset + 1] == 0)
                {
                    ValueNull = true;
                }
                else
                {
                    Byte[] FieldBytes = new Byte[FieldLength];
                    Array.Copy(RowBytes, FieldOffset, FieldBytes, 0, FieldLength);
                    if ((FieldBytes[0] & 0x80) > 0)
                    {
                        //Positive Integer
                        FieldBytes[0] = (Byte)(FieldBytes[0] & 0x7F);
                        Array.Reverse(FieldBytes);  //Reverse Endienness
                        Value_Short = (Int16)(BitConverter.ToUInt16(FieldBytes, 0));
                    }
                    else
                    {
                        //Negative Integer
                        Array.Reverse(FieldBytes);  //Reverse Endienness
                        Value_Short = (Int16)(0 - (Int16)(BitConverter.ToUInt16(FieldBytes, 0)));
                    }
                }
            }
            
            public override object Value()
            {
                return Value_Short;
            }
            public override Int16 ValueAsShort()
            {
                return Value_Short;
            }
        }

        public class ParadoxLong : ParadoxField
        {
            public ParadoxLong(Byte[] RowBytes, UInt16 FieldOffset, UInt16 FieldLength) : base(RowBytes, FieldOffset, FieldLength)
            {
                if (RowBytes[FieldOffset] == 0 && RowBytes[FieldOffset + 1] == 0 && RowBytes[FieldOffset + 2] == 0 && RowBytes[FieldOffset + 3] == 0)
                {
                    ValueNull = true;
                }
                else
                {
                    Byte[] FieldBytes = new Byte[FieldLength];
                    Array.Copy(RowBytes, FieldOffset, FieldBytes, 0, FieldLength);
                    if ((FieldBytes[0] & 0x80) > 0)
                    {
                        //Positive Integer
                        FieldBytes[0] = (Byte)(FieldBytes[0] & 0x7F);
                        Array.Reverse(FieldBytes);  //Reverse Endienness
                        Value_Long = (Int32)(BitConverter.ToUInt32(FieldBytes, 0));
                    }
                    else
                    {
                        //Negative Integer
                        Array.Reverse(FieldBytes);  //Reverse Endienness
                        Value_Long = 0 - (Int32)(BitConverter.ToUInt32(FieldBytes, 0));
                    }
                }
            }

            public override object Value()
            {
                return Value_Long;
            }
            public override Int32 ValueAsLong()
            {
                return Value_Long;
            }
        }

        public class ParadoxCurrency : ParadoxField
        {
            public ParadoxCurrency(Byte[] RowBytes, UInt16 FieldOffset, UInt16 FieldLength) : base(RowBytes, FieldOffset, FieldLength)
            {
                if (RowBytes[FieldOffset] == 0 && RowBytes[FieldOffset + 1] == 0 && RowBytes[FieldOffset + 2] == 0 && RowBytes[FieldOffset + 3] == 0 && RowBytes[FieldOffset + 4] == 0 && RowBytes[FieldOffset + 5] == 0 && RowBytes[FieldOffset + 6] == 0 && RowBytes[FieldOffset + 7] == 0)
                {
                    ValueNull = true;
                }
                else
                {
                    Byte[] FieldBytes = new Byte[FieldLength];
                    Array.Copy(RowBytes, FieldOffset, FieldBytes, 0, FieldLength);
                    FieldBytes[0] = (Byte)(FieldBytes[0] & 0x7F);
                    Value_Currency = BitConverter.ToDouble(FieldBytes, FieldOffset);
                }
            }

            public override object Value()
            {
                return Value_Currency;
            }
            public override double ValueAsCurrency()
            {
                return Value_Currency;
            }
        }

        public class ParadoxBoolean : ParadoxField
        {
            public ParadoxBoolean(Byte[] RowBytes, UInt16 FieldOffset, UInt16 FieldLength) : base(RowBytes, FieldOffset, FieldLength)
            {
                if (RowBytes[FieldOffset] == 0)
                {
                    ValueNull = true;
                }
                else
                {
                    if ((RowBytes[FieldOffset] & 0x7F) > 0)
                    {
                        Value_Boolean = true;
                    }
                    else
                    {
                        Value_Boolean = false;
                    }
                }
            }

            public override object Value()
            {
                return Value_Boolean;
            }
            public override bool  ValueAsBoolean()
            {
                return Value_Boolean;
            }
        }
    }

    public class FileHeader
    {
        RawStruct.FileHeader dbHeader;

        public FileHeader(byte[] HeaderBytes)
        {
            GCHandle handle = GCHandle.Alloc(HeaderBytes, GCHandleType.Pinned);
            dbHeader = (RawStruct.FileHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(RawStruct.FileHeader));
        }

        public UInt16 RecordSize
        {
            get
            {
                return dbHeader.RecordSize;
            }
        }
        public UInt16 HeaderSize
        {
            get
            {
                return dbHeader.HeaderSize;
            }
        }
        public Byte FileType
        {
            get
            {
                return dbHeader.FileType;
            }
        }
        public Byte MaxTableSize
        {
            get
            {
                return dbHeader.MaxTableSize;
            }
        }
        public UInt32 RecordCount
        {
            get
            {
                return dbHeader.RecordCount;
            }
        }
        public UInt16 NextBlock
        {
            get
            {
                return dbHeader.NextBlock;
            }
        }
        public UInt16 FileBlocks
        {
            get
            {
                return dbHeader.FileBlocks;
            }
        }
        public UInt16 FirstBlock
        {
            get
            {
                return dbHeader.FirstBlock;
            }
        }
        public UInt16 LastBlock
        {
            get
            {
                return dbHeader.LastBlock;
            }
        }
        public Byte FieldCount
        {
            get
            {
                return dbHeader.FieldCount;
            }
        }
        public int BlockSize
        {
            get
            {
                if (MaxTableSize == 1)
                {
                    return 0x0400;
                }
                else if (MaxTableSize == 2)
                {
                    return 0x0800;
                }
                else if (MaxTableSize == 3)
                {
                    return 0x0C00;
                }
                else if (MaxTableSize == 4)
                {
                    return 0x1000;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    public class TableHeader
    {
        public class ColumnHeader
        {
            int iIndex;
            UInt16 iAddress;
            RawStruct.ColumnHeader rawHeader;
            string sName;

            public ColumnHeader(int Index, UInt16 ColumnAddress, RawStruct.ColumnHeader RawColumnHeader, string ColumnName)
            {
                iIndex = Index;
                iAddress = ColumnAddress;
                rawHeader = RawColumnHeader;
                sName = ColumnName;
            }

            public int ColumnIndex
            {
                get
                {
                    return iIndex;
                }
            }

            public UInt16 ColumnAddress
            {
                get
                {
                    return iAddress;
                }
            }

            public Byte ColumnType
            {
                get
                {
                    return rawHeader.Type;
                }
            }
            public Byte ColumnSize
            {
                get
                {
                    return rawHeader.Length;
                }
            }
            public string ColumnName
            {
                get
                {
                    return sName;
                }
            }
        }

        public class ColumnSet
        {
            RawStruct.ColumnHeader[] rawHeaders = null;
            UInt16[] iColumnAddresses = null;
            string[] sColumnNames = null;

            public ColumnSet(RawStruct.ColumnHeader[] RawHeaders, UInt16[] ColumnAddresses, string[] Names)
            {
                rawHeaders = RawHeaders;
                sColumnNames = Names;
                iColumnAddresses = ColumnAddresses;
            }

            public ColumnHeader this[int Index]
            {
                get
                {
                    return new ColumnHeader(Index, iColumnAddresses[Index], rawHeaders[Index], sColumnNames[Index]);
                }
            }

            public ColumnHeader this[string Name]
            {
                get
                {
                    for (int i = 0; i < sColumnNames.Length; i++)
                    {
                        if (sColumnNames[i].Trim().ToLower() == Name.Trim().ToLower())
                        {
                            return new ColumnHeader(i, iColumnAddresses[i], rawHeaders[i], sColumnNames[i]);
                        }
                    }
                    throw new IndexOutOfRangeException("This column does not exist");
                }
            }


        }

        RawStruct.ColumnHeader[] rawColumnHeaders = null;
        UInt16[] iColumnAddrs = null;
        string sTableName;
        string[] sColumnNames = null;

        public TableHeader(FileHeader FileHeader, byte[] TableHeaderBytes)
        {
            //Read the column headers (the type and length). Also calculate the field addresses. Column names will come further down
            rawColumnHeaders = new RawStruct.ColumnHeader[FileHeader.FieldCount];
            iColumnAddrs = new UInt16[FileHeader.FieldCount];
            for (int i = 0; i < FileHeader.FieldCount; i++)
            {
                byte[] bColHeader = new byte[Marshal.SizeOf(typeof(RawStruct.ColumnHeader))];
                Array.Copy(TableHeaderBytes, i * Marshal.SizeOf(typeof(RawStruct.ColumnHeader)), bColHeader, 0, Marshal.SizeOf(typeof(RawStruct.ColumnHeader)));
                GCHandle handle = GCHandle.Alloc(bColHeader, GCHandleType.Pinned);
                rawColumnHeaders[i] = (RawStruct.ColumnHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(RawStruct.ColumnHeader));
                //Calculate the address
                iColumnAddrs[i] = (i == 0) ? (UInt16)0 : (UInt16)(iColumnAddrs[i - 1] + (rawColumnHeaders[i - 1].Length));
            }

            //Get a string containing the table name and the column names
            int iTableNameIndex = FileHeader.FieldCount * (Marshal.SizeOf(typeof(RawStruct.ColumnHeader)) + 4) + 4;
            string sNames = Encoding.ASCII.GetString(TableHeaderBytes, iTableNameIndex, FileHeader.HeaderSize - (iTableNameIndex + RawStruct.Constants.SIZEOF_FILEHEADER));

            //Read the table name (the first entry in the string)
            sTableName = sNames.Substring(0, sNames.IndexOf('\x00'));

            //Read the column names
            sNames = sNames.Substring(sNames.IndexOf('\x00'));
            sNames = sNames.TrimStart('\x00');
            sColumnNames = new string[FileHeader.FieldCount];

            for (int i = 0; i < FileHeader.FieldCount; i++)
            {
                sColumnNames[i] = sNames.Substring(0, sNames.IndexOf('\x00'));
                System.Diagnostics.Debug.WriteLine("Column: " + sColumnNames[i]);
                sNames = sNames.Substring(sNames.IndexOf('\x00') + 1);
            }
        }

        public string TableName
        {
            get
            {
                return sTableName;
            }
        }

        public ColumnSet Columns
        {
            get
            {
                return new ColumnSet(rawColumnHeaders, iColumnAddrs, sColumnNames);
            }
        }

    }

    public class Block
    {
        RawStruct.BlockHeader dbBlockHeader;
        TableHeader.ColumnSet ColumnSet;
        int iRecordCount;
        int iRecordSize;
        byte[] rawBlock;

        public Block(UInt16 RecordSize, TableHeader.ColumnSet Columns, byte[] BlockBytes)
        {
            iRecordSize = RecordSize;
            ColumnSet = Columns;
            GCHandle handle = GCHandle.Alloc(BlockBytes, GCHandleType.Pinned);
            dbBlockHeader = (RawStruct.BlockHeader)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(RawStruct.BlockHeader));

            rawBlock = BlockBytes;
            if (dbBlockHeader.AddDataSize + RecordSize < BlockBytes.Length)
            {
                iRecordCount = (dbBlockHeader.AddDataSize / iRecordSize) + 1;
            }
            else
            {
                //TODO: The 2012 exhibitors file seems to have some sort of problem
                iRecordCount = (BlockBytes.Length / iRecordSize);
            }
        }

        public Record GetRow(int RowNumber)
        {
            if (RowNumber < iRecordCount)
            {
                byte[] bRowBytes = new byte[iRecordSize];
                Array.Copy(rawBlock, Marshal.SizeOf(typeof(RawStruct.BlockHeader)) + (RowNumber * iRecordSize), bRowBytes, 0, iRecordSize);

                return new Record(ColumnSet, bRowBytes);
            }
            else
            {
                throw new IndexOutOfRangeException("Row number out of range");
            }
        }
    }

    public class Record
    {
        TableHeader.ColumnSet ColumnSet;
        byte[] bRowBytes;

        public Record(TableHeader.ColumnSet Columns, byte[] RecordBytes)
        {
            ColumnSet = Columns;
            bRowBytes = RecordBytes;
        }

        public Types.ParadoxField this[TableHeader.ColumnHeader Column]
        {
            get
            {
                return Types.ParadoxField.DecodeField(Column.ColumnType, bRowBytes, Column.ColumnAddress, Column.ColumnSize);
            }
        }

        public Types.ParadoxField this[int Index]
        {
            get
            {
                return this[ColumnSet[Index]];
            }
        }

        public Types.ParadoxField this[string Name]
        {
            get
            {
                return this[ColumnSet[Name]];
            }
        }
    }

    public class RecordSet : IEnumerable<Record>
    {
        FileHeader dbFile;
        TableHeader dbTable;
        Stream file;

        public RecordSet(FileHeader FileInfo, TableHeader Table, Stream FileData)
        {
            dbFile = FileInfo;
            dbTable = Table;
            file = FileData;
        }

        #region IEnumerable<Record> Members

        public IEnumerator<Record> GetEnumerator()
        {
            return new RecordSetEnumerator(dbFile, dbTable, file);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new RecordSetEnumerator(dbFile, dbTable, file);
        }

        #endregion

        public UInt32 Count
        {
            get
            {
                return dbFile.RecordCount;
            }
        }
    }

    public class RecordSetEnumerator : IEnumerator<Record>
    {
        TableHeader dbTable;
        FileHeader dbFile;
        Stream fileStream;
        int iRowIndex = 0;
        int iRowsRead = 0;

        Block CurrentBlock = null;
        Record CurrentRow = null;

        public RecordSetEnumerator(FileHeader FileInfo, TableHeader Table, Stream FileData)
        {
            dbFile = FileInfo;
            dbTable = Table;
            fileStream = FileData;
            fileStream.Seek(dbFile.HeaderSize, SeekOrigin.Begin);
            GetNextBlock();
        }

        private bool GetNextBlock()
        {
            byte[] BlockBytes = new byte[dbFile.BlockSize];
            if (fileStream.Read(BlockBytes, 0, dbFile.BlockSize) == dbFile.BlockSize)
            {
                System.Diagnostics.Debug.Print("Read block {0}", fileStream.Position);
                CurrentBlock = new Block(dbFile.RecordSize, dbTable.Columns, BlockBytes);
                return true;
            }
            else
            {
                System.Diagnostics.Debug.Print("Read {0} out of {0} records", iRowsRead, dbFile.RecordCount);
                return false;
            }
        }

        #region IEnumerator<Record> Members

        public Record Current
        {
            get { return CurrentRow; }
        }

        object System.Collections.IEnumerator.Current
        {
            get { return CurrentRow; }
        }

        public bool MoveNext()
        {
            try
            {
                CurrentRow = CurrentBlock.GetRow(iRowIndex);
                iRowsRead++;
                iRowIndex++;
                return true;
            }
            catch (IndexOutOfRangeException ex)
            {
                iRowIndex = 0;
                if (GetNextBlock())
                {
                    CurrentRow = CurrentBlock.GetRow(iRowIndex);
                    iRowsRead++;
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
        }

        public void Reset()
        {
            fileStream.Seek(dbFile.HeaderSize, SeekOrigin.Begin);
        }

        public void Dispose()
        {
            return;
        }

        #endregion
    }

    public class ParadoxFile
    {
        FileHeader dbFileHeader;
        TableHeader dbTableHeader;
        RecordSet dbRecords;

        public ParadoxFile(Stream File)
        {
            //Read the file header
            byte[] FileHeaderBytes = new byte[Marshal.SizeOf(typeof(RawStruct.FileHeader))];
            if (File.Read(FileHeaderBytes, 0, Marshal.SizeOf(typeof(RawStruct.FileHeader))) == Marshal.SizeOf(typeof(RawStruct.FileHeader)))
            {
                dbFileHeader = new FileHeader(FileHeaderBytes);
            }
            else
            {
                throw new EndOfStreamException("Unexpected end of file while reading the file header");
            }

            //Read the table header
            byte[] TableHeaderBytes = new byte[dbFileHeader.HeaderSize - RawStruct.Constants.SIZEOF_FILEHEADER];
            File.Seek(RawStruct.Constants.SIZEOF_FILEHEADER, SeekOrigin.Begin);
            if (File.Read(TableHeaderBytes, 0, dbFileHeader.HeaderSize - RawStruct.Constants.SIZEOF_FILEHEADER) == (dbFileHeader.HeaderSize - RawStruct.Constants.SIZEOF_FILEHEADER))
            {
                dbTableHeader = new TableHeader(dbFileHeader, TableHeaderBytes);
            }
            else
            {
                throw new EndOfStreamException("Unexpected end of file while reading the table header");
            }

            dbRecords = new RecordSet(dbFileHeader, dbTableHeader, File);
        }

        public string TableName
        {
            get
            {
                return dbTableHeader.TableName;
            }
        }


        public TableHeader.ColumnSet Columns
        {
            get
            {
                return dbTableHeader.Columns;
            }
        }

        public RecordSet Rows
        {
            get
            {
                return dbRecords;
            }
        }
    }
}
