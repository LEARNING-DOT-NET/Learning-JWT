namespace ViewModels.General
{
	public class ErrorViewModel : object
	{
		public ErrorViewModel(string message) : base()
		{
			if (string.IsNullOrWhiteSpace(message))
			{
				throw new System.ArgumentNullException(paramName: nameof(message));
			}

			Message = message;
		}

		public string Message { get; set; }
	}
}
