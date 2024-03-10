namespace ClockFace
{
    partial class ClockFace
    {
        /// <summary>
        /// Обязательная переменная конструктора.
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
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.secondTrackBar = new System.Windows.Forms.TrackBar();
            this.minuteTrackBar = new System.Windows.Forms.TrackBar();
            this.hourTrackBar = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.secondTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // secondTrackBar
            // 
            this.secondTrackBar.Location = new System.Drawing.Point(656, 267);
            this.secondTrackBar.Name = "secondTrackBar";
            this.secondTrackBar.Size = new System.Drawing.Size(143, 56);
            this.secondTrackBar.TabIndex = 0;
            this.secondTrackBar.ValueChanged += new System.EventHandler(this.secondTrackBar_ValueChanged);
            // 
            // minuteTrackBar
            // 
            this.minuteTrackBar.Location = new System.Drawing.Point(656, 329);
            this.minuteTrackBar.Name = "minuteTrackBar";
            this.minuteTrackBar.Size = new System.Drawing.Size(143, 56);
            this.minuteTrackBar.TabIndex = 1;
            this.minuteTrackBar.ValueChanged += new System.EventHandler(this.minuteTrackBar_ValueChanged);
            // 
            // hourTrackBar
            // 
            this.hourTrackBar.Location = new System.Drawing.Point(656, 391);
            this.hourTrackBar.Name = "hourTrackBar";
            this.hourTrackBar.Size = new System.Drawing.Size(143, 56);
            this.hourTrackBar.TabIndex = 2;
            this.hourTrackBar.ValueChanged += new System.EventHandler(this.hourTrackBar_ValueChanged);
            // 
            // ClockFace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.hourTrackBar);
            this.Controls.Add(this.minuteTrackBar);
            this.Controls.Add(this.secondTrackBar);
            this.Name = "ClockFace";
            this.Text = "ClockFace";
            this.Load += new System.EventHandler(this.ClockFace_Load);
            ((System.ComponentModel.ISupportInitialize)(this.secondTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.minuteTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hourTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar secondTrackBar;
        private System.Windows.Forms.TrackBar minuteTrackBar;
        private System.Windows.Forms.TrackBar hourTrackBar;
    }
}

