using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotifyCounter
{
    class PropertiesHolder
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name
        {
            get
            {
                return Properties.Settings.Default.Name;
            }

            set
            {
                Properties.Settings.Default.Name = value;
            }
        }

        /// <summary>
        /// Время начала 
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                return Properties.Settings.Default.StartDate;
            }

            set
            {
                Properties.Settings.Default.StartDate = value;
            }
        }

        public bool Autostart
        {
            get
            {
                return Properties.Settings.Default.Autostart;
            }

            set
            {
                Properties.Settings.Default.Autostart = value;
            }
        }

        /// <summary>
        /// Сохранить
        /// </summary>
        public void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
