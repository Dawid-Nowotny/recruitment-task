using RecruitmentTask.DTOs;

namespace RecruitmentTask.Services.Interfaces
{
	public interface IHotelService
	{
		bool LoadData(out List<HotelDto> hotels, out List<BookingDto> bookings);
		public void checkAvailability();
	}
}