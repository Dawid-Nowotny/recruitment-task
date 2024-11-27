namespace RecruitmentTask.Exceptions
{
	public class RoomTypeNotFoundException : Exception
	{
		public RoomTypeNotFoundException(string roomType)
			: base($"Room type '{roomType}' not found.")
		{
		}
	}
}