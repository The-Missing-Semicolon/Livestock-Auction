import struct

TotalRecords = 0

Records = []

ByZipCode = {}
ByStateCity = {}
CityDataFile = open("CityStateZipData.csv", 'r')
CityDataFile.seek(3, 0)	#Skip SQL Server's BOM...
structure = "4s28sI"
print struct.calcsize(structure)

blah = False

#Read in the records
for line in CityDataFile:
	data = line.strip().split(',')
	Records.append(struct.pack(structure, data[0], data[1], int(data[2])))
CityDataFile.close()

#Sort by State/City
Records.sort(key=lambda x: struct.unpack(structure, x)[0:2])
SortedData = open("DB\\CityStateZipDataByStateCity.bin", 'wb')
SortedData.write(struct.pack("I", len(Records)))
for record in Records:
	SortedData.write(record)
SortedData.close()

#Sort by Zip
Records.sort(key=lambda x: struct.unpack(structure, x)[2])
SortedData = open("DB\\CityStateZipDataByZip.bin", 'wb')
SortedData.write(struct.pack("I", len(Records)))
for record in Records:
	SortedData.write(record)
SortedData.close()