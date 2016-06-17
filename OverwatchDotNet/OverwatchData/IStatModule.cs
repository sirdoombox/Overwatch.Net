using AngleSharp.Dom;
using System.Threading.Tasks;

namespace OverwatchDotNet.OverwatchData
{
    public interface IStatModule
    {
        void LoadFromURL(string url);
        Task LoadFromURLAsync(string url);
        void LoadFromDocument(IDocument document);
    }
}
