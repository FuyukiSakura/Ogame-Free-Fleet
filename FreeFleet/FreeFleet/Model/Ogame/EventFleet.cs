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
        public int MissionType { get; set; }
        public string MissionTypeText { get; set; }

        // Alarm related
        public bool IsIgnored { get; set; }

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
}
