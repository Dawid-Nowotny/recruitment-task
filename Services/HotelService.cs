﻿using Newtonsoft.Json;
using RecruitmentTask.DTOs;
using RecruitmentTask.Exceptions;
using RecruitmentTask.Services.Interfaces;

namespace RecruitmentTask.Services
{
	public class HotelService : IHotelService
	{
		private const string HotelFilePath = "Data\\hotels.json";
		private const string BookingFilePath = "Data\\bookings.json";

		public bool LoadData(out List<HotelDto>? hotels, out List<BookingDto>? bookings)
		{
			hotels = null;
			bookings = null;

			try
			{
				string hotelJson = File.ReadAllText(HotelFilePath);
				string bookingJson = File.ReadAllText(BookingFilePath);

				hotels = JsonConvert.DeserializeObject<List<HotelDto>>(hotelJson);
				var bookingsDynamic = JsonConvert.DeserializeObject<dynamic>(bookingJson);

				if (bookingsDynamic != null)
				{
					bookings = new List<BookingDto>();
					foreach (var bookingDynamic in bookingsDynamic)
					{
						bookings.Add(BookingDto.ParseFromJson(bookingDynamic));
					}
				}
				else
				{
					return false;
				}

				return true;
			}
			catch (FileNotFoundException ex)
			{
				throw new DataLoadException("One or more data files were not found.", ex);
			}
			catch (JsonException ex)
			{
				throw new DataLoadException("Error parsing JSON data.", ex);
			}
			catch (Exception ex)
			{
				throw new DataLoadException("An unexpected error occurred while loading data.", ex);
			}
		}

		public bool CheckAvailability(List<HotelDto> hotels, List<BookingDto> bookings, string hotelId, string roomType, DateTime startDate, DateTime endDate)
		{
			var hotel = hotels.FirstOrDefault(h => h.Id == hotelId);
			if (hotel == null)
			{
				throw new HotelNotFoundException(hotelId);
			}

			if (startDate > endDate)
			{
				throw new InvalidDateException("Start date cannot be later than end date.");
			}

			if (!hotel.RoomTypes.Any(rt => rt.Code == roomType))
			{
				throw new RoomTypeNotFoundException(roomType);
			}

			foreach (var booking in bookings)
			{
				if (booking.HotelId == hotelId && booking.RoomType == roomType)
				{
					if (booking.Arrival < endDate && booking.Departure > startDate)
					{
						return false;
					}
				}
			}

			return true;
		}
	}
}
