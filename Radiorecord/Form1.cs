using System;
using System.Drawing;
using System.Windows.Forms;
using Radio.Properties;
using WMPLib;

namespace Radio
{
    public partial class Form1 : Form
    {
        private readonly WindowsMediaPlayer _wmPlayer = new WindowsMediaPlayer();
        private static readonly Hook Hook = new Hook(0x91);

        public Form1()
        {
            CreateButtons();

            InitializeComponent();
            Show();
            WindowState = FormWindowState.Normal;

            CheckFavoriteBitrate();

            Hook.KeyPressed += _hook_KeyPressed;
            Hook.SetHook();

            _wmPlayer.settings.volume = Vars.UsersVolume;
            KeyPreview = true;
            ((Label)Controls["labelVolume"]).Text = Resources.Form1_trackBar1_Scroll_Volume_ + _wmPlayer.settings.volume;
            ((TrackBar)Controls["trackBar"]).Value = _wmPlayer.settings.volume / 10;

            if (Vars.WhoIsPlaying > Vars.StationList.Count)
            {
                Vars.WhoIsPlaying = 1;
                Funks.SetFavoriteStation(Vars.WhoIsPlaying);
            }

            StartPlay(Vars.WhoIsPlaying);
        }

        private void CreateButtons()
        {
            const int buttonsInLine = 10;
            const int buttonWidth = 95;
            const int buttonHeight = 124;
            int formWidth;
            int formHeight;
            var lineBufer = 0;
            var elementNumber = 0;

            for (var id = 1; id < Vars.StationList.Count + 1; id++)
            {
                var lineNumber = (id - 1) / buttonsInLine;

                int rowPoint;
                if (lineNumber == lineBufer)
                {
                    rowPoint = elementNumber * buttonWidth;
                    elementNumber++;
                }
                else
                {
                    lineBufer = lineNumber;
                    elementNumber = 0;
                    rowPoint = elementNumber * buttonWidth;
                    elementNumber++;
                }

                var button = new Button
                {
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    BackgroundImage = Vars.StationList.Find(x => x.Id == id).Image,
                    BackgroundImageLayout = ImageLayout.Center,
                    ImageAlign = ContentAlignment.MiddleCenter,
                    Location = new Point(rowPoint, lineNumber * buttonHeight),
                    Name = "button" + id,
                    Size = new Size(buttonWidth, buttonHeight),
                    TabIndex = id,
                    UseVisualStyleBackColor = false,
                    Visible = true,
                    TabStop = false
                };

                button.FlatAppearance.BorderSize = 1;
                button.FlatAppearance.BorderColor = Color.Silver;
                button.Click += StartPlayer;
                button.MouseHover += Button_MouseHover;
                Controls.Add(button);
            }

            formWidth = buttonsInLine * buttonWidth;
            formHeight = lineBufer * buttonHeight;

            var buttonStop = new Button
            {
                BackColor = Color.Transparent,
                DialogResult = DialogResult.Cancel,
                Location = new Point(formWidth + 5, 0),
                Name = "button150",
                Size = new Size(75, 24),
                TabIndex = elementNumber + 1,
                Text = "Stop all",
                UseVisualStyleBackColor = true
            };
            buttonStop.Click += new EventHandler(StopPlayer);
            Controls.Add(buttonStop);

            var labelVolume = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold),
                ForeColor = SystemColors.ControlLightLight,
                Location = new Point(formWidth + 5, 111),
                Name = "labelVolume",
                Size = new Size(73, 13),
                TabIndex = 0,
                Text = "Volume 100",
                TextAlign = ContentAlignment.MiddleCenter
            };
            var trackBar = new TrackBar
            {
                BackColor = SystemColors.Control,
                Location = new Point(formWidth + 15, 131),
                Name = "trackBar",
                Orientation = Orientation.Vertical,
                Size = new Size(45, 242),
                TabIndex = 0,
                TickStyle = TickStyle.TopLeft
            };
            trackBar.Scroll += trackBar1_Scroll;
            Controls.Add(labelVolume);
            Controls.Add(trackBar);

