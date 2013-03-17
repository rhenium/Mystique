﻿using Inscribe.Configuration;
using Inscribe.Configuration.Settings;
using Livet;

namespace Inscribe.ViewModels.Dialogs.SettingSub
{
    public class NotifyConfigViewModel : ViewModel, IApplyable
    {
        public bool IsTranscender
        {
            get { return Setting.Instance.ExperienceProperty.IsTranscender; }
        }

        public NotifyConfigViewModel()
        {
            this._tabNotifyEnabledAsDefault = Setting.Instance.NotificationProperty.IsTabNotifyEnabledAsDefault;
            this._isEnabledNotificationBar = Setting.Instance.NotificationProperty.IsEnabledNotificationBar;
            this._isShowMultiple = Setting.Instance.NotificationProperty.ShowMultiple;

            this._notifyLocationIndex = (int)Setting.Instance.NotificationProperty.NotifyLocation;
            this._windowNotificationStrategyIndex = (int)Setting.Instance.NotificationProperty.WindowNotificationStrategy;
            this._soundNotificationStrategyIndex = (int)Setting.Instance.NotificationProperty.SoundNotificationStrategy;

            this.IsInvisibleSomethingEnabled = Setting.Instance.NotificationProperty.IsInvisibleSomethingEnabled;
            this.IsNotifyOthersFavoriteEnabled = Setting.Instance.NotificationProperty.IsNotifyOthersFavoriteEnabled;
        }

        private bool _tabNotifyEnabledAsDefault;
        public bool TabNotifyEnabledAsDefault
        {
            get { return _tabNotifyEnabledAsDefault; }
            set
            {
                _tabNotifyEnabledAsDefault = value;
                RaisePropertyChanged(() => TabNotifyEnabledAsDefault);
            }
        }

        private bool _isEnabledNotificationBar;
        public bool IsEnabledNotificationBar
        {
            get { return _isEnabledNotificationBar; }
            set
            {
                _isEnabledNotificationBar = value;
                RaisePropertyChanged(() => IsEnabledNotificationBar);
            }
        }

        private bool _isShowMultiple;
        public bool IsShowMultiple
        {
            get { return _isShowMultiple; }
            set
            {
                _isShowMultiple = value;
                RaisePropertyChanged(() => IsShowMultiple);
            }
        }

        private int _notifyLocationIndex;
        public int NotifyLocationIndex
        {
            get { return _notifyLocationIndex; }
            set
            {
                _notifyLocationIndex = value;
                RaisePropertyChanged(() => NotifyLocationIndex);
            }
        }

        private int _windowNotificationStrategyIndex;
        public int WindowNotificationStrategyIndex
        {
            get { return _windowNotificationStrategyIndex; }
            set
            {
                _windowNotificationStrategyIndex = value;
                RaisePropertyChanged(() => WindowNotificationStrategyIndex);
            }
        }

        private int _soundNotificationStrategyIndex;
        public int SoundNotificationStrategyIndex
        {
            get { return _soundNotificationStrategyIndex; }
            set
            {
                _soundNotificationStrategyIndex = value;
                RaisePropertyChanged(() => SoundNotificationStrategyIndex);
            }
        }

        private bool _isInvisibleSomethingEnabled;
        public bool IsInvisibleSomethingEnabled
        {
            get { return _isInvisibleSomethingEnabled; }
            set
            {
                _isInvisibleSomethingEnabled = value;
                RaisePropertyChanged(() => IsInvisibleSomethingEnabled);
            }
        }

        private bool _isNotifyOthersFavoriteEnabled;
        public bool IsNotifyOthersFavoriteEnabled
        {
            get { return _isNotifyOthersFavoriteEnabled; }
            set
            {
                _isNotifyOthersFavoriteEnabled = value;
                RaisePropertyChanged(() => IsNotifyOthersFavoriteEnabled);
            }
        }

        public void Apply()
        {
            Setting.Instance.NotificationProperty.IsTabNotifyEnabledAsDefault = this._tabNotifyEnabledAsDefault;
            Setting.Instance.NotificationProperty.IsEnabledNotificationBar = this._isEnabledNotificationBar;
            Setting.Instance.NotificationProperty.ShowMultiple = this._isShowMultiple;

            Setting.Instance.NotificationProperty.NotifyLocation = (NotifyLocation)this._notifyLocationIndex;
            Setting.Instance.NotificationProperty.WindowNotificationStrategy = (NotificationStrategy)this._windowNotificationStrategyIndex;
            Setting.Instance.NotificationProperty.SoundNotificationStrategy = (NotificationStrategy)this._soundNotificationStrategyIndex;

            Setting.Instance.NotificationProperty.IsInvisibleSomethingEnabled = this.IsInvisibleSomethingEnabled;
            Setting.Instance.NotificationProperty.IsNotifyOthersFavoriteEnabled = this.IsNotifyOthersFavoriteEnabled;
        }
    }
}
