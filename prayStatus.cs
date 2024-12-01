using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

class PrayStatusApp : Form
{
    private bool isDragging = false;
    private Point startPoint = new Point(0, 0);

    private int clickCount = 2;
    private Label clickableLabel;

    // Import Windows API to enable layered windows (transparent)
    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

    [DllImport("user32.dll")]
    private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

    [DllImport("user32.dll")]
    private static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

    // Constants for transparency
    private const int GWL_EXSTYLE = 200;
    private const int WS_EX_LAYERED = 0x80000;
    private const int LWA_COLORKEY = 0x1;
    private const int WS_EX_NOACTIVATE = 0x08000000;

    public PrayStatusApp()
    {
        // Set form properties
        this.Text = "Pray Status App";
        this.TopMost = true;
        this.StartPosition = FormStartPosition.Manual;
        this.Width = 320;
        this.Height = 400;
        this.FormBorderStyle = FormBorderStyle.None;
        this.BackColor = Color.Gray; // Magenta is used as the transparency key
        this.TransparencyKey = Color.Gray; // Make magenta color transparent

        // Set the window to use transparency and layered attributes
        int exStyle = GetWindowLong(this.Handle, GWL_EXSTYLE);
        SetWindowLong(this.Handle, GWL_EXSTYLE, exStyle | WS_EX_LAYERED | WS_EX_NOACTIVATE);
        SetLayeredWindowAttributes(this.Handle, (uint)Color.Gray.ToArgb(), 0, LWA_COLORKEY);

        // Prevent form background from drawing
        this.DoubleBuffered = true;

        // Position form at the top-right of the screen
        int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
        this.Location = new Point(screenWidth - this.Width, 0);

        // Create Logo with magenta border to blend with transparent background
        PictureBox logo = new PictureBox();
        try
        {
            logo.Image = Image.FromFile("logo.png"); // Replace with your logo path
        }
        catch
        {
            MessageBox.Show("Logo image not found! Check the path.");
        }
        logo.SizeMode = PictureBoxSizeMode.StretchImage;
        logo.Width = 320;
        logo.Height = 200;
        logo.Location = new Point(0, 0);
        logo.BorderStyle = BorderStyle.None; // Set a border for the logo
        logo.Padding = new Padding(5); // Optional padding for a border effect
        logo.BackColor = Color.Transparent; // Set border color to magenta to blend with transparency
        // this.BackColor = Color.Gray;

        this.Controls.Add(logo);

        // Create Clickable Label
        clickableLabel = new Label();
        clickableLabel.Text = "نماز ظهر";
        clickableLabel.AutoSize = false;
        clickableLabel.Width = 320;
        clickableLabel.Height = 140;
        clickableLabel.Location = new Point(0, 200); // Place label below the logo
        clickableLabel.TextAlign = ContentAlignment.MiddleCenter;
        clickableLabel.Font = new Font("B Nazanin", 46  , FontStyle.Bold);
        clickableLabel.ForeColor = ColorTranslator.FromHtml("#d90429");
        clickableLabel.BackColor = Color.Transparent; // Set label background transparent
        clickableLabel.BorderStyle = BorderStyle.None; // Remove border from label
        clickableLabel.Click += Label_Click;
        this.Controls.Add(clickableLabel);

        this.MouseDown += new MouseEventHandler(Form_MouseDown);
        this.MouseMove += new MouseEventHandler(Form_MouseMove);
        this.MouseUp += new MouseEventHandler(Form_MouseUp);

        foreach (Control control in this.Controls)
        {
            control.MouseDown += Form_MouseDown;
            control.MouseMove += Form_MouseMove;
            control.MouseUp += Form_MouseUp;
        }
    }

    // Handle click event to change label text
    private void Label_Click(object sender, EventArgs e)
    {
        clickCount++;
        if (clickCount > 5)
            clickCount = 1;
        switch (clickCount)
        {
            case 1:
                clickableLabel.Text = "نماز صبح";
                break;
            case 2:
                clickableLabel.Text = "نماز ظهر";
                break;
            case 3:
                clickableLabel.Text = "نماز عصر";
                break;
            case 4:
                clickableLabel.Text = "نماز مغرب";
                break;
            case 5:
                clickableLabel.Text = "نماز عشاء";
                break;
        }
    }
    private void Form_MouseDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = true;
            startPoint = new Point(e.X, e.Y);
        }
    }

    private void Form_MouseMove(object sender, MouseEventArgs e)
    {
        if (isDragging)
        {
            Point p = PointToScreen(e.Location);
            this.Location = new Point(p.X - startPoint.X, p.Y - startPoint.Y);
        }
    }

    private void Form_MouseUp(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDragging = false;
        }
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(new PrayStatusApp()); // Your form
    }
} 