using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using FreeFleet.Model.Ogame;
using FreeFleet.Services.Web;
using FreeFleet.ViewModels;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;

namespace FreeFleet.Core
{
    public class GameManager : BindableBase
    {
        private bool _isInGame;

        public ServerAccount LoginedUser { get; set; }
        public string LoggedInServerHost { get; private set; }
        public ObservableCollection<EventFleet> EventFleets { get; } = new ObservableCollection<EventFleet>();

        // Timer related
        private DateTime _lastUpdateTime;
        private int _nextUpdateLeft;
        private int _updateInterval = 30; // 3 minutes
        private int _randomUpToSeconds = 15;
        private int _randomizer = 0;

        public GameManager()
        {
            EventFleets.CollectionChanged += EventFleetChangedHandler;
        }

        public bool Login(string host)
        {
            IsLogin = true;
            LoggedInServerHost = host;
            return true;
        }

        /// <summary>
        /// Start the event fleets monitor
        /// </summary>
        public void StartMonitor()
        {
            var seconds = TimeSpan.FromSeconds(1);
            _lastUpdateTime = DateTime.Now;
            Device.StartTimer(seconds, RefreshTimerHandler);
        }

        /// <summary>
        /// Update the event fleet from server
        /// </summary>
        public async void UpdateEventFleets()
        {
            if(!IsLogin) return; // Not logged in, do nothing
            var fleets = await DependencyService.Get<IHttpService>().GetEventFleetsAsync(LoggedInServerHost);
            var eventFleets = EventFleets;

            // Remove flags no longer exists
            var removeFleets = eventFleets.Where(ef => fleets.All(f => f.Id != ef.Id)).ToArray();
            foreach (var removeFleet in removeFleets)
            {
                eventFleets.Remove(removeFleet);
            }

            // Add new event fleets
            foreach (var fleet in fleets)
            {
                if (eventFleets.All(f => f.Id != fleet.Id))
                {
                    eventFleets.Add(fleet);
                }
            }
        }

        #region Handlers

        /// <summary>
        /// Handles when timer ticks
        /// </summary>
        /// <returns></returns>
        private bool RefreshTimerHandler()
        {
            var timeNow = DateTime.Now;
            var requiredInterval = _updateInterval + _randomizer;
            var interval = (int)(timeNow - _lastUpdateTime).TotalSeconds;
            NextUpdateLeft = requiredInterval - interval;
            if (interval > requiredInterval)
            {
                // Update fleet
                UpdateEventFleets();
                _lastUpdateTime = timeNow;

                // Generate some randomness
                var rnd1 = new Random();
                _randomizer = rnd1.Next(0, _randomUpToSeconds);
            }
            return true; // Repeat the timer
        }

        /// <summary>
        /// Handles when EventFleets is modified
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void EventFleetChangedHandler(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Add) return;
            if (!(e.NewItems[0] is EventFleet eventFleet)) return; // Type mismatch, return

            MissionType[] offensiveMission = { MissionType.AcsAttack, MissionType.Attack, MissionType.MoonDestruction };
            if (!offensiveMission.Contains(eventFleet.MissionType)) return; // Not an offensive mission, return

            // Offensive mission, play alert
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var audioStream = assembly.GetManifestResourceStream("FreeFleet.Resources.Audio." + "alarm.mp3");
            var player = CrossSimpleAudioPlayer.Current;
            player.Loop = true;
            player.Load(audioStream);
            player.Play();
        }

        #endregion

        #region Auto Properties

        /// <summary>
        /// Gets or Sets the value of how frequent the timer will update in seconds
        /// </summary>
        public int UpdateInterval
        {
            get => _updateInterval;
            set
            {
                _updateInterval = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the value of how long until the next update will run in seconds
        /// </summary>
        public int NextUpdateLeft
        {
            get => _nextUpdateLeft;
            private set
            {
                _nextUpdateLeft = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or Sets the value of how many seconds of randomness the user want in the update
        /// </summary>
        public int RandomUpToSeconds
        {
            get => _randomUpToSeconds;
            set
            {
                _randomUpToSeconds = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or Sets whether the user is currently in game.
        /// </summary>
        public bool IsInGame
        {
            get => _isInGame;
            set
            {
                _isInGame = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets whether the game manager is logged in.
        /// </summary>
        public bool IsLogin { get; private set; }

        #endregion
    }
}
