using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using Common;

namespace Clova
{
    [ValueConversion(typeof(ClovaSpeaker), typeof(string))]
    public class SpeakerToStringValueConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            ClovaSpeaker speaker = (ClovaSpeaker)value;
            return speaker.KoreanName;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)value;
            return ClovaSpeaker.Speakers.FirstOrDefault(item => item.KoreanName == name);
        }
    }
    // TODO:
    // Json 파일로 설정 값 빼기
    public class ClovaSpeaker : BindableBase
    {
        private string _englishName;
        public string EnglishName { get => _englishName; set => SetProperty(ref _englishName, value); }
        private string _koreanName;
        public string KoreanName { get => _koreanName; set => SetProperty(ref _koreanName, value); }
        private string _description;
        public string Description { get => _description; set => SetProperty(ref _description, value); }

        public ClovaSpeaker(string englishName, string koreanName, string description)
        {
            EnglishName = englishName;
            KoreanName = koreanName;
            Description = description;
        }

        public static List<ClovaSpeaker> Speakers = new List<ClovaSpeaker>()
        {
            new ClovaSpeaker("nara", "아라", "한국어, 여성 음색                   "),
            new ClovaSpeaker("nara_call", "아라(상담원)", "한국어, 여성 음색      "),
            new ClovaSpeaker("nminyoung", "민영", "한국어, 여성 음색              "),
            new ClovaSpeaker("nyejin", "예진", "한국어, 여성 음색                 "),
            new ClovaSpeaker("mijin", "미진", "한국어, 여성 음색                  "),
            new ClovaSpeaker("jinho", "진호", "한국어, 남성 음색                  "),
            new ClovaSpeaker("clara", "클라라", "영어, 여성 음색                  "),
            new ClovaSpeaker("matt", "매트", "영어, 남성 음색                     "),
            new ClovaSpeaker("shinji", "신지", "일본어, 남성 음색                 "),
            new ClovaSpeaker("meimei", "메이메이", "중국어, 여성 음색             "),
            new ClovaSpeaker("liangliang", "량량", "중국어, 남성 음색             "),
            new ClovaSpeaker("jose", "호세", "스페인어, 남성 음색                 "),
            new ClovaSpeaker("carmen", "카르멘", "스페인어, 여성 음색             "),
            new ClovaSpeaker("nminsang", "민상", "한국어, 남성 음색               "),
            new ClovaSpeaker("nsinu", "신우", "한국어, 남성 음색                  "),
            new ClovaSpeaker("nhajun", "하준", "한국어, 아동 음색 (남)            "),
            new ClovaSpeaker("ndain", "다인", "한국어, 아동음색 (여)              "),
            new ClovaSpeaker("njiyun", "지윤", "한국어, 여성 음색                 "),
            new ClovaSpeaker("nsujin", "수진", "한국어, 여성 음색                 "),
            new ClovaSpeaker("njinho", "진호", "한국어, 남성 음색                 "),
            new ClovaSpeaker("njihun", "지훈", "한국어, 남성 음색                 "),
            new ClovaSpeaker("njooahn", "주안", "한국어, 남성 음색                "),
            new ClovaSpeaker("nseonghoon", "성훈", "한국어, 남성 음색             "),
            new ClovaSpeaker("njihwan", "지환", "한국어, 남성 음색                "),
            new ClovaSpeaker("nsiyoon", "시윤", "한국어, 남성 음색                "),
            new ClovaSpeaker("ngaram", "가람", "한국어, 아동음색 (여)             "),
            new ClovaSpeaker("ntomoko", "토모코", "일본어, 여성 음색              "),
            new ClovaSpeaker("nnaomi", "나오미", "일본어, 여성 음색               "),
            new ClovaSpeaker("dnaomi_joyful", "나오미(기쁨)", "일본어, 여성 음색  "),
            new ClovaSpeaker("dnaomi_formal", "나오미(뉴스)", "일본어, 여성 음색  "),
            new ClovaSpeaker("driko", "리코", "일본어, 여성 음색                  "),
            new ClovaSpeaker("deriko", "에리코", "일본어, 여성 음색               "),
            new ClovaSpeaker("nsayuri", "사유리", "일본어, 여성 음색              "),
            new ClovaSpeaker("ngoeun", "고은", "한국어, 여성 음색                 "),
            new ClovaSpeaker("neunyoung", "은영", "한국어, 여성 음색              "),
            new ClovaSpeaker("nsunkyung", "선경", "한국어, 여성 음색              "),
            new ClovaSpeaker("nyujin", "유진", "한국어, 여성 음색                 "),
            new ClovaSpeaker("ntaejin", "태진", "한국어, 남성 음색                "),
            new ClovaSpeaker("nyoungil", "영일", "한국어, 남성 음색               "),
            new ClovaSpeaker("nseungpyo", "승표", "한국어, 남성 음색              "),
            new ClovaSpeaker("nwontak", "원탁", "한국어, 남성 음색                "),
            new ClovaSpeaker("dara_ang", "아라(화남)", " 한국어, 여성 음색        "),
            new ClovaSpeaker("nsunhee", "선희", " 한국어, 여성 음색               "),
            new ClovaSpeaker("nminseo", "민서", " 한국어, 여성 음색               "),
            new ClovaSpeaker("njiwon", "지원", " 한국어, 여성 음색                "),
            new ClovaSpeaker("nbora", "보라", " 한국어, 여성 음색                 "),
            new ClovaSpeaker("njonghyun", "종현", " 한국어, 남성 음색             "),
            new ClovaSpeaker("njoonyoung", "준영", " 한국어, 남성 음색            "),
            new ClovaSpeaker("njaewook", "재욱", " 한국어, 남성 음색              "),
            new ClovaSpeaker("danna", "안나", "영어, 여성 음색                    "),
            new ClovaSpeaker("djoey", "조이", "영어, 여성 음색                    "),
            new ClovaSpeaker("dhajime", "하지메", "일본어, 남성 음색              "),
            new ClovaSpeaker("ddaiki", "다이키", "일본어, 남성 음색               "),
            new ClovaSpeaker("dayumu", "아유무", "일본어, 남성 음색               "),
            new ClovaSpeaker("dmio", "미오", "일본어, 여성 음색                   "),
            new ClovaSpeaker("chiahua", "차화", "대만어, 여성 음색                "),
            new ClovaSpeaker("kuanlin", "관린", "대만어, 남성 음색                "),
            new ClovaSpeaker("nes_c_hyeri", "혜리", "한국어, 여성 음색            "),
            new ClovaSpeaker("nes_c_sohyun", "소현", "한국어, 여성 음색           "),
            new ClovaSpeaker("nes_c_mikyung", "미경", "한국어, 여성 음색          "),
            new ClovaSpeaker("nes_c_kihyo", "기효", "한국어, 남성 음색            "),
            new ClovaSpeaker("ntiffany", "기서", "한국어, 여성 음색               "),
            new ClovaSpeaker("napple", "늘봄", "한국어, 여성 음색                 "),
            new ClovaSpeaker("njangj", "드림", "한국어, 여성 음색                 "),
            new ClovaSpeaker("noyj", "봄달", "한국어, 여성 음색                   "),
        };

        public static string ConvertKoreanToEnglish(string korean)
        {
            return Speakers.FirstOrDefault(item => item.KoreanName == korean)?.EnglishName;
        }

        public static string ConvertEnglishToKorean(string korean)
        {
            return Speakers.FirstOrDefault(item => item.EnglishName == korean)?.KoreanName;
        }
    }

}
