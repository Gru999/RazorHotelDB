using Microsoft.AspNetCore.DataProtection;
using RazorHotelDB.Models;
using RazorHotelDB.Services;
using System.Data.Common;

namespace HotelTest
{
    [TestClass]
    public class HotelServiceTest
    {
        //connectionString = Secret.ConnectionString;
        [TestMethod]
        public void TestAddHotel()
        {

            //Arrange
            HotelService hotelService = new HotelService(connectionString);
            List<Hotel> hotels = hotelService.GetAllHotelAsync().Result;

            //Act
            int numberOfHotels = hotels.Count;
            Hotel newhotel = new Hotel(99, "TestHotel", "TestVej");
            bool ok = hotelService.CreateHotelAsync(newhotel).Result;
            hotels = hotelService.GetAllHotelAsync().Result;

            int numberOfHotelsAfterTest = hotels.Count;
            Hotel h = hotelService.DeleteHotelAsync(newhotel.HotelNr).Result;

            //Assert
            Assert.AreEqual(numberOfHotels + 1, numberOfHotelsAfterTest);
            Assert.IsTrue(ok);
            Assert.AreEqual(h.HotelNr, newhotel.HotelNr);
        }
    }
}