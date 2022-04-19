using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Clova
{ 
    // TODO:
    // Json 파일로 설정 값 빼기
    public class Speaker : BindableBase
    {
        private string _englishName;
        public string EnglishName { get => _englishName; set => SetProperty(ref _englishName, value); }
        private string _koreanName;
        public string KoreanName { get => _koreanName; set => SetProperty(ref _koreanName, value); }
        private string _description;
        public string Description { get => _description; set => SetProperty(ref _description, value); }

        public Speaker(string englishName, string koreanName, string description)
        {
            EnglishName = englishName;
            KoreanName = koreanName;
            Description = description;
        }

        public static List<Speaker> Speakers = new List<Speaker>()
        {
            new Speaker("nara", "아라", "한국어, 여성 음색                   "),
            new Speaker("nara_call", "아라(상담원)", "한국어, 여성 음색      "),
            new Speaker("nminyoung", "민영", "한국어, 여성 음색              "),
            new Speaker("nyejin", "예진", "한국어, 여성 음색                 "),
            new Speaker("mijin", "미진", "한국어, 여성 음색                  "),
            new Speaker("jinho", "진호", "한국어, 남성 음색                  "),
            new Speaker("clara", "클라라", "영어, 여성 음색                  "),
            new Speaker("matt", "매트", "영어, 남성 음색                     "),
            new Speaker("shinji", "신지", "일본어, 남성 음색                 "),
            new Speaker("meimei", "메이메이", "중국어, 여성 음색             "),
            new Speaker("liangliang", "량량", "중국어, 남성 음색             "),
            new Speaker("jose", "호세", "스페인어, 남성 음색                 "),
            new Speaker("carmen", "카르멘", "스페인어, 여성 음색             "),
            new Speaker("nminsang", "민상", "한국어, 남성 음색               "),
            new Speaker("nsinu", "신우", "한국어, 남성 음색                  "),
            new Speaker("nhajun", "하준", "한국어, 아동 음색 (남)            "),
            new Speaker("ndain", "다인", "한국어, 아동음색 (여)              "),
            new Speaker("njiyun", "지윤", "한국어, 여성 음색                 "),
            new Speaker("nsujin", "수진", "한국어, 여성 음색                 "),
            new Speaker("njinho", "진호", "한국어, 남성 음색                 "),
            new Speaker("njihun", "지훈", "한국어, 남성 음색                 "),
            new Speaker("njooahn", "주안", "한국어, 남성 음색                "),
            new Speaker("nseonghoon", "성훈", "한국어, 남성 음색             "),
            new Speaker("njihwan", "지환", "한국어, 남성 음색                "),
            new Speaker("nsiyoon", "시윤", "한국어, 남성 음색                "),
            new Speaker("ngaram", "가람", "한국어, 아동음색 (여)             "),
            new Speaker("ntomoko", "토모코", "일본어, 여성 음색              "),
            new Speaker("nnaomi", "나오미", "일본어, 여성 음색               "),
            new Speaker("dnaomi_joyful", "나오미(기쁨)", "일본어, 여성 음색  "),
            new Speaker("dnaomi_formal", "나오미(뉴스)", "일본어, 여성 음색  "),
            new Speaker("driko", "리코", "일본어, 여성 음색                  "),
            new Speaker("deriko", "에리코", "일본어, 여성 음색               "),
            new Speaker("nsayuri", "사유리", "일본어, 여성 음색              "),
            new Speaker("ngoeun", "고은", "한국어, 여성 음색                 "),
            new Speaker("neunyoung", "은영", "한국어, 여성 음색              "),
            new Speaker("nsunkyung", "선경", "한국어, 여성 음색              "),
            new Speaker("nyujin", "유진", "한국어, 여성 음색                 "),
            new Speaker("ntaejin", "태진", "한국어, 남성 음색                "),
            new Speaker("nyoungil", "영일", "한국어, 남성 음색               "),
            new Speaker("nseungpyo", "승표", "한국어, 남성 음색              "),
            new Speaker("nwontak", "원탁", "한국어, 남성 음색                "),
            new Speaker("dara_ang", "아라(화남)", " 한국어, 여성 음색        "),
            new Speaker("nsunhee", "선희", " 한국어, 여성 음색               "),
            new Speaker("nminseo", "민서", " 한국어, 여성 음색               "),
            new Speaker("njiwon", "지원", " 한국어, 여성 음색                "),
            new Speaker("nbora", "보라", " 한국어, 여성 음색                 "),
            new Speaker("njonghyun", "종현", " 한국어, 남성 음색             "),
            new Speaker("njoonyoung", "준영", " 한국어, 남성 음색            "),
            new Speaker("njaewook", "재욱", " 한국어, 남성 음색              "),
            new Speaker("danna", "안나", "영어, 여성 음색                    "),
            new Speaker("djoey", "조이", "영어, 여성 음색                    "),
            new Speaker("dhajime", "하지메", "일본어, 남성 음색              "),
            new Speaker("ddaiki", "다이키", "일본어, 남성 음색               "),
            new Speaker("dayumu", "아유무", "일본어, 남성 음색               "),
            new Speaker("dmio", "미오", "일본어, 여성 음색                   "),
            new Speaker("chiahua", "차화", "대만어, 여성 음색                "),
            new Speaker("kuanlin", "관린", "대만어, 남성 음색                "),
            new Speaker("nes_c_hyeri", "혜리", "한국어, 여성 음색            "),
            new Speaker("nes_c_sohyun", "소현", "한국어, 여성 음색           "),
            new Speaker("nes_c_mikyung", "미경", "한국어, 여성 음색          "),
            new Speaker("nes_c_kihyo", "기효", "한국어, 남성 음색            "),
            new Speaker("ntiffany", "기서", "한국어, 여성 음색               "),
            new Speaker("napple", "늘봄", "한국어, 여성 음색                 "),
            new Speaker("njangj", "드림", "한국어, 여성 음색                 "),
            new Speaker("noyj", "봄달", "한국어, 여성 음색                   "),
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
