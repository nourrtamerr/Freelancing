namespace Freelancing.DTOs.AuthDTOs
{
	public class UsersRequestingVerificationViewDTO
	{
		public string firstname { get; set; }
		public string lastname { get; set; }
		public DateOnly DateOfBirth { get; set; }
		public string City { get; set; }
		public string Country { get; set; }
		public string? ProfilePicture { get; set; }
		public string? NationalId { get; set; }
	}
}
