using Microsoft.Win32;
using MLIDS.ScriptEditor.ViewModels.Base;
using MLIDS.Scripter.lib;
using MLIDS.Scripter.lib.Common;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;

namespace MLIDS.ScriptEditor.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<BaseVector> _scriptEntries;

        public ObservableCollection<BaseVector> ScriptEntries;

        private string _fileName;

        private bool _unsavedChanges = false;
        
        public MainWindowViewModel()
        {
            ScriptEntries = new ObservableCollection<BaseVector>();
        }

        private bool ConfirmUnsavedChanges() => MessageBox.Show("Proceed without saving changes?", "Unsaved changes detected...",
               MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;

        public void NewScript()
        {
            ScriptEntries = new ObservableCollection<BaseVector>();

            _fileName = string.Empty;
        }

        public void OpenScript()
        {
            if (_unsavedChanges && !ConfirmUnsavedChanges())
            {
                return;
            }

            var ofd = new OpenFileDialog
            {
                Filter = $"MLIDS Script (*{Constants.FILE_EXTENSION})"
            };

            var result = ofd.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return;
            }

            // TODO: Read File

            _fileName = ofd.FileName;
        }

        public void SaveScript()
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                // TODO: Show Save Dialog
            }

            File.WriteAllText(_fileName, VectorParser.ToJson(ScriptEntries));

            _unsavedChanges = false;
        }
    }
}