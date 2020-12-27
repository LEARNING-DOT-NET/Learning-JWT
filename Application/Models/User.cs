//namespace Application.Models

namespace Models
{
	public class User : object
	{
		public User() : base()
		{
		}

		public int Id { get; set; }

		public string Username { get; set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public string Password { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }
	}
}
