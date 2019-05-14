using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Livestock_Auction_Tests
{
    [TestClass]
    public class GeoDatabaseTests
    {
        const string STATECITYZIP_CSV_PATH = "..\\..\\..\\..\\Livestock Auction\\CityStateZipData.csv";
        static Dictionary<string, HashSet<string>> dictCitiesByState = new Dictionary<string, HashSet<string>>();
        static Dictionary<Tuple<string, string>, List<string>> dictZipCodesByStateCity = new Dictionary<Tuple<string, string>, List<string>>();
        static Dictionary<int, Tuple<string, string>> dictStateCityByZipCode = new Dictionary<int, Tuple<string, string>>();

        [ClassInitialize]
        public static void InitalizeCityStateZipData(TestContext context)
        {
            //Read in the original CSV file and index it
            StreamReader srCityStateZipData = new StreamReader(STATECITYZIP_CSV_PATH);
            while (!srCityStateZipData.EndOfStream)
            {
                string[] sRecord = srCityStateZipData.ReadLine().Split(',');

                //Update the CitiesByState dictionary
                if (!dictCitiesByState.ContainsKey(sRecord[0]))
                {
                    dictCitiesByState[sRecord[0]] = new HashSet<string>();
                }
                dictCitiesByState[sRecord[0]].Add(sRecord[1]);

                //Update the ZipCodesByStateCity dictionary
                if (!dictZipCodesByStateCity.ContainsKey(new Tuple<string, string>(sRecord[0], sRecord[1])))
                {
                    dictZipCodesByStateCity[new Tuple<string, string>(sRecord[0], sRecord[1])] = new List<string>();
                }
                dictZipCodesByStateCity[new Tuple<string, string>(sRecord[0], sRecord[1])].Add(int.Parse(sRecord[2]).ToString("00000"));

                //Update the dictStateCityByZipCode dictionary
                dictStateCityByZipCode[int.Parse(sRecord[2])] = new Tuple<string, string>(sRecord[0], sRecord[1]);
            }

        }

        [TestMethod]
        public void FindCityByState()
        {
            //Ensure that all of the state/city pairs that appear in the csv file can be found in the resource
            foreach (KeyValuePair<string, HashSet<string>> StateCity in dictCitiesByState)
            {
                string[] sActualCities = Livestock_Auction.GeoDatabase.FindCityByState(StateCity.Key);
                string[] sExpectedCities = new string[StateCity.Value.Count];
                StateCity.Value.CopyTo(sExpectedCities);
                Array.Sort(sExpectedCities);

                Assert.AreEqual<int>(StateCity.Value.Count, sActualCities.Length, string.Format("Number of cities for {0} do not match", StateCity.Key));
                for (int i = 0; i < sActualCities.Length; i++ )
                {
                    Assert.AreEqual<string>(sExpectedCities[i], sActualCities[i], string.Format("Cities for {0} do not match", StateCity.Key));
                }
            }

            //Search for a non-existent state
            string[] sNonExistentCities = Livestock_Auction.GeoDatabase.FindCityByState("M0");
            Assert.AreEqual<int>(0, sNonExistentCities.Length, "Expected no cities for non-existent state");
        }

        [TestMethod]
        public void FindZipByStateCity()
        {
            //Ensure that all of the state/city pairs that appear in the csv file can be found in the resource
            foreach (KeyValuePair<Tuple<string, string>, List<string>> StateCityZip in dictZipCodesByStateCity)
            {
                string[] sActualZips = Livestock_Auction.GeoDatabase.FindZipByStateCity(StateCityZip.Key.Item1, StateCityZip.Key.Item2);
                string[] sExpectedZips = new string[StateCityZip.Value.Count];
                StateCityZip.Value.CopyTo(sExpectedZips);
                Array.Sort(sExpectedZips);


                Assert.AreEqual<int>(StateCityZip.Value.Count, sActualZips.Length, string.Format("Number of zip codes for {0},{1} do not match", StateCityZip.Key.Item2, StateCityZip.Key.Item1));
                for (int i = 0; i < sActualZips.Length; i++)
                {
                    Assert.AreEqual<string>(sExpectedZips[i], sActualZips[i], string.Format("Zip code for {0},{1} do not match", StateCityZip.Key.Item2, StateCityZip.Key.Item1));
                }
            }

            //Search for a non-existent state/city
            string[] sNonExistentCities = Livestock_Auction.GeoDatabase.FindZipByStateCity("M0", "BlahTown");
            Assert.AreEqual<int>(0, sNonExistentCities.Length, "Expected no cities for non-existent state");

            //Search for a non-existent city against all states
            foreach (KeyValuePair<string, HashSet<string>> StateCity in dictCitiesByState)
            {
                string[] sNonExistentZips = Livestock_Auction.GeoDatabase.FindZipByStateCity(StateCity.Key, "BlahTown");
                Assert.AreEqual<int>(0, sNonExistentZips.Length, string.Format("Expected no zip codes for non-existent city in state {0}", StateCity.Key));
            }
        }

        [TestMethod]
        public void FindStateByZip()
        {
            foreach(KeyValuePair<int, Tuple<string, string>> KeyValue in dictStateCityByZipCode)
            {
                string sExpectedState = KeyValue.Value.Item1;
                string sActualState = Livestock_Auction.GeoDatabase.FindStateByZip(KeyValue.Key);
                Assert.AreEqual<string>(sExpectedState, sActualState, string.Format("States did not match for zip code {0}", KeyValue.Key.ToString("00000")));
            }

            Assert.AreEqual<string>("", Livestock_Auction.GeoDatabase.FindStateByZip(0), "Expected no state for non-existent zip code 0");
            Assert.AreEqual<string>("", Livestock_Auction.GeoDatabase.FindStateByZip(10000), "Expected no state for non-existent zip code 10000");
            Assert.AreEqual<string>("", Livestock_Auction.GeoDatabase.FindStateByZip(99999), "Expected no state for non-existent zip code 99999");
        }

        [TestMethod]
        public void FindCityByZip()
        {
            foreach (KeyValuePair<int, Tuple<string, string>> KeyValue in dictStateCityByZipCode)
            {
                string sExpectedCity = KeyValue.Value.Item2;
                string sActualCity = Livestock_Auction.GeoDatabase.FindCityByZip(KeyValue.Key);
                Assert.AreEqual<string>(sExpectedCity, sActualCity, string.Format("Cities did not match for zip code {0}", KeyValue.Key.ToString("00000")));
            }

            Assert.AreEqual<string>("", Livestock_Auction.GeoDatabase.FindCityByZip(0), "Expected no city for non-existent zip code 0");
            Assert.AreEqual<string>("", Livestock_Auction.GeoDatabase.FindCityByZip(10000), "Expected no city for non-existent zip code 10000");
            Assert.AreEqual<string>("", Livestock_Auction.GeoDatabase.FindCityByZip(99999), "Expected no city for non-existent zip code 99999");
        }
    }
}
