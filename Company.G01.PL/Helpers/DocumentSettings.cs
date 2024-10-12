namespace Company.G01.PL.Helpers
{
    public class DocumentSettings
    {
        // 1. Upload
        public static string UploadFile(IFormFile file,string FolderName) 
        {
            // 1. Get Location Folder Path
            //string folderPath = Directory.GetCurrentDirectory() +@"wwwroot\files" + FolderName;
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", FolderName);

            // 2. Get file name make it Uniqe

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            // 3. file path ===> folderPath + fileName

            string filePath = Path.Combine(folderPath, fileName);

            // 4. save file as stream : data per time

            using var fileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(fileStream);
            return fileName;


        }



        // 2. Delete
        public static  void DeleteFile(string fileName, string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\files", folderName,fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            


        }
    }
}
