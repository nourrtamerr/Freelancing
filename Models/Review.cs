﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Freelancing.Models
{
	public class Review
	{
		public int id { set; get; }
		public int Rating { get; set; }
		public string Comment { get; set; }


		[ForeignKey("Reviewee")]
		public string RevieweeId { set; get; }
		public AppUser Reviewee { set; get; }



		[ForeignKey("Reviewer")]
		public string ReviewerId { set; get; }
		public AppUser Reviewer { set; get; }

	}
}
