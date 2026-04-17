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
        private Bitmap bitmap; // 비트맵 이미지 객체를 저장하기 위한 변수

        private int photoWidth = 890; // 사진의 너비를 설정
        private int photoHeight = 500; // 사진의 높이를 설정
        public int selectedValue;
        public int printbooknum;

        private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // 바탕화면 경로를 가져오는 변수
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox.Items.Add("Basic1Images");
            comboBox.Items.Add("Basic2Images");
            comboBox.Items.Add("Basic3Images");
            comboBox.Items.Add("Concept1Images");
            comboBox.Items.Add("Concept2Images");
            comboBox.Items.Add("Concept3Images");
            comboBox.Items.Add("Gif1Images");
            comboBox.Items.Add("Gif2Images");
            comboBox.Items.Add("Gif3Images");
            comboBox.Items.Add("Event1Images");
            comboBox.Items.Add("Event2Images");
            comboBox.Items.Add("Event3Images");
            comboBox.Items.Add("Collab1Images");
            comboBox.Items.Add("Collab2Images");
            comboBox.Items.Add("Collab3Images");
            comboBox.Items.Add("Collab4Images");
            comboBox.Items.Add("Collab5Images");
            comboBox.Items.Add("Collab6Images");
            comboBox.Items.Add("Collab7Images");
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            comboBox2.Items.Add("1");
            comboBox2.Items.Add("2");
            comboBox2.Items.Add("3");
            comboBox2.SelectedIndexChanged += ComboBox2_SelectedIndexChanged;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox에서 선택된 항목에 따라 변수에 값 대입
            switch (comboBox.SelectedItem.ToString())
            {
                case "Basic1Images":
                    selectedValue = 1;
                    break;
                case "Basic2Images":
                    selectedValue = 2;
                    break;
                case "Basic3Images":
                    selectedValue = 3;
                    break;
                case "Concept1Images":
                    selectedValue = 4;
                    break;
                case "Concept2Images":
                    selectedValue = 5;
                    break;
                case "Concept3Images":
                    selectedValue = 6;
                    break;
                case "Gif1Images":
                    selectedValue = 7;
                    break;
                case "Gif2Images":
                    selectedValue = 8;
                    break;
                case "Gif3Images":
                    selectedValue = 9;
                    break;
                case "Event1Images":
                    selectedValue = 10;
                    break;
                case "Event2Images":
                    selectedValue = 11;
                    break;
                case "Event3Images":
                    selectedValue = 12;
                    break;
                case "Collab1Images":
                    selectedValue = 13;
                    break;
                case "Collab2Images":
                    selectedValue = 14;
                    break;
                case "Collab3Images":
                    selectedValue = 15;
                    break;
                case "Collab4Images":
                    selectedValue = 16;
                    break;
                case "Collab5Images":
                    selectedValue = 17;
                    break;
                case "Collab6Images":
                    selectedValue = 18;
                    break;
                case "Collab7Images":
                    selectedValue = 19;
                    break;
                default:
                    selectedValue = 1;
                    break;
            }

            // 값 확인을 위해 출력
            MessageBox.Show("Selected Value: " + selectedValue);
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedItem.ToString())
            {
                case "1":
                    printbooknum = 1;
                    break;
                case "2":
                    printbooknum = 2;
                    break;
                case "3":
                    printbooknum = 3;
                    break;
                default:
                    printbooknum = 1;
                    break;
            }

            // 값 확인을 위해 출력
            MessageBox.Show("Selected Value: " + printbooknum);
        }

        private void folderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // 폴더 선택 다이얼로그 표시
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // 선택한 폴더 경로를 변수에 저장
                selectedFolderPath = folderBrowserDialog.SelectedPath;

                // 선택한 폴더 경로를 텍스트 박스에 표시
                txtFolderPath.Text = selectedFolderPath;
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            Trace.WriteLine(selectedValue);
            Task task = PrintFujiPrinter(selectedValue, printbooknum);
            MessageBox.Show("인쇄가 완료되었습니다.");
        }

        // 인쇄 이벤트 핸들러
        private void PrintImage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, e.PageBounds); // 비트맵 이미지를 페이지 경계에 맞춰 그리기
        }

        private async Task PrintFujiPrinter(int frame, int printbooknum)
        {
            // PrintDocument 객체를 생성하고, PrintPage 이벤트에 핸들러를 추가
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintImage;

            // 비트맵 객체를 생성하고, 해상도를 설정
            bitmap = new Bitmap(photoWidth, photoHeight);
            bitmap.SetResolution(960, 640);

            // 그래픽 객체를 생성하여 비트맵에 그리기
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                // 그래픽 객체의 초기 설정
                graphics.Clear(Color.White); // 배경을 흰색으로 설정
                graphics.CompositingQuality = CompositingQuality.HighQuality; // 합성 품질을 높게 설정
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic; // 보간 품질을 높게 설정
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality; // 픽셀 오프셋 모드를 높게 설정
                graphics.SmoothingMode = SmoothingMode.AntiAlias; // 앤티에일리어싱 모드를 설정

                // 출력 폴더 경로와 이미지 파일 경로 설정
                string folderPath = Path.GetFullPath(selectedFolderPath);
                Trace.WriteLine(selectedFolderPath);
                List<string> imageFiles = new List<string>(Directory.GetFiles(folderPath, "*.jpg"));
                List<string> basicImage1Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Basic1Images"), "*.png"));
                List<string> basicImage2Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Basic2Images"), "*.png"));
                List<string> basicImage3Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Basic3Images"), "*.png"));
                List<string> conceptImage1Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Concept1Images"), "*.png"));
                List<string> conceptImage2Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Concept2Images"), "*.png"));
                List<string> conceptImage3Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Concept3Images"), "*.png"));
                List<string> gifImage1Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Gif1Images"), "*.png"));
                List<string> gifImage2Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Gif2Images"), "*.png"));
                List<string> gifImage3Files = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Gif3Images"), "*.png"));
                List<string> event1ImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Event1Images"), "*.png"));
                List<string> event2ImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Event2Images"), "*.png"));
                List<string> event3ImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Frames", "Event3Images"), "*.png"));
                List<string> collab4ImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Ext", "collab4ImageFiles"), "*.png"));
                List<string> collab5ImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Ext", "collab5ImageFiles"), "*.png"));
                List<string> collab7ImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "Ext", "collab7ImageFiles"), "*.png"));

                // 배경 이미지 파일을 로드
                Image CoverBackground = Image.FromFile(Path.Combine(desktopPath, "CoverBackground.png"));
                Image collab1Image = Image.FromFile(Path.Combine(desktopPath, "Ext", "Collab1FrameImage.png"));
                Image collab2Image = Image.FromFile(Path.Combine(desktopPath, "Ext", "Collab2FrameImage.png"));
                Image collab3Image = Image.FromFile(Path.Combine(desktopPath, "Ext", "Collab3FrameImage.png"));
                Image collab6Image = Image.FromFile(Path.Combine(desktopPath, "Ext", "Collab6FrameImage.png"));

                // 인쇄 횟수만큼 반복
                // 이미지 파일들을 역순으로 처리
                for (int i = 0; i < printbooknum; i++) //Flipbook.NumOrders
                {
                    for (int j = imageFiles.Count - 2; j >= 0; j--) //imageFiles.Count
                    {
                        string photoFilePath = imageFiles[j]; // 이미지 파일 경로 가져오기
                        Image originalPhoto = Image.FromFile(photoFilePath); // 원본 이미지 로드
                        Bitmap photo = new Bitmap(originalPhoto); // 비트맵 이미지 생성

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

                        // 이미지 파일의 이름을 가져오기
                        string fileName = Path.GetFileName(photoFilePath);

                        // 이미지가 커버 이미지인 경우
                        if (fileName == "coverImage.jpg")
                        {
                            graphics.DrawImage(photo, coverRect); // 커버 이미지를 지정된 사각형 영역에 그리기
                            graphics.DrawImage(CoverBackground, backgroundRect);
                        }
                        else
                        {
                            // 프레임 선택에 따른 이미지 처리
                            switch (frame)
                            {
                                case 1:
                                    Image Basic1Image = Image.FromFile(basicImage1Files[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Basic1Image, backgroundRect); // 프레임을 배경에 넣기
                                    Basic1Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 2:
                                    Image Basic2Image = Image.FromFile(basicImage1Files[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Basic2Image, backgroundRect); // 프레임을 배경에 넣기
                                    Basic2Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 3:
                                    Image Basic3Image = Image.FromFile(basicImage3Files[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Basic3Image, backgroundRect); // 프레임을 배경에 넣기
                                    Basic3Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 4:
                                    Image Concept1Image = Image.FromFile(conceptImage1Files[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Concept1Image, backgroundRect); // 프레임을 배경에 넣기
                                    Concept1Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 5:
                                    Image Concept2Image = Image.FromFile(conceptImage2Files[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Concept2Image, backgroundRect); // 프레임을 배경에 넣기
                                    Concept2Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 6:
                                    Image Concept3Image = Image.FromFile(conceptImage3Files[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Concept3Image, backgroundRect); // 프레임을 배경에 넣기
                                    Concept3Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 7:
                                    Image Gif1Image = Image.FromFile(gifImage1Files[indexInImageFiles - 1]);
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Gif1Image, backgroundRect); // 엽서 컨셉 배경을 배경 사각형 영역에 그리기
                                    Gif1Image.Dispose(); // 이벤트 이미지 리소스 해제
                                    break;
                                case 8:
                                    Image Gif2Image = Image.FromFile(gifImage2Files[indexInImageFiles - 1]);
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Gif2Image, backgroundRect); // 엽서 컨셉 배경을 배경 사각형 영역에 그리기
                                    Gif2Image.Dispose(); // 이벤트 이미지 리소스 해제
                                    break;
                                case 9:
                                    Image Gif3Image = Image.FromFile(gifImage3Files[indexInImageFiles - 1]);
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Gif3Image, backgroundRect); // 엽서 컨셉 배경을 배경 사각형 영역에 그리기
                                    Gif3Image.Dispose(); // 이벤트 이미지 리소스 해제
                                    break;
                                case 10:
                                    Image Event1Image = Image.FromFile(event1ImageFiles[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Event1Image, backgroundRect); // 프레임을 배경에 넣기
                                    Event1Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 11:
                                    Image Event2Image = Image.FromFile(event2ImageFiles[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Event2Image, backgroundRect); // 프레임을 배경에 넣기
                                    Event2Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 12:
                                    Image Event3Image = Image.FromFile(event3ImageFiles[indexInImageFiles - 1]); // 프레임 1~19까지 배열에 넣기
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(Event3Image, backgroundRect); // 프레임을 배경에 넣기
                                    Event3Image.Dispose(); //배경 이미지 리소스 해제
                                    break;
                                case 13:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(collab1Image, backgroundRect); // 필름 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 14:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(collab2Image, backgroundRect); // 필름 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 15:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(collab3Image, backgroundRect); // 필름 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 16:
                                    Image collab4Image = Image.FromFile(collab4ImageFiles[indexInImageFiles - 1]);
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(collab4Image, backgroundRect); // 모래시계 이미지를 GIF 사각형 영역에 그리기
                                    collab4Image.Dispose(); // 모래시계 이미지 리소스 해제
                                    break;
                                case 17:
                                    Image collab5Image = Image.FromFile(collab5ImageFiles[indexInImageFiles - 1]);
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(collab5Image, backgroundRect); // 모래시계 이미지를 GIF 사각형 영역에 그리기
                                    collab5Image.Dispose(); // 모래시계 이미지 리소스 해제
                                    break;
                                case 18:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(collab6Image, backgroundRect); // 필름 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 19:
                                    Image collab7Image = Image.FromFile(collab7ImageFiles[indexInImageFiles - 1]);
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(collab7Image, backgroundRect); // 모래시계 이미지를 GIF 사각형 영역에 그리기
                                    collab7Image.Dispose(); // 모래시계 이미지 리소스 해제
                                    break;
                            }
                        }

                        photo.Dispose(); // 사진 비트맵 리소스 해제

                        printDoc.Print(); // 사진 출력
                    }
                }
                // 배경 이미지 리소스 해제
                collab1Image.Dispose();
                collab2Image.Dispose();
                collab3Image.Dispose();
                collab6Image.Dispose();
            }

            bitmap.Dispose(); // 비트맵 리소스 해제
        }
    }
}
