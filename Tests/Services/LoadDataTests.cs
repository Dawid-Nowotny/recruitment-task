using RecruitmentTask.Exceptions;
using RecruitmentTask.Services;

namespace RecruitmentTaskTests.Services
{
	public class LoadDataTests
	{
		private readonly HotelService _hotelService;
		public LoadDataTests()
		{
			_hotelService = new HotelService();
		}

		[Fact]
		public void LoadData_JsonValid()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string dataDir = Path.Combine(basePath, "..", "..", "..", "Services", "Data");
			string hotelsFilePath = Path.Combine(dataDir, "hotels.json");
			string bookingsFilePath = Path.Combine(dataDir, "bookings.json");

			var hotelService = new HotelService(hotelsFilePath, bookingsFilePath);

			bool result = hotelService.LoadData(out var hotels, out var bookings);

			Assert.True(result);
			Assert.NotNull(hotels);
			Assert.NotNull(bookings);

			var hotel = hotels[0];
			Assert.Equal("H1", hotel.Id);
			Assert.Equal("Hotel California", hotel.Name);
			Assert.Equal(2, hotel.RoomTypes.Count);
			Assert.Contains(hotel.RoomTypes, rt => rt.Code == "SGL" && rt.Description == "Single Room");
			Assert.Contains(hotel.RoomTypes, rt => rt.Code == "DBL" && rt.Description == "Double Room");

			var booking = bookings[0];
			Assert.Equal("H1", booking.HotelId);
			Assert.Equal(new DateTime(2024, 9, 1), booking.Arrival);
			Assert.Equal(new DateTime(2024, 9, 3), booking.Departure);
			Assert.Equal("DBL", booking.RoomType);
			Assert.Equal("Prepaid", booking.RoomRate);
		}

		[Fact]
		public void LoadData_FileException()
		{
			var hotelService = new HotelService();

			var exception = Assert.Throws<DataLoadException>(() =>
			{
				hotelService.LoadData(out var _, out var _);
			});

			Assert.Contains("An unexpected error occurred while loading data.", exception.Message);
		}

		[Fact]
		public void LoadData_FileNotFound()
		{
			string invalidHotelsFilePath = "hotels1.json";
			string invalidBookingsFilePath = "booking1.json";
			var hotelService = new HotelService(invalidHotelsFilePath, invalidBookingsFilePath);

			var exception = Assert.Throws<DataLoadException>(() =>
			{
				hotelService.LoadData(out var _, out var _);
			});

			Assert.Contains("One or more data files were not found.", exception.Message);
		}

		[Fact]
		public void LoadData_InvalidJson()
		{
			string basePath = AppDomain.CurrentDomain.BaseDirectory;
			string dataDir = Path.Combine(basePath, "..", "..", "..", "Services", "Data");
			string invalidHotelsFilePath = Path.Combine(dataDir, "invalid_hotels.json");
			string invalidBookingsFilePath = Path.Combine(dataDir, "invalid_bookings.json");

			var hotelService = new HotelService(invalidHotelsFilePath, invalidBookingsFilePath);

			var exception = Assert.Throws<DataLoadException>(() =>
			{
				hotelService.LoadData(out var _, out var _);
			});

			Assert.Contains("Error parsing JSON data.", exception.Message);
		}
	}
}
