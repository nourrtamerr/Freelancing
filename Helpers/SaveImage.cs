namespace Freelancing.Helpers
{
	public static class SaveImage
	{
		public static string Save(this IFormFile file, IWebHostEnvironment _webHostEnvironment)
		{
			var covername = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "MyPrivateUploads"); Directory.CreateDirectory(folderPath);
			string filePath = Path.Combine($"{_webHostEnvironment.WebRootPath}{ImageSettings.ImagesPath}", covername);
			using (var stream = System.IO.File.Create(filePath))
			{
				file.CopyTo(stream);
			}
			return covername;
		}
	}
}
