namespace RecruitmentTask.Exceptions
{
	public class DataLoadException : Exception
	{
		public DataLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}
}