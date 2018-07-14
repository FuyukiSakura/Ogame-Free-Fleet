using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FreeFleet.Model.Ogame;
using FreeFleet.ViewModels;
using Plugin.SimpleAudioPlayer;

namespace FreeFleet.Core
{
    public class GameManager : BindableBase
    {
        private bool _isLogin;
        public ObservableCollection<EventFleet> EventFleets { get; } = new ObservableCollection<EventFleet>();

        public GameManager()
        {
            EventFleets.CollectionChanged += EventFleetChangedHandler;
        }

        private static void EventFleetChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            if (!(e.NewItems[0] is EventFleet eventFleet)) return; // Type mismatch, return

            MissionType[] offensiveMission = { MissionType.AcsAttack, MissionType.Attack, MissionType.MoonDestruction };
            if (!offensiveMission.Contains(eventFleet.MissionType)) return; // Not an offensive mission, return

            // Affensive mission, play alert
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var audioStream = assembly.GetManifestResourceStream("FreeFleet.Resources.Audio." + "alarm.mp3");
            var player = CrossSimpleAudioPlayer.Current;
            player.Loop = true;
            player.Load(audioStream);
            player.Play();
        }

        #region Auto Properties

        public bool IsLogin
        {
            get => _isLogin;
            set
            {
                _isLogin = value;
                OnPropertyChanged();
            }
        }

        #endregion
    }
}
