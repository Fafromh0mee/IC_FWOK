public class Directory
{
    public string Name { get; set; }
    public List<File> Files { get; set; } = new List<File>();

    public void AddFile(File file)
    {
        Files.Add(file);
    }
}
