using MLIDS.ScriptEditor.ViewModels.Base;
using MLIDS.Scripter.lib;

using System.Collections.ObjectModel;
using System.IO;

namespace MLIDS.ScriptEditor.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<BaseVector> _scriptEntries;

        public ObservableCollection<BaseVector> ScriptEntries;

        private string _fileName;

        public MainWindowViewModel()
        {
            ScriptEntries = new ObservableCollection<BaseVector>();
        }

        public void NewScript()
        {
            ScriptEntries = new ObservableCollection<BaseVector>();

            _fileName = string.Empty;
        }

        public void SaveScript()
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                // TODO: Show Save Dialog
            }

            File.WriteAllText(_fileName, VectorParser.ToJson(ScriptEntries));
        }
    }
}