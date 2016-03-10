using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotifyCounter
{
    public partial class Form1 : Form
    {
        PropertiesHolder _ph = new PropertiesHolder();
        int _interval = 0;

        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            //Проверить наличие данныйх
            if (this.WindowState == FormWindowState.Minimized)
            {
                if (String.IsNullOrEmpty(_ph.Name) || _ph.StartDate == null)
                {
                    this.WindowState = FormWindowState.Normal;
                }
                else
                {
                    tbName.Text = _ph.Name;
                    dtpStartData.Value = _ph.StartDate;
                    chbStartWithWindows.Checked = _ph.Autostart;
                    SetInfo();
                    notifyIcon1.ShowBalloonTip(10);
                    timer1.Enabled = true;
                    this.Hide();
                }
            }
        }

        #region Контекстное меню
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void customToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.Show();
            this.Activate();
        }
        #endregion

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetInfo();
            notifyIcon1.ShowBalloonTip(10);
        }

        private void SetInfo()
        {
            TimeSpan ts = DateTime.Today - _ph.StartDate;
            _interval = ts.Days;
            notifyIcon1.Text = String.Format("С {0} прошло {1}", tbName.Text, getStringDay(_interval));
            notifyIcon1.BalloonTipTitle = tbName.Text;
            notifyIcon1.BalloonTipText = getStringDay(_interval);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            _ph.Name = tbName.Text;
            _ph.StartDate = dtpStartData.Value;
            _ph.Save();
            timer1.Enabled = true;
            SetInfo();
            this.WindowState = FormWindowState.Minimized;
        }

        private void chbStartWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            _ph.Autostart = ((CheckBox)sender).Checked;
            SetAutorunValue(((CheckBox)sender).Checked);
        }

        public bool SetAutorunValue(bool autorun)
        {
            string ExePath = System.Windows.Forms.Application.ExecutablePath;
            RegistryKey reg;
            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");
            try
            {
                if (autorun)
                    reg.SetValue("Notifyer", ExePath);
                else
                    reg.DeleteValue("Notifyer");

                reg.Close();
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из приложения?", "", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
            }
            else
            {
                _ph.Save();
            }
        }

        private string getStringDay(int eNumber)
        {
            var str = String.Empty;
            var number = eNumber % 10;
            if(number >= 11 && number <= 19)
            {
                str = "дней";
            }
            else
            {
                switch(number % 10)
                {
                    case 1: str = "день"; break;
                    case 2:
                    case 3:
                    case 4: str = "дня"; break;
                    default: str = "дней"; break;
                }
            }

            return String.Format("{0} {1}", eNumber, str);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                SetInfo();
                notifyIcon1.ShowBalloonTip(10);
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
        }

        private void dtpStartData_ValueChanged(object sender, EventArgs e)
        {
            _ph.StartDate = ((DateTimePicker)sender).Value;
        }
    }
}
