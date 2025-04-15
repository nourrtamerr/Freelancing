namespace Freelancing.Helpers
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Linq;

    public class FileValidationAttribute : ValidationAttribute
    {
        public int MaxFileSizeMb { get; set; }
        public string[] AllowedExtensions { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                // Validate file size
                var maxBytes = MaxFileSizeMb * 1024 * 1024;
                if (file.Length > maxBytes)
                {
                    return new ValidationResult($"File size cannot exceed {MaxFileSizeMb}MB.");
                }

                // Validate file extension
                var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
                if (!AllowedExtensions.Contains(extension))
                {
                    return new ValidationResult($"Invalid file type. Allowed types: {string.Join(", ", AllowedExtensions)}");
                }
            }
            return ValidationResult.Success;
        }
    }
}