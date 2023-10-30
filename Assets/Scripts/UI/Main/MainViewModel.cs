using UnityMvvmToolkit.Core;
using UnityMvvmToolkit.Core.Interfaces;
using Zenject;

namespace Land.UI.Main
{
    public class MainViewModel : IBindingContext
    {
        public IProperty<string> Header { get; }
        
        public MainViewModel()
        {
            Header = new Property<string>();
        }

    }
}