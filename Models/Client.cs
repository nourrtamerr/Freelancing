namespace Freelancing.Models
{
	public class Client:AppUser
	{
		public bool PaymentVerified { get; set; } = false;
	}
}
