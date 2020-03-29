using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;

namespace TestXamarinFirebase.Helper
{
    class FBStorage
    {
        FirebaseStorage firebaseStorage;

        public FBStorage(string urlStorage)
        {
            firebaseStorage = new FirebaseStorage(urlStorage);
        }
        public async Task<string> UploadFile(Stream fileStream, string tableName, string fileName)
        {
            var imageUrl = await firebaseStorage.Child(tableName).Child(fileName).PutAsync(fileStream);
            return imageUrl;
        }
        public async Task<string> DownloadFile(string tableName, string fileName)
        {
            return await firebaseStorage.Child(tableName).Child(fileName).GetDownloadUrlAsync();
        }
        public async Task DeleteFile(string tableName, string fileName)
        {
            await firebaseStorage.Child(tableName).Child(fileName).DeleteAsync();
        }
    }
}
