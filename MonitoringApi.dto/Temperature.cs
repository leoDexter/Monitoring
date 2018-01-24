using Dapper.Contrib.Extensions;
using MonitoringApi.dto.Interfaces;
using System;

namespace MonitoringApi.dto
{
    [Table("dbo.Temperatures")]
    public class Temperature : IEntity
    {
        #region Attributes
        
        private int _id;        
        private int _cityId;
        private DateTime _date;
        private double _temperature;

        #endregion

        #region Properties

        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int CityId
        {
            get { return _cityId; }
            set { _cityId = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public double Degrees
        {
            get { return _temperature; }
            set { _temperature = value; }
        }

        #endregion
    }
}
