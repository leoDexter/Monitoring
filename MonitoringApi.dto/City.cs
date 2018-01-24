using Dapper.Contrib.Extensions;
using MonitoringApi.dto.Interfaces;
using System.Collections.Generic;

namespace MonitoringApi.dto
{
    [Table("dbo.City")]
    public class City : IEntity
    {
        #region Attributes

        private int _id;
        private string _name;
        private IEnumerable<Temperature> _temperatures;

        #endregion

        #region Properties

        [Key]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        [Write(false)]
        public IEnumerable<Temperature> Temperatures
        {
            get { return _temperatures; }
            set { _temperatures = value; }
        }

        #endregion
    }
}
