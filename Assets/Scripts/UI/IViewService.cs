namespace Land.UI
{
    public interface IViewService
    {
        void PushView<TView>();
        void PopView();
    }
}