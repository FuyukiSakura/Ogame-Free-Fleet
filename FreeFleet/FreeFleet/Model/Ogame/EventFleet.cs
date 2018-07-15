using System;
using System.Collections.Generic;
using System.Text;
using FreeFleet.ViewModels;

namespace FreeFleet.Model.Ogame
{
    public class EventFleet : BindableBase
    {
        private long _detailsFleet;

        public string Id { get; set; } 
        public string CoordsOrigin { get; set; }
        public string CoordsDest { get; set; }
        public MissionType MissionType { get; set; }

        // Alarm related
        public bool IsIgnored { get; set; }

        public static MissionType[] GetOffensiveMissionTypes()
        {
            return new[]{ MissionType.AcsAttack,
                MissionType.Attack,
                MissionType.MoonDestruction };
        }

        #region Auto Property

        public long DetailsFleet
        {
            get => _detailsFleet;
            set
            {
                _detailsFleet = value;
                OnPropertyChanged();
            }
        }

        #endregion

    }

    public enum MissionType
    {
        Attack = 1,
        AcsAttack = 2,
        Transport = 3,
        Deploy = 4,
        AcsDefend = 5,
        Espionage = 6,
        Colonisation = 7,
        Recycle = 8,
        MoonDestruction = 9,
        Expedition = 15
    }
}
