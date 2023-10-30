namespace Land.UI
{
    public interface IView
    {
        float SortOrder { get; set; }
        void Show();
        void Hide();
    }
}