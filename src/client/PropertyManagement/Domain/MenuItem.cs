namespace PropertyManagement.Domain
{
    public class MenuItem
    {
        public string Name { get; set; }
        public object Content { get; set; }

        public MenuItem(string name, object content)
        {
            Name = name;
            Content = content;
        }
    }
}
