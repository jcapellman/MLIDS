﻿using MLIDS.lib.ML.Objects;
using MLIDS.lib.Windows.ViewModels;

using System;

namespace MLIDS.DataCapture.ViewModels
{
    public class MainViewModel : BaseCaptureMainViewModel
    {
        private static readonly NLog.Logger Log = NLog.LogManager.GetCurrentClassLogger();

        private bool _enableSaveStream;

        public bool EnableSaveStream
        {
            get => _enableSaveStream;

            set
            {
                _enableSaveStream = value;

                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            StartBtnEnabled = false;
        }

        public override void PacketProcessing(PayloadItem payloadItem)
        {
            if (payloadItem == null)
            {
                throw new ArgumentNullException(nameof(payloadItem));
            }

            if (EnableSaveStream)
            {
                Log.Debug($"Saving Packet to DAL: {payloadItem}");

                SelectedDataLayer.WritePacketAsync(payloadItem);
            }
            else
            {
                Log.Debug($"Saving Packet to Collection: {payloadItem}");

                Packets.Add(payloadItem.ToString());
            }
        }

        public override void StartButtonEnablement()
        {
            StartBtnEnabled = SelectedDataLayer.IsSelectable && !IsRunning;
        }
    }
}