namespace RecruitmentTask.Exceptions
{
	public class HotelNotFoundException : Exception
	{
		public HotelNotFoundException(string hotelId)
			: base($"Hotel with ID '{hotelId}' not found.")
		{
		}
	}
}