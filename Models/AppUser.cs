using Freelancing.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class AppUser :IdentityUser
{

    public string firstname { get; set; }
	public string lastname { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
	public string? ProfilePicture { get; set; }

	public bool isDeleted { get; set; } = false;
	public bool IsVerified { get; set; } = false;

    public string? NationalId { get; set; }


	[InverseProperty("Reviewee")]
	public virtual List<Review> Reviewed { get; set; }



	[InverseProperty("Reviewer")]
	public virtual List<Review> Reviews { get; set; }



	public virtual List<Ban> Bans { get; set; }



	[InverseProperty("Receiver")]
	public virtual List<Chat> ReceivedChats { get; set; }



	[InverseProperty("Sender")]
	public virtual List<Chat> SentChats { get; set; }


	public virtual List<Notification> Notifications { get; set; }
}