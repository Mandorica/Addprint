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

        private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // 바탕화면 경로를 가져오는 변수
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox.Items.Add("화이트 프레임");
            comboBox.Items.Add("하늘 프레임");
            comboBox.Items.Add("블랙 프레임");
            comboBox.Items.Add("필름 프레임");
            comboBox.Items.Add("생일 프레임");
            comboBox.Items.Add("카툰 프레임");
            comboBox.Items.Add("gif1 프레임");
            comboBox.Items.Add("gif2 프레임");
            comboBox.Items.Add("gif3 프레임");
            comboBox.Items.Add("이벤트 프레임1");
            comboBox.Items.Add("이벤트 프레임2");
            comboBox.Items.Add("이벤트 프레임3");
            comboBox.SelectedIndexChanged += ComboBox_SelectedIndexChanged;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // ComboBox에서 선택된 항목에 따라 변수에 값 대입
            switch (comboBox.SelectedItem.ToString())
            {
                case "화이트 프레임":
                    selectedValue = 1;
                    break;
                case "하늘 프레임":
                    selectedValue = 2;
                    break;
                case "블랙 프레임":
                    selectedValue = 3;
                    break;
                case "필름 프레임":
                    selectedValue = 4;
                    break;
                case "생일 프레임":
                    selectedValue = 5;
                    break;
                case "카툰 프레임":
                    selectedValue = 6;
                    break;
                case "gif1 프레임":
                    selectedValue = 7;
                    break;
                case "gif2 프레임":
                    selectedValue = 8;
                    break;
                case "gif3 프레임":
                    selectedValue = 9;
                    break;
                case "이벤트 프레임1":
                    selectedValue = 10;
                    break;
                case "이벤트 프레임2":
                    selectedValue = 11;
                    break;
                case "이벤트 프레임3":
                    selectedValue = 12;
                    break;
                default:
                    selectedValue = 1;
                    break;
            }

            // 값 확인을 위해 출력
            MessageBox.Show("Selected Value: " + selectedValue);
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
            Task task = PrintFujiPrinter(selectedValue);
            MessageBox.Show("인쇄가 완료되었습니다.");
        }

        // 인쇄 이벤트 핸들러
        private void PrintImage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, e.PageBounds); // 비트맵 이미지를 페이지 경계에 맞춰 그리기
        }

        private async Task PrintFujiPrinter(int frame)
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
                List<string> sandTimerImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "SandTimerImages"), "*.png"));
                List<string> catImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "CatImages"), "*.png"));
                List<string> birdImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "BirdImages"), "*.png"));
                List<string> imageFiles = new List<string>(Directory.GetFiles(folderPath, "*.jpg"));
                //List<string> eventImageFiles = new List<string>(Directory.GetFiles(Path.Combine(desktopPath, "EventImages"), "*.png"));

                // 배경 이미지 파일을 로드
                Image CoverBackground = Image.FromFile(Path.Combine(desktopPath, "CoverBackground.png"));
                Image filmConceptBackground = Image.FromFile(Path.Combine(desktopPath, "FilmConceptFrameImage.png"));
                Image postcardConceptBackground = Image.FromFile(Path.Combine(desktopPath, "PostcardConceptFrameImage.png"));
                Image cartoonConceptBackground = Image.FromFile(Path.Combine(desktopPath, "CartoonConceptFrameImage.png"));
                Image event1Background = Image.FromFile(Path.Combine(desktopPath, "Event1ConceptFrameImage.png"));
                Image event2Background = Image.FromFile(Path.Combine(desktopPath, "Event2ConceptFrameImage.png"));
                Image event3Background = Image.FromFile(Path.Combine(desktopPath, "Event3ConceptFrameImage.png"));

                // 인쇄 횟수만큼 반복
                // 이미지 파일들을 역순으로 처리
                for (int j = imageFiles.Count-2; j >= 0; j--) //imageFiles.Count
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
                                    // 흰색배경
                                    graphics.FillRectangle(new SolidBrush(Color.White), backgroundRect); // 배경을 검은색으로 설정
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    break;
                                case 2:
                                // 파랑색 배경
                                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(228, 243, 249)), backgroundRect); // 배경을 특정 색상으로 설정
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    break;
                                case 3:
                                    
                                    // 검은색 배경
                                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(46, 46, 46)), backgroundRect); // 배경을 흰색으로 설정
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    break;
                                case 4:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(filmConceptBackground, backgroundRect); // 필름 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 5:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(postcardConceptBackground, backgroundRect); // 엽서 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 6:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(cartoonConceptBackground, backgroundRect); // 만화 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 7:
                                    // gif 프레임 1번 로드
                                    Image sandTimerImage = Image.FromFile(sandTimerImageFiles[indexInImageFiles - 1]);
                                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(159, 204, 213)), backgroundRect); // 배경을 특정 색상으로 설정
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(sandTimerImage, gifRect); // 모래시계 이미지를 GIF 사각형 영역에 그리기
                                    sandTimerImage.Dispose(); // 모래시계 이미지 리소스 해제
                                    break;
                                case 8:
                                    // gif 프레임 2번 로드
                                    Image catImage = Image.FromFile(catImageFiles[indexInImageFiles - 1]);
                                    graphics.FillRectangle(new SolidBrush(Color.White), backgroundRect); // 배경을 특정 색상으로 설정
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(catImage, gifRect); // 고양이 이미지를 GIF 사각형 영역에 그리기
                                    catImage.Dispose(); // 고양이 이미지 리소스 해제
                                    break;
                                case 9:
                                    // gif 프레임 3번 로드
                                    Image birdImage = Image.FromFile(birdImageFiles[indexInImageFiles - 1]);
                                    graphics.FillRectangle(new SolidBrush(Color.FromArgb(242, 206, 213)), backgroundRect); // 배경을 특정 색상으로 설정
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(birdImage, gifRect); // 새 이미지를 새 GIF 사각형 영역에 그리기
                                    birdImage.Dispose(); // 새 이미지 리소스 해제
                                    break;
                                case 10:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(event1Background, backgroundRect); // 엽서 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 11:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(event2Background, backgroundRect); // 엽서 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                                case 12:
                                    graphics.DrawImage(photo, innerRect); // 사진을 내부 사각형 영역에 그리기
                                    graphics.DrawImage(event3Background, backgroundRect); // 엽서 컨셉 배경을 배경 사각형 영역에 그리기
                                    break;
                        }
                        }

                        photo.Dispose(); // 사진 비트맵 리소스 해제

                        printDoc.Print(); // 사진 출력
                    }
                // 배경 이미지 리소스 해제
                filmConceptBackground.Dispose();
                postcardConceptBackground.Dispose();
                cartoonConceptBackground.Dispose();
                event1Background.Dispose();
                event2Background.Dispose();
                event3Background.Dispose();
            }

            bitmap.Dispose(); // 비트맵 리소스 해제
        }
    }
}
