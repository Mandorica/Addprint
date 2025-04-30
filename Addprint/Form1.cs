using System.Drawing.Printing;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace Addprint
{
    public partial class Form1 : Form
    {
        public string selectedFolderPath = string.Empty;
        private Bitmap bitmap; // ��Ʈ�� �̹��� ��ü�� �����ϱ� ���� ����

        private int photoWidth = 890; // ������ �ʺ� ����
        private int photoHeight = 500; // ������ ���̸� ����
        public int selectedValue;

        private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // ����ȭ�� ��θ� �������� ����
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox.Items.Add("ȭ��Ʈ ������");
            comboBox.Items.Add("�ϴ� ������");
            comboBox.Items.Add("�� ������");
            comboBox.Items.Add("�ʸ� ������");
            comboBox.Items.Add("���� ������");
            comboBox.Items.Add("ī�� ������");
            comboBox.Items.Add("gif1 ������");
            comboBox.Items.Add("gif2 ������");
            comboBox.Items.Add("gif3 ������");
            comboBox.Items.Add("�̺�Ʈ ������1");
            comboBox.Items.Add("�̺�Ʈ ������2");
            comboBox.Items.Add("�̺�Ʈ ������3");
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox���� ���õ� �׸� ���� ������ �� ����
            switch (comboBox.SelectedItem.ToString())
            {
                case "ȭ��Ʈ ������":
                    selectedValue = 1;
                    break;
                case "�ϴ� ������":
                    selectedValue = 2;
                    break;
                case "�� ������":
                    selectedValue = 3;
                    break;
                case "�ʸ� ������":
                    selectedValue = 4;
                    break;
                case "���� ������":
                    selectedValue = 5;
                    break;
                case "ī�� ������":
                    selectedValue = 6;
                    break;
                case "gif1 ������":
                    selectedValue = 7;
                    break;
                case "gif2 ������":
                    selectedValue = 8;
                    break;
                case "gif3 ������":
                    selectedValue = 9;
                    break;
                case "�̺�Ʈ ������1":
                    selectedValue = 10;
                    break;
                case "�̺�Ʈ ������2":
                    selectedValue = 11;
                    break;
                case "�̺�Ʈ ������3":
                    selectedValue = 12;
                    break;
                default:
                    selectedValue = 1;
                    break;
            }

            // �� Ȯ���� ���� ���
            MessageBox.Show("Selected Value: " + selectedValue);
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // ���� ���� ���̾�α� ǥ��
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // ������ ���� ��θ� ������ ����
                selectedFolderPath = folderBrowserDialog.SelectedPath;

                // ������ ���� ��θ� �ؽ�Ʈ �ڽ��� ǥ��
                txtFolderPath.Text = selectedFolderPath;
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            Trace.WriteLine(selectedValue);
            Task task = PrintFujiPrinter(selectedValue);
            MessageBox.Show("�μⰡ �Ϸ�Ǿ����ϴ�.");
        }

        // �μ� �̺�Ʈ �ڵ鷯
        private void PrintImage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, e.PageBounds); // ��Ʈ�� �̹����� ������ ��迡 ���� �׸���
        }

        private async Task PrintFujiPrinter(int frame)
        {
            // PrintDocument ��ü�� �����ϰ�, PrintPage �̺�Ʈ�� �ڵ鷯�� �߰�
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintImage;

            // ��Ʈ�� ��ü�� �����ϰ�, �ػ󵵸� ����
            bitmap = new Bitmap(photoWidth, photoHeight);
            bitmap.SetResolution(960, 640);

            // �׷��� ��ü�� �����Ͽ� ��Ʈ�ʿ� �׸���
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // �׷��� ��ü�� �ʱ� ����
                graphics.Clear(Color.White); // ����� ������� ����
                graphics.CompositingQuality = CompositingQuality.HighQuality; // �ռ� ǰ���� ���� ����
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic; // ���� ǰ���� ���� ����
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; // �ȼ� ������ ��带 ���� ����
                graphics.SmoothingMode = SmoothingMode.AntiAlias; // ��Ƽ���ϸ���� ��带 ����

                // ��� ���� ��ο� �̹��� ���� ��� ����
                string folderPath = Path.GetFullPath(selectedFolderPath);
                Trace.WriteLine(selectedFolderPath);
                List<string> sandTimerImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "SandTimerImages"), "*.png"));
                List<string> catImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "CatImages"), "*.png"));
                List<string> birdImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "BirdImages"), "*.png"));
                List<string> imageFiles = new List<string>(Directory.GetFiles(folderPath, "*.jpg"));
                //List<string> eventImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "EventImages"), "*.png"));

                // ��� �̹��� ������ �ε�
                Image CoverBackground = Image.FromFile(Path.Combine(desktopPath, "CoverBackground.png"));
                Image filmConceptBackground = Image.FromFile(Path.Combine(desktopPath, "FilmConceptFrameImage.png"));
                Image postcardConceptBackground = Image.FromFile(Path.Combine(desktopPath, "PostcardConceptFrameImage.png"));
                Image cartoonConceptBackground = Image.FromFile(Path.Combine(desktopPath, "CartoonConceptFrameImage.png"));
                Image event1Background = Image.FromFile(Path.Combine(desktopPath, "Event1ConceptFrameImage.png"));
                Image event2Background = Image.FromFile(Path.Combine(desktopPath, "Event2ConceptFrameImage.png"));
                Image event3Background = Image.FromFile(Path.Combine(desktopPath, "Event3ConceptFrameImage.png"));

                // �μ� Ƚ����ŭ �ݺ�
                // �̹��� ���ϵ��� �������� ó��
                for (int j = imageFiles.Count-2; j >= 0; j--) //imageFiles.Count
                    {
                        string photoFilePath = imageFiles[j]; // �̹��� ���� ��� ��������
                        Image originalPhoto = Image.FromFile(photoFilePath); // ���� �̹��� �ε�
                        Bitmap photo = new Bitmap(originalPhoto); // ��Ʈ�� �̹��� ����

                        int indexInImageFiles = imageFiles.IndexOf(photoFilePath);

                        Rectangle rect = new Rectangle(
                            0,
                            0,
                            photoWidth,
                            photoHeight
                        );


                        Rectangle coverRect = new Rectangle(
                            80,
                            0,
                            photoWidth - 80,
                            photoHeight + 40
                        );


                        Rectangle innerRect = new Rectangle(
                            230,
                            30,
                            photoWidth - 230,
                            photoHeight - 60
                        );


                        Rectangle backgroundRect = new Rectangle(
                                0,
                                0,
                                890,
                                500
                            );


                        Rectangle gifRect = new Rectangle(
                        700,
                        320,
                        165,
                        165
                        );


                        Rectangle birdGifRect = new Rectangle(
                            675,
                            270,
                            200,
                            200
                        );

                        // �̹��� ������ �̸��� ��������
                        string fileName = Path.GetFileName(photoFilePath);

                        // �̹����� Ŀ�� �̹����� ���
                        if (fileName == "coverImage.jpg")
                        {
                            graphics.DrawImage(photo, coverRect); // Ŀ�� �̹����� ������ �簢�� ������ �׸���
                            graphics.DrawImage(CoverBackground, backgroundRect);
                        }
                        else
                        {
                            // ������ ���ÿ� ���� �̹��� ó��
                            switch (frame)
                            {
                                case 1:
                                    // ������
                                    graphics.FillRectangle(new SolidBrush(Color.White), backgroundRect); // ����� ���������� ����
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    break;
                                case 2:
                                // �Ķ��� ���
                                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(228, 243, 249)), backgroundRect); // ����� Ư�� �������� ����
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    break;
                                case 3:
                                    
                                    // ������ ���
                                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(46, 46, 46)), backgroundRect); // ����� ������� ����
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    break;
                                case 4:
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(filmConceptBackground, backgroundRect); // �ʸ� ���� ����� ��� �簢�� ������ �׸���
                                    break;
                                case 5:
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(postcardConceptBackground, backgroundRect); // ���� ���� ����� ��� �簢�� ������ �׸���
                                    break;
                                case 6:
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(cartoonConceptBackground, backgroundRect); // ��ȭ ���� ����� ��� �簢�� ������ �׸���
                                    break;
                                case 7:
                                    // gif ������ 1�� �ε�
                                    Image sandTimerImage = Image.FromFile(sandTimerImageFiles[indexInImageFiles - 1]);
                                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(159, 204, 213)), backgroundRect); // ����� Ư�� �������� ����
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(sandTimerImage, gifRect); // �𷡽ð� �̹����� GIF �簢�� ������ �׸���
                                    sandTimerImage.Dispose(); // �𷡽ð� �̹��� ���ҽ� ����
                                    break;
                                case 8:
                                    // gif ������ 2�� �ε�
                                    Image catImage = Image.FromFile(catImageFiles[indexInImageFiles - 1]);
                                    graphics.FillRectangle(new SolidBrush(Color.White), backgroundRect); // ����� Ư�� �������� ����
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(catImage, gifRect); // ����� �̹����� GIF �簢�� ������ �׸���
                                    catImage.Dispose(); // ����� �̹��� ���ҽ� ����
                                    break;
                                case 9:
                                    // gif ������ 3�� �ε�
                                    Image birdImage = Image.FromFile(birdImageFiles[indexInImageFiles - 1]);
                                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(242, 206, 213)), backgroundRect); // ����� Ư�� �������� ����
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(birdImage, gifRect); // �� �̹����� �� GIF �簢�� ������ �׸���
                                    birdImage.Dispose(); // �� �̹��� ���ҽ� ����
                                    break;
                                case 10:
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(event1Background, backgroundRect); // ���� ���� ����� ��� �簢�� ������ �׸���
                                    break;
                                case 11:
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(event2Background, backgroundRect); // ���� ���� ����� ��� �簢�� ������ �׸���
                                    break;
                                case 12:
                                    graphics.DrawImage(photo, innerRect); // ������ ���� �簢�� ������ �׸���
                                    graphics.DrawImage(event3Background, backgroundRect); // ���� ���� ����� ��� �簢�� ������ �׸���
                                    break;
                        }
                        }

                        photo.Dispose(); // ���� ��Ʈ�� ���ҽ� ����

                        printDoc.Print(); // ���� ���
                    }
                // ��� �̹��� ���ҽ� ����
                filmConceptBackground.Dispose();
                postcardConceptBackground.Dispose();
                cartoonConceptBackground.Dispose();
                event1Background.Dispose();
                event2Background.Dispose();
                event3Background.Dispose();
            }

            bitmap.Dispose(); // ��Ʈ�� ���ҽ� ����
        }
    }
}
