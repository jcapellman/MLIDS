﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

using MLIDS.lib.Containers;
using MLIDS.lib.DAL.Base;
using MLIDS.lib.Extensions;
using MLIDS.lib.ML.Objects;

namespace MLIDS.lib.DAL
{
    public class CSVFileDAL : BaseDAL
    {
        public class CSVWriter
        {
            private string _filepath { get; set; }

            private static readonly object locker = new Object();

            public CSVWriter(string filePath)
            {
                _filepath = filePath;
            }

            public void WriteToFile(string text)
            {
                lock (locker)
                {
                    using (var file = new FileStream(_filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
                    {
                        using (var writer = new StreamWriter(file, Encoding.Unicode))
                        {
                            writer.Write(text);
                        }
                    }
                }
            }
        }

        private CSVWriter _writer;

        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private const string DEFAULT_CSV_FILE = "db.csv";

        private string _fileName;

        public CSVFileDAL(SettingsItem settingsItem) : base(settingsItem)
        {
        }

        public override string Description => "CSV";

        public override bool IsSelectable => true;

        public override async Task<List<PayloadItem>> GetHostPacketsAsync(string hostName)
        {
            if (!File.Exists(_fileName))
            {
                return new List<PayloadItem>();
            }

            var lines = await File.ReadAllLinesAsync(_fileName);

            var data = new List<PayloadItem>();

            foreach (var line in lines)
            {
                data.Add(line.FromCSV<PayloadItem>());
            }

            return data.Where(a => a.HostName == hostName).ToList();
        }

        public override bool Initialize()
        {
            _fileName = settingsItem.DAL_FileName ?? DEFAULT_CSV_FILE;

            _writer = new CSVWriter(_fileName);

            return File.Exists(_fileName);
        }

        public override async Task<List<PayloadItem>> QueryPacketsAsync(Expression<Func<PayloadItem, bool>> queryExpression)
        {
            if (queryExpression == null)
            {
                Log.Error($"CSVFileDAL::QueryPacketsAsync - Query Expression was null");

                throw new ArgumentNullException(nameof(queryExpression));
            }

            if (!File.Exists(_fileName))
            {
                return new List<PayloadItem>();
            }

            var lines = await File.ReadAllLinesAsync(_fileName);

            var data = new List<PayloadItem>();

            foreach (var line in lines)
            {
                data.Add(line.FromCSV<PayloadItem>());
            }

            return data.AsQueryable().Where(queryExpression).ToList();
        }

        public override async Task<bool> WritePacketAsync(PayloadItem packet)
        {
            if (packet == null)
            {
                Log.Error($"CSVFileDAL::WritePacketAsync - packet was null");

                throw new ArgumentNullException(nameof(packet));
            }

            _writer.WriteToFile(packet.ToCSV<PayloadItem>());

            return true;
        }
    }
}