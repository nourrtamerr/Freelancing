﻿namespace Freelancing.DTOs
{
    public class CertificateDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime IssueDate { get; set; }
        public string FreelancerName { get; set; }

    }
}
