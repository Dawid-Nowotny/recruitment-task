using RecruitmentTask.DTOs;

namespace RecruitmentTask.Services.Interfaces
{
	public interface IHotelService
	{
		bool LoadData(out List<HotelDto>? hotels, out List<BookingDto>? bookings);
		public bool CheckAvailability(List<HotelDto> hotels, List<BookingDto> bookings, string hotelId, string roomType, DateTime startDate, DateTime endDate);
	}
}