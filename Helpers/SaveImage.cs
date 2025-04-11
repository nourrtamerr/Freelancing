namespace Freelancing.Helpers
{
	public static class SaveImage
	{
		public static string Save(this IFormFile file, IWebHostEnvironment _webHostEnvironment)
		{
			var covername = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), ImageSettings.ImagesPath); 
			Directory.CreateDirectory(folderPath);
			string filePath = Path.Combine(folderPath, covername);
			using (var stream = System.IO.File.Create(filePath))
			{
				file.CopyTo(stream);
			}
			return covername;
		}
	}
}
