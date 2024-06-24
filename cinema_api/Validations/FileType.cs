using cinema_api.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace cinema_api.Validations
{
	public class FileType : ValidationAttribute
	{
		private readonly string[] validFileTypes;

		public FileType(EFileType validFileTypes)
		{
			if (validFileTypes == EFileType.Image)
			{
				this.validFileTypes = new string[] { "image/jpeg", "image/png" };
			}
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (value == null)
			{
				return ValidationResult.Success;
			}

			IFormFile file = value as IFormFile;

			if (file == null)
			{
				return ValidationResult.Success;
			}

			if (!validFileTypes.Contains(file.ContentType))
			{
				return new ValidationResult($"The file type must be one of the following: {string.Join(", ", validFileTypes)}.");
			}

			return ValidationResult.Success;
		}
	}
}