            int radioPointHeight = 28;
            var labelBitrate = new Label
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold),
                ForeColor = SystemColors.ControlLightLight,
                Location = new Point(formWidth + 5, radioPointHeight),
                Name = "labelBitrate",
                Size = new Size(44, 13),
                TabIndex = 0,
                Text = "Bitrate"
            };
            var radioButton1 = new RadioButton
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location = new Point(formWidth + 15, radioPointHeight + 15),
                Name = "radioButton1",
                Size = new Size(37, 17),
                TabIndex = 0,
                Text = "64",
                UseVisualStyleBackColor = false,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold)
            };
            var radioButton2 = new RadioButton
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location = new Point(formWidth + 15, radioPointHeight + 15 + 18),
                Name = "radioButton2",
                Size = new Size(37, 17),
                TabIndex = 0,
                Text = "128",
                UseVisualStyleBackColor = false,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold)
            };
            var radioButton3 = new RadioButton
            {
                AutoSize = true,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Location = new Point(formWidth + 15, radioPointHeight + 15 + 18 + 18),
                Name = "radioButton3",
                Size = new Size(37, 17),
                TabIndex = 0,
                Text = "320",
                UseVisualStyleBackColor = false,
                Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold)
            };
            Controls.Add(labelBitrate);
            Controls.Add(radioButton1);
            Controls.Add(radioButton2);
            Controls.Add(radioButton3);

            formWidth = formWidth + 5 + 75 + 5;

            ClientSize = new Size(formWidth, formHeight);
            CancelButton = buttonStop;
        }

        private void Button_MouseHover(object sender, EventArgs e)
        {
            if (sender.GetType().Name == "Button")
            {
                Button but = new Button();
                but = (Button)sender;

                for (var i = 1; i <= Vars.StationList.Count; i++)
                {
                    string butName = "button" + i;

                    if (butName != "button" + Vars.WhoIsPlaying)
                    {
                        if (butName != but.Name)
                        {
                            ((Button)Controls[butName]).FlatAppearance.BorderSize = 1;
                            ((Button)Controls[butName]).FlatAppearance.BorderColor = Color.Silver;
                        }
                        else
                        {
                            ((Button)Controls[butName]).FlatAppearance.BorderSize = 2;
                            ((Button)Controls[butName]).FlatAppearance.BorderColor = Color.Tomato;
                        }
                    }
                }
            }
        }

        private void CheckFavoriteBitrate()
        {
            for (var i = 1; i <= 3; i++)
            {
                if (Vars.UsersBitrate == Convert.ToInt32(((RadioButton)Controls["radioButton" + i]).Text))
                {
                    ((RadioButton)Controls["radioButton" + i]).Checked = true;
                }
            }
        }

        private void _hook_KeyPressed(object sender, KeyPressEventArgs e)
        {
            if (Visible)
            {
                if (Vars.WhoIsPlaying == 0)
                {
                    ((Button)Controls["button1"]).PerformClick();
                    Funks.SetFavoriteStation(1);
                }
                else
                {
                    ((Button)Controls["button" + Vars.WhoIsPlaying]).PerformClick();
                }
            }
            else
            {
                if (Vars.WhoIsPlaying == 0)
                {
                    Funks.SetFavoriteStation(1);
                }

                StartPlay(Vars.WhoIsPlaying);
            }
        }

        private void StartPlay(int id)
        {
            if (id == Vars.PlayPauseButtonId) return;

            if (Vars.WhoIsPlaying != id || (_wmPlayer.playState != WMPPlayState.wmppsPlaying && _wmPlayer.playState != WMPPlayState.wmppsPaused))
            {
                var bitrate = Vars.UsersBitrate;
                for (var i = 1; i <= 3; i++)
                {
                    if (((RadioButton)Controls["radioButton" + i]).Checked)
                    {
                        bitrate = Convert.ToInt32(((RadioButton)Controls["radioButton" + i]).Text);
                        ((RadioButton)Controls["radioButton" + i]).BackColor = Color.Violet;
                    }
                    else
                    {
                        ((RadioButton)Controls["radioButton" + i]).BackColor = Color.Transparent;
                    }

                    ((RadioButton)Controls["radioButton" + i]).Enabled = false;
                }
                _wmPlayer.controls.stop();
                _wmPlayer.URL = Vars.radio_url_bitrate(bitrate, id);
                _wmPlayer.controls.play();
                Funks.SetFavoriteStation(id);
                Text = $@"{Vars.AName} - {Vars.StationList.Find(x => x.Id == id).Name} now playing";

                Funks.SetFavoriteBitrate();
                Funks.SetFavoriteVolume();
            }
            else
            {
                PausePlay(id);
            }

            ActivateDeactivateButtons(id);
        }

        private void PausePlay(int id)
        {
            if (_wmPlayer.playState == WMPPlayState.wmppsPlaying)
            {
                _wmPlayer.controls.stop();
                Text = $@"{Vars.AName} - {Vars.StationList.Find(x => x.Id == id).Name} is stopped";
            }
            else
            {
                _wmPlayer.controls.play();
                Text = $@"{Vars.AName} - {Vars.StationList.Find(x => x.Id == id).Name} now playing";
            }

            for (var i = 1; i <= 3; i++)
            {
                ((RadioButton)Controls["radioButton" + i]).Enabled = false;
            }

            ActivateDeactivateButtons(id);
        }

        private void StartPlayer(object sender, EventArgs e)
        {
            StartPlay(Convert.ToInt32(((Button)sender).TabIndex));
        }

        private void StopPlayer(object sender, EventArgs e)
        {
            _wmPlayer.controls.stop();
            Text = $@"{Vars.AName}";

            for (var i = 1; i <= 3; i++)
            {
                ((RadioButton)Controls["radioButton" + i]).Enabled = true;
            }

            ActivateDeactivateButtons();
        }

        private void ActivateDeactivateButtons(int? id = null)
        {
            if (id == null) return;

            for (var i = 1; i <= Vars.StationList.Count; i++)
            {
                ((Button)Controls["button" + i]).FlatAppearance.BorderSize = i != id ? 1 : 6;
                ((Button)Controls["button" + i]).FlatAppearance.BorderColor = i != id ? Color.Silver : Color.Tomato;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var panel1 = new Panel { Dock = DockStyle.Fill, Name = "panel1", BackColor = Color.Transparent };
            Controls.Add(panel1);
            Controls["panel1"].SuspendLayout();
            Controls["panel1"].ResumeLayout();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;
            Hide();
            notifyIcon1.Visible = true;
            new Funks.AddNotifyUserDelegate(Funks.NotifyUser).BeginInvoke("The radio will continue to work in the tray.\n\rStart/Stop playback - Scroll Lock", null, null);
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                MessageBox.Show(Resources.Form1_Form1_KeyDown_, Resources.Form1_Form1_KeyDown_microHelp, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ((Label)Controls["labelVolume"]).Text = Resources.Form1_trackBar1_Scroll_Volume_ + ((TrackBar)Controls["trackBar"]).Value * 10;
            _wmPlayer.settings.volume = ((TrackBar)Controls["trackBar"]).Value * 10;

            Vars.UsersVolume = _wmPlayer.settings.volume;

            Funks.SetFavoriteVolume();
        }
    }
}
