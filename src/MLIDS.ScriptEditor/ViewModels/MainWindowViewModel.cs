using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

using Microsoft.Win32;

using MLIDS.ScriptEditor.ViewModels.Base;
using MLIDS.Scripter.lib;
using MLIDS.Scripter.lib.Common;

namespace MLIDS.ScriptEditor.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private ObservableCollection<BaseVector> _scriptEntries;

        public ObservableCollection<BaseVector> ScriptEntries
        {
            get => _scriptEntries;

            set
            {
                _scriptEntries = value;

                OnPropertyChanged();
            }
        }

        private string _fileName;

        private bool _unsavedChanges = false;
        
        public MainWindowViewModel()
        {
            ScriptEntries = new ObservableCollection<BaseVector>();
        }

        private bool ConfirmUnsavedChanges() => MessageBox.Show("Proceed without saving changes?", "Unsaved changes detected...",
               MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes;

        internal void SaveAsScript()
        {
            var sfd = new SaveFileDialog
            {
                Filter = $"MLIDS script (*{Constants.FILE_EXTENSION})"
            };

            var result = sfd.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return;
            }

            _fileName = sfd.FileName;

            File.WriteAllText(_fileName, VectorParser.ToJson(ScriptEntries));

            _unsavedChanges = false;
        }

        public void NewScript()
        {
            ScriptEntries = new ObservableCollection<BaseVector>();

            _fileName = string.Empty;
        }

        public bool Exit()
        {
            if (_unsavedChanges && !ConfirmUnsavedChanges())
            {
                return false;
            }

            return true;
        }

        public async void OpenScript()
        {
            if (_unsavedChanges && !ConfirmUnsavedChanges())
            {
                return;
            }

            var ofd = new OpenFileDialog
            {
                Filter = $"MLIDS script (*{Constants.FILE_EXTENSION})"
            };

            var result = ofd.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return;
            }

            _unsavedChanges = false;

            try
            {
                var entries = await VectorParser.ParseScriptAsync(ofd.FileName);

                ScriptEntries = new ObservableCollection<BaseVector>(entries);

                _fileName = ofd.FileName;
            } catch (Exception ex)
            {
                MessageBox.Show($"Error opening file: {ex}");
            }
        }

        public void SaveScript()
        {
            // If the filename isn't set - treat it like SaveAs
            if (string.IsNullOrEmpty(_fileName))
            {
                SaveAsScript();

                return;
            }

            File.WriteAllText(_fileName, VectorParser.ToJson(ScriptEntries));

            _unsavedChanges = false;
        }
    }
}