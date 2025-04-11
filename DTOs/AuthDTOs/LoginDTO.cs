namespace Freelancing.DTOs.AuthDTOs
{
	public class LoginDTO
	{
		public string? Usernameoremail { set; get; }
		public string? loginPassword { set; get; }
		public bool? rememberme { set; get; } = false;
	}
}
