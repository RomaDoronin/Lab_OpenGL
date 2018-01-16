namespace Lab_One_OpenGL
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.glControl1 = new OpenTK.GLControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Description_for_player = new System.Windows.Forms.Label();
            this.Sound_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.soundBar = new System.Windows.Forms.TrackBar();
            this.ProsentOfSound = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.soundBar)).BeginInit();
            this.SuspendLayout();
            // 
            // glControl1
            // 
            this.glControl1.BackColor = System.Drawing.SystemColors.Desktop;
            this.glControl1.ForeColor = System.Drawing.SystemColors.Control;
            this.glControl1.Location = new System.Drawing.Point(32, 39);
            this.glControl1.Name = "glControl1";
            this.glControl1.Size = new System.Drawing.Size(651, 515);
            this.glControl1.TabIndex = 0;
            this.glControl1.VSync = false;
            this.glControl1.Load += new System.EventHandler(this.glControl1_Load);
            this.glControl1.Paint += new System.Windows.Forms.PaintEventHandler(this.glControl1_Paint);
            this.glControl1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glControl1_KeyDown);
            this.glControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseClick);
            this.glControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glControl1_MouseMove);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(577, 508);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "10";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(506, 508);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Shots:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(610, 508);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 24);
            this.label3.TabIndex = 5;
            this.label3.Text = "/10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(263, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(0, 24);
            this.label4.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Desktop;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(276, 285);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 39);
            this.label5.TabIndex = 7;
            // 
            // Description_for_player
            // 
            this.Description_for_player.AutoSize = true;
            this.Description_for_player.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Description_for_player.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.Description_for_player.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Description_for_player.Location = new System.Drawing.Point(32, 9);
            this.Description_for_player.Name = "Description_for_player";
            this.Description_for_player.Size = new System.Drawing.Size(333, 18);
            this.Description_for_player.TabIndex = 9;
            this.Description_for_player.Text = "Control of the ship: W - Forward | S - Back | LMB - Shoot";
            // 
            // Sound_button
            // 
            this.Sound_button.BackColor = System.Drawing.SystemColors.Control;
            this.Sound_button.BackgroundImage = global::Lab_One_OpenGL.Properties.Resources.Sound_on_25x25;
            this.Sound_button.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Sound_button.Location = new System.Drawing.Point(701, 5);
            this.Sound_button.Name = "Sound_button";
            this.Sound_button.Size = new System.Drawing.Size(25, 25);
            this.Sound_button.TabIndex = 10;
            this.Sound_button.UseVisualStyleBackColor = false;
            this.Sound_button.Click += new System.EventHandler(this.Sound_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label6.Location = new System.Drawing.Point(469, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(214, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Press \'J\' to change sound ON/OFF";
            // 
            // soundBar
            // 
            this.soundBar.LargeChange = 1;
            this.soundBar.Location = new System.Drawing.Point(701, 39);
            this.soundBar.Name = "soundBar";
            this.soundBar.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.soundBar.Size = new System.Drawing.Size(45, 104);
            this.soundBar.TabIndex = 12;
            this.soundBar.Value = 10;
            this.soundBar.Scroll += new System.EventHandler(this.soundBar_Scroll);
            // 
            // ProsentOfSound
            // 
            this.ProsentOfSound.AutoSize = true;
            this.ProsentOfSound.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.ProsentOfSound.Location = new System.Drawing.Point(701, 150);
            this.ProsentOfSound.Name = "ProsentOfSound";
            this.ProsentOfSound.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ProsentOfSound.Size = new System.Drawing.Size(41, 16);
            this.ProsentOfSound.TabIndex = 13;
            this.ProsentOfSound.Text = "100%";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(704, 189);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(13, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(758, 566);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ProsentOfSound);
            this.Controls.Add(this.soundBar);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Sound_button);
            this.Controls.Add(this.Description_for_player);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.glControl1);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "StarWars - Game";
            ((System.ComponentModel.ISupportInitialize)(this.soundBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl glControl1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label Description_for_player;
        private System.Windows.Forms.Button Sound_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TrackBar soundBar;
        private System.Windows.Forms.Label ProsentOfSound;
        private System.Windows.Forms.Label label7;
    }
}

