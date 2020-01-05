namespace PropertyManagement.DataContainers
{
    public class NavigationMenuItem
    {
        public string Name { get; set; }
        public object Content { get; set; }

        public NavigationMenuItem(string name, object content)
        {
            Name = name;
            Content = content;
        }
    }
}
