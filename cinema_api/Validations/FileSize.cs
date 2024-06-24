using System.ComponentModel.DataAnnotations;

namespace cinema_api.Validations
{
	public class FileSize : ValidationAttribute
	{
		private readonly int fileMaxSizeMB;

		public FileSize(int fileMaxSizeMB)
		{
			this.fileMaxSizeMB = fileMaxSizeMB;
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

			if (file.Length > fileMaxSizeMB * 1024 * 1024)
			{
				return new ValidationResult($"The image size must not exceed {fileMaxSizeMB} MB.");
			}

			return ValidationResult.Success;
		}
	}
}
