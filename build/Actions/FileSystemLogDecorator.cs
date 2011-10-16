namespace CommitStage
{
    public class FileSystemLogDecorator : IFileSystem
    {
        private readonly IFileSystem system;
        private readonly Logger log;

        public FileSystemLogDecorator(IFileSystem system, Logger log)
        {
            this.system = system;
            this.log = log;
        }

        public void DeleteFolderContents(string path)
        {
            log.Log("Delete folder contents {0}".FormatInvariant(path));
            system.DeleteFolderContents(path);
        }

        public void DeleteFolder(string path)
        {
            log.Log("Delete folder {0}".FormatInvariant(path));
            system.DeleteFolder(path);
        }

        public void CopyFolderContents(string path, string remotePath)
        {
            log.Log("Copy folder contents from {0} to {1}".FormatInvariant(path, remotePath));
            system.CopyFolderContents(path, remotePath);
        }

        public void CopyFolderContents(string path, string remotePath, bool copySubFolders)
        {
            log.Log("Copy folder contents from {0} to {1}. Copy subfolders: {2}".FormatInvariant(path, remotePath, copySubFolders));
            system.CopyFolderContents(path, remotePath, copySubFolders);
        }

        public void CopyFolderWithSubfoldersFilter(string sourcePath, string destPath, string excludePattern)
        {
            log.Log("Copy folder contents from {0} to {1} with filter '{2}'".FormatInvariant(sourcePath, destPath, excludePattern));
            system.CopyFolderWithSubfoldersFilter(sourcePath, destPath, excludePattern);
        }

        public void CopyFileToFolder(string path, string destPath)
        {
            log.Log("Copy file {0} to folder {1}".FormatInvariant(path, destPath));
            system.CopyFileToFolder(path, destPath);
        }

        public void CopyFile(string path, string destFileName)
        {
            log.Log("Copy file contents from {0} to {1}".FormatInvariant(path, destFileName));
            system.CopyFile(path, destFileName);
        }

        public void CreateFolder(string remotePath)
        {
            log.Log("Create folder {0}".FormatInvariant(remotePath));
            system.CreateFolder(remotePath);
        }

        public bool FolderExists(string path)
        {
            var exists = system.FolderExists(path);

            log.Log("Checking folder '{0}' existence: {1}".FormatInvariant(path, exists));

            return exists;
        }

        public bool FileExists(string path)
        {
            var exists = system.FileExists(path);

            log.Log("Checking file '{0}' existence: {1}".FormatInvariant(path, exists));

            return exists;
        }

        public void DeleteFile(string path)
        {
            log.Log("Delete file {0}".FormatInvariant(path));
            system.DeleteFile(path);
        }
    }
}