using RecruitmentTask.DTOs;
using RecruitmentTask.Exceptions;
using RecruitmentTask.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentTaskTests.Services
{
	public class CheckAvailabilityTest
	{
		private readonly HotelService _hotelService;

		public CheckAvailabilityTest()
		{
			_hotelService = new HotelService();
		}

		[Fact]
		public void CheckAvailability_RoomAvailable()
		{
			var hotels = new List<HotelDto>
			{
				new HotelDto
				{
					Id = "H1",
					Name = "Hotel California",
					RoomTypes = new List<RoomTypeDto>
					{
						new RoomTypeDto { Code = "DBL", Description = "Double Room" }
					},
					Rooms = new List<RoomDto>
					{
						new RoomDto { RoomType = "DBL", RoomId = "201" }
					}
				}
			};

			var bookings = new List<BookingDto>
			{
				new BookingDto
				{
					HotelId = "H1",
					Arrival = new DateTime(2024, 9, 1),
					Departure = new DateTime(2024, 9, 3),
					RoomType = "DBL",
					RoomRate = "Prepaid"
				}
			};

			var hotelId = "H1";
			var roomType = "DBL";
			var startDate = new DateTime(2024, 9, 4);
			var endDate = new DateTime(2024, 9, 6);

			var result = _hotelService.CheckAvailability(hotels, bookings, hotelId, roomType, startDate, endDate);

			Assert.True(result);
		}

		[Fact]
		public void CheckAvailability_RoomBooked()
		{
			var hotels = new List<HotelDto>
			{
				new HotelDto
				{
					Id = "H1",
					Name = "Hotel California",
					RoomTypes = new List<RoomTypeDto>
					{
						new RoomTypeDto { Code = "DBL", Description = "Double Room" }
					},
					Rooms = new List<RoomDto>
					{
						new RoomDto { RoomType = "DBL", RoomId = "201" }
					}
				}
			};

			var bookings = new List<BookingDto>
			{
				new BookingDto
				{
					HotelId = "H1",
					Arrival = new DateTime(2024, 9, 1),
					Departure = new DateTime(2024, 9, 3),
					RoomType = "DBL",
					RoomRate = "Prepaid"
				}
			};

			var hotelId = "H1";
			var roomType = "DBL";
			var startDate = new DateTime(2024, 9, 2);
			var endDate = new DateTime(2024, 9, 4);

			var result = _hotelService.CheckAvailability(hotels, bookings, hotelId, roomType, startDate, endDate);

			Assert.False(result);
		}

		[Fact]
		public void CheckAvailability_NonExistingHotel()
		{
			var hotels = new List<HotelDto>();
			var bookings = new List<BookingDto>();
			var hotelId = "H999";
			var roomType = "DBL";
			var startDate = new DateTime(2024, 9, 1);
			var endDate = new DateTime(2024, 9, 3);

			Assert.Throws<HotelNotFoundException>(() =>
			_hotelService.CheckAvailability(hotels, bookings, hotelId, roomType, startDate, endDate));
		}
		
		[Fact]
		public void CheckAvailability_InvalidDate()
		{
			var hotels = new List<HotelDto>
			{
				new HotelDto
				{
					Id = "H1",
					Name = "Hotel California",
					RoomTypes = new List<RoomTypeDto>
					{
						new RoomTypeDto { Code = "DBL", Description = "Double Room" }
					},
					Rooms = new List<RoomDto>
					{
						new RoomDto { RoomType = "DBL", RoomId = "201" }
					}
				}
			};

			var bookings = new List<BookingDto>();
			var hotelId = "H1";
			var roomType = "DBL";
			var startDate = new DateTime(2024, 9, 5);
			var endDate = new DateTime(2024, 9, 1);

			Assert.Throws<InvalidDateException>(() =>
				_hotelService.CheckAvailability(hotels, bookings, hotelId, roomType, startDate, endDate));
		}

		[Fact]
		public void CheckAvailability_NonExistingRoomType()
		{
			var hotels = new List<HotelDto>
			{
				new HotelDto
				{
					Id = "H1",
					Name = "Hotel California",
					RoomTypes = new List<RoomTypeDto>
					{
						new RoomTypeDto { Code = "SGL", Description = "Single Room" }
					}
				}
			};

			var bookings = new List<BookingDto>();
			var hotelId = "H1";
			var roomType = "DBL";
			var startDate = new DateTime(2024, 9, 1);
			var endDate = new DateTime(2024, 9, 3);

			Assert.Throws<RoomTypeNotFoundException>(() =>
				_hotelService.CheckAvailability(hotels, bookings, hotelId, roomType, startDate, endDate));
		}
	}
}
